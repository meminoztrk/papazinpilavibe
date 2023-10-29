using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        bool UniqueUsername(string text);
        User GetByUsername(string username);
        Task<UserProfileDto> GetUserProfileByUserId(string userid);
        Task<AdminBaseDto<AdminUserDto>> GetUsers(FilterPaginationDto filterPagination);
    }
}
