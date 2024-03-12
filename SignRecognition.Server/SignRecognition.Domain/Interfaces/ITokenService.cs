namespace SignRecognition.Domain.Interfaces
{
    public interface ITokenService
    {
        string GetJwtToken(Guid userId);
    }
}