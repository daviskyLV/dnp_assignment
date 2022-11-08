using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HttpCllients.ClientInterfaces;

public interface IUserService
{
    public Task<ActionResult<User>> CreateUser([FromBody] ApiUser user);

}