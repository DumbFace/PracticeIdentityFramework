using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PracticeIdentity.DTOS;

namespace PracticeIdentity.Profiles
{
    public class PracticeIdentityProfile : Profile
    {
        public PracticeIdentityProfile()
        {
            CreateMap<IdentityUser, RegisterDTO>();
            CreateMap<IdentityRole, RoleDTO>();
            CreateMap<RoleDTO, IdentityRole>();
            CreateMap<IdentityUser, UserDTO>();
            CreateMap<UserDTO, IdentityUser>();

        }
    }
}