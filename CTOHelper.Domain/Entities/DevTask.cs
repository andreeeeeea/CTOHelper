using System.ComponentModel.DataAnnotations;

namespace CTOHelper.Domain.Entities;

public class DevTask
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string Title { get; set; } = string.Empty;
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? AssignedToEmployeeId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public User? AssignedTo { get; set; } = null!;

    public Guid CreatedByUserId { get; set; }
    public User CreatedBy { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "New";
    public DateTime UpdatedAt { get; set; }
}