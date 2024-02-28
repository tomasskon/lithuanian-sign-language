using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignRecognition.Contract.User;
using SignRecognition.Domain.Interfaces;
using SignRecognition.Domain.Models;

namespace SignRecognition.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController  : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<User>> Get()
    {
        var users = await _userService.GetAllAsync();

        return Ok(_mapper.Map<List<UserContract>>(users));
    }
}