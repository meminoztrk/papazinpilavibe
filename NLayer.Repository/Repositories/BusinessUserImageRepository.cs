using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class BusinessUserImageRepository : GenericRepository<BusinessUserImage>, IBusinessUserImageRepository
    {
        public BusinessUserImageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<string>> GetPreviewImagesByUserId(string userid)
        {
            return await _context.BusinessUserImage
                .Where(x => !x.IsDeleted && x.IsActive && x.UserId == _context.Users.Where(x => x.UserId == userid).FirstOrDefault().Id)
                .Select(x => x.Image)
                .ToListAsync();
        }
    }
}
