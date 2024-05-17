using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface IRentService
{
    public DateTime StartDate();
    public DateTime End(Rent item);
    public void Update(Rent item);
    public void Delete(long id);
    public IEnumerable<Rent> Search();
}

public class RentService():IRentService
{
    private readonly DataContext _dbContext;
    
    public RentService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public DateTime StartDate()
    {
        throw new NotImplementedException();
    }

    public DateTime End(Rent item)
    {
        throw new NotImplementedException();
    }

    public void Update(Rent item)
    {
        var currentItem = _dbContext.Rents.Where(x => x.Id == item.Id).FirstOrDefault();
        if (currentItem == null)
        {
            throw new Exception($"There is no record with that Id : {item.Id}");
        }

        currentItem.DayPrice = item.DayPrice;
        
        _dbContext.SaveChanges();
    }

    public void Delete(long id)
    {
        var itemToDelete = _dbContext.Rents.Where(x=> x.Id == id).FirstOrDefault();
        if (itemToDelete == null)
        {
            throw new Exception($"There is no record with that Id : {id}");
        }

        _dbContext.Rents.Remove(itemToDelete);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Rent> Search()
    {
        throw new NotImplementedException();
    }
}