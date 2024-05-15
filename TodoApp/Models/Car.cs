namespace TodoApp.Models;

public class Car:BaseEntity
{
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public DateTime ModelYear { get; set; }
    
    public Boolean IsAvailable { get; set; }
    
}