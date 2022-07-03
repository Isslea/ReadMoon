using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadMoon.Data;
using ReadMoon.Data.Services;

namespace ReadMoon.Controllers;
[Authorize]
public class ReviewController : Controller
{
    private readonly IReviewService _service;
    private readonly IUserService _userService;

    public ReviewController(IReviewService service, IUserService userService)
    {
        _service = service;
        _userService = userService;
    }
    public IActionResult AddComment()
    {
        return View();
    }
    [HttpPost]
    [Route("Review/AddComment/{bookId}", Name="Review")]
    public async Task<IActionResult> AddComment(ReviewVM review, int bookId)
    {
        if (!ModelState.IsValid)
        {
            return View(review);
        }

        await _service.AddNewReviewAsync(review, User, bookId);
        return RedirectToAction("Details", "Book",new { Id = bookId });
    }
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _service.GetReviewByIdAsync(id);
        if (result == null) return View("NotFound");
        if (_userService.GetUserId() != result.UserId) return View("NoAccess");
       
        var response = new ReviewVM()
        {
            Text = result.Text,
        };
        return View(response);
    }
        
    [HttpPost]
    public async Task<IActionResult> Edit(int id, ReviewVM review)
    {
        var result = await _service.GetReviewByIdAsync(id);
        if (id != result.Id) return View("NotFound");
        var bookId = result.BookId;
        
        if (!ModelState.IsValid)
        {
            return View(review);
        }

        await _service.UpdateReviewAsync(review, id);
        return RedirectToAction("Details", "Book", new { Id = bookId });
    }
    
    public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetReviewByIdAsync(id);
            if (result == null) return View("NotFound");
            if (_userService.GetUserId() != result.UserId) return View("NoAccess");

            var response = new ReviewVM()
            {
                Id = result.Id,
                Text = result.Text,
                BookId = result.BookId,
                CreatedOn = result.CreatedOn,
                UserId = result.UserId
            };

            return View(response);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _service.GetReviewByIdAsync(id);
            if (result == null) return View("NotFound");

            var response = new ReviewVM()
            {
                Id = result.Id,
                Text = result.Text,
                BookId = result.BookId,
                CreatedOn = result.CreatedOn,
                UserId = result.UserId
            };
            

            await _service.DeleteNewReviewAsync(id);
            return RedirectToAction("Details", "Book", new { Id = result.BookId });
        }
}