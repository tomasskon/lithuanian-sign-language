using System.ComponentModel.DataAnnotations;

namespace SignRecognition.Repository.Entities;

public class SignEntity : GenericIdentityEntity
{
    [Required]
    public string SignName { get; set; }
    
    [Required]
    public string SignVideoUrl { get; set; }
}