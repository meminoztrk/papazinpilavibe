using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.About;
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
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<About, AboutDto>();
        }
    }
}
