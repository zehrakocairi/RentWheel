using TodoApp.Models;

namespace TodoApp.Dtos;

public class CompanyDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public List<CarDto> CompanyCars { get; set; }

    public int CustomerCount { get; set; }
}