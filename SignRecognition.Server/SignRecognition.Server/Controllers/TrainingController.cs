using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;
using SignRecognition.Server.Controllers.Extensions;

namespace SignRecognition.Server.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class TrainingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrainingService _trainingService;
        private readonly ITokenService _tokenService;
        
        public TrainingController(IMapper mapper, ITrainingService trainingService, ITokenService tokenService)
        {
            _mapper = mapper;
            _trainingService = trainingService;
            _tokenService = tokenService;
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitData([FromBody] byte[] submittedData, [FromQuery] Guid userId, [FromQuery] Guid signId)
        {
            try
            {
                await _trainingService.SubmitTrainingDataAsync(new TrainingData
                {
                    UserId = userId,
                    SignId = signId,
                    Data = submittedData
                });

                return Ok();
            }
            catch (UserNotFoundException e)
            {
                return BadRequest(e.ToStandardResponse());
            }
            catch (SignNotFoundException e)
            {
                return BadRequest(e.ToStandardResponse());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSubmittedDataIds([FromQuery] Guid userId)
        {
            return Ok(await _trainingService.GetUsersTrainingDataIdsAsync(userId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
           var userId = _tokenService.GetUserIdFromToken(HttpContext.GetAuthorizationToken());

           try
           {
               var zip = await _trainingService.GetGroupedTrainingDataAsync(userId);
               return File(zip, "application/zip", "training_data.zip");
           }
           catch (UserNotFoundException e)
           {
               return BadRequest(e.ToStandardResponse());
           }
           catch (UnauthorizedAccessException e)
           {
               return Unauthorized(e.ToStandardResponse());
           }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserData([FromQuery] Guid signId)
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext.GetAuthorizationToken());
            await _trainingService.DeleteUserTrainingDataAsync(userId, signId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDataFile([FromQuery] Guid signId)
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext.GetAuthorizationToken());
            var trainingData = await _trainingService.GetUserDataAsync(userId, signId);
            
            return File(trainingData.Data, "application/zip", $"{signId}.zip");
        }
    }
}