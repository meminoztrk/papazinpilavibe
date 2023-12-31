﻿using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs.BusinessCommentDTOs;

namespace NLayer.Core.Services
{
    public interface IFavoriteCommentService:IService<FavoriteComment>
    {
        Task<CustomResponseDto<List<BusinessCommentByUserDto>>> GetFavoriteComments(string userid, int page);
    }
}
