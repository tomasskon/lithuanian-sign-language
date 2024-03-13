using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Contract;
using SignRecognition.Contract.Authentication;
using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;

namespace SignRecognition.Server.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class VideoController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        
        public VideoController(IAuthenticationService authenticationService, IMapper mapper)
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
    }
}