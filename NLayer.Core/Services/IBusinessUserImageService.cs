﻿using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IBusinessUserImageService:IService<BusinessUserImage>
    {
        Task<CustomResponseDto<List<string>>> GetPreviewImagesByUserId(string userid);
    }
}
