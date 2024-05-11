using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    
    public DbSet<Product> Products { get; set; } = null!;
}