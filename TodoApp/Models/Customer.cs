namespace TodoApp.Models;

public class Customer:BaseEntity
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Email { get; set; }

    public IEnumerable<Rent> Rents { get; set; } = new List<Rent>();
}