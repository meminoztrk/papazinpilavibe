using NLayer.Core.DTOs;
using NLayer.Core.DTOs.ProvinceDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProvinceService:IService<Province>
    {
        Task<CustomResponseDto<List<ProvinceWithStateDto>>> GetSearchingProvinces(string value);
    }
}
