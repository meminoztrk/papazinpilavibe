﻿using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;

namespace NLayer.Core.Services
{
    public interface IUserService:IService<User>
    {
        bool UniqueEmail(string text);
        bool UniqueUsername(string text);
        User GetByUsername(string username);
        Task<CustomResponseDto<UserProfileDto>> GetUserProfileByUserId(string userid);
        Task<CustomResponseDto<AdminBaseDto<AdminUserDto>>> GetUsers(FilterPaginationDto filterPagination);
    }
}
