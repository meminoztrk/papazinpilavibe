using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IBusinessCommentRepository:IGenericRepository<BusinessComment>
    {
        Task<List<BusinessCommentByUserDto>> GetUserComments(string userid,int page);
    }
}
