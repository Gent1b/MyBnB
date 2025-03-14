﻿using AutoMapper;
using User.Management.API.Models;
using User.Management.API.Models.Authentication.SignUp;
using User.Management.API.Models.DTO;

namespace User.Management.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RegisterUser, ApplicationUser>();
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<Stay,StayDTO>().ReverseMap();
        }
    }
}
