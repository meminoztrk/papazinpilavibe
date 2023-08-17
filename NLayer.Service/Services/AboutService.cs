using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.About;
using NLayer.Core.DTOs.ProvinceDTOs;
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
    public class AboutService : Service<About>, IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        private readonly IMapper _mapper;
        public AboutService(IGenericRepository<About> repository, IUnitOfWork unitOfWork, IAboutRepository aboutRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _aboutRepository = aboutRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<AboutDto>> GetAbout()
        {
            var about = await _aboutRepository.GetAll().FirstOrDefaultAsync();
            var abotDto = _mapper.Map<AboutDto>(about);

            return CustomResponseDto<AboutDto>.Success(200, abotDto);
        }
    }
}
//mh. şehir. il >> gelen değerin stateidsi 0 ise sadece il döndür >> gelen ilçe ise ustidye bak il ve ilçeyi yazdır >> gelen mh ise üst ıd nin üst id sine bak yazdır

