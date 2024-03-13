using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Contract.Signs;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;

namespace SignRecognition.Server.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class SignController : ControllerBase
    {
        private readonly ISignService _signService;
        private readonly IMapper _mapper;
        
        public SignController(IMapper mapper, ISignService signService)
        {
            _mapper = mapper;
            _signService = signService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddSigns([FromBody] IEnumerable<AddSignsContract> signs)
        {
            var singsToAdd = _mapper.Map<IEnumerable<Sign>>(signs);
            
            await _signService.AddSignsAsync(singsToAdd);
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSigns()
        {
            var signs = await _signService.GetAllSignsAsync();

            return Ok(_mapper.Map<IEnumerable<SignContract>>(signs));
        }
    }
}