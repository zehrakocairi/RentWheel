namespace TodoApp.Dtos;

public class StartRentalDto
{
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public int DurationInDays { get; set; }
}