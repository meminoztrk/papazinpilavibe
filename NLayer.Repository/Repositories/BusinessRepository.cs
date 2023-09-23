using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
    {
        public BusinessRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<BusinessDto> GetBusinessWithIncludeById(int id)
        {
            return await _context.Business.Include(x => x.BusinessImages).Include(x => x.Province).Where(x => x.Id == id && x.IsDeleted == false).Select(x => new BusinessDto
            {
                Id = x.Id,
                BusinessName = x.BusinessName,
                MinHeader = x.MinHeader,
                About = x.About,
                FoodTypes = x.FoodTypes,
                BusinessProps = x.BusinessProps,
                BusinessServices = x.BusinessServices,
                Adress = x.Adress,
                Phone = x.Phone,
                MapIframe = x.MapIframe,
                BusinessLicense = x.BusinessLicense,
                CommentTypes = x.CommentTypes,
                BusinessType = x.BusinessType,
                ProvinceId = x.ProvinceId.Value,
                Cities = x.Province.UstID,
                City = _context.Provinces.Where(y=>y.Id == x.Province.UstID).FirstOrDefault().UstID,
                Website = x.Website,
                Facebook = x.Facebook,
                Instagram = x.Instagram,
                Twitter = x.Twitter,
                Mo = x.Mo,
                Tu = x.Tu,
                We = x.We,
                Th = x.Th,
                Fr = x.Fr,
                Sa = x.Sa,
                Su  = x.Su,
                BusinessImages = x.BusinessImages.Select(y=>y.Image).ToList(),
        
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<BusinessByUserDto>> GetBusinessesWithIncludeByUserId(int id)
        {
            return await _context.Business.Include(x=>x.BusinessImages).Include(x=>x.Province).Where(x=>x.UserId == id).Select(x=> new BusinessByUserDto { 
                Id = x.Id,
                BusinessName = x.BusinessName,
                BusinessImage = x.BusinessImages.FirstOrDefault().Image,
                BusinessType = x.BusinessType,
                Location = fLetter(Regex.Replace(x.Province.SehirIlceMahalleAdi, @"\([^)]*\)", "").Trim()),
                Process = x.Process,
                Views = x.Views,
                IsActive = x.IsActive,
            }).AsNoTracking().ToListAsync();
        }

        private static string fLetter(string input)
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
