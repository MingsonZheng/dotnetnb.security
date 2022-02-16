using System.ComponentModel.DataAnnotations;

namespace DotNetNB.WebApplication.ViewModels;

public class LoginRequest
{
    public class LoginModel  
    {  
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }  
    }  
}