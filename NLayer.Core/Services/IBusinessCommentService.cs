using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;

namespace NLayer.Core.Services
{
    public interface IBusinessCommentService:IService<BusinessComment>
    {
        Task<CustomResponseDto<NoContentDto>> AddComment(BusinessCommentAddDto businessComment);
        Task<CustomResponseDto<List<BusinessCommentByUserDto>>> GetUserComments(string userid, int page);
        Task<CustomResponseDto<BusinessCommentByUserDto>> GetUserCommentById(int id);
        Task<CustomResponseDto<AdminBaseDto<AdminBusinessCommentDto>>> GetCommentsWithUser(FilterPaginationDto filterPagination);
    }
}
