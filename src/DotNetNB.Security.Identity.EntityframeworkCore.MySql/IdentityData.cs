using System.Security.Claims;

namespace DotNetNB.Security.Identity.EntityframeworkCore.MySql;

public class IdentityData
{
    public  List<User> Users { get; set; }

    public  List<Role> Roles { get; set; }
}

public class Role
{
    public string Name { get; set; }

    public List<Claim> Claims { get; set; } = new List<Claim>();
}

public class User
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public List<Claim> Claims { get; set; } = new List<Claim>();

    public List<string> Roles { get; set; } = new List<string>();
}