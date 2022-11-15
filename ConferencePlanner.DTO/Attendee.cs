using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.DTO;

public class Attendee
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "First Name")]
    public virtual string? FirstName { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Last Name")]
    public virtual string? LastName { get; set; }

    [Required]
    [StringLength(200)]
    public string? UserName { get; set; }

    [StringLength(256)]
    [Display(Name = "E-mail")]
    public virtual string? EmailAddress { get; set; }
}
