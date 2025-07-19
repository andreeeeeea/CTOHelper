using CTOHelper.Domain.Entities;
using CTOHelper.Domain.Enums;
using CTOHelper.Infrastructure.Database;
using CTOHelper.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CTOHelper.Tests;

public class UserServiceTests : IDisposable
{
    private readonly AppDbContext _db;
    private readonly UserService _us;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _db = new AppDbContext(options);
        _db.Database.OpenConnection();
        _db.Database.EnsureCreated();

        _us = new UserService(_db);
    }

    [Fact]
    public async Task CreateUser_ShouldCreateUser()
    {
        var user = new User
        {
            Name = "Test User",
            Email = "test@user",
            Role = UserRole.Developer
        };

        var result = await _us.CreateUserAsync(user);

        // Assertions to check if the user was created successfully
        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
        var savedUser = await _db.Users.FindAsync(result.Id);
        Assert.NotNull(savedUser);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser()
    {
        var user = new User
        {
            Name = "Test User",
            Email = "test@user",
            Role = UserRole.Developer
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var result = await _us.GetUserByIdAsync(user.Id);

        // Assertions to check if the user was retrieved successfully
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal("Test User", result.Name);
    }
    [Fact]
    public async Task GetUserByEmail_ShouldReturnUser()
    {
        var user = new User
        {
            Name = "Test User",
            Email = "test@user",
            Role = UserRole.Developer
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var result = await _us.GetUserByEmailAsync(user.Email);

        // Assertions to check if the user was retrieved successfully
        Assert.NotNull(result);
        Assert.Equal(user.Email, result.Email);
    }
    [Fact]
    public async Task UpdateUser_ShouldUpdateUser()
    {
        var user = new User
        {
            Name = "Test User",
            Email = "test@user",
            Role = UserRole.Developer
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        user.Name = "Updated User";
        var result = await _us.UpdateUserAsync(user);

        // Assertions to check if the user was updated successfully
        Assert.True(result);
        var updatedUser = await _db.Users.FindAsync(user.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal("Updated User", updatedUser.Name);
    }
    [Fact]
    public async Task DeleteUser_ShouldDeleteUser()
    {
        var user = new User
        {
            Name = "Test User",
            Email = "test@user",
            Role = UserRole.Developer
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var result = await _us.DeleteUserAsync(user.Id);

        // Assertions to check if the user was deleted successfully
        Assert.True(result);
        var deletedUser = await _db.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);
    }
    [Fact]
    public async Task GetUsersByRole_ShouldReturnUsersByRole()
    {
        var user1 = new User
        {
            Name = "User One",
            Email = "user1@user",
            Role = UserRole.Developer
        };
        var user2 = new User
        {
            Name = "User Two",
            Email = "user2@user",
            Role = UserRole.Developer
        };
        _db.Users.AddRange(user1, user2);
        await _db.SaveChangesAsync();

        var result = await _us.GetUsersByRoleAsync(UserRole.Developer);

        // Assertions to check if the users were retrieved successfully
        Assert.NotNull(result);
        Assert.Contains(result, u => u.Email == "user1@user");
        Assert.Contains(result, u => u.Email == "user2@user");
    }
    [Fact]
    public async Task GetUsers_ShouldReturnAllUsers()
    {
        var user1 = new User
        {
            Name = "User One",
            Email = "user1@user",
            Role = UserRole.Developer
        };
        var user2 = new User
        {
            Name = "User Two",
            Email = "user2@user",
            Role = UserRole.Developer
        };
        _db.Users.AddRange(user1, user2);
        await _db.SaveChangesAsync();

        var result = await _us.GetUsersAsync();

        // Assertions to check if all users were retrieved successfully
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    public void Dispose()
    {
        _db.Database.EnsureDeleted();
        _db.Dispose();
    }
}