using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaintController : ControllerBase
{
    public async Task<ActionResult<List<Point>>> UpdatePoints()
    {
        throw new NotImplementedException();
    }
}