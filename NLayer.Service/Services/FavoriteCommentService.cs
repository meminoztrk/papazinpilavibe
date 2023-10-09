using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class FavoriteCommentService : Service<FavoriteComment>, IFavoriteCommentService
    {
        private readonly IFavoriteCommentRepository _favoriteCommentRepository;
        public FavoriteCommentService(IGenericRepository<FavoriteComment> repository, IUnitOfWork unitOfWork, IFavoriteCommentRepository favoriteCommentRepository) : base(repository, unitOfWork)
        {
            _favoriteCommentRepository = favoriteCommentRepository;
        }

        public async Task<CustomResponseDto<List<BusinessCommentByUserDto>>> GetFavoriteComments(string userid, int page)
        {
            var favoriteCommentsByUser = await _favoriteCommentRepository.GetFavoriteComments(userid,page);

            return CustomResponseDto<List<BusinessCommentByUserDto>>.Success(200, favoriteCommentsByUser);
        }
    }
}
