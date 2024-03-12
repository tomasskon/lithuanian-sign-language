using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IUserPasswordRepository _userPasswordRepository;

        public AuthenticationService(ITokenService tokenService, IUserService userService, IPasswordService passwordService, IUserPasswordRepository userPasswordRepository)
        {
            _tokenService = tokenService;
            _userService = userService;
            _passwordService = passwordService;
            _userPasswordRepository = userPasswordRepository;
        }

        public async Task<string> UserLogin(string emailAddress, string password)
        {
            var user = await _userService.GetByEmailAsync(emailAddress);
            var userPassword = await _userPasswordRepository.GetByUserId(user.Id);

            if (userPassword is null)
                throw new BadUserException($"The user id: {user.Id} does not have a password");
                
            _passwordService.ValidatePassword(password, userPassword.PasswordHash, userPassword.PasswordSalt);
            return _tokenService.GetJwtToken(user.Id);
        }

        public async Task<string> UserRegister(User user, string password)
        {
            var userId = await _userService.CreateAsync(user);

            var (passwordHash, passwordSalt) = _passwordService.CreateHashedPassword(password);
            await _userPasswordRepository.Create(passwordHash, passwordSalt, userId);
            
            return _tokenService.GetJwtToken(userId);
        }
    }
}