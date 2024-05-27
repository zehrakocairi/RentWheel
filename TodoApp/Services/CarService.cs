using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface ICarService
{ 
    public  Task<CarDto> GetSingleCar(long id); 
    public Task<IEnumerable<CarDto>> GetCars();
    public void Create(CreateCarDto dto);
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

    public async Task<CarDto> GetSingleCar([FromRoute]long id)
    {
        // TODO : fix here
        var car = await _dbContext.Cars.Where(c => c.Id == id).Select(c=>new CarDto
        {
            Id = c.Id,
            Brand = c.Brand,
            Model = c.Model,
            ModelYear = c.ModelYear,
            IsAvailable = c.IsAvailable,
            DailyPrice = c.DailyPrice,
            CompanyName = c.Company.Name,
            
        }).SingleAsync();
        
        return car;
    }
    public async Task<IEnumerable<CarDto>> GetCars()
    {
        // TODO : fix here
        var cars = await _dbContext.Cars.Select(c => new CarDto
        {
            Id = c.Id,
            Brand = c.Brand,
            Model = c.Model,
            ModelYear = c.ModelYear,
            IsAvailable = c.IsAvailable,
            DailyPrice = c.DailyPrice,
            CompanyName = c.Company.Name,
        }).ToListAsync();

        return cars;
    }

    public void Create(CreateCarDto dto)
    {
        var newCar = new Car(dto); 
        
        _dbContext.Cars.Add(newCar);
        
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
        currentItem.DailyPrice = item.DailyPrice;
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