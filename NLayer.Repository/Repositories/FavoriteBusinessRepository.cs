using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class FavoriteBusinessRepository : GenericRepository<FavoriteBusiness>, IFavoriteBusinessRepository
    {
        public FavoriteBusinessRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<BusinessFavoriteDto>> GetFavoriteBusinesses(string userid, int page)
        {
            int user = _context.Users.Where(x => x.UserId == userid).FirstOrDefault().Id;
            return await _context.Business
                .Include(x=>x.FavoriteBusinesses)
                .Include(x=>x.BusinessComments)
                .Include(x=>x.BusinessImages)
                .Include(x=>x.Province)
                .Where(x => x.IsActive && !x.IsDeleted && x.FavoriteBusinesses.Any(x => x.UserId == user))
                .Select(x => new BusinessFavoriteDto
                {
                    Id = x.Id,
                    BusinessImage = x.BusinessImages.FirstOrDefault().Image,
                    BusinessType = x.BusinessType,
                    BusinessName = x.BusinessName,
                    TotalComment = x.BusinessComments.Count(),
                    Rate = x.BusinessComments.Where(x => x.IsActive && !x.IsDeleted).Select(x => x.Rate).DefaultIfEmpty().Average(),
                    Location = x.Province.MergedArea,
                    FoodTypes = x.FoodTypes,
                    BusinessProps = x.BusinessProps,
                    BusinessServices = x.BusinessServices,
                    Created = x.FavoriteBusinesses.FirstOrDefault().CreatedDate,
                })
                .OrderByDescending(x => x.Created)
                .Skip((page - 1) * 20)
                .Take(20)
                .ToListAsync();
        }
    }
}
