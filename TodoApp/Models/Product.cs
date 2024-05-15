namespace TodoApp.Models;

public class Product
{
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    public int LikeCount { get; set; }
}