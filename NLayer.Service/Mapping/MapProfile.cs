using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.About;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {

            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserGoogleRegisterDto>().ReverseMap();
            CreateMap<User, UserGoogleRegisterDto>();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserWithTokenDto>();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, User>();

            CreateMap<Business, BusinessAddDto>().ReverseMap();
            CreateMap<Business, BusinessUpdateDto>().ReverseMap();

            CreateMap<About, AboutDto>();
        }
    }
}
