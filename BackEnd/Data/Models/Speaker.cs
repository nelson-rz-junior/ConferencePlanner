namespace BackEnd.Data.Models;

public class Speaker : ConferencePlanner.DTO.Speaker
{
    public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
}
