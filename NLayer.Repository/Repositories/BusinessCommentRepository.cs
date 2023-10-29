using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessSubCommentDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class BusinessCommentRepository : GenericRepository<BusinessComment>, IBusinessCommentRepository
    {
        public BusinessCommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<AdminBaseDto<AdminBusinessCommentDto>> GetCommentsWithUser(FilterPaginationDto filterPagination)
        {
            var model = new AdminBaseDto<AdminBusinessCommentDto>();
            IQueryable<AdminBusinessCommentDto> businessQuery = _context.BusinessComment.AsNoTracking().Include(x => x.User).Include(x => x.Business).Where(x => !x.IsDeleted)
                .Select(x => new AdminBusinessCommentDto
            {
                Id = x.Id,
                BusinessId = x.BusinessId.Value,
                BusinessName = x.Business.BusinessName,
                FullName = x.User.Name + " " + x.User.Surname,
                Email = x.User.Email,
                Comment = x.Comment,
                CommentType = x.CommentType,
                Rate = x.Rate,
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

            Expression<Func<AdminBusinessCommentDto, object>> keySelector = filterPagination.Sorter.Field switch
            {
                "fullName" => comment => comment.FullName,
                "businessName" => comment => comment.BusinessName,
                "email" => comment => comment.Email,
                "rate" => comment => comment.Rate,
                "created" => comment => comment.Created,
                _ => comment => comment.Id
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

        public async Task<BusinessCommentByUserDto> GetUserCommentById(int id)
        {
            return await _context.BusinessComment
               .Include(x => x.Business)
               .ThenInclude(x => x.BusinessImages)
               .Include(x => x.Business)
               .ThenInclude(x => x.Province)
               .Include(x => x.BusinessUserImages)
               .Include(x => x.BusinessSubComments)
               .Include(x => x.FavoriteComments)
               .Where(x => !x.IsDeleted && x.IsActive && x.Id == id)
               .Select(x => new BusinessCommentByUserDto
               {
                   Id = x.Id,
                   BusinessId = x.BusinessId.Value,
                   BusinessImage = x.Business.BusinessImages.FirstOrDefault().Image,
                   BusinessName = x.Business.BusinessName,
                   Location = x.Business.Province.MergedArea,
                   Images = x.BusinessUserImages.Select(x => x.Image).ToList(),
                   Rate = x.Rate,
                   Comment = x.Comment,
                   CommentType = x.CommentType,
                   Created = x.CreatedDate,
                   LikedUsers = x.FavoriteComments.Select(x => x.UserId).ToList(),
                   SubComments = x.BusinessSubComments.Where(y => y.IsActive && !y.IsDeleted).Select(y => new BusinessSubCommentDto
                   {
                       Id = y.Id,
                       UserId = _context.Users.Where(z => z.Id == y.UserId).FirstOrDefault().UserId,
                       Comment = y.Comment,
                       Created = y.CreatedDate,
                   }).ToList()
               }).FirstOrDefaultAsync();
        }

        public async Task<List<BusinessCommentByUserDto>> GetUserComments(string userid, int page)
        {
            return await _context.BusinessComment
                .Include(x=>x.Business)
                .ThenInclude(x=>x.BusinessImages)
                .Include(x => x.Business)
                .ThenInclude(x => x.Province)
                .Include(x=>x.BusinessUserImages)
                .Include(x=>x.BusinessSubComments)
                .Include(x=>x.FavoriteComments)
                .Where(x=>!x.IsDeleted && x.IsActive && x.UserId == _context.Users.Where(x=>x.UserId == userid).FirstOrDefault().Id)
                .Select(x=> new BusinessCommentByUserDto
                {
                    Id = x.Id,
                    BusinessId = x.BusinessId.Value,
                    BusinessImage = x.Business.BusinessImages.FirstOrDefault().Image,
                    BusinessName = x.Business.BusinessName,
                    Location = x.Business.Province.MergedArea,
                    Images = x.BusinessUserImages.Select(x=>x.Image).ToList(),
                    Rate = x.Rate,
                    Comment = x.Comment,
                    CommentType = x.CommentType,
                    Created = x.CreatedDate,
                    LikedUsers = x.FavoriteComments.Select(x => x.UserId).ToList(),
                    SubComments = x.BusinessSubComments.Where(y => y.IsActive && !y.IsDeleted).Select(y => new BusinessSubCommentDto
                    {
                        Id = y.Id,
                        UserId = _context.Users.Where(z => z.Id == y.UserId).FirstOrDefault().UserId,
                        Comment = y.Comment,
                        Created = y.CreatedDate,
                    }).ToList()
                }).OrderByDescending(x=>x.Created).Skip((page - 1) * 10).Take(10).ToListAsync();
        }
    }
}
