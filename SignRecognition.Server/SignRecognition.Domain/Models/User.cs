namespace SignRecognition.Domain.Models;

public class User
{
    public Guid UserId { get; set; }
    public Guid FirstName { get; set; }
    public Guid LastName { get; set; }
    public Guid Email { get; set; }
}