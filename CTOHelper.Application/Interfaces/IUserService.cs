using CTOHelper.Domain.Entities;
using CTOHelper.Domain.Enums;

namespace CTOHelper.Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid userId);
    Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
    Task<IEnumerable<User>> GetUsersAsync();
}