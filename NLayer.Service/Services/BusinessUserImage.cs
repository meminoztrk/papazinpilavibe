using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class BusinessUserImageService : Service<BusinessUserImage>, IBusinessUserImageService
    {
        private readonly IBusinessUserImageRepository _businessUserImageRepository;
        public BusinessUserImageService(IGenericRepository<BusinessUserImage> repository, IUnitOfWork unitOfWork, IBusinessUserImageRepository businessUserImageRepository) : base(repository, unitOfWork)
        {
            _businessUserImageRepository = businessUserImageRepository;
        }

        public async Task<CustomResponseDto<List<string>>> GetPreviewImagesByUserId(string userid)
        {
            return CustomResponseDto<List<string>>.Success(200, await _businessUserImageRepository.GetPreviewImagesByUserId(userid));
        }
    }
}
