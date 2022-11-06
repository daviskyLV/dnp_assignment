using DNP1_Server.Models;

namespace Shared.DTOs;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
}