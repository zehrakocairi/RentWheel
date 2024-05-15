using Microsoft.AspNetCore.Mvc;
using TodoApp.Infrastructure;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly ILogger<TodoItemsController> _logger;
    private readonly ITodoItemService _todoItemService;

    public TodoItemsController(ILogger<TodoItemsController> logger, ITodoItemService todoItemService)
    {
        _logger = logger;
        _todoItemService = todoItemService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var allItems = _todoItemService.GetItems();
        return Ok(allItems);
    }
    
    [HttpPost]
    public IActionResult CreateTodoItem(TodoItem item)
    {
        if (item == null)
        {
            return BadRequest();
        }

        _todoItemService.Create(item);
        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateTodoItem(TodoItem item)
    {
        if (item == null || item.Id < 1)
        {
            return BadRequest();
        }

        _todoItemService.Update(item);
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteTodoItem(long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _todoItemService.Delete(id);

        return Ok();
    }
}