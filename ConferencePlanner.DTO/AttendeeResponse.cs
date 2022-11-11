namespace ConferencePlanner.DTO;

public class AttendeeResponse : Attendee
{
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
