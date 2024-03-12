using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignRecognition.Client.ViewModels.Authentication;

public class UserLoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
}