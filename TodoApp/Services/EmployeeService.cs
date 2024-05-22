using Microsoft.EntityFrameworkCore;
using TodoApp.Dtos;
using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;

public interface IEmployeeService
{
    public Task<IEnumerable<Employee>> GetEmployees();

    public Task CreateEmployee(CreateEmployeeDto dto);

    public Task UpdateEmployee(Employee item);

    public void DeleteEmployee(long id);
    
}

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _dbContext;

    public EmployeeService(DataContext dbContex)
    {
        _dbContext = dbContex; 
    }
    
    public async Task<IEnumerable<Employee>> GetEmployees()
    {
      return await (_dbContext.Employees.Include(x=>x.Companies).ToListAsync());
    }
    
    public async Task CreateEmployee(CreateEmployeeDto dto)
    {
        var newEmployee = new Employee(dto);
        
        _dbContext.Employees.AddAsync(newEmployee);
        _dbContext.SaveChangesAsync();

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

        _dbContext.SaveChangesAsync();

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