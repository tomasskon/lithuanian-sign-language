using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignRecognition.Client.ViewModels.Authentication;

public class UserRegistrationViewModel
{
    private const string PasswordRegexPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}|:<>?`~\-=\[\]\\;',./])[A-Za-z\d!@#$%^&*()_+{}|:<>?`~\-=\[\]\\;',./]{8,}$";
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    [RegularExpression(PasswordRegexPattern, ErrorMessage = "Slaptažodis turi turėti bent 8 simbolius, įskaitant bent vieną didžiąją raidę, vieną skaičių ir vieną specialų simbolį (pvz., !, @, #, $).")]
    public string Password { get; set; }
}