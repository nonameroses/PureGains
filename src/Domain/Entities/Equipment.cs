using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Equipment
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}