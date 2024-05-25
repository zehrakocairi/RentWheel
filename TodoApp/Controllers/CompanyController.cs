using Microsoft.AspNetCore.Mvc;
using TodoApp.Dtos;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyService _companyService;

    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
    {
        _logger = logger;
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompanies()
    {
        var allItems =await _companyService.GetCompanies();
        return Ok(allItems);
    }
    
    [HttpPost]
    public IActionResult CreateCompany(CreateCompanyDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }
        _companyService.Create(dto);
        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateCompany(Company item)
    {
        if (item == null || item.Id < 1)
        {
            return BadRequest();
        }

        _companyService.Update(item);
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteCompany(long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _companyService.Delete(id);

        return Ok();
    }
}