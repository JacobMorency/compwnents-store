using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jmH60A01.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProdCatId { get; set; }

    public string? Description { get; set; }

    public string? Manufacturer { get; set; }

    [Required(ErrorMessage = "Stock is required")]
    public int Stock { get; set; }

    public decimal? BuyPrice { get; set; }

    public decimal? SellPrice { get; set; }

    public virtual ProductCategory ProdCat { get; set; } = null!;
}
