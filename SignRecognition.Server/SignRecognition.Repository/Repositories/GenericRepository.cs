using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignRecognition.Repository.Entities;
using SignRecognition.Repository.Repositories.Interface;

namespace SignRecognition.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository where T : GenericIdentityEntity
{
    protected readonly DatabaseContext Context;
    protected readonly DbSet<T> Set;
    protected readonly IMapper RepoMapper;

    public GenericRepository(DatabaseContext context, IMapper repoMapper)
    {
        Context = context;
        Set = context.Set<T>();
        RepoMapper = repoMapper;
    }
}