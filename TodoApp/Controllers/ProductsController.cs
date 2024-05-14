using Microsoft.AspNetCore.Mvc;
using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController:ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly DataContext _dbContext;

    public ProductsController(ILogger<ProductsController> logger, DataContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var allProducts = _dbContext.Products.ToList();
        return Ok(allProducts);
    }

    [HttpPost]
    public IActionResult CreateProduct(Product item)
    {
        if (item == null)
        {
            return BadRequest("No product!");
        }

        _dbContext.Products.Add(item);
        
        _dbContext.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateProduct(Product item)
    {
        if (item == null)
        {
            return BadRequest("No product to be updated!");
        }

        var existingProduct = _dbContext.Products.Where(p => p.Id == item.Id).FirstOrDefault();

        if (existingProduct==null)
        {
            return BadRequest($"There is no record with that Id : {item.Id}");
        }

        existingProduct.LikeCount = item.LikeCount;

        _dbContext.SaveChanges();

        return Ok(); 
    }

    [HttpDelete]
    public IActionResult DeleteProduct(long Id)
    {
        var productToDelete = _dbContext.Products.Where(p => p.Id == Id).FirstOrDefault();
        if (productToDelete==null)
        {
           return NotFound();
        }

        _dbContext.Products.Remove(productToDelete);
        _dbContext.SaveChanges();

        return Ok();
    }
}