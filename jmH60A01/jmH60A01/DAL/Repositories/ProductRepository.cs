using Microsoft.EntityFrameworkCore;
using jmH60A01.Models;
using jmH60A01.DAL.Repositories;

namespace jmH60A01.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly H60assignmentDbJmContext _context;

    public ProductRepository(H60assignmentDbJmContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Product> GetProducts()
    {
        var products = _context.Products.Include(p => p.ProdCat).OrderBy(p => p.Description).ToList();
        return products;
    }

    public Product GetProductById(int? id)
    {
        if (id == null)
        {
            return null;
        }
        // var product = _context.Products.Include(p => p.ProdCat).Find(id);
        var product = _context.Products.Include(p => p.ProdCat).FirstOrDefault(p => p.ProductId == id);
        return product;
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }



    public void UpdateProduct(Product product)
    {
        _context.Update(product);
        _context.SaveChanges();
    }

    public void UpdateStock(int id, int newStock)
    {
        var product = _context.Products.Find(id);
        if (product != null && product.Stock + newStock >= 0)
        {
            product.Stock += newStock;
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("Stock cannot be negative.");
        }
    }

    public void UpdateBuyPrice(int id, decimal? newPrice)
    {
        if (newPrice == null)
        {
            throw new ArithmeticException("Price is not a valid number");
        }
        
        newPrice = Math.Round(newPrice.Value, 2);
        
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }
        var product = _context.Products.Find(id);
        if (product != null)
        {
            if (product.SellPrice <= newPrice)
            {
                throw new Exception("Buy price cannot be greater than the Sell Price.");
            }
            product.BuyPrice = newPrice;
            _context.SaveChanges();
        }
    }

    public void UpdateSellPrice(int id, decimal? newPrice)
    {
        if (newPrice == null)
        {
            throw new ArithmeticException("Price is not a valid number");
        }
        
        newPrice = Math.Round(newPrice.Value, 2);
        
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }
        var product = _context.Products.Find(id);
        if (product != null)
        {
            if (newPrice < product.BuyPrice)
            {
                throw new Exception("Buy price cannot be greater than the Sell Price.");
            }
            product.SellPrice = newPrice;
            _context.SaveChanges();
        }
    }

    public IEnumerable<Product> GetProductByCategory(int categoryId)
    {
        var products = _context.Products.Where(p => p.ProdCatId == categoryId).ToList();
        return products;
    }


    public IEnumerable<ProductCategory> GetProductCategories()
    {
        var productCategories = _context.ProductCategories.ToList();
        return productCategories;
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
    
}