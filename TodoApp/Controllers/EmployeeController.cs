using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Dtos;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController: ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var allItems =await _employeeService.GetEmployees();
        
        return Ok(allItems);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }

       await _employeeService.CreateEmployee(dto);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateEmployee(Employee item)
    {
        if (item == null || item.Id < 1)
        {
            return BadRequest();
        }

      await _employeeService.UpdateEmployee(item);
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteEmployee(long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _employeeService.DeleteEmployee(id);

        return Ok();
    }
    
}


