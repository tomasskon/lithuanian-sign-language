using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Contract;
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
                return BadRequest(new StandardExceptionResponse(e));
            }
            catch (SignNotFoundException e)
            {
                return BadRequest(new StandardExceptionResponse(e));
            }
        }
    }
}