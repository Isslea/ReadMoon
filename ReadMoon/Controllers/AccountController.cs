using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReadMoon.Data;
using ReadMoon.Data.Services;
using ReadMoon.Models;

namespace ReadMoon.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signInManager;


    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
    }

    public IActionResult Login() => View(new LoginVM());
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid) return View(loginVM);

        var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
        if(user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Book");
                }
            }
            TempData["Error"] = "Podane złe dane!";
            return View(loginVM);
        }

        TempData["Error"] = "Podane złe dane!";
        return View(loginVM);
    }

    public IActionResult Register() => View(new RegisterVM());
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid) return View(registerVM);

        var userEmail = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
        var userName = await _userManager.FindByNameAsync(registerVM.UserName);

        if(userEmail != null)
        {
            TempData["Error"] = "Ten E-mail jest zajęty";
            return View(registerVM);
        }

        if (userName != null)
        {
            TempData["Error"] = "Ta nazwa jest zajęta";
            return View(registerVM);
        }

        var newUser = new User()
        {
            Email = registerVM.EmailAddress,
            UserName = registerVM.EmailAddress
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

        if (newUserResponse.Succeeded)
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        return View("RegisterCompleted");
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Book");
    }
    
    public IActionResult ChangePassword() => View(new ChangePasswordVM());
    
    [HttpPost]
    public async Task<ActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.ChangePasswordAsync(changePasswordVM);
            if (result.Succeeded)
            {
                ModelState.Clear();
                TempData["Success"] = "Hasło zostało zmienione!";
                return View();
            }
            TempData["Error"] = "Podano złe hasło!";
        }

        return View(changePasswordVM);
    }
}