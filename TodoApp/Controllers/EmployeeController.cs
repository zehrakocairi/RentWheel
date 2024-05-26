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
    [Route("{id}")]
    public async Task<IActionResult> GetEmployeeById([FromRoute]long id)
    {
        var employee = await _employeeService.GetEmployee(id);
        
        return Ok(employee);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var allItems =await _employeeService.GetEmployees();
        
        return Ok(allItems);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeDto dto)
    { 
      var createdEmployee = await _employeeService.CreateEmployee(dto);
       return Ok(createdEmployee);
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute]long id, [FromBody]Employee item)
    {
        if (item.Id != id)
        {
            return BadRequest("The ID in the route does not match the ID in the model.");
        }

        if (item.Id < 1)
        {
            return BadRequest("Invalid ID.");
        }
        
        await _employeeService.UpdateEmployee(item);
        var updatedEmployee = await _employeeService.GetEmployee(id);
        
         return Ok(updatedEmployee);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteEmployee([FromRoute]long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        _employeeService.DeleteEmployee(id);

        return Ok();
    }
    
}


