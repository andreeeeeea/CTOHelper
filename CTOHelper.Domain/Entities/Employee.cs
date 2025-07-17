namespace CTOHelper.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime DateOfJoining { get; set; }
    public Guid? DepartmentId { get; set; }

}