using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Exceptions;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DNP1_Server.Controllers; 

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase {
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] ApiUser user)
    {
        try {
            var dbResponse = await Program.Database.CreateUserAsync(
                new User(user.UserName, user.Password)
            );
            return dbResponse;
        } catch (DuplicateDataException e) {
            return StatusCode(400, e.Message);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}
    
       
    

    
