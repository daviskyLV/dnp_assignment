using Microsoft.AspNetCore.Mvc;

namespace DNP1_Server.Controllers.ApiClasses;

[ApiController]
[Route("[controller]")]

public class LogoutController : ControllerBase
{
    
    [HttpPost]
    public async Task<ActionResult<string>> Logout([FromBody] ApiUser login)
    {
        
        Console.WriteLine(login.UserName);
        Console.WriteLine(login.Password);
        try
        {
            
            if (login.UserName != null && login.Password != null)
            {
                Program.LoginLogic.Logout(login.UserName, login.Password);
            }

            return Ok("user has been logged out");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(401, "something went wrong");
        }
    }
}