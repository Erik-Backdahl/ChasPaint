using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        RegisterUserHandler registerUserHandler,
        SignInManager<ApplicationUser> signInManager)
    {
        _registerUserHandler = registerUserHandler;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand cmd)
    {
        var result = await _registerUserHandler.Handle(cmd);
        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();        
        return NoContent();
    }
}