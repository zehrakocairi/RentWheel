namespace TodoApp.Models;

public class Rent:BaseEntity
{
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public Boolean DayPrice { get; set; }
    
}