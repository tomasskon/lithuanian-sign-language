namespace SignRecognition.Contract.Signs;

public class SignContract
{
    public Guid Id { get; set; }
    
    public string SignName { get; set; }
    
    public string SignVideoUrl { get; set; }
}