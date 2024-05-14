using TodoApp.Infrastructure;
using TodoApp.Models;
 
namespace TodoApp.Services;

public interface ITodoItemService
{
    public IEnumerable<TodoItem> GetItems();
    public void Create(TodoItem item);
    public void Update(TodoItem item);
    public void Delete(long id);
}

public class TodoItemService : ITodoItemService
{
    private readonly DataContext _dbContext;

    public TodoItemService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<TodoItem> GetItems()
    {
        return _dbContext.TodoItems.ToList();
    }

    public void Create(TodoItem item)
    {
        _dbContext.TodoItems.Add(item);
        _dbContext.SaveChanges();
    }
    
    public void Update(TodoItem item)
    {
        var currentItem = _dbContext.TodoItems.Where(x => x.Id == item.Id).FirstOrDefault();
        if (currentItem == null)
        {
            throw new Exception($"There is no record with that Id : {item.Id}");
        }

        currentItem.Name = item.Name;
        currentItem.IsComplete = item.IsComplete;
        
        _dbContext.SaveChanges();
    }
    public void Delete(long id)
    {
        var itemToDelete = _dbContext.TodoItems.Where(x=> x.Id == id).FirstOrDefault();
        if (itemToDelete == null)
        {
            throw new Exception($"There is no record with that Id : {id}");
        }

        _dbContext.TodoItems.Remove(itemToDelete);
        _dbContext.SaveChanges();
    }
}