using TodoApp.Infrastructure;
using TodoApp.Models;

namespace TodoApp.Services;
public interface IProductService
{
    public IEnumerable<Product> GetProducts();

    public void Create(Product product);

    public void Update(Product product);

    public void Delete(long id);
}
public class ProductService:IProductService
{
    private readonly DataContext _dbContext;
    
    public ProductService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<Product> GetProducts() 
    {
        return _dbContext.Products.ToList();
    }
    public void Create(Product product)
    {
        _dbContext.Products.Add(product);
        
        _dbContext.SaveChanges();
    }
    public void Update(Product product)
    {
        var existingProduct = _dbContext.Products.Where(p => p.Id == product.Id).FirstOrDefault();

        if (existingProduct==null)
        {
            throw new Exception($"There is no record with that Id : {product.Id}");
        }

        existingProduct.Name = product.Name;
        existingProduct.LikeCount = product.LikeCount;
        existingProduct.Description = product.Description;
        
        _dbContext.SaveChanges();

    }
    public void Delete(long id)
    {
        var productToDelete = _dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
        if (productToDelete==null)
        {
            throw new Exception($"There is no record with that Id : {id}");
        }

        _dbContext.Products.Remove(productToDelete);
        _dbContext.SaveChanges();
    }
}