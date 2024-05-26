using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Dtos;


namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RentController:ControllerBase
{
    private readonly IRentService _rentService;
    
    public RentController(IRentService rentService)
    {
        _rentService = rentService;
    }

    [HttpGet("{id}")]
    public  async Task<IActionResult> GetRentalById([FromRoute]long id)
    {
        return Ok(await _rentService.GetSingleRental(id));
    }
    
    [HttpGet("employee-rentals")]
    public async Task<IActionResult> GetEmployeeRentals([FromQuery]long empoloyeeId)
    { 
        return Ok(await _rentService.GetEmployeesRental(empoloyeeId));
    }

    [HttpPost("start-rental")]
    public IActionResult StartRental([FromBody]StartRentalDto dto)
    {
        _rentService.StartRental(dto);
        return Ok();
    }
    
    [HttpGet("{id}/end-rental")]
    public IActionResult EndRental([FromRoute]int id)
    {
        _rentService.EndRental(id);
        return Ok();
    }

    [HttpPut("{rentId}/update-end-date")]
    public IActionResult UpdateRent([FromRoute]int rentId, [FromQuery]DateTime newEndDate)
    {
        if (rentId< 1)
        {
            return BadRequest();
        }

        _rentService.UpdateRentEndDate(rentId, newEndDate);
        
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