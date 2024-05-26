using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface ICustomerService
{
    public Task<CustomerDto> GetSingleCustomer(long id);
    public Task<IEnumerable<CustomerDto>> GetCustomers();
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
    public async Task<CustomerDto> GetSingleCustomer(long id)
    {
        var customer = await _dbContext.Customers
            .Where(x=>x.Id==id)
            .Include(c => c.Rents)
            .ThenInclude(r => r.Car)
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Rents = c.Rents.Select(r => new RentDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalPrice = r.TotalPrice,
                    Car = new CarDto
                    {
                        Id = r.Car.Id,
                        Brand = r.Car.Brand,
                        Model = r.Car.Model,
                        ModelYear = r.Car.ModelYear,
                        IsAvailable = r.Car.IsAvailable,
                        DailyPrice = r.Car.DailyPrice,
                        CompanyName = r.Car.Company.Name
                    }
                }).ToList(),
                RentedCars = c.Rents
                    .Where(r => !r.Car.IsAvailable)
                    .Select(r => new CarDto
                    {
                        Id = r.Car.Id,
                        Brand = r.Car.Brand,
                        Model = r.Car.Model,
                        ModelYear = r.Car.ModelYear,
                        IsAvailable = r.Car.IsAvailable,
                        DailyPrice = r.Car.DailyPrice,
                        CompanyName = r.Car.Company.Name
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        return customer;
    }
    public async Task<IEnumerable<CustomerDto>> GetCustomers()
    {
        var allCustomers = await _dbContext.Customers
            .Include(c => c.Rents)
            .ThenInclude(r => r.Car)
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Rents = c.Rents.Select(r => new RentDto
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalPrice = r.TotalPrice,
                    Car = new CarDto
                    {
                        Id = r.Car.Id,
                        Brand = r.Car.Brand,
                        Model = r.Car.Model,
                        ModelYear = r.Car.ModelYear,
                        IsAvailable = r.Car.IsAvailable,
                        DailyPrice = r.Car.DailyPrice,
                        CompanyName = r.Car.Company.Name
                    }
                }).ToList(),
                RentedCars = c.Rents
                    .Where(r => !r.Car.IsAvailable)
                    .Select(r => new CarDto
                    {
                        Id = r.Car.Id,
                        Brand = r.Car.Brand,
                        Model = r.Car.Model,
                        ModelYear = r.Car.ModelYear,
                        IsAvailable = r.Car.IsAvailable,
                        DailyPrice = r.Car.DailyPrice,
                        CompanyName = r.Car.Company.Name
                    }).ToList()
            })
            .ToListAsync();

        return allCustomers;
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