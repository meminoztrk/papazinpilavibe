using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
