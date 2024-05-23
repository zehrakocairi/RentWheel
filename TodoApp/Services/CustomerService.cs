using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface ICustomerService
{
    public  IEnumerable<Customer> GetCustomers();
    public void Create(Customer item);
    public void Update(Customer item);
    public void Delete(long id);

}

public class CustomerService:ICustomerService
{
    private readonly DataContext _dbContext;
    
    public CustomerService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Customer> GetCustomers()
    {
        return _dbContext.Customers.ToList();
    }

    public void Create(Customer item)
    {
        _dbContext.Customers.Add(item);
        
        _dbContext.SaveChanges();
    }

    public void Update(Customer item)
    {
        var currentItem = _dbContext.Customers.Where(x => x.Id == item.Id).FirstOrDefault();
        if (currentItem == null)
        {
            throw new Exception($"There is no record with that Id : {item.Id}");
        }

        currentItem.Name = item.Name;
        currentItem.Address = item.Address;
        currentItem.Email = item.Email;
        
        _dbContext.SaveChanges();
    }

    public void Delete(long id)
    {
        var itemToDelete = _dbContext.Customers.Where(x=> x.Id == id).FirstOrDefault();
        if (itemToDelete == null)
        {
            throw new Exception($"There is no record with that Id : {id}");
        }

        _dbContext.Customers.Remove(itemToDelete);
        _dbContext.SaveChanges();
    }
}