using Microsoft.AspNetCore.Mvc;
using ReadMoon.Data.Services;
using ReadMoon.Models;

namespace ReadMoon.Controllers;

public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCategories = await _service.GetAllAsync();
            return View(allCategories);
        }

        //GET: category/details
        public async Task<IActionResult> Details(int id)
        {
            var categoryDetails = await _service.GetCategoryByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        //GET: category/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            await _service.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        //GET: category/edit
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDetails = await _service.GetCategoryByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (!ModelState.IsValid) return View(category);

            if (id == category.Id)
            {
                await _service.UpdateAsync(id, category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET: category/delete
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _service.GetCategoryByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var categoryDetails = await _service.GetCategoryByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
}