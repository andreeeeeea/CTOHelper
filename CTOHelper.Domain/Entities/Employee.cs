using System.ComponentModel.DataAnnotations;

namespace CTOHelper.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(50)]
    public string Role { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfJoining { get; set; }
    public Guid? DepartmentId { get; set; }

}