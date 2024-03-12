using System.ComponentModel.DataAnnotations;

namespace SignRecognition.Repository.Entities;

public class UserEntity : GenericIdentityEntity
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
}