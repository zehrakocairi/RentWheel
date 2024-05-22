namespace TodoApp.Models;

public class Employee
{
    public string? Name { get; set; }
    
    public string? LastName { get; set; }
    
    public long CompanyId { get; set; }
    
    public IEnumerable<Rent> Rents { get; set; }
    
}