namespace BackEnd.Data.Models;

public class Attendee : ConferencePlanner.DTO.Attendee
{
    public virtual ICollection<SessionAttendee> SessionAttendees { get; set; } = null!;
}
