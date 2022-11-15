using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Pages.Models;

public class Session : ConferencePlanner.DTO.Session
{
    [Display(Name = "Start Time")]
    public override DateTimeOffset? StartTime { get => base.StartTime; set => base.StartTime = value; }

    [Display(Name = "End Time")]
    public override DateTimeOffset? EndTime { get => base.EndTime; set => base.EndTime = value; }
}
