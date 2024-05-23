using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAllCustomers()
    {
        var allItems = _customerService.GetCustomers();
        return Ok(allItems);
    }
    
    [HttpPost]
    public IActionResult CreateCustomer(Customer item)
    {
        if (item == null)
        {
            return BadRequest();
        }
        _customerService.Create(item);
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