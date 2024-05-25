using TodoApp.Dtos;

namespace TodoApp.Models;

public class Employee: BaseEntity
{
    public Employee()
    {
        
    }
    public Employee(CreateEmployeeDto dto)
    {
        Name = dto.Name;
        LastName = dto.LastName;
        CompanyId = dto.CompanyId;
        CompanyName = dto.CompanyName;
    }
    public string Name { get; set; }
    
    public string LastName { get; set; }
    
    public string CompanyName { get; set; }
    
    public long CompanyId { get; set; }
    
    public IEnumerable<Rent> Rents { get; set; }
}