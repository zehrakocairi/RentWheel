using Microsoft.AspNetCore.Mvc;
using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
   

    private readonly ILogger<TodoItemsController> _logger;
    private readonly DataContext _dbContext;

    public TodoItemsController(ILogger<TodoItemsController> logger, DataContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var allItems = _dbContext.TodoItems.ToList();
        return Ok(allItems);
    }
    
    [HttpPost]
    public IActionResult CreateTodoItem(TodoItem item)
    {
        if (item == null)
        {
            return BadRequest();
        }

        _dbContext.TodoItems.Add(item);
        
        _dbContext.SaveChanges();
        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateTodoItem(TodoItem item)
    {
        if (item == null || item.Id == null)
        {
            return BadRequest();
        }

        var currentItem = _dbContext.TodoItems.Where(x => x.Id == item.Id).FirstOrDefault();
        if (currentItem == null)
        {
            return BadRequest($"There is no record with that Id : {item.Id}");
        }

        currentItem.Name = item.Name;
        currentItem.IsComplete = item.IsComplete;
        
        _dbContext.SaveChanges();
        
        return Ok();
    }
}