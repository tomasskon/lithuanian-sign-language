using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Entities;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Repository.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<UserEntity> _dbSet;
    private readonly IMapper _mapper;

    public UserRepository(DatabaseContext context, IMapper mapper)
    {
        _mapper = mapper;
        _dbSet = context.Users;
    }

    public async Task<List<User>> GetAllAsync()
    {
        var userEntities = await _dbSet.ToListAsync();

        return _mapper.Map<List<User>>(userEntities);
    }
}