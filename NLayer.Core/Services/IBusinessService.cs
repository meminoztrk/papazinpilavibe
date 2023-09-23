﻿using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BusinessDTOs;
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
        Task<CustomResponseDto<BusinessDto>> GetBusinessById(int id);
    }
}