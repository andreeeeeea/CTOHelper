using CTOHelper.Domain.Entities;
using CTOHelper.Domain.Enums;
using CTOHelper.Application.Interfaces;
using CTOHelper.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CTOHelper.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly UserService _us;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _db.Users.FindAsync(id);
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<bool> UpdateUserAsync(User user)
    {
        var existingUser = await _db.Users.FindAsync(user.Id);
        if (existingUser == null) return false;

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Role = user.Role;
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var existingUser = await _db.Users.FindAsync(userId);
        if (existingUser == null) return false;

        _db.Users.Remove(existingUser);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
    {
        return await _db.Users.Where(u => u.Role == role).ToListAsync();
    }
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _db.Users.ToListAsync();
    }
}
