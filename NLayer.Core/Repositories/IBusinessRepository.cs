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
    public interface IBusinessRepository:IGenericRepository<Business>
    {
        Task<BusinessWithCommentDto> GetBusinessesWithCommentById(int id);
        Task<List<BusinessCommentDto>> GetBusinessCommentsWithPaginationById(int id, int page, int take, bool isAsc, string commentType, int rate, string search);
        Task<List<BusinessByUserDto>> GetBusinessesWithIncludeByUserId(int id);
        Task<BusinessDto> GetBusinessWithIncludeById(int id);
    }
}
