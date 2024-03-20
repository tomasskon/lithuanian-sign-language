using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Contract.User;
using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;
using SignRecognition.Server.Controllers.Extensions;

namespace SignRecognition.Server.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        
        public UserController(IMapper mapper, ITrainingService trainingService, ITokenService tokenService, IUserService userService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext.GetAuthorizationToken());

            try
            {
                var user = await _userService.GetAsync(userId);
                return Ok(_mapper.Map<UserContract>(user));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.ToStandardResponse());
            }
        }
    }
}