namespace SignRecognition.Contract.User;

public class UserContract
{
    public Guid UserId { get; set; }
    public Guid FirstName { get; set; }
    public Guid LastName { get; set; }
    public Guid Email { get; set; }
}