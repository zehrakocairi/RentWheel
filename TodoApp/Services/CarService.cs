using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface ICarService
{
    public IEnumerable<Car> GetCars();
    public void Create(Car item);
    public void Update(Car item);
    public void Delete(long id);
    
}

public class CarService:ICarService
{
    private readonly DataContext _dbContext;
    
    public CarService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<Car> GetCars()
    {
        return _dbContext.Cars.ToList();
    }

    public void Create(Car item)
    {
        _dbContext.Cars.Add(item);
        
        _dbContext.SaveChanges();
    }

    public void Update(Car item)
    {
        var currentItem = _dbContext.Cars.Where(x => x.Id == item.Id).FirstOrDefault();
        if (currentItem == null)
        {
            throw new Exception($"There is no record with that Id : {item.Id}");
        }

        currentItem.Brand = item.Brand;
        currentItem.Model = item.Brand;
        currentItem.ModelYear = item.ModelYear;
        currentItem.IsAvailable = item.IsAvailable;
        currentItem.CompanyId = item.CompanyId;
        
        _dbContext.SaveChanges();
    }

    public void Delete(long id)
    {
        var itemToDelete = _dbContext.Cars.Where(x=> x.Id == id).FirstOrDefault();
        if (itemToDelete == null)
        {
            throw new Exception($"There is no record with that Id : {id}");
        }

        _dbContext.Cars.Remove(itemToDelete);
        _dbContext.SaveChanges();
    }
}