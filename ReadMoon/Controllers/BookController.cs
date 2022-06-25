using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadMoon.Data;
using ReadMoon.Data.Services;
using ReadMoon.Models;

namespace ReadMoon.Controllers;
[Authorize(Roles = UserRoles.Admin)]
public class BookController : Controller
{
 private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await _service.GetAllBooksAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allBooks
                    .Where(b => b.Title.ToUpper().Contains(searchString.ToUpper()) || 
                                b.Author.Any(n => n.FullName.ToUpper().Contains(searchString.ToUpper())) || 
                                b.Publisher.Name.ToUpper().Contains(searchString.ToUpper()));
                return View("Index", filteredResult);
            }

            return View("Index", allBooks);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchCategory)
        {
            var books = await _service.GetAllBooksAsync();
            
            ViewBag.All = String.IsNullOrEmpty(searchCategory) ? "" : "";
            ViewBag.Fantasy = searchCategory == "" ? "" : "fantasy";
            ViewBag.Horror = searchCategory == "" ? "" : "horror";
            ViewBag.Biography = searchCategory == "" ? "" : "biography";
           
            switch (searchCategory)
            {
                case "fantasy":
                     books = books.Where(X => X.CategoryId.Equals(1));
                     break;
                 case "horror":
                     books = books.Where(X => X.CategoryId.Equals(2));
                     break;
                 case "biography":
                     books = books.Where(X => X.CategoryId.Equals(3));
                     break;
             }
            return View(books);
        }

        //GET: Book/Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            return View(bookDetails);
        }

        //GET: Book/Create
        public async Task<IActionResult> CreateAsync()
        {
            var bookDropDown = await _service.GetNewBookDropdownsValues();

            ViewBag.Authors = new SelectList(bookDropDown.Authors, "Id", "FullName");
            ViewBag.Publishers = new SelectList(bookDropDown.Publishers, "Id", "Name");
            ViewBag.Categories = new SelectList(bookDropDown.Categories, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropDown = await _service.GetNewBookDropdownsValues();

                ViewBag.Authors = new SelectList(bookDropDown.Authors, "Id", "FullName");
                ViewBag.Publishers = new SelectList(bookDropDown.Publishers, "Id", "Name");
                ViewBag.Categories = new SelectList(bookDropDown.Categories, "Id", "Name");

                return View(book);
            }

            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        //GET: Book/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Title = bookDetails.Title,
                Description = bookDetails.Description,
                RelaseDate = bookDetails.RelaseDate,
                ImageURL = bookDetails.ImageURL,
                ISBN = bookDetails.ISBN,
                CategoryId = bookDetails.CategoryId,
                PublisherId = bookDetails.PublisherId,
                AuthorId = bookDetails.Author.Select(n => n.Id).ToList(),
            };

            var bookDropDown = await _service.GetNewBookDropdownsValues();
            ViewBag.Authors = new SelectList(bookDropDown.Authors, "Id", "FullName");
            ViewBag.Publishers = new SelectList(bookDropDown.Publishers, "Id", "Name");
            ViewBag.Categories = new SelectList(bookDropDown.Categories, "Id", "Name");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewBookVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var bookDropDown = await _service.GetNewBookDropdownsValues();
                ViewBag.Authors = new SelectList(bookDropDown.Authors, "Id", "FullName");
                ViewBag.Publishers = new SelectList(bookDropDown.Publishers, "Id", "Name");
                ViewBag.Categories = new SelectList(bookDropDown.Categories, "Id", "Name");
                return View(movie);
            }

            await _service.UpdateBookAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //GET: Book/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Title = bookDetails.Title,
                Description = bookDetails.Description,
                RelaseDate = bookDetails.RelaseDate,
                ImageURL = bookDetails.ImageURL,
                ISBN = bookDetails.ISBN,
                CategoryId = bookDetails.CategoryId,
                PublisherId = bookDetails.PublisherId,
                AuthorId = bookDetails.Author.Select(n => n.Id).ToList()
            };

            var bookDropDown = await _service.GetNewBookDropdownsValues();
            ViewBag.Authors = new SelectList(bookDropDown.Authors, "Id", "FullName");
            ViewBag.Publishers = new SelectList(bookDropDown.Publishers, "Id", "Name");
            ViewBag.Categories = new SelectList(bookDropDown.Categories, "Id", "Name");

            return View(response);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Title = bookDetails.Title,
                Description = bookDetails.Description,
                RelaseDate = bookDetails.RelaseDate,
                ImageURL = bookDetails.ImageURL,
                ISBN = bookDetails.ISBN,
                CategoryId = bookDetails.CategoryId,
                PublisherId = bookDetails.PublisherId,
                AuthorId = bookDetails.Author.Select(n => n.Id).ToList(),
            };

            var bookDropDown = await _service.GetNewBookDropdownsValues();
            ViewBag.Authors = new SelectList(bookDropDown.Authors, "Id", "FullName");
            ViewBag.Publishers = new SelectList(bookDropDown.Publishers, "Id", "Name");
            ViewBag.Categories = new SelectList(bookDropDown.Categories, "Id", "Name");

            await _service.DeleteNewBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
}