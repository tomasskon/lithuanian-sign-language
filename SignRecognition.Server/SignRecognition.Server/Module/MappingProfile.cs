using AutoMapper;
using SignRecognition.Contract.Authentication;
using SignRecognition.Contract.Signs;
using SignRecognition.Contract.User;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Entities;

namespace SignRecognition.Server.Module;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap<UserPassword, UserPasswordEntity>().ReverseMap();
        CreateMap<SignEntity, Sign>().ReverseMap();
        CreateMap<TrainingDataEntity, TrainingData>().ReverseMap();
        
        CreateMap<User, UserContract>().ReverseMap();
        CreateMap<User, UserRegisterContract>().ReverseMap();
        CreateMap<Sign, AddSignsContract>().ReverseMap();
        CreateMap<Sign, SignContract>().ReverseMap();
    }
}