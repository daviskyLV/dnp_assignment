using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DNP1_Server.Controllers;
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{   
    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] ApiUser login)
    {
        try {
            if (login.UserName == null && login.Password == null)
                return StatusCode(400, "username or password not provided");

            var cookie = Program.LoginLogic.Login(login.UserName, login.Password);

            return "" + cookie;
        } catch (NotFoundException e) {
            return StatusCode(404, e.Message);
        } catch (DataMismatchException e) {
            return StatusCode(400, e.Message);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}