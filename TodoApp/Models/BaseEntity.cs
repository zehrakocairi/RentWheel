using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.Now;
}