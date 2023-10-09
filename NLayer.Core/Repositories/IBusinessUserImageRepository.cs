using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface  IBusinessUserImageRepository:IGenericRepository<BusinessUserImage>
    {
        Task<List<string>> GetPreviewImagesByUserId(string userid);
    }
}
