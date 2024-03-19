using SignRecognition.Contract;

namespace SignRecognition.Domain.Exceptions
{
    public static class ExceptionsExtension
    {
        public static ErrorResponseContract ToStandardResponse(this Exception exception)
        {
            return new ErrorResponseContract
            {
                Error = exception.GetType().Name,
                Message = exception.Message
            };
        }
    }
}