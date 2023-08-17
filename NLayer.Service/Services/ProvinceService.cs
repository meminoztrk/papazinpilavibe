using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.ProvinceDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Migrations;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProvinceService : Service<Province>,IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;
        public ProvinceService(IGenericRepository<Province> repository, IUnitOfWork unitOfWork,IProvinceRepository provinceRepository) : base(repository, unitOfWork)
        {
            _provinceRepository = provinceRepository;
        }

        public async Task<CustomResponseDto<List<ProvinceWithStateDto>>> GetSearchingProvinces(string value)
        {
            List<ProvinceWithStateDto> dto = new List<ProvinceWithStateDto>();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var provinces = await _provinceRepository.GetAll().ToListAsync();       
                dto = provinces
                    .Where(x => x.SehirIlceMahalleAdi.ToLower().StartsWith(value.ToLower()))
                    .Select(x => new ProvinceWithStateDto()
                    {
                        Id = x.Id,
                        City = State(x,provinces),
                    }).Take(10).ToList();
            }
    
            
            return CustomResponseDto<List<ProvinceWithStateDto>>.Success(200, dto);
        }

        private string State(Province prov,List<Province> provinces)
        {
            string result = String.IsNullOrEmpty(prov.MahalleID) && prov.Id > 81 ? fLetter(Regex.Replace(prov.SehirIlceMahalleAdi, @"\([^)]*\)", "").Trim()) + ", " : fLetter(prov.SehirIlceMahalleAdi);
            int ustid = prov.UstID;
            while (ustid != 0)
            {
                var item = provinces.Where(x => x.Id == ustid).SingleOrDefault();
                result += (item.UstID == 0 ? " / " : "") + fLetter(item.SehirIlceMahalleAdi);
                ustid = item.UstID;
            }
            return result;
        }

        private string fLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string firstLetter = input.Substring(0, 1).ToUpper();
            string restOfText = input.Substring(1).ToLower();

            return firstLetter + restOfText;
        }

    }
}


