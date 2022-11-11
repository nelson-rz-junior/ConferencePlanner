namespace BackEnd.Data.Models;

public class Session : ConferencePlanner.DTO.Session
{
    public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = null!;

    public virtual ICollection<SessionAttendee> SessionAttendees { get; set; } = null!;

    public Track Track { get; set; } = null!;
}
