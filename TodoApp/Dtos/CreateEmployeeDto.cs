using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class CreateEmployeeDto
{
    [Required] 
    [StringLength(20)]
    public string Name { get; set; }
    
    [Required] 
    [StringLength(20)]
    public string LastName { get; set; }
    
    [Required] 
    public long CompanyId { get; set; }
}