namespace TodoApp.Models;

public class Company: BaseEntity
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public IEnumerable<Car> Cars { get; set; }
}