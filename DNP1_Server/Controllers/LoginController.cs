using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Database.Enums;
using DNP1_Server.Logic;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DNP1_Server.Controllers;
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{   
    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] ApiLogin login)
    {
        try
        {
            if (login.Username == null && login.Password == null)
                return StatusCode(400, "username or password not provided");

            var cookie = Program.LoginLogic.Login(login.Username, login.Password);

            return ""+cookie;
        }
        catch (Exception e)
        {
            return StatusCode(401, "something went wrong");
        }
    }
}