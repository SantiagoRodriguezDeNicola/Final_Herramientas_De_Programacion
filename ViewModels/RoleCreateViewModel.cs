using System.ComponentModel.DataAnnotations;

namespace Final.ViewModels;

public class RoleCreateViewModel
{
    [Display(Name="Rol")]
    public string RoleName { get; set; }
}