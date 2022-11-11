using BackEnd.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Context;

public class ConferencePlannerContext : DbContext
{
    public ConferencePlannerContext(DbContextOptions<ConferencePlannerContext> options) : base(options)
    {
    }

    // https://learn.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset
    public DbSet<Speaker> Speakers => Set<Speaker>();

    public DbSet<Session> Sessions => Set<Session>();

    public DbSet<Track> Tracks => Set<Track>();

    public DbSet<Attendee> Attendees => Set<Attendee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendee>()
            .HasIndex(a => a.UserName)
            .IsUnique();

        // Many-to-many: Session <-> Attendee
        modelBuilder.Entity<SessionAttendee>()
            .HasKey(ca => new { ca.SessionId, ca.AttendeeId });

        // Many-to-many: Speaker <-> Session
        modelBuilder.Entity<SessionSpeaker>()
            .HasKey(ss => new { ss.SessionId, ss.SpeakerId });
    }
}
