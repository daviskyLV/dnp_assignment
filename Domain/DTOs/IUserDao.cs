using DNP1_Server.Models;

namespace Shared.DTOs;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
}