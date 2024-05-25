using TodoApp.Models;

namespace TodoApp.Dtos;

public class CarDto
{
    public long Id { get; set; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public DateTime ModelYear { get; set; }
    
    public Boolean IsAvailable { get; set; }

    public double DailyPrice { get; set; }

    public string CompanyName { get; set; }
}