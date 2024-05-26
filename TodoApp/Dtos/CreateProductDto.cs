using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class CreateProductDto
{    
    [Required]
    public long Id { get; set; }

    [Required]
    [StringLength(20)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? Description { get; set; }
    
    [Required]
    public int LikeCount { get; set; }
}