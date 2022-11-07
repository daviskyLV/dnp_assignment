using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Database.Enums;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DNP1_Server.Controllers; 

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase {
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] ApiUser user)
    {


        var dbResponse = Program.Database.CreateUser(user.UserName, user.Password);
        return dbResponse.Item1 switch
        {
            CreateUserEnum.Success => dbResponse.Item2,
            CreateUserEnum.AlreadyExists => StatusCode(400, "Already exists"),
            CreateUserEnum.InternalError => StatusCode(500, "Database error"),
            _ => StatusCode(500, "Unknown server error")
        };
    }


}
    
       
    

    
