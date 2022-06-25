using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public interface IUserService
{
    string GetUserId();
    Task<IdentityResult> ChangePasswordAsync(ChangePasswordVM changePasswordVM);
}
public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordVM changePasswordVM)
    {
        var userId = GetUserId();
        var user = await _userManager.FindByIdAsync(userId);
            
        return await _userManager.ChangePasswordAsync(user, changePasswordVM.OldPassword,
            changePasswordVM.NewPassword);
    }
}