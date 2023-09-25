using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.BusinessSubCommentDTOs;
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

        public async Task<BusinessWithCommentDto> GetBusinessesWithCommentById(int id)
        {
            return await _context.Business
                .Include(x => x.User)
                .Include(x => x.Province)
                .Include(x => x.BusinessImages)
                .Include(x => x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted)).ThenInclude(x => x.BusinessUserImages)
                .Include(x => x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted)).ThenInclude(x => x.BusinessSubComments)
                .Where(x => x.Id == id && x.IsActive && !x.IsDeleted)
                .Select(x => new BusinessWithCommentDto
                {
                    Id = x.Id,
                    UserId = x.User.UserId,
                    BusinessName = x.BusinessName,
                    MinHeader = x.MinHeader,
                    About = x.About,
                    FoodTypes = x.FoodTypes,
                    BusinessProps = x.BusinessProps,
                    BusinessServices = x.BusinessServices,
                    Adress = x.Adress,
                    Phone = x.Phone,
                    MapIframe = x.MapIframe,
                    CommentTypes = x.CommentTypes,
                    BusinessType = x.BusinessType,
                    CityId = _context.Provinces.Where(y => y.Id == x.Province.UstID).FirstOrDefault().UstID,
                    City = fLetter(Regex.Replace(_context.Provinces.Where(z=>z.Id == _context.Provinces.Where(y => y.Id == x.Province.UstID).FirstOrDefault().UstID).FirstOrDefault().SehirIlceMahalleAdi, @"\([^)]*\)", "").Trim()),
                    DistrictId = x.Province.UstID,
                    District = fLetter(Regex.Replace(_context.Provinces.Where(y => y.Id == x.Province.UstID).FirstOrDefault().SehirIlceMahalleAdi, @"\([^)]*\)", "").Trim()),
                    NeighborhoodId = x.ProvinceId.Value,
                    Neighborhood = fLetter(Regex.Replace(x.Province.SehirIlceMahalleAdi, @"\([^)]*\)", "").Trim()),
                    Rate = x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted).Select(x=>x.Rate).DefaultIfEmpty().Average(),
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
                    Su = x.Su,
                    BusinessImages = NestedListToList(x.BusinessImages.Select(y => y.Image).ToList(), x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted).Select(y => y.BusinessUserImages.Select(x => x.Image)).ToList()),
                    BusinessComments = x.BusinessComments.Where(x=> x.IsActive && !x.IsDeleted).Select(x=> new BusinessCommentDto {
                        Id = x.Id,
                        BusinessId = x.Id,
                        UserId = _context.Users.Where(y=> y.Id == x.UserId.Value).FirstOrDefault().UserId,
                        Rate = x.Rate,
                        Comment = x.Comment,
                        Created = x.CreatedDate,
                        Images = x.BusinessUserImages.Select(y => y.Image).ToList(),
                        SubComments = x.BusinessSubComments.Where(y=> y.IsActive && !y.IsDeleted).Select(y=> new BusinessSubCommentDto
                        {
                            Id = y.Id,
                            UserId = _context.Users.Where(y => y.Id == x.UserId.Value).FirstOrDefault().UserId,
                            Comment = y.Comment, 
                            Created = y.CreatedDate,
                        }).ToList()
                        
                    }).ToList(),
                    
                }).AsNoTracking().FirstOrDefaultAsync();
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
            return await _context.Business.Include(x=>x.BusinessImages).Include(x=>x.Province).Where(x=>x.UserId == id && x.IsDeleted == false).Select(x=> new BusinessByUserDto { 
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

        private static List<string> NestedListToList(List<string> list,List<IEnumerable<string>> nestedList)
        {
            List<string> newList = new List<string>();
            newList.AddRange(list);
            foreach (var nest in nestedList)
            {
                foreach (var item in nest)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

    }
}
