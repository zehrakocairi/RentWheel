using TodoApp.Models;

namespace TodoApp.Dtos;

public class RentDto
{
    public long Id { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public double TotalPrice { get; set; }
    
    public CarDto Car { get; set; }
}