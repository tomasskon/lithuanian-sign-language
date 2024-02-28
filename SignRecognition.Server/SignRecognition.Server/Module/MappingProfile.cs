﻿using AutoMapper;
using SignRecognition.Contract.User;
using SignRecognition.Domain.Models;
using SignRecognition.Repository.Entities;

namespace SignRecognition.Server.Module;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();

        CreateMap<User, UserContract>().ReverseMap();
    }
}