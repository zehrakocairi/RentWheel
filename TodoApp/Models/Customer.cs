namespace TodoApp.Models;

public class Customer:BaseEntity
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Email { get; set; }

    public string CompanyId { get; set; }
    public IEnumerable<Company> Companies { get; set; }
    
    public IEnumerable<Rent> Rents { get; set; } = new List<Rent>();
}