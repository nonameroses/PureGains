using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Identity;
[ApiController]
[Route("api/messages")]
public class AuthController : Controller
{
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok(new
        {
            Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
        });
    }

    [HttpGet("private-scoped")]
    [Authorize("read:messages")]
    public IActionResult Scoped()
    {

        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated and have a scope of read:messages to see this."
        });
    }

    [HttpGet("private")]
    [Authorize]
    public IActionResult Private()
    {
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated to see this."
        });
    }

    [HttpGet("protected")]
    [Authorize]
    public IActionResult GetProtectedMessage()
    {
        return Ok(new
        {
            Message = "Hello from a proctecd endpoint! You need to be authenticated to see this."
        });
    }

    [HttpGet("admin")]
    [Authorize("read:admin-messages")]
    public IActionResult GetAdminMessage()
    {
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated to see this."
        });
    }

    // This is a helper action. It allows you to easily view all the claims of the token.
    [HttpGet("claims")]
    public IActionResult Claims()
    {
        return Ok(User.Claims.Select(c =>
            new
            {
                c.Type,
                c.Value
            }));
    }
}
