using TodoApp.Models;

namespace TodoApp.Dtos;

public class CustomerDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }

    public List<RentDto> Rents { get; set; }

    public List<CarDto> RentedCars { get; set; }
}