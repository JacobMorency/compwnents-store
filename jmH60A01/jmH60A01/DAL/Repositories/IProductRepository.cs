using jmH60A01.Models;

namespace jmH60A01.DAL.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    
    IEnumerable<ProductCategory> GetProductCategories();
    
    Product GetProductById(int? id);
    
    void AddProduct(Product product);
    
    void DeleteProduct(Product product);
    
    void UpdateProduct(Product product);
    
    void UpdateStock(int id, int stock);
    
    void UpdateBuyPrice(int id, decimal? price);
    
    void UpdateSellPrice(int id, decimal? price);
    public IEnumerable<Product> GetProductByCategory(int categoryId);
 
    void Save();
}