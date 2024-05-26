using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class CreateCustomerDto
{
    [Required] 
    [StringLength(20)]
    public string Name { get; set; }
    
    [Required] 
    [StringLength(20)]
    public string Address { get; set; }
    
    [Required] 
    public string Email { get; set; }
    
    [Required] 
    public long CompanyId { get; set; }
    
    [Required] 
    public long RentId { get; set; }
}