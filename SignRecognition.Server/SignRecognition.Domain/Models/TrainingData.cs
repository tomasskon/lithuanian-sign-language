namespace SignRecognition.Domain.Models;

public class TrainingData
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid SignId { get; set; }
    
    public byte[] Data { get; set; }
}