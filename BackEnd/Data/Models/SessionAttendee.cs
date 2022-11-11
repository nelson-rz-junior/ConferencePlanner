using ConferencePlanner.DTO;

namespace BackEnd.Data.Models;

public class SessionAttendee
{
    public int SessionId { get; set; }

    public Session Session { get; set; } = null!;

    public int AttendeeId { get; set; }

    public Attendee Attendee { get; set; } = null!;
}
