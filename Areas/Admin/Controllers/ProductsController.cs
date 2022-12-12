using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rinboku.Infraestructure;
using Rinboku.Models;

namespace Rinboku.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesDir;

        public ProductsController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
        }

        private async void UploadImage(Product product)
        {
            if (product.Image != null)
            {
                RemoveImage(product.Image);
            }

            string imageName = Guid.NewGuid() + "_" + product.ImageUpload.FileName;

            string filePath = Path.Combine(_imagesDir, imageName);

            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await product.ImageUpload.CopyToAsync(fileStream);

            fileStream.Close();

            product.Image = imageName;
        }

        private void RemoveImage(string? image)
        {
            if (!string.Equals(image, "no-image.png"))
            {
                string filePath = Path.Combine(_imagesDir, image);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = page;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int) Math.Ceiling((decimal) _context.Products.Count() / pageSize);

            return View(await _context.Products.OrderByDescending(product => product.Id)
                                                .Include(product => product.Category)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                // Search a product with the same slug
                var match = await _context.Products.FirstOrDefaultAsync(item => item.Slug == product.Slug);

                // Stop if it exist
                if (match != null)
                {
                    ModelState.AddModelError("", "The product already exists.");

                    return View(product);
                }

                // If an image is loaded, upload it
                if (product.ImageUpload != null)
                {
                    UploadImage(product);
                }

                _context.Add(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been created!";

                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(long id)
        {
            Product product = await _context.Products.FindAsync(id);

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                // Find a product with the same Slug and Id
                var match = await _context.Products.FirstOrDefaultAsync(item => item.Slug == product.Slug && item.Id != product.Id);

                if (match != null)
                {
                    ModelState.AddModelError("", "The product already exists.");

                    return View(product);
                }

                if (product.ImageUpload != null)
                {
                    UploadImage(product);
                }

                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been edited!";
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(long id)
        {
            Product product = await _context.Products.FindAsync(id);

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            if (product.Image != null)
            {
                RemoveImage(product.Image);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }
    }
}
