using jmH60A01.Models;
using Microsoft.EntityFrameworkCore;

namespace jmH60A01.DAL.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly H60assignmentDbJmContext _context;

    public ProductCategoryRepository(H60assignmentDbJmContext context)
    {
        _context = context;
    }
    public IEnumerable<ProductCategory> GetProductCategories()
    {
        var productCategories = _context.ProductCategories.OrderBy(c => c.ProdCat).ToList();
        return productCategories;
    }

    public ProductCategory GetProductCategoryById(int? id)
    {
        if (id == null)
        {
            return null;
        }
        var productCategory = _context.ProductCategories.Find(id);
        return productCategory;
    }

    public void AddProductCategory(ProductCategory productCategory)
    {
        _context.ProductCategories.Add(productCategory);
        _context.SaveChanges();
    }

    public void DeleteProductCategory(int id)
    {
        int defaultCategory = 1;

        var products = _context.Products.Where(p => p.ProdCatId == id).ToList();
        foreach (var product in products)
        {
            product.ProdCatId = defaultCategory;
        }
        
        _context.SaveChanges();
        
        var productCategory = _context.ProductCategories.Find(id);
        _context.ProductCategories.Remove(productCategory);
        _context.SaveChanges();
    }

    public void UpdateProductCategory(ProductCategory productCategory)
    {
        _context.ProductCategories.Update(productCategory);
        _context.SaveChanges();
    }
    
    public IEnumerable<Product> GetProductByCategory(int categoryId)
    {
        var products = _context.Products.Where(p => p.ProdCatId == categoryId).OrderBy(p => p.Description).ToList();
        return products;
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}