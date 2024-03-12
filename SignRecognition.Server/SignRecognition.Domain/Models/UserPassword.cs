namespace SignRecognition.Domain.Models
{
    public class UserPassword
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        
        public string PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }
    }
}