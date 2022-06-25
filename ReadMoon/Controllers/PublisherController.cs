using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadMoon.Data.Services;
using ReadMoon.Models;

namespace ReadMoon.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class PublisherController : Controller
{
    private readonly IPublisherService _service;

        public PublisherController(IPublisherService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allPublishers = await _service.GetAllAsync();
            return View(allPublishers);
        }

        //GET: publisher/details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var publisherDetails = await _service.GetPublisherByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        //GET: publisher/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ImageURL,Name,Description")] Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);

            await _service.AddAsync(publisher);
            return RedirectToAction(nameof(Index));
        }

        //GET: publisher/edit
        public async Task<IActionResult> Edit(int id)
        {
            var publisherDetails = await _service.GetPublisherByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);

            if (id == publisher.Id)
            {
                await _service.UpdateAsync(id, publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        //GET: publisher/delete
        public async Task<IActionResult> Delete(int id)
        {
            var publisherDetails = await _service.GetPublisherByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var publisherDetails = await _service.GetPublisherByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
}