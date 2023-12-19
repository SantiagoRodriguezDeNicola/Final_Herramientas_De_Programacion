using Final.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Final.Services;

public class RolesService : IRolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesService(RoleManager<IdentityRole> roleManager){
        _roleManager = roleManager;
    }

    public void create(RoleCreateViewModel obj)
    {
        var role = new IdentityRole(obj.RoleName);
        _roleManager.CreateAsync(role);
    }

    public List<IdentityRole> GetAll()
    {
        var roles = _roleManager.Roles.ToList();
        return roles;
    }
    public List<IdentityRole> GetAll(string namefilter)
    {
        var roles = _roleManager.Roles.Where(x => x.Name.ToLower().Contains(namefilter.ToLower())).ToList();
        return roles;
    }


    public async void Delete(RoleCreateViewModel obj) 
    {
        var role = await _roleManager.FindByNameAsync(obj.RoleName);
        if (role != null)
        {
             await _roleManager.DeleteAsync(role);
        }
    }
    public async Task<RoleCreateViewModel?> GetById(string id)
    {
        var user = await _roleManager.FindByIdAsync(id);

        var userViewModel = new RoleCreateViewModel();
        userViewModel.RoleName = user.Name ?? string.Empty;

        return userViewModel;
    }
}