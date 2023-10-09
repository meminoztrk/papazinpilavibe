using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.About;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.FavoriteBusinessDTOs;
using NLayer.Core.DTOs.FavoriteCommentDTOs;
using NLayer.Core.DTOs.OwnerRequestDTOs;
using NLayer.Core.DTOs.ReportDTOs;
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

            CreateMap<BusinessComment, BusinessCommentAddDto>().ReverseMap();

            CreateMap<FavoriteComment, FavoriteCommentAddDto>().ReverseMap();

            CreateMap<FavoriteBusiness, FavoriteBusinessAddDto>().ReverseMap();

            CreateMap<Report, ReportAddDto>().ReverseMap();

            CreateMap<OwnerRequest, OwnerRequestAddDto>().ReverseMap();

            CreateMap<About, AboutDto>();
        }
    }
}
