using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessSubCommentDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class FavoriteCommentRepository : GenericRepository<FavoriteComment>, IFavoriteCommentRepository
    {
        public FavoriteCommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<BusinessCommentByUserDto>> GetFavoriteComments(string userid, int page)
        {
            int user = _context.Users.Where(x => x.UserId == userid).FirstOrDefault().Id;
            return await _context.BusinessComment
                .Include(x=>x.User)
                .Include(x => x.Business)
                .ThenInclude(x => x.BusinessImages)
                .Include(x => x.Business)
                .ThenInclude(x => x.Province)
                .Include(x => x.BusinessUserImages)
                .Include(x => x.BusinessSubComments)
                .Include(x => x.FavoriteComments)
                .Where(x => !x.IsDeleted && x.IsActive && x.FavoriteComments.Any(x=>x.UserId == user))
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
                    UserId = x.User.UserId,
                    UserPhoto = x.User.UserPhoto,
                    UserName = x.User.Name,
                    UserSurname = x.User.Surname,
                    TotalComment = _context.BusinessComment.Count(y => y.IsActive && !y.IsDeleted && y.UserId == x.User.Id),
                    Created = x.FavoriteComments.FirstOrDefault().CreatedDate,
                    LikedUsers = x.FavoriteComments.Select(x => x.UserId).ToList(),
                    SubComments = x.BusinessSubComments.Where(y => y.IsActive && !y.IsDeleted).Select(y => new BusinessSubCommentDto
                    {
                        Id = y.Id,
                        UserId = _context.Users.Where(z => z.Id == y.UserId).FirstOrDefault().UserId,
                        Comment = y.Comment,
                        Created = y.CreatedDate,
                    }).ToList()
                }).OrderByDescending(x => x.Created).Skip((page - 1) * 10).Take(10).ToListAsync();
        }
    }
}
