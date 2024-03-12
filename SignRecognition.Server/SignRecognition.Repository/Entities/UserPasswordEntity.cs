namespace SignRecognition.Repository.Entities
{
    public class UserPasswordEntity : GenericIdentityEntity
    {
        public Guid UserId { get; set;  }
        
        public virtual string PasswordHash { get; set; }
        
        public virtual byte[] PasswordSalt { get; set; }
        
        public virtual UserEntity User { get; set; }
    }
}