using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Domain.Entities;

namespace CampCenter.IntegrationTests;

/// Pricing and payment as the owner works them now that Przelewy24 is off: rates
/// they set themselves, a price they can change per group, and a paid/unpaid
/// state they record by hand.
public class AdminPricingApiTests : IntegrationTestBase
{
    public AdminPricingApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    [Fact]
    public async Task Rates_PrefillANewBooking_AndCanBeRepricedPerGroup()
    {
        var admin = await CreateAuthenticatedClientAsync();

        // Capacity 11 is unique to this test (the room inventory is shared).
        for (var i = 1; i <= 2; i++)
        {
            var room = await admin.PostAsJsonAsync(
                "/api/admin/rooms",
                new CreateRoomRequestDto($"PR-{i}", 11, null)
            );
            Assert.Equal(HttpStatusCode.Created, room.StatusCode);
        }

        // The centre's rates: 150 zł per person per night, 40 zł of it as deposit.
        var setRates = await admin.PutAsJsonAsync(
            "/api/admin/pricing",
            new UpdatePricingDefaultsRequestDto(15_000, 4_000)
        );
        Assert.Equal(HttpStatusCode.OK, setRates.StatusCode);
        var rates = (await setRates.Content.ReadFromJsonAsync<PricingDefaultsDto>())!;
        Assert.Equal(15_000, rates.PricePerPersonPerNightGrosze);
        Assert.NotNull(rates.UpdatedAt);

        // A deposit above the price is refused.
        var badRates = await admin.PutAsJsonAsync(
            "/api/admin/pricing",
            new UpdatePricingDefaultsRequestDto(15_000, 20_000)
        );
        Assert.Equal(HttpStatusCode.BadRequest, badRates.StatusCode);

        var start = new DateOnly(2033, 6, 1);
        var end = new DateOnly(2033, 6, 5); // 4 nights
        var create = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                start,
                end,
                "Pricing Org",
                "Kasia Testowa",
                "pricing@example.com",
                "+48 600 600 600",
                10,
                null,
                null,
                "pl"
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<AdminBookingDto>())!;

        // Snapshotted from the rates: 150 zł × 10 people × 4 nights.
        Assert.Equal(15_000, booking.PricePerPersonPerNightGrosze);
        Assert.Equal(600_000, booking.TotalGrosze);
        Assert.Equal(160_000, booking.DepositGrosze);
        Assert.Equal(nameof(BookingPaymentState.Unpaid), booking.PaymentState);

