using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models;

public class Car : BaseEntity
{
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public DateTime ModelYear { get; set; }
    
    public Boolean IsAvailable { get; set; }

    public long CompanyId { get; set; }
    
    [ForeignKey("CompanyId")]
    public Company Company { get; set; }

    public IEnumerable<Rent> Rents { get; set; } = new List<Rent>();

}