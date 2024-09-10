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
    public class ProductCategoriesController : Controller
    {
        private readonly H60assignmentDbJmContext _context;
        private readonly IProductCategoryRepository _prodCategoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductCategoriesController(H60assignmentDbJmContext context, IProductCategoryRepository prodCategoryRepository, IProductRepository productRepository)
        {
            _context = context;
            _prodCategoryRepository = prodCategoryRepository;
            _productRepository = productRepository;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            return View(_prodCategoryRepository.GetProductCategories());
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = _prodCategoryRepository.GetProductCategoryById(id);
            
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ProdCat")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _prodCategoryRepository.AddProductCategory(productCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = _prodCategoryRepository.GetProductCategoryById(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ProdCat")] ProductCategory productCategory)
        {
            if (id != productCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _prodCategoryRepository.UpdateProductCategory(productCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = _prodCategoryRepository.GetProductCategoryById(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategory = _prodCategoryRepository.GetProductCategoryById(id);
            if (productCategory != null)
            {
                _prodCategoryRepository.DeleteProductCategory(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetProductByCategory(int categoryId)
        {
            var products = _productRepository.GetProductByCategory(categoryId);
            var productCategory = _prodCategoryRepository.GetProductCategoryById(categoryId);
            
            var joinProductByCategory = new JoinProductByCategory()
            {
                Category = productCategory,
                Products = products
            };
            return View(joinProductByCategory);
        }
        
        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategories.Any(e => e.CategoryId == id);
        }
    }
}
