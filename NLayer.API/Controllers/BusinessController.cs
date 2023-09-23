using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly CustomImageProcessing _image;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BusinessController(IMapper mapper, IUserService userService, JwtService jwtService, CustomImageProcessing image, IBusinessService businessService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _image = image;
            _businessService = businessService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> AddBusiness([FromForm] BusinessAddDto business)
        {
            return CreateActionResult(await _businessService.AddBusiness(business));
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
        public async Task<IActionResult> GetBusinessById(int id)
        {
            return CreateActionResult(await _businessService.GetBusinessById(id));
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
