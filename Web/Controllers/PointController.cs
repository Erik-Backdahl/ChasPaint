using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PointController : ControllerBase
{
    private readonly IPointService _pointService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUserService;
    public PointController(
        IPointService pointService,
        UserManager<ApplicationUser> userManager,
        ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
        _pointService = pointService;
    }
    [HttpGet("points")]
    public async Task<ActionResult<List<PointDTO>>> GetPoints(
        [FromBody] PointCoordinateSpanDTO pointSpan
    )
    {
        var result = await _pointService.GetPoints(pointSpan);

        return Ok(result);
    }
    [Authorize]
    [HttpPatch("update")]
    public async Task<IActionResult> UpdatePoints(
        [FromBody] List<PointDTO> points)
    {
        var domainUser = await _currentUserService.GetCurrentUserAsync();
        if (domainUser is null) return Unauthorized();

        await _pointService.UpdateBatch(points, domainUser);

        return NoContent();
    }
}