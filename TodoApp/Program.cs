using Microsoft.EntityFrameworkCore;
using TodoApp.Infrastructure;
using TodoApp.Services;

namespace TodoApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddTransient<ICompanyService, CompanyService>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<ICarService, CarService>();
        builder.Services.AddTransient<ICustomerService, CustomerService>();
        builder.Services.AddTransient<IRentService, RentService>();
        builder.Services.AddTransient<IEmployeeService, EmployeeService>();
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddDbContext<DataContext>(opt =>
            opt.UseSqlServer(builder.Configuration["ConnectionStrings:SqlDefaultConnection"])
            );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}