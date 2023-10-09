using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs.BusinessDTOs;

namespace NLayer.Core.Services
{
    public interface IFavoriteBusinessService:IService<FavoriteBusiness>
    {
        Task<CustomResponseDto<List<BusinessFavoriteDto>>> GetFavoriteBusinesses(string userid, int page);
    }
}
