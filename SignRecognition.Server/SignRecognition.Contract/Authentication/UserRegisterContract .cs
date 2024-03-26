using System.ComponentModel.DataAnnotations;

namespace SignRecognition.Contract.Authentication
{
    public class UserRegisterContract
    {
        private const string PasswordRegexPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}|:<>?`~\-=\[\]\\;',./])[A-Za-z\d!@#$%^&*()_+{}|:<>?`~\-=\[\]\\;',./]{8,}$";
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
        
        [RegularExpression(PasswordRegexPattern)]
        public string Password { get; set; }
    }
}