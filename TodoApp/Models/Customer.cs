namespace TodoApp.Models;

public class Customer:BaseEntity
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Email { get; set; }

    public long CompanyId { get; set; }
    public IEnumerable<Company> Companies { get; set; }
    
    public long RentId { get; set; }
    public IEnumerable<Rent> Rents { get; set; } = new List<Rent>();
}