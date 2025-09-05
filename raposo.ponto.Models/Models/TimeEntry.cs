using System.ComponentModel.DataAnnotations;

namespace raposo.ponto.Models.Models;

public class TimeEntry
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    [Range(0, int.MaxValue)]
    public int TransitTimeMinutes { get; set; }

    [Required]
    [StringLength(200)]
    public string Workplace { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Observations { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}