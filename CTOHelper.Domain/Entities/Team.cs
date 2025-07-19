using System.ComponentModel.DataAnnotations;

namespace CTOHelper.Domain.Entities;

public class Team
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid TeamLeadId { get; set; }
    public User TeamLead { get; set; } = null!;
    public ICollection<Employee> Members { get; set; } = new List<Employee>();

}