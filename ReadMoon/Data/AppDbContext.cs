using Microsoft.EntityFrameworkCore;
using ReadMoon.Models;

namespace ReadMoon.Data;

public class AppDbContext : DbContext
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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Review)
            .WithOne(a => a.User)
            .HasForeignKey<Review>(a => a.UserId);

        modelBuilder.Entity<Book>(eb =>
        {
            eb.HasOne(c => c.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(k => k.CategoryId);

            eb.HasOne(c => c.Publisher)
                .WithMany(b => b.Books)
                .HasForeignKey(k => k.PublisherId);

            eb.HasMany(a => a.Authors)
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