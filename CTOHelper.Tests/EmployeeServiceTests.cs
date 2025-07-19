using CTOHelper.Domain.Entities;
using CTOHelper.Infrastructure.Database;
using CTOHelper.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CTOHelper.Tests;

public class EmployeeServiceTests : IDisposable
{
    private readonly AppDbContext _db;
    private readonly EmployeeService _es;

    public EmployeeServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
             .UseSqlite("DataSource=:memory:")
             .Options;

        _db = new AppDbContext(options);
        _db.Database.OpenConnection();
        _db.Database.EnsureCreated();

        _es = new EmployeeService(_db);
    }

    [Fact]
    public async Task CreateEmployee_ShouldCreateEmployee()
    {
        var employee = new Employee
        {
            Name = "John Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };

        var result = await _es.CreateEmployeeAsync(employee);

        // Assertions to check if the employee was created successfully
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        var savedEmployee = await _db.Employees.FindAsync(result.Id);
        Assert.NotNull(savedEmployee);
        Assert.Equal("john@doe", savedEmployee.Email);
        Assert.Equal("Developer", savedEmployee.Role);
        Assert.Equal(employee.DepartmentId, savedEmployee.DepartmentId);
    }

    [Fact]
    public async Task GetEmployeeById_ShouldReturnEmployee()
    {
        var employee = new Employee
        {
            Name = "John Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        var result = await _es.GetEmployeeByIdAsync(employee.Id);

        // Assertions to check if the employee was retrieved successfully
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetEmployeeByEmail_ShouldReturnEmployee()
    {
        var employee = new Employee
        {
            Name = "John Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        var result = await _es.GetEmployeeByEmailAsync("john@doe");

        // Assertions to check if the employee was retrieved successfully
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task UpdateEmployee_ShouldUpdateEmployee()
    {
        var employee = new Employee
        {
            Name = "John Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        employee.Name = "John Smith";
        var result = await _es.UpdateEmployeeAsync(employee);

        // Assertions to check if the employee was updated successfully
        Assert.True(result);
        var updatedEmployee = await _db.Employees.FindAsync(employee.Id);
        Assert.NotNull(updatedEmployee);
        Assert.Equal("John Smith", updatedEmployee.Name);
    }

    [Fact]
    public async Task DeleteEmployee_ShouldDeleteEmployee()
    {
        var employee = new Employee
        {
            Name = "john Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        var result = await _es.DeleteEmployeeAsync(employee.Id);

        // Assertions to check if the employee was deleted successfully
        Assert.True(result);
        var deletedEmployee = await _db.Employees.FindAsync(employee.Id);
        Assert.Null(deletedEmployee);
    }

    [Fact]
    public async Task AssignToDepartment_ShouldAssignEmployeeToDepartment()
    {
        var employee = new Employee
        {
            Name = "john Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();
        var newDepartmentId = Guid.NewGuid();

        var result = await _es.AssignToDepartmentAsync(employee.Id, newDepartmentId);

        // Assertions to check if the employee was assigned to the new department successfully
        Assert.True(result);
        var updatedEmployee = await _db.Employees.FindAsync(employee.Id);
        Assert.NotNull(updatedEmployee);
        Assert.Equal(newDepartmentId, updatedEmployee.DepartmentId);
    }

    [Fact]
    public async Task RemoveFromDepartment_ShouldRemoveEmployeeFromDepartment()
    {
        var employee = new Employee
        {
            Name = "john Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();
        var departmentId = employee.DepartmentId;

        var result = await _es.RemoveFromDepartmentAsync(employee.Id, employee.DepartmentId.Value);

        // Assertions to check if the employee was removed from the department successfully
        Assert.True(result);
        var updatedEmployee = await _db.Employees.FindAsync(employee.Id);
        Assert.NotNull(updatedEmployee);
        Assert.Null(updatedEmployee.DepartmentId);
    }

    [Fact]
    public async Task GetAllEmployees_ShouldReturnAllEmployees()
    {
        var employee1 = new Employee
        {
            Name = "John Doe",
            Email = "john@doe",
            Role = "Developer",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        var employee2 = new Employee
        {
            Name = "Jane Smith",
            Email = "jane@smith",
            Role = "Manager",
            DateOfJoining = DateTime.Now,
            DepartmentId = Guid.NewGuid()
        };
        _db.Employees.AddRange(employee1, employee2);
        await _db.SaveChangesAsync();

        var employees = await _es.GetEmployeesAsync();

        // Assertions to check if all employees were retrieved successfully
        Assert.NotNull(employees);
        Assert.Equal(2, employees.Count());
    }
    public void Dispose()
    {
        _db.Database.CloseConnection();
        _db.Dispose();
    }
}