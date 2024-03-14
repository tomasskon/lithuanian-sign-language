namespace SignRecognition.Repository.Entities;

public class TrainingDataEntity : GenericIdentityEntity
{
    public Guid UserId { get; set; }
    
    public Guid SignId { get; set; }
    
    public byte[] Data { get; set; }
    
    public UserEntity User { get; set; }
    
    public SignEntity Sign { get; set; }
}