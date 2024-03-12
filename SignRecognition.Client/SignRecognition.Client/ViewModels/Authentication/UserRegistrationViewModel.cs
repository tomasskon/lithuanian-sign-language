using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignRecognition.Client.ViewModels.Authentication;

public class UserRegistrationViewModel
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
}