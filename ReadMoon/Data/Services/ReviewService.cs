using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReadMoon.Data.Base;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public interface IReviewService : IEntityBaseRepository<Review>
{
    Task AddNewReviewAsync(ReviewVM data, ClaimsPrincipal claimsPrincipal, int bookId);
    Task<Review> GetReviewByIdAsync(int id);
    Task UpdateReviewAsync(ReviewVM data, int id);
    Task DeleteNewReviewAsync(int id);
}


public class ReviewService: EntityBaseRepository<Review>, IReviewService
{
    
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;


    public ReviewService(AppDbContext db, UserManager<User> userManager) : base(db)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<Review> GetReviewByIdAsync(int id)
    {
        var reviewDetails = await _db.Reviews
            .Include(a => a.Users)
            .Include(x => x.Books)
            .FirstOrDefaultAsync(n => n.Id == id);
        return reviewDetails;
    }
    public async Task AddNewReviewAsync(ReviewVM data, ClaimsPrincipal claimsPrincipal, int bookId)
    {
        var user = _userManager.GetUserId(claimsPrincipal);

        var newReview = new Review()
        {
            Text = data.Text,
            CreatedOn = DateTime.Now,
            UserId = user,
            BookId = bookId
        };
            
        await _db.Reviews.AddAsync(newReview);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(ReviewVM data, int id)
    {
        var dbBook = await _db.Reviews.FirstOrDefaultAsync(n => n.Id == id);

        if (dbBook != null)
        {
            dbBook.Text = data.Text;
            await _db.SaveChangesAsync();
        }
    }
    
    public async Task DeleteNewReviewAsync(int id)
    {
        var reviewDetails = await _db.Reviews.FirstOrDefaultAsync(n => n.Id == id);
        _db.Reviews.Remove(reviewDetails);
        await _db.SaveChangesAsync();
    }
    

}