using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
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
        Task<BusinessCommentWithCountDto> GetBusinessCommentsWithPaginationById(int id, int page, int take, bool isAsc, string commentType, int rate, string search);
        Task<BusinessWithCountBySearching> GetBusinessWithCountBySearching(int page, int take,int provinceId, bool isMostReview, string search);
        Task<List<BusinessByUserDto>> GetBusinessesWithIncludeByUserId(int id);
        Task<BusinessDto> GetBusinessWithIncludeById(int id);
        Task<AdminBaseDto<AdminBusinessDto>> GetBusinessesWithUser(FilterPaginationDto filterPagination);
    }
}
