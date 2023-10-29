using NLayer.Core.DTOs;
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

namespace NLayer.Core.Services
{
    public interface IBusinessService:IService<Business>
    {
        Task<CustomResponseDto<NoContentDto>> AddBusiness(BusinessAddDto business);
        Task<CustomResponseDto<NoContentDto>> UpdateBusiness(int id, BusinessUpdateDto business);
        Task<CustomResponseDto<List<BusinessByUserDto>>> GetBusinessesByUserId(string userId);
        Task<CustomResponseDto<BusinessWithCommentDto>> GetBusinessesWithCommentById(int id);
        Task<CustomResponseDto<BusinessCommentWithCountDto>> GetBusinessCommentsWithPaginationById(int id,int page,int take, bool isAsc, string commentType, int rate, string search);
        Task<CustomResponseDto<BusinessWithCountBySearching>> GetBusinessWithCountBySearching(int page, int take, int provinceId, bool isMostReview, string search);
        Task<CustomResponseDto<BusinessDto>> GetBusinessById(int id);
        Task<CustomResponseDto<AdminBaseDto<AdminBusinessDto>>> GetBusinessesWithUser(FilterPaginationDto paginationFilter);

    }
}
