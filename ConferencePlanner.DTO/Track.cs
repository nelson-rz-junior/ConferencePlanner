using System.ComponentModel.DataAnnotations;

namespace ConferencePlanner.DTO;

public class Track
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string? Name { get; set; }
}
