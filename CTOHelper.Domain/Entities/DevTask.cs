namespace CTOHelper.Domain.Entities;

public class DevTask
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string AssignedTo { get; set; } = string.Empty;
}