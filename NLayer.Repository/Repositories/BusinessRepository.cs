using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.BusinessSubCommentDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace NLayer.Repository.Repositories
{
    public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
    {
        public BusinessRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<AdminBaseDto<AdminBusinessDto>> GetBusinessesWithUser(FilterPaginationDto filterPagination)
        {
            var model = new AdminBaseDto<AdminBusinessDto>();
            IQueryable<AdminBusinessDto> businessQuery = _context.Business.AsNoTracking().Include(x => x.User).Include(x => x.Province).Where(x => !x.IsDeleted).Select(x => new AdminBusinessDto
            {
                Id = x.Id,
                BusinessName = x.BusinessName,
                BusinessType = x.BusinessType,
                Phone = x.Phone,
                Location = x.Province.MergedArea,
                Process = x.Process,
                Email = x.User.Email,
                Created = x.CreatedDate,
                IsActive = x.IsActive
            });
            
            var filterSearch = filterPagination.Filters.Where(y => y.ColumnValue != null);
            

            if (filterSearch.Count() > 0)
            {
                string query = string.Empty;
                foreach (var item in filterSearch)
                {       
                    query += $"{item.ColumnName}.ToLower().Contains(\"{item.ColumnValue.ToLower()}\")";
                    query += item == filterSearch.Last() ? "" : " AND ";
                }
                businessQuery = businessQuery.Where(query);
            }

            Expression<Func<AdminBusinessDto, object>> keySelector = filterPagination.Sorter.Field switch
            {
                "businessName" => business => business.BusinessName,
                "businessType" => business => business.BusinessType,
                "location" => business => business.Location,
                "process" => business => business.Process,
                "email" => business => business.Email,
                "phone" => business => business.Phone,
                "created" => business => business.Created,
                _ => business => business.Id
            };

            if (filterPagination.Sorter.Order == "descend")
            {
                businessQuery = businessQuery.OrderByDescending(keySelector);
            }
            else
            {
                businessQuery = businessQuery.OrderBy(keySelector);
            }
            model.ItemCount = businessQuery.Count();
            model.Items = await businessQuery.Skip((filterPagination.Pagination.Current - 1) * filterPagination.Pagination.PageSize).Take(filterPagination.Pagination.PageSize).ToListAsync();
            return model;
        }

        public async Task<BusinessWithCountBySearching> GetBusinessWithCountBySearching(int page, int take,int provinceId, bool isMostReview, string search)
        {
            BusinessWithCountBySearching businessSearching = new BusinessWithCountBySearching();
            bool test = string.IsNullOrEmpty(search);
            string trimLower = !string.IsNullOrEmpty(search) ? search.Trim().ToLower() : string.Empty;
            var Searching = await _context.Business
                .Include(x=>x.BusinessComments.Where(x=>x.IsActive && !x.IsDeleted))
                .Include(x=>x.BusinessImages)
                .Include(x=>x.Province)
                .Where(x => x.IsActive
                && !x.IsDeleted
                && (x.Province.Id == provinceId || x.Province.UstID == provinceId || x.Province.CityId == provinceId)
                && (string.IsNullOrEmpty(search) 
                    || x.BusinessName.ToLower().Contains(trimLower)
                    || x.FoodTypes.ToLower().Contains(trimLower)
                    || x.BusinessProps.ToLower().Contains(trimLower)
                    || x.BusinessServices.ToLower().Contains(trimLower)
                    || x.BusinessComments.Any(y=> y.Comment.ToLower().Contains(trimLower))
                    )
                ).ToListAsync();

            businessSearching.BusinessCount = Searching.Count();
            businessSearching.ProvinceId = provinceId;
            businessSearching.ProvinceName = _context.Provinces.Where(x=>x.Id == provinceId).FirstOrDefault().MergedArea;
            businessSearching.BusinessesBySearching = Searching.Select(x => new BusinessBySearching {
                Id = x.Id,
                BusinessImage = x.BusinessImages.FirstOrDefault() != null ? x.BusinessImages.FirstOrDefault().Image : "defaultbusiness.png",
                BusinessType = x.BusinessType,
                BusinessName = x.BusinessName,
                TotalComment = x.BusinessComments.Count(),
                SearchCommentCount = x.BusinessComments.Where(x=>x.Comment.ToLower().Contains(trimLower)).Count(),
                SearchComment = CheckParagraph(search, x.BusinessComments.Where(x => x.Comment.ToLower().Contains(trimLower)).FirstOrDefault() == null ? "" : x.BusinessComments.Where(x => x.Comment.ToLower().Contains(trimLower)).FirstOrDefault().Comment),
                Rate = x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted).Select(x => x.Rate).DefaultIfEmpty().Average(),
                Location = _context.Provinces.Where(y => y.Id == x.ProvinceId).FirstOrDefault().MergedArea,
                FoodTypes = x.FoodTypes,
                BusinessProps = x.BusinessProps,
                BusinessServices = x.BusinessServices,
            }).OrderByDescending(x=> isMostReview ? x.TotalComment : x.Rate).Skip((page - 1) * take).Take(take).ToList();

            return businessSearching;
        }

        public async Task<BusinessCommentWithCountDto> GetBusinessCommentsWithPaginationById(int id, int page, int take, bool isAsc, string commentType, int rate, string search)
        {
            var listed = _context.BusinessComment
                .Include(x=>x.FavoriteComments)
                .Include(x => x.BusinessSubComments)
                .Where(x => x.IsActive && !x.IsDeleted && x.BusinessId == id && (string.IsNullOrEmpty(search) || x.Comment.ToLower().Contains(search.Trim().ToLower())))
                .Select(x => new BusinessCommentDto
                {
                    Id = x.Id,
                    BusinessId = x.Id,
                    Name = x.User.Name,
                    Surname = x.User.Surname,
                    UserId = x.User.UserId,
                    UserImage = x.User.UserPhoto,
                    TotalComment = _context.BusinessComment.Count(y => y.UserId == x.UserId && y.IsActive && !y.IsDeleted),
                    Rate = x.Rate,
                    Comment = x.Comment,
                    CommentType = x.CommentType,
                    Created = x.CreatedDate,
                    Images = x.BusinessUserImages.Select(y => y.Image).ToList(),
                    LikedUsers = x.FavoriteComments.Select(x => x.UserId).ToList(),
                    SubComments = x.BusinessSubComments.Where(y => y.IsActive && !y.IsDeleted).Select(y => new BusinessSubCommentDto
                    {
                        Id = y.Id,
                        UserId = _context.Users.Where(y => y.Id == x.UserId.Value).FirstOrDefault().UserId,
                        Comment = y.Comment,
                        Created = y.CreatedDate,
                    }).ToList()
                }).AsNoTracking();

            BusinessCommentWithCountDto comments = new BusinessCommentWithCountDto();


            switch (true)
            {
                case bool n when n == (!isAsc && string.IsNullOrEmpty(commentType) && rate == 0)://011
                    comments.CommentCount = listed.Count();
                    comments.BusinessComments = await listed.OrderByDescending(x => x.Id).Skip((page - 1) * take).Take(take).ToListAsync();
                    break;
                case bool n when n == (isAsc && string.IsNullOrEmpty(commentType) && rate == 0): //111
                    comments.CommentCount = listed.Count();
                    comments.BusinessComments = await listed.Skip((page - 1) * take).Take(take).ToListAsync();
                    break;
                case bool n when n == (isAsc && string.IsNullOrEmpty(commentType) && rate != 0)://110
                    comments.CommentCount = listed.Count(x => x.Rate == rate);
                    comments.BusinessComments = await listed.Where(x=>x.Rate== rate).Skip((page - 1) * take).Take(take).ToListAsync();
                    break;
                case bool n when n == (isAsc && !string.IsNullOrEmpty(commentType) && rate == 0)://101
                    comments.CommentCount = listed.Count(x => x.CommentType.Contains(commentType));
                    comments.BusinessComments = await listed.Where(x=>x.CommentType.Contains(commentType)).Skip((page - 1) * take).Take(take).ToListAsync();
                    break;
                case bool n when n == (isAsc && !string.IsNullOrEmpty(commentType) && rate != 0)://100
                    comments.CommentCount = listed.Count(x => x.CommentType.Contains(commentType) && x.Rate == rate);
                    comments.BusinessComments = await listed.Where(x=> x.CommentType.Contains(commentType) && x.Rate == rate).Skip((page - 1) * take).Take(take).ToListAsync();
                    break;
                case bool n when n == (!isAsc && !string.IsNullOrEmpty(commentType) && rate != 0)://000
                    comments.CommentCount = listed.Count(x => x.CommentType.Contains(commentType) && x.Rate == rate);
                    comments.BusinessComments = await listed.OrderByDescending(x=>x.Id).Where(x => x.CommentType.Contains(commentType) && x.Rate == rate).Skip((page - 1) * take).Take(take).ToListAsync();
                    break;
                case bool n when n == (!isAsc && !string.IsNullOrEmpty(commentType) && rate == 0)://001
                    comments.CommentCount = listed.Count(x => x.CommentType.Contains(commentType));
                    comments.BusinessComments = await listed.OrderByDescending(x => x.Id).Where(x => x.CommentType.Contains(commentType)).Skip((page - 1) * take).Take(take).ToListAsync();
                    break;
                case bool n when n == (!isAsc && string.IsNullOrEmpty(commentType) && rate != 0)://010
                    comments.CommentCount = listed.Count(x => x.Rate == rate);
                    comments.BusinessComments = await listed.OrderByDescending(x => x.Id).Where(x => x.Rate == rate).Skip((page - 1) * take).Take(take).ToListAsync();
                    break;    
                default:
                    comments.CommentCount = listed.Count();
                    comments.BusinessComments = await listed.ToListAsync();
                    break;
            }

            return comments;
        }

        public async Task<BusinessWithCommentDto> GetBusinessesWithCommentById(int id)
        {
            var comments = _context.BusinessComment.Where(x => x.IsActive && !x.IsDeleted && x.BusinessId == id);
            int commentCount = comments.Count();
            return await _context.Business
                .Include(x => x.User)
                .Include(x => x.Province)
                .Include(x => x.BusinessImages)
                .Include(x => x.FavoriteBusinesses)
                .Include(x => x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted)).ThenInclude(x => x.BusinessUserImages)
                .Include(x => x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted)).ThenInclude(x => x.User)
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
                    LikedUsers = x.FavoriteBusinesses.Select(x=>x.UserId).ToList(),
                    CommentCount = commentCount,
                    FivePercent = commentCount > 0 ? comments.Count(y => y.Rate == 5) * 100 / commentCount : 0,
                    FourPercent = commentCount > 0 ? comments.Count(y => y.Rate == 4) * 100 / commentCount : 0,
                    ThreePercent = commentCount > 0 ? comments.Count(y => y.Rate == 3) * 100 / commentCount : 0,
                    TwoPercent = commentCount > 0 ? comments.Count(y => y.Rate == 2) * 100 / commentCount : 0,
                    OnePercent = commentCount > 0 ? comments.Count(y => y.Rate == 1) * 100 / commentCount : 0,
                    BusinessImages = NestedListToList(x.BusinessImages.Select(y => y.Image).ToList(), x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted).Select(y => y.BusinessUserImages.Select(x => x.Image)).ToList()),
                    //BusinessComments = x.BusinessComments.Where(x=> x.IsActive && !x.IsDeleted).Select(x=> new BusinessCommentDto {
                    //    Id = x.Id,
                    //    BusinessId = x.Id,
                    //    Name = x.User.Name,
                    //    Surname = x.User.Surname,
                    //    UserId = x.User.UserId,
                    //    UserImage = x.User.UserPhoto,
                    //    TotalComment = _context.BusinessComment.Count(y => y.UserId == x.UserId && y.IsActive && !y.IsDeleted),
                    //    Rate = x.Rate,
                    //    Comment = x.Comment,
                    //    CommentType = x.CommentType,
                    //    Created = x.CreatedDate,
                    //    Images = x.BusinessUserImages.Select(y => y.Image).ToList(),
                    //    SubComments = x.BusinessSubComments.Where(y=> y.IsActive && !y.IsDeleted).Select(y=> new BusinessSubCommentDto
                    //    {
                    //        Id = y.Id,
                    //        UserId = _context.Users.Where(y => y.Id == x.UserId.Value).FirstOrDefault().UserId,
                    //        Comment = y.Comment, 
                    //        Created = y.CreatedDate,
                    //    }).ToList()
                        
                    //}).OrderByDescending(x=>x.Created).ToList(),
                    
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

        private static string CheckParagraph(string search, string paragraph)
        {
            string result = "";
            var splitString = paragraph.ToLower().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray(); 
            if (splitString.Any(x=>x.Contains(search)))
            {
                int wordIndex = Array.FindIndex(splitString, s => s.Contains(search));

                result = "..." + String.Join(" ", splitString.Where((k, index) => index <= wordIndex + 5).Select(k => k).Skip(wordIndex - 5)) + "...";
            }
            return result;
        }

        private static bool ContainsAny(string text, string[] needles)
        {
            foreach (string needle in needles)
            {
                if (text.Contains(needle))
                    return true;
            }

            return false;
        }
    }
}
