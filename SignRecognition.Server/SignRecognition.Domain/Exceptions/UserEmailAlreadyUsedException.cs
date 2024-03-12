namespace MailSystem.Exception
{
    public class UserEmailAlreadyUsedException : System.Exception
    {
        public UserEmailAlreadyUsedException()
            : base("User not found")
        {
        }

        public UserEmailAlreadyUsedException(string message)
            : base(message)
        {
        }

        public UserEmailAlreadyUsedException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}