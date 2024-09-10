using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jmH60A01.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jmH60A01.Models;

namespace jmH60A01.Controllers
{
    public class ProductsController : Controller
    {
        private readonly H60assignmentDbJmContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductsController(H60assignmentDbJmContext context, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_productRepository.GetProducts());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProdCatId"] = new SelectList(_context.ProductCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            // if (ModelState.IsValid)
            // {
            //     _productRepository.AddProduct(product);
            //     return RedirectToAction(nameof(Index));
            // }
            _productRepository.AddProduct(product);
            return RedirectToAction(nameof(Index));
            // ViewData["ProdCatId"] = new SelectList(_productRepository.GetProductCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            // return View(product);
        }


        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProdCatId"] = new SelectList(_productCategoryRepository.GetProductCategories(), "CategoryId", "ProdCat", product.ProdCatId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _productRepository.UpdateProduct(product);
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!ProductExists(product.ProductId))
            //         {
            //             return NotFound();
            //         }
            //         else
            //         {
            //             throw;
            //         }
            //     }
            
            
            // }
            _productRepository.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
            // ViewData["ProdCatId"] = new SelectList(_productCategoryRepository.GetProductCategories(), "CategoryId", "ProdCat", product.ProdCatId);
            // return View(product);
        }


        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                _productRepository.DeleteProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult UpdateStock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productRepository.GetProductById(id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            ViewData["ProdCatId"] = new SelectList(_productRepository.GetProductCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            return View(product);
            
            
         
            
            
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStock(int id, [Bind("ProductId,Stock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _productRepository.UpdateStock(id, product.Stock);
            //         return RedirectToAction(nameof(Index));
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!ProductExists(id))
            //         {
            //             return NotFound();
            //         }
            //         else
            //         {
            //             throw;
            //         }
            //     }
            // }
            _productRepository.UpdateStock(id, product.Stock);
            return RedirectToAction(nameof(Index));
            // return View(product);
        }

        [HttpGet]
        public IActionResult UpdateBuyPrice(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProdCatId"] = new SelectList(_productRepository.GetProductCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateBuyPrice(int id, [Bind("ProductId,BuyPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _productRepository.UpdateBuyPrice(id, product.BuyPrice);
            //         return RedirectToAction(nameof(Index));
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!ProductExists(id))
            //         {
            //             return NotFound();
            //         }
            //         else
            //         {
            //             throw;
            //         }
            //     }
            // }
            _productRepository.UpdateBuyPrice(id, product.BuyPrice);
            return RedirectToAction(nameof(Index));
            // return View(product);
        }
        
        [HttpGet]
        public IActionResult UpdateSellPrice(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProdCatId"] = new SelectList(_productRepository.GetProductCategories(), "CategoryId", "CategoryId", product.ProdCatId);
            return View(product);
        }
        
        [HttpPost]
        public IActionResult UpdateSellPrice(int id, [Bind("ProductId,SellPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _productRepository.UpdateSellPrice(id, product.SellPrice);
            //         return RedirectToAction(nameof(Index));
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!ProductExists(id))
            //         {
            //             return NotFound();
            //         }
            //         else
            //         {
            //             throw;
            //         }
            //     }
            // }
            _productRepository.UpdateSellPrice(id, product.SellPrice);
            return RedirectToAction(nameof(Index));
            // return View(product);
        }

        



        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
