using TodoApp.Models;

namespace TodoApp.Dtos;

public class EmployeeDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string LastName { get; set; }
    
    public string CompanyName { get; set; }
}