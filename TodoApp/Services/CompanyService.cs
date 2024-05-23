using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;
 
namespace TodoApp.Services;

public interface ICompanyService
{
    public IEnumerable<Company> GetCompanies();
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
    public IEnumerable<Company> GetCompanies()
    {
        return _dbContext.Companies.Include(x=>x.Cars).ToList();
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