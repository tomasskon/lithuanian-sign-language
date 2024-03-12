namespace SignRecognition.Domain.Exceptions
{
    public class InvalidPasswordException : System.Exception
    {
        public InvalidPasswordException()
            : base("Incorrect password")
        {
        }

        public InvalidPasswordException(string message)
            : base(message)
        {
        }

        public InvalidPasswordException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}