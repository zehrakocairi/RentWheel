namespace TodoApp.Dtos;

public class CreateEmployeeDto
{
    public string? Name { get; set; }
    
    public string? LastName { get; set; }
    
    public long CompanyId { get; set; }
}