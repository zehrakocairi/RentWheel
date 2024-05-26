using TodoApp.Dtos;

namespace TodoApp.Models;

public class Product
{
    public Product(CreateProductDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Description = dto.Description;
        LikeCount = dto.LikeCount;
    }
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int LikeCount { get; set; }
}