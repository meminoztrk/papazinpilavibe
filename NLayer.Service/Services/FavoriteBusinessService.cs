using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class FavoriteBusinessService : Service<FavoriteBusiness>, IFavoriteBusinessService
    {
        private readonly IFavoriteBusinessRepository _favoriteBusinessRepository;
        public FavoriteBusinessService(IGenericRepository<FavoriteBusiness> repository, IUnitOfWork unitOfWork, IFavoriteBusinessRepository favoriteBusinessRepository) : base(repository, unitOfWork)
        {
            _favoriteBusinessRepository = favoriteBusinessRepository;
        }

        public async Task<CustomResponseDto<List<BusinessFavoriteDto>>> GetFavoriteBusinesses(string userid, int page)
        {
            return CustomResponseDto<List<BusinessFavoriteDto>>.Success(200, await _favoriteBusinessRepository.GetFavoriteBusinesses(userid, page));
        }
    }
}
