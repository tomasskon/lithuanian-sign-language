using AutoMapper;
using MailSystem.Exception;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Contract.Authentication;
using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;

namespace SignRecognition.Server.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        
        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        /// <response code="404">UserNotFoundException</response>
        /// <response code="400">InvalidPasswordException</response>
        /// <response code="400">BadUserException</response>
        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginContract userLoginContract)
        {
            try
            {
                return Ok(await _authenticationService.UserLoginAsync(userLoginContract.EmailAddress,
                    userLoginContract.Password));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.ToStandardResponse());
            }
            catch (BadUserException ex)
            {
                return BadRequest(ex.ToStandardResponse());
            }
            catch (InvalidPasswordException ex)
            {
                return BadRequest(ex.ToStandardResponse()); 
            }
        }
        
        /// <response code="400">UserEmailAlreadyUsedException</response>
        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterContract userRegisterContract)
        {
            var user = _mapper.Map<User>(userRegisterContract);
            
            try
            {
                return Ok(await _authenticationService.UserRegisterAsync(user, userRegisterContract.Password));
            }
            catch (UserEmailAlreadyUsedException ex)
            {
                return BadRequest(ex.ToStandardResponse());
            }
        }
    }
}