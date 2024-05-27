using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;
 
namespace TodoApp.Services;

public interface ICompanyService
{ 
    public Task<CompanyDto> GetSingleCompany(long id);
    public Task<IEnumerable<CompanyDto>>  GetCompanies();
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

    public async Task<CompanyDto> GetSingleCompany(long id)
    {
        var company = await _dbContext.Companies.Where(x => x.Id == id).Include(x => x.Cars).ThenInclude(c=>c.Company).Include(x=>x.Customers).Select(x =>
            new CompanyDto()
            {
                Name = x.Name,
                Address = x.Address,
                CustomerCount = x.Customers.Count(),
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
            }).SingleAsync();
        
        company.CustomerCount = _dbContext.Customers.Count(c => c.CompanyId == id);
        
        return company;
    }
    public async Task<IEnumerable<CompanyDto>> GetCompanies()
    {
        var allCompanies= await _dbContext.Companies.Include(x=>x.Cars).ThenInclude(c=>c.Company).Include(x=>x.Customers).Select(x=> new CompanyDto()
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
        }).ToListAsync();

        var countMap = _dbContext.Customers
            .Select(x=>x.CompanyId)
            .GroupBy(c => c)
            .ToDictionary(x => x.Key, x => x.Count());

        foreach (var item in allCompanies)
        {
            item.CustomerCount = countMap[item.Id];
        }

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