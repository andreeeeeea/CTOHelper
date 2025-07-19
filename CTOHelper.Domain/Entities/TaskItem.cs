using System.ComponentModel.DataAnnotations;

namespace CTOHelper.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string Title { get; set; } = string.Empty;
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; } = DateTime.UtcNow;
    public Guid? AssignedToUserId { get; set; } 
    public User? AssignedTo { get; set; } = null!;
}