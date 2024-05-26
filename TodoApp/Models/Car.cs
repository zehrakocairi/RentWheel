using System.ComponentModel.DataAnnotations.Schema;
using TodoApp.Dtos;

namespace TodoApp.Models;

public class Car : BaseEntity
{
    public Car(CreateCarDto dto)
    {
        Brand = dto.Brand;
        Model = dto.Model;
        ModelYear = dto.ModelYear;
        IsAvailable = dto.IsAvailable;
        DailyPrice = dto.DailyPrice;
        CompanyId = dto.CompanyId;
    }
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public DateTime ModelYear { get; set; }
    
    public Boolean IsAvailable { get; set; }

    public double DailyPrice { get; set; }

    public long CompanyId { get; set; }
    
    [ForeignKey("CompanyId")] 
    public Company Company { get; set; }

    public IEnumerable<Rent> Rents { get; set; } = new List<Rent>();

}