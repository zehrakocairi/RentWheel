namespace TodoApp.Models;

public class Client:BaseEntity
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Email { get; set; }
}