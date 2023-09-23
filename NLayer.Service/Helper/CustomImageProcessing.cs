using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NLayer.Core.Models;
using NLayer.Core.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Helper
{
    public class CustomImageProcessing
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBusinessImageService _businessImageService;
        public CustomImageProcessing(IWebHostEnvironment webHostEnvironment, IBusinessImageService businessImageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _businessImageService = businessImageService;
        }

        public async Task<List<string>> ImageProcessing(List<IFormFile> uploadImage, string folder)
        {
            List<string> imagesPath = new List<string>();
            if (uploadImage != null)
            {
                foreach (IFormFile file in uploadImage)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string extensions = Path.GetExtension(file.FileName);
                    string now = DateTime.Now.ToString("yymmssfff");
                    imagesPath.Add(folder + now + extensions);
                    string path = Path.Combine(wwwRootPath + "/img/" + folder + "/", folder + now + extensions);
                    string thumbnailpath = Path.Combine(wwwRootPath + "/img/" + folder + "/", "thumbnail" + folder + now + extensions);

                    using (var image = Image.Load(file.OpenReadStream()))
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

            }
            return imagesPath;
        }

        public async Task<List<string>> ImageProcessing(List<IFormFile> uploadImage, string folder, bool Delete, string DeletePath)
        {
            List<string> imagesPath = new List<string>();
            if (uploadImage.Count() > 0)
            {
                foreach (IFormFile file in uploadImage)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string extensions = Path.GetExtension(file.FileName);
                    string now = DateTime.Now.ToString("yymmssfff");
                    imagesPath.Add(folder + now + extensions);
                    string path = Path.Combine(wwwRootPath + "/img/" + folder + "/", folder + now + extensions);
                    string thumbnailpath = Path.Combine(wwwRootPath + "/img/" + folder + "/", "thumbnail" + folder + now + extensions);

                    using (var image = Image.Load(file.OpenReadStream()))
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

            }
            if (DeletePath != null && Delete)
            {
                var deletepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\"+folder, DeletePath);
                var deletepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\"+folder, "thumbnail" + DeletePath);
                if (System.IO.File.Exists(deletepath))
                {
                    System.IO.File.Delete(deletepath);
                    System.IO.File.Delete(deletepath2);
                }
            }

            return imagesPath;
        }

        public async Task BusinessUpdateImageProcessing(int businessId,List<IFormFile> uploadImage)
        {
            List<string> imagesPath = new List<string>();
            if (uploadImage != null && uploadImage.Count() > 0)
            {
                foreach (IFormFile file in uploadImage)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string extensions = Path.GetExtension(file.FileName);
                    string now = DateTime.Now.ToString("yymmssfff");
                    imagesPath.Add("business" + now + extensions);
                    string path = Path.Combine(wwwRootPath + "/img/business/", "business" + now + extensions);
                    string thumbnailpath = Path.Combine(wwwRootPath + "/img/business/", "thumbnail" + "business" + now + extensions);

                    using (var image = Image.Load(file.OpenReadStream()))
                    {
                        string newSize = ImageResize(image, 800, 800);
                        string[] sizeArray = newSize.Split(',');
                        image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                        await image.SaveAsync(path);

                        string tnewSize = ImageResize(image, 280, 280);
                        string[] tsizeArray = tnewSize.Split(',');
                        image.Mutate(x => x.Resize(Convert.ToInt32(tsizeArray[1]), Convert.ToInt32(tsizeArray[0])));
                        await image.SaveAsync(thumbnailpath);
                    }
                }

            }          

            var businessImages = _businessImageService.Where(x=>x.BusinessId == businessId).ToList();
            await _businessImageService.RemoveRangeAsync(businessImages);
            foreach(var businessImage in businessImages)
            {
                var deletepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\business", businessImage.Image);
                var deletepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\business", "thumbnail" + businessImage.Image);
                if (System.IO.File.Exists(deletepath))
                {
                    System.IO.File.Delete(deletepath);
                    System.IO.File.Delete(deletepath2);
                }
            }

            var selected = imagesPath.Select(x => new BusinessImage { Image = x, BusinessId = businessId }).ToList();
            await _businessImageService.AddRangeAsync(selected);
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
