using TodoApp.Dtos;

namespace TodoApp.Models;

public class Customer:BaseEntity
{
    public Customer(CreateCustomerDto dto)
    {
        Name = dto.Name;
        Address = dto.Address;
        Email = dto.Email;
        CompanyId = dto.CompanyId;
        RentId = dto.RentId;
    }
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Email { get; set; }

    public long CompanyId { get; set; }
    
    public Company Company { get; set; }
    
    public long RentId { get; set; }
    public IEnumerable<Rent> Rents { get; set; } = new List<Rent>();
}