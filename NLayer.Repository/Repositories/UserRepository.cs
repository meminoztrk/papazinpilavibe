using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
using NLayer.Core.DTOs.UserDTOs;
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
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserProfileDto> GetUserProfileByUserId(string userid)
        {
            return await _context.Users
                .Include(x => x.BusinessUserImages)
                .ThenInclude(x=>x.BusinessComment)
                .Include(x => x.BusinessComments)
                .Include(x=>x.Province)
                .Where(x => !x.IsDeleted && x.IsActive && x.UserId == userid)
                .Select(x => new UserProfileDto
                {
                    UserId = x.UserId,
                    Name = x.Name,
                    Surname = x.Surname,
                    UserPhoto = x.UserPhoto,
                    Location = x.Province.MergedArea,
                    TotalComment = x.BusinessComments.Count(x=> !x.IsDeleted && x.IsActive),
                    TotalImage = x.BusinessUserImages.Count(x=> !x.BusinessComment.IsDeleted && x.BusinessComment.IsActive),
                    Created = x.CreatedDate,
                    IsAdmin = x.IsAdmin,
                    IsBusiness = x.IsBusiness,
                    IsGoogle = x.IsGoogle,
                    IsUser = x.IsUser,
                }).FirstOrDefaultAsync();
        }

        public User  GetByUsername(string mail)
        {
            return _context.Users.FirstOrDefault(x => x.Email == mail);  
        } 

        public bool UniqueUsername(string text)
        {
            bool s = !_context.Users.Any(u => u.Email.Trim().ToLower() == text.Trim().ToLower());
            return s;
        }

        public async Task<AdminBaseDto<AdminUserDto>> GetUsers(FilterPaginationDto filterPagination)
        {
            var model = new AdminBaseDto<AdminUserDto>();
            IQueryable<AdminUserDto> businessQuery = _context.Users.AsNoTracking().Include(x => x.Province).Where(x => !x.IsDeleted).Select(x => new AdminUserDto
            {
                Id = x.Id,
                GuidId = x.UserId,
                FullName = x.Name + " " + x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                UserPhoto = x.UserPhoto,
                Location = x.Province.MergedArea,
                DateOfBirth = x.DateOfBirth,
                Created = x.CreatedDate,
                IsActive = x.IsActive,
                IsAdmin = x.IsAdmin,
                IsBusiness = x.IsBusiness,
                IsGoogle = x.IsGoogle,
                IsUser = x.IsUser
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

            Expression<Func<AdminUserDto, object>> keySelector = filterPagination.Sorter.Field switch
            {
                "fullName" => user => user.FullName,
                "email" => user => user.Email,
                "phone" => user => user.Phone,
                "location" => user => user.Location,
                "dateOfBirth" => user => user.DateOfBirth,
                "created" => user => user.Created,
                "isAdmin" => user => user.IsAdmin,
                "isUser" => user => user.IsUser,
                "isActive" => user => user.IsActive,
                "isGoogle" => user => user.IsGoogle,
                "isBusiness" => user => user.IsBusiness,
                _ => user => user.Id
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
    }
}
