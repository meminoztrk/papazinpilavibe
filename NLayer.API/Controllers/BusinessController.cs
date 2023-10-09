using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Helper;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    public class BusinessController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IBusinessService _businessService;
        private readonly IBusinessCommentService _businessCommentService;
        private readonly IBusinessUserImageService _businessUserImageService;
        private readonly CustomImageProcessing _image;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BusinessController(IMapper mapper, IUserService userService, JwtService jwtService, CustomImageProcessing image, IBusinessService businessService, IWebHostEnvironment webHostEnvironment, IBusinessCommentService businessCommentService, IBusinessUserImageService businessUserImageService)
        {
            _mapper = mapper;
            _image = image;
            _businessService = businessService;
            _webHostEnvironment = webHostEnvironment;
            _businessCommentService = businessCommentService;
            _businessUserImageService = businessUserImageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBusiness([FromForm] BusinessAddDto business)
        {
            return CreateActionResult(await _businessService.AddBusiness(business));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddComment([FromForm] BusinessCommentAddDto businessComment)
        {
            return CreateActionResult(await _businessCommentService.AddComment(businessComment));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommentWithPagination(int id, bool isAsc, string commentType, int rate, string search, int page = 1, int take = 1)
        {
            return CreateActionResult(await _businessService.GetBusinessCommentsWithPaginationById(id,page,take,isAsc,commentType,rate,search));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBusinessWithCountBySearching(int provinceId, bool isMostReview, string search, int page = 1, int take = 1)
        {
            return CreateActionResult(await _businessService.GetBusinessWithCountBySearching(page, take, provinceId, isMostReview, search));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBusiness(int id,[FromForm] BusinessUpdateDto business)
        {
            return CreateActionResult(await _businessService.UpdateBusiness(id,business));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBusinessesByUserId(string userId)
        {
            return CreateActionResult(await _businessService.GetBusinessesByUserId(userId));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBusinessesWithCommentById(int id)
        {
            return CreateActionResult(await _businessService.GetBusinessesWithCommentById(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommentsByUser(string userId, int page = 1)
        {
            return CreateActionResult(await _businessCommentService.GetUserComments(userId, page));

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPreviewImagesByUserId(string userId)
        {
            return CreateActionResult(await _businessUserImageService.GetPreviewImagesByUserId(userId));
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetBusinessById(int id)
        {
            return CreateActionResult(await _businessService.GetBusinessById(id));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument entity)
        {
            await _businessService.UpdatePatchAsync(id, entity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]")]
        public IActionResult GetImage(string path,string name)
        {
            string imagePath = _webHostEnvironment.WebRootPath + "/img/" + path + "/" + name;
            Byte[] image = null;
            if (System.IO.File.Exists(imagePath))
            {
                image = System.IO.File.ReadAllBytes(imagePath);
                return File(image, "image/png");
            }
            return BadRequest();
        }
    }
}
