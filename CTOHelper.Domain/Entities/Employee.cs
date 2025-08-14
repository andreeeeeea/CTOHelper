using System.ComponentModel.DataAnnotations;

namespace CTOHelper.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfJoining { get; set; } = DateTime.UtcNow;
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }

    public Guid? DepartmentId { get; set; }

}