using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ReportDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Core.DTOs.FavoriteBusinessDTOs;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    public class FavoriteBusinessController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteBusinessService _favoriteBusinessService;
        public FavoriteBusinessController(IFavoriteBusinessService favoriteBusinessService, IMapper mapper)
        {
            _favoriteBusinessService = favoriteBusinessService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFavoriteBusinessesByUserId(string userid, int page)
        {
            return CreateActionResult(await _favoriteBusinessService.GetFavoriteBusinesses(userid, page));
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteBusiness(FavoriteBusinessAddDto favoriteBusiness)
        {
            var mappedFavoriteBusiness = _mapper.Map<FavoriteBusiness>(favoriteBusiness);
            await _favoriteBusinessService.AddAsync(mappedFavoriteBusiness);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteBusiness(int businessId, int userid)
        {
            var entity = _favoriteBusinessService.Where(x => x.BusinessId == businessId && x.UserId == userid).FirstOrDefault();
            await _favoriteBusinessService.RemoveAsync(entity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
