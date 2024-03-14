namespace SignRecognition.Domain.Exceptions
{
    public class SignNotFoundException : System.Exception
    {
        public SignNotFoundException()
            : base("Sign not found")
        {
        }

        public SignNotFoundException(string message)
            : base(message)
        {
        }

        public SignNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}