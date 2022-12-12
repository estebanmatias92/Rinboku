using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rinboku.Infraestructure;
using Rinboku.Models;

namespace Rinboku.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string categorySlug = "", int page = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = page;
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = categorySlug;

            if (categorySlug == "")
            {
                ViewBag.TotalPages = (int) Math.Ceiling((decimal) _context.Products.Count() / pageSize);

                return View(await _context.Products.OrderByDescending(product => product.Id)
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync());
            }

            Category category = await _context.Categories.Where(category => category.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            var productsByCategory = _context.Products.Where(product => product.CategoryId == category.Id);
            ViewBag.TotalPages = (int) Math.Ceiling((decimal) productsByCategory.Count() / pageSize);

            return View(await productsByCategory.OrderByDescending(product => product.Id)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync());
        }
    }
}
