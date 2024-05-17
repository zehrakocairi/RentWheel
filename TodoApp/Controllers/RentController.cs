using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RentController:ControllerBase
{
    private readonly ILogger<RentController> _logger;
    private readonly IRentService _rentService;
    
    public RentController(ILogger<RentController> logger, IRentService rentService)
    {
        _logger = logger;
        _rentService = rentService;
    }
    
    public IActionResult UpdateRent(Rent item)
    {
        if (item == null || item.Id < 1)
        {
            return BadRequest();
        }

        _rentService.Update(item);
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteRent(long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _rentService.Delete(id);

        return Ok();
    }

}