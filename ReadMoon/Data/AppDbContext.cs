using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadMoon.Models;

namespace ReadMoon.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Review>(eb =>
        {

                eb.HasOne(x => x.Users)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId);

           
                eb.HasOne(x => x.Books)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.BookId);
        });
        
        modelBuilder.Entity<Book>(eb =>
        {
            eb.HasOne(c => c.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(k => k.CategoryId);

            eb.HasOne(c => c.Publisher)
                .WithMany(b => b.Books)
                .HasForeignKey(k => k.PublisherId);

            eb.HasMany(a => a.Author)
                .WithMany(b => b.Books)
                .UsingEntity<BookAuthor>(
                    a => a.HasOne(ab => ab.Author)
                        .WithMany()
                        .HasForeignKey(ab => ab.AuthorId),
                    a => a.HasOne(ab => ab.Book)
                        .WithMany()
                        .HasForeignKey(ab => ab.BookId),
                    ab => { ab.HasKey(x => new {x.AuthorId, x.BookId}); });
        });
    }
}