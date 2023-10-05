using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

                var splitted = value.ToLower().Split();
                var provinces = await _provinceRepository.GetAll().ToListAsync();
                dto = provinces
                    .Where(x => splitted.All(y => x.MergedArea.ToLower().Contains(y)))
                    .Select(x => new ProvinceWithStateDto()
                    {
                        Id = x.Id,
                        City = x.MergedArea,
                    }).Take(10).ToList();
            }


            return CustomResponseDto<List<ProvinceWithStateDto>>.Success(200, dto);
        }

        //private string State(Province prov,List<Province> provinces)
        //{
        //    string result = String.IsNullOrEmpty(prov.MahalleID) && prov.Id > 81 ? fLetter(prov.SehirIlceMahalleAdi) + ", " : fLetter(prov.SehirIlceMahalleAdi);
        //    int ustid = prov.UstID;
        //    while (ustid != 0)
        //    {
        //        var item = provinces.Where(x => x.Id == ustid).SingleOrDefault();
        //        result += (item.UstID == 0 ? " / " : "") + fLetter(item.SehirIlceMahalleAdi);
        //        ustid = item.UstID;
        //    }
        //    return result;
        //}

        //private string fLetter(string sentence)
        //{
        //    sentence = sentence.Replace("(MERKEZ)","").Trim();
        //    string[] words = sentence.Trim().Split();
        //    StringBuilder result = new StringBuilder();

        //    foreach (string word in words)
        //    {
        //        if (word == "") { continue; }
        //        if (word.StartsWith("("))
        //        {
        //            // Kelime parantez içindeyse, içeriğini koruyarak baş harfi büyük geri kalan küçük yap.
        //            result.Append("(");
        //            result.Append(char.ToUpper(word[1]));
        //            result.Append(word.Substring(2).ToLower());
        //        }
        //        else
        //        {
        //            // Diğer kelimelerin baş harfini büyük geri kalanını küçük yap.
        //            result.Append(char.ToUpper(word[0]));
        //            result.Append(word.Substring(1).ToLower());
        //        }

        //        result.Append(" ");
        //    }

        //    return result.ToString().Trim();
        //}



    }
}


