using AutoMapper;
using MailSystem.Exception;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Contract;
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
                return Ok(await _authenticationService.UserLogin(userLoginContract.EmailAddress,
                    userLoginContract.Password));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new StandardExceptionResponse(ex));
            }
            catch (BadUserException ex)
            {
                return BadRequest(new StandardExceptionResponse(ex));
            }
            catch (InvalidPasswordException ex)
            {
                return BadRequest(new StandardExceptionResponse(ex)); 
            }
        }
        
        /// <response code="400">UserEmailAlreadyUsedException</response>
        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterContract userRegisterContract)
        {
            try
            {
                var user = _mapper.Map<User>(userRegisterContract);
            
                return Ok(await _authenticationService.UserRegister(user, userRegisterContract.Password));
            }
            catch (UserEmailAlreadyUsedException ex)
            {
                return BadRequest(new StandardExceptionResponse(ex));
            }
        }
    }
}