using Final.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final.Services;

namespace Final.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUsersService _usersService;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(
        ILogger<HomeController> logger,
        IUsersService usersService)
    {
        _logger = logger;
        _usersService = usersService;
    }

    [Authorize(Roles = "admin")]
    public IActionResult Index(string? usernameFilter)
    {
        UserSearchViewModel userSearchViewModel;
        List<IdentityUser> usersList;

        if(!string.IsNullOrEmpty(usernameFilter)){
                usersList = _usersService.GetAll(usernameFilter);
            }else{
                usersList = _usersService.GetAll();
        }
        userSearchViewModel = new UserSearchViewModel();
        userSearchViewModel.Users = usersList;       
        return View(userSearchViewModel);
    }

    public async Task<IActionResult> AssignRole(string id)
    {
        var userViewModel = await _usersService.GetById(id);
        return View(userViewModel);
    }

    [HttpPost]
    public IActionResult AssignRole(UserEditViewModel model)
    {
        _usersService.Assign(model);
        return RedirectToAction("Index", 
            routeValues: new {mensaje = "Rol asignado con exito a" + model.UserName});
    }

    public async Task<IActionResult> RemoveRole(string id)
    {
        var userViewModel = await _usersService.GetById(id);
        return View(userViewModel);
    }

    [HttpPost]
    public IActionResult RemoveRole(UserEditViewModel model)
    {
        _usersService.Remove(model);
        return RedirectToAction("Index", 
        routeValues: new {mensaje = "Rol removido con exito a" + model.UserName});
    }
}