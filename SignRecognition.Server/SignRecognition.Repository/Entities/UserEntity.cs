using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignRecognition.Repository.Entities;

public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid FirstName { get; set; }
    
    [Required]
    public Guid LastName { get; set; }
    
    [Required]
    public Guid Email { get; set; }
}