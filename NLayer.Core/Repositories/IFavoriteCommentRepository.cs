using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IFavoriteCommentRepository:IGenericRepository<FavoriteComment>
    {
        Task<List<BusinessCommentByUserDto>> GetFavoriteComments(string userid, int page);
    }
}