        // Re-pricing this group: a new rate, total left to the arithmetic.
        var reprice = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/pricing",
            new UpdateBookingPricingRequestDto(12_000, 100_000, null)
        );
        Assert.Equal(HttpStatusCode.OK, reprice.StatusCode);
        var repriced = (await reprice.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(480_000, repriced.TotalGrosze);
        Assert.Equal(100_000, repriced.DepositGrosze);

        // A flat, negotiated total overrides that arithmetic.
        var flat = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/pricing",
            new UpdateBookingPricingRequestDto(12_000, 100_000, 400_000)
        );
        var flatBooking = (await flat.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(400_000, flatBooking.TotalGrosze);

        // A deposit larger than the total is refused.
        var badDeposit = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/pricing",
            new UpdateBookingPricingRequestDto(12_000, 500_000, 400_000)
        );
        Assert.Equal(HttpStatusCode.BadRequest, badDeposit.StatusCode);

        // Raising the centre's rates leaves the group already on the books alone.
        await admin.PutAsJsonAsync(
            "/api/admin/pricing",
            new UpdatePricingDefaultsRequestDto(30_000, 5_000)
        );
        var unchanged = (
            await admin.GetFromJsonAsync<AdminBookingDto>($"/api/admin/bookings/{booking.Id}")
        )!;
        Assert.Equal(400_000, unchanged.TotalGrosze);
        Assert.Equal(12_000, unchanged.PricePerPersonPerNightGrosze);
    }

    [Fact]
    public async Task RecordingTheDeposit_ConfirmsABookingWaitingOnIt()
    {
        var admin = await CreateAuthenticatedClientAsync();

        var room = await admin.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto("PS-1", 12, null)
        );
        Assert.Equal(HttpStatusCode.Created, room.StatusCode);

        var create = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                new DateOnly(2033, 8, 1),
                new DateOnly(2033, 8, 6),
                "Payment Org",
                "Marek Testowy",
                "payment@example.com",
                "+48 700 700 700",
                12,
                null,
                nameof(BookingStatus.PendingDeposit),
                "pl"
            )
        );
        var booking = (await create.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(nameof(BookingStatus.PendingDeposit), booking.Status);

        var deposit = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/payment-state",
            new SetBookingPaymentStateRequestDto(nameof(BookingPaymentState.DepositPaid))
        );
        Assert.Equal(HttpStatusCode.OK, deposit.StatusCode);
        var confirmed = (await deposit.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(nameof(BookingPaymentState.DepositPaid), confirmed.PaymentState);
        Assert.True(confirmed.DepositPaid);
        Assert.False(confirmed.FinalPaid);
        // Money in hand is what the booking was waiting for.
        Assert.Equal(nameof(BookingStatus.Confirmed), confirmed.Status);

        var paid = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/payment-state",
            new SetBookingPaymentStateRequestDto(nameof(BookingPaymentState.Paid))
        );
        var fully = (await paid.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.True(fully.FinalPaid);
        Assert.False(fully.FinalOverdue);

        // Un-recording a payment does not un-confirm a group already told it has a place.
        var back = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/payment-state",
            new SetBookingPaymentStateRequestDto(nameof(BookingPaymentState.Unpaid))
        );
        var reverted = (await back.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(nameof(BookingPaymentState.Unpaid), reverted.PaymentState);
        Assert.Equal(nameof(BookingStatus.Confirmed), reverted.Status);

        var nonsense = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/payment-state",
            new SetBookingPaymentStateRequestDto("Whenever")
        );
        Assert.Equal(HttpStatusCode.BadRequest, nonsense.StatusCode);
    }

    /// The panel drives bookings through one merged control; this is the whole list
    /// it offers, in a round trip.
    [Fact]
    public async Task MergedState_DrivesPaymentAndStatusTogether()
    {
        var admin = await CreateAuthenticatedClientAsync();

        var room = await admin.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto("BS-1", 13, null)
        );
        Assert.Equal(HttpStatusCode.Created, room.StatusCode);

        var create = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                new DateOnly(2033, 9, 1),
                new DateOnly(2033, 9, 4),
                "State Org",
                "Ewa Testowa",
                "state@example.com",
                "+48 800 800 800",
                13,
                null,
                nameof(BookingStatus.PendingDeposit),
                "pl"
            )
        );
        var booking = (await create.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(nameof(BookingState.AwaitingPayment), booking.State);

        async Task<AdminBookingDto> SetAsync(BookingState state)
        {
            var response = await admin.PutAsJsonAsync(
                $"/api/admin/bookings/{booking.Id}/state",
                new SetBookingStateRequestDto(state.ToString())
            );
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            return (await response.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        }

        // A payment confirms the booking underneath.
        var deposit = await SetAsync(BookingState.DepositPaid);
        Assert.Equal(nameof(BookingState.DepositPaid), deposit.State);
        Assert.Equal(nameof(BookingStatus.Confirmed), deposit.Status);

        var paid = await SetAsync(BookingState.Paid);
        Assert.Equal(nameof(BookingState.Paid), paid.State);
        Assert.True(paid.FinalPaid);

        // Cancelling from the same control frees the rooms, exactly as the cancel
        // action did.
        var cancelled = await SetAsync(BookingState.Cancelled);
        Assert.Equal(nameof(BookingState.Cancelled), cancelled.State);
        Assert.Empty(cancelled.Assignments);

        // And reviving takes them back.
        var revived = await SetAsync(BookingState.DepositPaid);
        Assert.Equal(nameof(BookingState.DepositPaid), revived.State);
        Assert.NotEmpty(revived.Assignments);

        // Cancelled and Completed outrank what was paid.
        var completed = await SetAsync(BookingState.Completed);
        Assert.Equal(nameof(BookingState.Completed), completed.State);
        Assert.Equal(nameof(BookingPaymentState.DepositPaid), completed.PaymentState);

        var nonsense = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/state",
            new SetBookingStateRequestDto("Someday")
        );
        Assert.Equal(HttpStatusCode.BadRequest, nonsense.StatusCode);
    }
}
