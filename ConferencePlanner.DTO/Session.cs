using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.DTO;

public class Session
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(4000)]
    public virtual string? Abstract { get; set; }

    [Display(Name = "Start Time")]
    public virtual DateTimeOffset? StartTime { get; set; }

    [Display(Name = "End Time")]
    public virtual DateTimeOffset? EndTime { get; set; }

    public TimeSpan Duration => EndTime?.Subtract(StartTime ?? EndTime ?? DateTimeOffset.MinValue) ?? TimeSpan.Zero;

    public int? TrackId { get; set; }
}
