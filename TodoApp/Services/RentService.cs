using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface IRentService
{
    public Task StartRental(StartRentalDto dto);
    public Task EndRental(int rentId);
    public Task UpdateRentEndDate(int rentId ,DateTime dateToEndRental);
    public void Delete(long id);
    public IEnumerable<Rent> Search();
}

public class RentService:IRentService
{
    private readonly DataContext _dbContext;
    
    public RentService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task StartRental(StartRentalDto dto)
    {
        var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == dto.CarId);
        if (car == null)
        {
            throw new Exception($"Car couldn't be found with id: {dto.CarId}");
        }

        var now = DateTime.UtcNow;
        var rental = new Rent
        {
            CustomerId = dto.CustomerId,
            CarId = dto.CarId,
            TotalPrice = dto.DurationInDays * car.DailyPrice,
            StartDate = now,
            EndDate = now.AddDays(dto.DurationInDays),
        };
        
        _dbContext.Rents.Add(rental);
        car.IsAvailable = false;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task EndRental(int rentId)
    {
        var rent = await _dbContext.Rents.Include(x=>x.Car).FirstOrDefaultAsync(x => x.Id == rentId);
        if (rent == null)
        {
            throw new Exception($"Rent couldn't be found with id: {rentId}");
        }
        
        if (rent.Car == null)
        {
            throw new Exception($"Car couldn't be found with id: {rent.Car.Id}");
        }

        rent.EndDate = DateTime.UtcNow;
       
        rent.Car.IsAvailable = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRentEndDate(int rentId ,DateTime dateToEndRental)
    {
        var rent = await _dbContext.Rents.Where(x => x.Id == rentId).FirstOrDefaultAsync();
        if (rent == null)
        {
            throw new Exception($"There is no record with that Id : {rentId}");
        }

        rent.EndDate = dateToEndRental;
        
        await _dbContext.SaveChangesAsync();
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