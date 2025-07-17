using CTOHelper.Domain.Entities;

namespace CTOHelper.Application.Interfaces;

public interface IEmployeeService
{
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<Employee?> GetEmployeeByIdAsync(Guid id);
    Task<Employee?> GetEmployeeByEmailAsync(string email);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(Guid employeeId);
    Task<bool> AssignToDepartmentAsync(Guid employeeId, Guid departmentId);
    Task<bool> RemoveFromDepartmentAsync(Guid employeeId, Guid departmentId);
    Task<IEnumerable<Employee>> GetEmployeesAsync(Guid? departmentId = null, string? role = null);
}