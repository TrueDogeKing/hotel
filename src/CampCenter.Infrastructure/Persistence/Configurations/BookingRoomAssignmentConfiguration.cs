using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class BookingRoomAssignmentConfiguration : IEntityTypeConfiguration<BookingRoomAssignment>
{
    public void Configure(EntityTypeBuilder<BookingRoomAssignment> builder)
    {
        builder.ToTable("BookingRoomAssignments");

        builder.HasKey(x => x.Id);

        // THE double-booking guard: a room can be assigned at most once per session,
        // enforced by the database regardless of application-level races.
        builder.HasIndex(x => new { x.CampSessionId, x.RoomId }).IsUnique();

        builder.HasIndex(x => x.BookingId);

        // Deleting a booking takes its assignments with it; rooms must never be
        // deletable while assigned (Restrict).
        builder
            .HasOne(x => x.Booking)
            .WithMany(b => b.RoomAssignments)
            .HasForeignKey(x => x.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
