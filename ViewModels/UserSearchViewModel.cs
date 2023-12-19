using Microsoft.AspNetCore.Identity;

namespace Final.ViewModels;

public class UserSearchViewModel{

    public List<IdentityUser>? Users { get; set;}

    public string? UsernameFilter { get; set; }

    public string Mensaje { get; set; }

}