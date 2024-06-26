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

        public async Task<string> UserLoginAsync(string emailAddress, string password)
        {
            var user = await _userService.GetByEmailAsync(emailAddress);
            var userPassword = await _userPasswordRepository.GetByUserIdAsync(user.Id);

            if (userPassword is null)
                throw new BadUserException($"The user id: {user.Id} does not have a password");
                
            _passwordService.ValidatePassword(password, userPassword.PasswordHash, userPassword.PasswordSalt);
            return _tokenService.GetJwtToken(user);
        }

        public async Task<string> UserRegisterAsync(User user, string password)
        {
            var createdUser = await _userService.CreateAsync(user);

            var (passwordHash, passwordSalt) = _passwordService.CreateHashedPassword(password);
            await _userPasswordRepository.CreateAsync(passwordHash, passwordSalt, createdUser.Id);
            
            return _tokenService.GetJwtToken(createdUser);
        }
    }
}