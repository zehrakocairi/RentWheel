using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface IEmployeeService
{
    public Task<IEnumerable<EmployeeDto>> GetEmployees();

    public Task<EmployeeDto?> GetEmployee(long id);

    public Task<Employee?> CreateEmployee(CreateEmployeeDto dto);

    public Task UpdateEmployee(Employee item);

    public void DeleteEmployee(long id);
    
}

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _dbContext;

    public EmployeeService(DataContext dbContext)
    {
        _dbContext = dbContext; 
    }
    
    public async Task<IEnumerable<EmployeeDto>> GetEmployees()
    {
        var allEmployees = await _dbContext.Employees
            .Include(x => x.CompanyName)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                LastName = e.LastName,
                CompanyName = e.CompanyName
            })
            .ToListAsync();
        
        return allEmployees;
    }

    public async Task<EmployeeDto?> GetEmployee(long id)
    {
        var employee = await _dbContext.Employees
            .Include(x => x.CompanyName)
            .Where(x => x.Id == id)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                LastName = e.LastName,
                CompanyName = e.CompanyName
            })
            .FirstOrDefaultAsync();

        return employee;
    }

    public async Task<Employee?> CreateEmployee(CreateEmployeeDto dto)
    {
        var isCompanyExist = await _dbContext.Companies.Where(c => c.Id == dto.CompanyId).AnyAsync();
        
        if (!isCompanyExist)
        {
            throw new Exception($"There is no company with that Id : {dto.CompanyId}");
        }

        var newEmployee = new Employee(dto);
        
      await  _dbContext.Employees.AddAsync(newEmployee);
      await _dbContext.SaveChangesAsync();

      return newEmployee;
    }
    
    public async Task UpdateEmployee(Employee item)
    {
        var employeeToUpdate = _dbContext.Employees.Where(x => x.Id == item.Id).FirstOrDefault();
        
        if (employeeToUpdate == null)
        {
            throw new Exception($"There is no record with that Id : {item.Id}");
        }

        employeeToUpdate.Name = item.Name;
        employeeToUpdate.LastName = item.LastName;
        
        var isCompanyExist = await _dbContext.Companies.Where(c => c.Id == item.CompanyId).AnyAsync();
        if (!isCompanyExist)
        {
            throw new Exception($"There is no company with that Id : {item.Id}");
        }
        
        employeeToUpdate.CompanyId = item.CompanyId;

       await _dbContext.SaveChangesAsync();

    }

    public void DeleteEmployee(long id)
    {
        var itemToDelete = _dbContext.Employees.Where(x=> x.Id == id).FirstOrDefault();
        if (itemToDelete == null)
        {
            throw new Exception($"There is no record with that Id : {id}");
        }

        _dbContext.Employees.Remove(itemToDelete);
        _dbContext.SaveChanges();
    }
}