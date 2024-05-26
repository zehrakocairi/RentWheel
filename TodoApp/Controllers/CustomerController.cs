using Microsoft.AspNetCore.Mvc;
using TodoApp.Dtos;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomerById([FromRoute]long id)
    {
        var customer =await _customerService.GetSingleCustomer(id);
        return Ok(customer);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var allItems =await _customerService.GetCustomers();
        return Ok(allItems);
    }
    
    [HttpPost]
    public IActionResult CreateCustomer(CreateCustomerDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }
        _customerService.Create(dto);
        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateCustomer(Customer item)
    {
        if (item == null || item.Id < 1)
        {
            return BadRequest();
        }

        _customerService.Update(item);
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteCustomer(long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _customerService.Delete(id);

        return Ok();
    }
}