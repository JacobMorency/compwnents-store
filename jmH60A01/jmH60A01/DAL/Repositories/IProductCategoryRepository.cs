using jmH60A01.Models;

namespace jmH60A01.DAL.Repositories;

public interface IProductCategoryRepository
{
    IEnumerable<ProductCategory> GetProductCategories();
    
    ProductCategory GetProductCategoryById(int? id);
    
    void AddProductCategory(ProductCategory productCategory);
    
    void DeleteProductCategory(int id);
    
    void UpdateProductCategory(ProductCategory productCategory);

    public void Save();
}