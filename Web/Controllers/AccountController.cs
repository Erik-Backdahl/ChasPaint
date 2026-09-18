using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;

    public AccountController(RegisterUserHandler registerUserHandler)
    {
        _registerUserHandler = registerUserHandler; 
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand cmd)
    {
        var result = await _registerUserHandler.Handle(cmd);
        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }
}