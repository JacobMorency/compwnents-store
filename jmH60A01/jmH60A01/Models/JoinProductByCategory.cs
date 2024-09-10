
namespace jmH60A01.Models;

public class JoinProductByCategory
{
    public ProductCategory Category { get; set; }
    public IEnumerable<Product> Products { get; set; }
}