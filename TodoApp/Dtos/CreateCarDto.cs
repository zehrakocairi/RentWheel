using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class CreateCarDto
{
    [Required] 
    [StringLength(20)]
    public string Brand { get; set; }
    
    [Required] 
    [StringLength(30)]
    public string Model { get; set; }
    
    [Required]
    public DateTime ModelYear { get; set; }
    
    [Required]
    public Boolean IsAvailable { get; set; }
 
    [Required]
    public double DailyPrice { get; set; }

    [Required]
    public long CompanyId { get; set; }

}