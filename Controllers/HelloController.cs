using Microsoft.AspNetCore.Mvc;

namespace RPG.Controllers;

[ApiController]
[Route("api")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }
}
