using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CarController:ControllerBase
{
    private readonly ILogger<CarController> _logger;
    private readonly ICarService _carService;
    
    public CarController(ILogger<CarController> logger, ICarService companyService)
    {
        _logger = logger;
        _carService = companyService;
    }

    [HttpGet]
    public IActionResult GetAllCars()
    {
        var allCars = _carService.GetCars();
        return Ok(allCars);
    }
    
    [HttpPost]
    public IActionResult CreateCompany(Car item)
    {
        if (item == null)
        {
            return BadRequest();
        }
        _carService.Create(item);
        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateCompany(Car item)
    {
        if (item == null || item.Id < 1)
        {
            return BadRequest();
        }

        _carService.Update(item);
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteCompany(long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _carService.Delete(id);

        return Ok();
    }
}