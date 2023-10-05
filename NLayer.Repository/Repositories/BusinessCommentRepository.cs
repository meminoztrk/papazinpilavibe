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
    public class BusinessCommentRepository : GenericRepository<BusinessComment>, IBusinessCommentRepository
    {
        public BusinessCommentRepository(AppDbContext context) : base(context)
        {
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
