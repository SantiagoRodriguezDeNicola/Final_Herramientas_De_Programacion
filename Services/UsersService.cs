using Final.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final.Services;

public class UsersService : IUsersService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager){
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public List<IdentityUser> GetAll()
    {
       return _userManager.Users.ToList();
    }

    public List<IdentityUser> GetAll(string usernameFilter)
    {
        return _userManager.Users.Where(x => x.UserName.ToLower().Contains(usernameFilter.ToLower()) || x.Email.ToLower().Contains(usernameFilter.ToLower())).ToList();
    }

    public async Task<UserEditViewModel?> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        var userViewModel = new UserEditViewModel();
        userViewModel.UserName = user.UserName ?? string.Empty;
        userViewModel.Email = user.Email ?? string.Empty;
        userViewModel.Roles = new SelectList(_roleManager.Roles.ToList());

        return userViewModel;
    }

    public async void Assign(UserEditViewModel obj)
    {
        var user = await _userManager.FindByNameAsync(obj.UserName);
        if (user != null)
        {
             await _userManager.AddToRoleAsync(user, obj.Role);
        }
    }

    public async void Remove(UserEditViewModel obj)
    {
        var user = await _userManager.FindByNameAsync(obj.UserName);
        if (user != null)
        {
             await _userManager.RemoveFromRoleAsync(user, obj.Role);
        }
    }
}