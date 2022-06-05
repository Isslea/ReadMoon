using Microsoft.EntityFrameworkCore;
using ReadMoon.Data.Base;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public class BookService: EntityBaseRepository<Book>, IBookService
    {
        private readonly AppDbContext _db;
        public BookService(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var bookDetails = await _db.Books
                .Include(p => p.Publisher)
                .Include(ab => ab.Author)
                .Include(c => c.Category)
                .ToListAsync();
            return bookDetails;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails = await _db.Books
                .Include(p => p.Publisher)
                .Include(ab => ab.Author)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(n => n.Id == id);
            return bookDetails;
        }

        //DropDown
        public async Task<NewDropDownVM> GetNewBookDropdownsValues()
        {
            var response = new NewDropDownVM()
            {
                Authors = await _db.Authors.OrderBy(n => n.FullName).ToListAsync(),
                Publishers = await _db.Publishers.OrderBy(n => n.Name).ToListAsync(),
                Categories = await _db.Categories.OrderBy(n => n.Name).ToListAsync()
            };

            return response;
        }

        //Add book
        public async Task AddNewBookAsync(NewBookVM data)
        {
            var newBook = new Book()
            {
                Title = data.Title,
                Description = data.Description,
                RelaseDate = data.RelaseDate,
                ImageURL = data.ImageURL,
                ISBN = data.ISBN,
                PublisherId = data.PublisherId,
                CategoryId = data.CategoryId
            };
            await _db.Books.AddAsync(newBook);
            await _db.SaveChangesAsync();


            foreach (var authorId in data.AuthorId)
            {
                var newActorMovie = new BookAuthor()
                {
                    BookId = newBook.Id,
                    AuthorId = authorId
                };
                await _db.BookAuthors.AddAsync(newActorMovie);
            }
            await _db.SaveChangesAsync();
        }

        //Update book
        public async Task UpdateBookAsync(NewBookVM data)
        {
            var dbBook = await _db.Books.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbBook != null)
            {

                dbBook.Title = data.Title;
                dbBook.Description = data.Description;
                dbBook.RelaseDate = data.RelaseDate;
                dbBook.ImageURL = data.ImageURL;
                dbBook.ISBN = data.ISBN;
                dbBook.PublisherId = data.PublisherId;
                dbBook.CategoryId = data.CategoryId;
                await _db.SaveChangesAsync();
            }

            var existingAuthor = _db.BookAuthors.Where(n => n.BookId == data.Id).ToList();
            _db.BookAuthors.RemoveRange(existingAuthor);
            await _db.SaveChangesAsync();

            foreach (var authorid in data.AuthorId)
            {
                var newAuthorBook = new BookAuthor()
                {
                    BookId = data.Id,
                    AuthorId = authorid
                };
                await _db.BookAuthors.AddAsync(newAuthorBook);
            }
            await _db.SaveChangesAsync();
        }
        //Delete book
        public async Task DeleteNewBookAsync(int id)
        {
            var bookDetails = await _db.Books.FirstOrDefaultAsync(n => n.Id == id);
            _db.Books.Remove(bookDetails);
            await _db.SaveChangesAsync();
        }
    
}