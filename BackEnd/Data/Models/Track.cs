namespace BackEnd.Data.Models;

public class Track : ConferencePlanner.DTO.Track
{
    public virtual ICollection<Session> Sessions { get; set; } = null!;
}
