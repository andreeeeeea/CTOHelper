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
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

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

    public void Dispose()
    {
        _db.Database.CloseConnection();
        _db.Dispose();
    }
}