namespace SignRecognition.Domain.Models;

public class Sign
{
    public Guid Id { get; set; }
    
    public string SignName { get; set; }
    
    public string SignVideoUrl { get; set; }
}