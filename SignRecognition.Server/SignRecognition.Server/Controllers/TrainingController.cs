using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Domain.Exceptions;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;

namespace SignRecognition.Server.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class TrainingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrainingService _trainingService;
        
        public TrainingController(IMapper mapper, ITrainingService trainingService)
        {
            _mapper = mapper;
            _trainingService = trainingService;
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitTrainingData([FromBody] byte[] submittedData, [FromQuery] Guid userId, [FromQuery] Guid signId)
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
        public async Task<IActionResult> GetUserSubmittedTrainingDataIds([FromQuery] Guid userId)
        {
            return Ok(await _trainingService.GetUsersTrainingDataIdsAsync(userId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainingData()
        {
            var zip = await _trainingService.GetGroupedTrainingDataAsync();

            return File(zip, "application/zip", "training_data.zip");
        }
    }
}