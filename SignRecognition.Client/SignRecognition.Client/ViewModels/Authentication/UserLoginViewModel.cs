using System.ComponentModel.DataAnnotations;

namespace SignRecognition.Client.ViewModels.Authentication;

public class UserLoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}