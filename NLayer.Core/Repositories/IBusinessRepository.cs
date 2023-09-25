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
        Task<List<BusinessByUserDto>> GetBusinessesWithIncludeByUserId(int id);
        Task<BusinessDto> GetBusinessWithIncludeById(int id);
    }
}
