using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace NLayer.API.Controllers
{
    public class StockController : CustomBaseController
    {
        private readonly IStockService _stockService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StockController(IStockService stockService, IWebHostEnvironment webHostEnvironment)
        {
            _stockService = stockService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Stock stock,IFormFile uploadImage)
        {
            stock.Image = await ImageResizeUpload(uploadImage);
            var rstock = await _stockService.AddAsync(stock);
            return CreateActionResult(CustomResponseDto<Stock>.Success(201, rstock));
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _stockService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<Stock>>.Success(200, stocks.Where(x=>x.IsDeleted==false).ToList()));
        }

        



        private async Task<string> ImageResizeUpload(IFormFile uploadImage)
        {
            string imagePath = "defaultfood.png";
            if (uploadImage != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(uploadImage.FileName);
                string extensions = Path.GetExtension(uploadImage.FileName);
                string now = DateTime.Now.ToString("yymmssfff");
                imagePath = fileName + now + extensions;
                string path = Path.Combine(wwwRootPath + "/img/stock/", fileName + now + extensions);
                string thumbnailpath = Path.Combine(wwwRootPath + "/img/stock/", "thumbnail" + fileName + now + extensions);

                using (var image = Image.Load(uploadImage.OpenReadStream()))
                {
                    string newSize = ImageResize(image, 900, 900);
                    string[] sizeArray = newSize.Split(',');
                    image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                    await image.SaveAsync(path);

                    string tnewSize = ImageResize(image, 280, 280);
                    string[] tsizeArray = tnewSize.Split(',');
                    image.Mutate(x => x.Resize(Convert.ToInt32(tsizeArray[1]), Convert.ToInt32(tsizeArray[0])));
                    await image.SaveAsync(thumbnailpath);
                }
            }
            return imagePath;
        }

        private string ImageResize(Image img, int maxWidth, int maxHeight)
        {
            if (img.Width > maxWidth || img.Height > maxHeight)
            {
                double wratio = (double)img.Width / (double)maxWidth;
                double hratio = (double)img.Height / (double)maxHeight;
                double ratio = Math.Max(wratio, hratio);
                int newWidth = (int)(img.Width / ratio);
                int newHeigth = (int)(img.Height / ratio);
                return newHeigth.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }

    }
}
