using CTOHelper.Domain.Entities;
using CTOHelper.Application.Interfaces;
using CTOHelper.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CTOHelper.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _db;

    public EmployeeService(AppDbContext db)
    {
        _db = db;
    }
    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();
        return employee;
    }
    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        return await _db.Employees.FindAsync(id);
    }
    public async Task<Employee?> GetEmployeeByEmailAsync(string email)
    {
        return await _db.Employees.FirstOrDefaultAsync(e => e.Email == email);
    }
    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        var existingEmployee = await _db.Employees.FindAsync(employee.Id);
        if (existingEmployee == null) return false;

        existingEmployee.Name = employee.Name;
        existingEmployee.Email = employee.Email;
        existingEmployee.Role = employee.Role;
        existingEmployee.DepartmentId = employee.DepartmentId;

        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
    {
        var existingEmployee = await _db.Employees.FindAsync(employeeId);
        if (existingEmployee == null) return false;

        _db.Employees.Remove(existingEmployee);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> AssignToDepartmentAsync(Guid employeeId, Guid departmentId)
    {
        var existingEmployee = await _db.Employees.FindAsync(employeeId);
        if (existingEmployee == null) return false;

        existingEmployee.DepartmentId = departmentId;
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> RemoveFromDepartmentAsync(Guid employeeId, Guid departmentId)
    {
        var existingEmployee = await _db.Employees.FindAsync(employeeId);
        if (existingEmployee == null) return false;

        if (existingEmployee.DepartmentId != departmentId) return false;

        existingEmployee.DepartmentId = null;
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid? departmentId = null, string? role = null)
    {
        var query = _db.Employees.AsQueryable();

        if (departmentId.HasValue)
        {
            query = query.Where(e => e.DepartmentId == departmentId.Value);
        }

        if (!string.IsNullOrEmpty(role))
        {
            query = query.Where(e => e.Role == role);
        }

        return await query.ToListAsync();
    }

}