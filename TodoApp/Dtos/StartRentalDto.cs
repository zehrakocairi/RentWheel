using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class StartRentalDto
{
    [Range(1,int.MaxValue)]
    public int CustomerId { get; set; }
    [Range(1,int.MaxValue)]
    public int CarId { get; set; }
    public int DurationInDays { get; set; }
}