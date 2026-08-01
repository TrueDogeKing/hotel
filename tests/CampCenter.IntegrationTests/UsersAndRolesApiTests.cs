using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.DTOs.Users;

namespace CampCenter.IntegrationTests;

/// Panel accounts and what each role may do.
public class UsersAndRolesApiTests : IntegrationTestBase
{
    public UsersAndRolesApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    private const string Password = "Worker123!";

    /// Logins are shared across a run, so each test makes its own.
    private static string UniqueLogin(string prefix) =>
        $"{prefix}{Guid.NewGuid():N}"[..Math.Min(prefix.Length + 12, 32)];

    private async Task<(HttpClient Admin, AdminUserDto Worker)> CreateWorkerAsync(string prefix)
    {
        var admin = await CreateAuthenticatedClientAsync();
        var login = UniqueLogin(prefix);
        var response = await admin.PostAsJsonAsync(
            "/api/admin/users",
            new CreateUserRequestDto(login, Password, "Worker")
        );
        Assert.True(
            response.StatusCode == HttpStatusCode.Created,
            $"Create failed: {response.StatusCode} {await response.Content.ReadAsStringAsync()}"
        );
        return (admin, (await response.Content.ReadFromJsonAsync<AdminUserDto>())!);
    }

    [Fact]
    public async Task Administrator_CanAdd_ChangeRole_AndDelete_Accounts()
    {
        var (admin, worker) = await CreateWorkerAsync("crud");
        Assert.Equal("Worker", worker.Role);
        Assert.False(worker.IsSelf);

        var list = (await admin.GetFromJsonAsync<List<AdminUserDto>>("/api/admin/users"))!;
        Assert.Contains(list, u => u.Id == worker.Id);
        // The seeded administrator is the caller, and says so.
        Assert.Contains(list, u => u.Login == AdminLogin && u.IsSelf);

        // Promote, then demote.
        var promoted = await admin.PutAsJsonAsync(
            $"/api/admin/users/{worker.Id}/role",
            new SetUserRoleRequestDto("Administrator")
        );
        Assert.Equal(HttpStatusCode.OK, promoted.StatusCode);
        Assert.Equal(
            "Administrator",
            (await promoted.Content.ReadFromJsonAsync<AdminUserDto>())!.Role
        );

        var demoted = await admin.PutAsJsonAsync(
            $"/api/admin/users/{worker.Id}/role",
            new SetUserRoleRequestDto("Worker")
        );
        Assert.Equal(HttpStatusCode.OK, demoted.StatusCode);

        var deleted = await admin.DeleteAsync($"/api/admin/users/{worker.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleted.StatusCode);
        var after = (await admin.GetFromJsonAsync<List<AdminUserDto>>("/api/admin/users"))!;
        Assert.DoesNotContain(after, u => u.Id == worker.Id);
    }

    [Fact]
    public async Task DuplicateLogin_AndWeakPassword_AreRejected()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var login = UniqueLogin("dup");

        var first = await admin.PostAsJsonAsync(
            "/api/admin/users",
            new CreateUserRequestDto(login, Password, "Worker")
        );
        Assert.Equal(HttpStatusCode.Created, first.StatusCode);

        var second = await admin.PostAsJsonAsync(
            "/api/admin/users",
            new CreateUserRequestDto(login, Password, "Worker")
        );
        Assert.Equal(HttpStatusCode.BadRequest, second.StatusCode);

        var weak = await admin.PostAsJsonAsync(
            "/api/admin/users",
            new CreateUserRequestDto(UniqueLogin("weak"), "short", "Worker")
        );
        Assert.Equal(HttpStatusCode.BadRequest, weak.StatusCode);

        var badRole = await admin.PostAsJsonAsync(
            "/api/admin/users",
            new CreateUserRequestDto(UniqueLogin("role"), Password, "Wizard")
        );
        Assert.Equal(HttpStatusCode.BadRequest, badRole.StatusCode);
    }

    /// The two ways the panel could lock everyone out of itself.
    [Fact]
    public async Task SelfDelete_AndLastAdministrator_AreRefused()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var me = Assert.Single(
            (await admin.GetFromJsonAsync<List<AdminUserDto>>("/api/admin/users"))!,
            u => u.IsSelf
        );

