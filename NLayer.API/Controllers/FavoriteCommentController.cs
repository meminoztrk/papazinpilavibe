using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ReportDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Core.DTOs.FavoriteCommentDTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;

namespace NLayer.API.Controllers
{
    public class FavoriteCommentController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteCommentService _favoriteCommentService;
        public FavoriteCommentController(IFavoriteCommentService favoriteCommentService, IMapper mapper)
        {
            _favoriteCommentService = favoriteCommentService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFavoriteCommentsByUserId(string userid, int page)
        {
            return CreateActionResult(await _favoriteCommentService.GetFavoriteComments(userid, page));
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteComment(FavoriteCommentAddDto favoriteComment)
        {
            var mappedFavoriteComment = _mapper.Map<FavoriteComment>(favoriteComment);
            await _favoriteCommentService.AddAsync(mappedFavoriteComment);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteComment(int businessCommentId,int userid)
        {
            var entity = _favoriteCommentService.Where(x => x.BusinessCommentId == businessCommentId && x.UserId == userid).FirstOrDefault();
            await _favoriteCommentService.RemoveAsync(entity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
