using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.ProvinceDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    public class GenericController : CustomBaseController
    {
        private readonly IProvinceService _provinceService;
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GenericController(IProvinceService provinceService, IAboutService aboutService, IWebHostEnvironment webHostEnvironment)
        {
            _provinceService = provinceService;
            _aboutService = aboutService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSearchingProvinces(string value)
        {
            return CreateActionResult(await _provinceService.GetSearchingProvinces(value));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProvinces()
        {
            var data = await _provinceService.Where(x => x.UstID == 0 || x.MahalleID != null).ToListAsync();
            return CreateActionResult(CustomResponseDto<List<Province>>.Success(200, data));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAbout()
        {
            return CreateActionResult(await _aboutService.GetAbout());
        }


    }
}
