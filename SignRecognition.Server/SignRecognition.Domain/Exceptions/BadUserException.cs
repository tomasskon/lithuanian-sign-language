namespace SignRecognition.Domain.Exceptions
{
    public class BadUserException : System.Exception
    {
        public BadUserException()
            : base("User account has issues")
        {
        }

        public BadUserException(string message)
            : base(message)
        {
        }

        public BadUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}