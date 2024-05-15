namespace TodoApp.Models;

public class Rent:BaseEntity
{
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public Boolean DayPrice { get; set; }

    public int ClientId { get; set; }
    
    public int CarId { get; set; }
    
    public Client Car { get; set; }
    public Client Client { get; set; }

}