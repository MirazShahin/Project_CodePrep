using CodePrep.Application.Dashboard.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _service;

    public DashboardController(IDashboardService service)
    {
        _service = service;
    }

    // Ekhane {userId:guid} add kora hoyeche dynamic route capture korar jnno
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetDashboard(Guid userId)
    {
        var result = await _service.GetDashboardAsync(userId);

        if (result == null)
            return Ok(new { }); // Null hole blank object return korbe, 404 dekhabe na

        return Ok(result);
    }
}