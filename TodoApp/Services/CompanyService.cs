using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;
 
namespace TodoApp.Services;

public interface ICompanyService
{
    public Task<IEnumerable<CompanyDto>>  GetCompanies();

    public Task<CompanyDto> GetCompany(long id);
    public void Create(CreateCompanyDto dto);
    public void Update(Company item);
    public void Delete(long id);
}

public class CompanyService : ICompanyService
{
    private readonly DataContext _dbContext;

    public CompanyService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CompanyDto> GetCompany(long id)
    {
        var company = await _dbContext.Companies.Where(x => x.Id == id).Include(x => x.Cars).Select(x =>
            new CompanyDto()
            {
                Name = x.Name,
                Address = x.Address,
                CompanyCars = x.Cars.Select(c => new CarDto()
                {
                    Id = c.Id,
                    IsAvailable = c.IsAvailable,
                    Brand = c.Brand,
                    Model = c.Model,
                    DailyPrice = c.DailyPrice,
                    CompanyName = c.Company.Name,
                    ModelYear = c.ModelYear
                }).ToList(),
                CustomerCount = _dbContext.Customers.Count(c => c.CompanyId == x.Id)
            }).FirstOrDefaultAsync();
        
        return company;
    }
    public async Task<IEnumerable<CompanyDto>> GetCompanies()
    {
        var allCompanies= await _dbContext.Companies.Include(x=>x.Cars).Select(x=> new CompanyDto()
        {
            Name = x.Name,
            Address = x.Address,
            CompanyCars = x.Cars.Select(c => new CarDto()
            {
                Id = c.Id,
                IsAvailable = c.IsAvailable,
                Brand = c.Brand,
                Model = c.Model,
                DailyPrice = c.DailyPrice,
                CompanyName = c.Company.Name,
                ModelYear = c.ModelYear
            }).ToList(),
            CustomerCount = _dbContext.Customers.Count(c => c.CompanyId == x.Id)
        }).ToListAsync();

        return allCompanies;
    }

    public void Create(CreateCompanyDto dto)
    {
        var itemToCreate = new Company(dto);
        
        _dbContext.Companies.Add(itemToCreate);
        _dbContext.SaveChanges();
    }
    
    public void Update(Company item)
    {
        var currentItem = _dbContext.Companies.Where(x => x.Id == item.Id).FirstOrDefault();
        if (currentItem == null)
        {
            throw new Exception($"There is no record with that Id : {item.Id}");
        }

        currentItem.Name = item.Name;
        currentItem.Address = item.Address;
        
        _dbContext.SaveChanges();
    }
    public void Delete(long id)
    {
        var itemToDelete = _dbContext.Companies.Where(x=> x.Id == id).FirstOrDefault();
        if (itemToDelete == null)
        {
            throw new Exception($"There is no record with that Id : {id}");
        }

        _dbContext.Companies.Remove(itemToDelete);
        _dbContext.SaveChanges();
    }
}