        var selfDelete = await admin.DeleteAsync($"/api/admin/users/{me.Id}");
        Assert.Equal(HttpStatusCode.BadRequest, selfDelete.StatusCode);

        // Whether demoting the caller is refused depends on how many administrators
        // exist, so assert against that rather than assuming a fresh database.
        var administrators = (await admin.GetFromJsonAsync<List<AdminUserDto>>(
            "/api/admin/users"
        ))!.Count(u => u.Role == "Administrator");
        var demoteSelf = await admin.PutAsJsonAsync(
            $"/api/admin/users/{me.Id}/role",
            new SetUserRoleRequestDto("Worker")
        );

        if (administrators <= 1)
        {
            Assert.Equal(HttpStatusCode.BadRequest, demoteSelf.StatusCode);
        }
        else
        {
            // Put it back: the shared host signs in as this account for every test.
            Assert.Equal(HttpStatusCode.OK, demoteSelf.StatusCode);
            var restored = await admin.PutAsJsonAsync(
                $"/api/admin/users/{me.Id}/role",
                new SetUserRoleRequestDto("Administrator")
            );
            Assert.Equal(HttpStatusCode.OK, restored.StatusCode);
        }
    }

    /// The point of the role: a worker sees every section an administrator does,
    /// and cannot change anything in any of them.
    [Fact]
    public async Task Worker_ReadsEverySection_ButCannotWriteAnywhere()
    {
        var (_, workerAccount) = await CreateWorkerAsync("read");
        var worker = await CreateClientForAsync(workerAccount.Login, Password);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        // Every section reads, the accounts list included.
        foreach (
            var url in new[]
            {
                "/api/admin/dashboard",
                $"/api/admin/dashboard/groups?category=Current",
                "/api/admin/users",
                "/api/admin/bookings",
                "/api/admin/rooms",
                "/api/admin/closures",
                "/api/admin/tasks",
                "/api/admin/meal-times",
                $"/api/admin/housekeeping/day/{today:yyyy-MM-dd}",
                $"/api/admin/occupancy?start={today:yyyy-MM-dd}&end={today.AddDays(7):yyyy-MM-dd}",
                $"/api/admin/schedule/calendar?start={today:yyyy-MM-dd}&end={today.AddDays(7):yyyy-MM-dd}",
                $"/api/admin/schedule/day/{today:yyyy-MM-dd}",
            }
        )
        {
            var read = await worker.GetAsync(url);
            Assert.True(
                read.StatusCode == HttpStatusCode.OK,
                $"Worker should be able to read {url}, got {read.StatusCode}"
            );
        }

        // And no write anywhere. The requests never reach a service, so the shape
        // of each body is irrelevant — only the method and the role are.
        var writes = new (string Method, string Url, object? Body)[]
        {
            (
                "POST",
                "/api/admin/schedule/entries",
                new CreateScheduleEntryRequestDto(
                    Guid.NewGuid(),
                    "Activity",
                    null,
                    today,
                    new TimeOnly(10, 0),
                    new TimeOnly(11, 0),
                    "Nie powinno przejść",
                    null,
                    null,
                    null,
                    null
                )
            ),
            ("DELETE", $"/api/admin/schedule/entries/{Guid.NewGuid()}", null),
            ("POST", "/api/admin/rooms", new { Number = "W-1", Capacity = 4, Description = (string?)null }),
            ("DELETE", $"/api/admin/rooms/{Guid.NewGuid()}", null),
            ("DELETE", $"/api/admin/closures/{Guid.NewGuid()}", null),
            ("DELETE", $"/api/admin/tasks/{Guid.NewGuid()}", null),
            ("POST", $"/api/admin/bookings/{Guid.NewGuid()}/cancel", null),
            (
                "PUT",
                $"/api/admin/housekeeping/day/{today:yyyy-MM-dd}/rooms/{Guid.NewGuid()}",
                new { Status = "Done", Note = (string?)null }
            ),
            // Its own account included: a worker cannot promote itself.
            (
                "PUT",
                $"/api/admin/users/{workerAccount.Id}/role",
                new SetUserRoleRequestDto("Administrator")
            ),
            (
                "POST",
                "/api/admin/users",
                new CreateUserRequestDto(UniqueLogin("nope"), Password, "Administrator")
            ),
        };

        foreach (var (method, url, body) in writes)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), url);
            if (body is not null)
            {
                request.Content = JsonContent.Create(body, body.GetType());
            }

            var response = await worker.SendAsync(request);
            Assert.True(
                response.StatusCode == HttpStatusCode.Forbidden,
                $"Worker should be refused {method} {url}, got {response.StatusCode}"
            );
        }
    }

    /// A demotion has to end the demoted account's sessions: the role rides in the
    /// access token, so a still-valid one would outlive the change.
    [Fact]
    public async Task RoleChange_EndsTheAffectedAccountsSessions()
    {
        var (admin, account) = await CreateWorkerAsync("sess");
        var worker = await CreateClientForAsync(account.Login, Password);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        Assert.Equal(
            HttpStatusCode.OK,
            (await worker.GetAsync($"/api/admin/schedule/day/{today:yyyy-MM-dd}")).StatusCode
        );

        var promoted = await admin.PutAsJsonAsync(
            $"/api/admin/users/{account.Id}/role",
            new SetUserRoleRequestDto("Administrator")
        );
        Assert.Equal(HttpStatusCode.OK, promoted.StatusCode);

        // The refresh cookie it holds is revoked, so it cannot silently mint a new
        // access token; the old one still works until it expires, which is why the
        // revocation matters.
        var refresh = await worker.PostAsync("/api/auth/refresh", null);
        Assert.Equal(HttpStatusCode.Unauthorized, refresh.StatusCode);

        // Signing in again picks up the new role.
        var promotedClient = await CreateClientForAsync(account.Login, Password);
        Assert.Equal(
            HttpStatusCode.OK,
            (await promotedClient.GetAsync("/api/admin/rooms")).StatusCode
        );

        await admin.DeleteAsync($"/api/admin/users/{account.Id}");
    }

    /// An administrator can reset any account's password, including its own —
    /// and doing so ends that account's sessions, the same as a role change.
    [Fact]
    public async Task Administrator_CanResetAPassword_AndItEndsThatAccountsSessions()
    {
        var (admin, account) = await CreateWorkerAsync("pwd");
        var worker = await CreateClientForAsync(account.Login, Password);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        Assert.Equal(
            HttpStatusCode.OK,
            (await worker.GetAsync($"/api/admin/schedule/day/{today:yyyy-MM-dd}")).StatusCode
        );

        const string NewPassword = "NewWorker456!";
        var reset = await admin.PutAsJsonAsync(
            $"/api/admin/users/{account.Id}/password",
            new SetUserPasswordRequestDto(NewPassword)
        );
        Assert.True(
            reset.StatusCode == HttpStatusCode.OK,
            $"Reset failed: {reset.StatusCode} {await reset.Content.ReadAsStringAsync()}"
        );

        // The old refresh cookie no longer works…
        var refresh = await worker.PostAsync("/api/auth/refresh", null);
        Assert.Equal(HttpStatusCode.Unauthorized, refresh.StatusCode);

        // …the old password no longer signs in…
        var loginWithOldPassword = await CreateClient()
            .PostAsJsonAsync(
                "/api/auth/login",
                new CampCenter.Application.DTOs.Auth.LoginRequestDto(account.Login, Password)
            );
        Assert.Equal(HttpStatusCode.Unauthorized, loginWithOldPassword.StatusCode);

        // …and the new one does.
        var signedInAgain = await CreateClientForAsync(account.Login, NewPassword);
        Assert.Equal(
            HttpStatusCode.OK,
            (await signedInAgain.GetAsync($"/api/admin/schedule/day/{today:yyyy-MM-dd}")).StatusCode
        );

        // A weak replacement is refused, same policy as creating an account.
        var weak = await admin.PutAsJsonAsync(
            $"/api/admin/users/{account.Id}/password",
            new SetUserPasswordRequestDto("short")
        );
        Assert.Equal(HttpStatusCode.BadRequest, weak.StatusCode);

        await admin.DeleteAsync($"/api/admin/users/{account.Id}");
    }
}
