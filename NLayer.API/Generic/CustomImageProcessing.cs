using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace NLayer.API.Generic
{
    public class CustomImageProcessing
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomImageProcessing(IWebHostEnvironment webHostEnvironment = null)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<string>> ImageProcessing(List<IFormFile> uploadImage, string folder)
        {
            List<string> imagesPath = new List<string>();
            if (uploadImage != null)
            {
                foreach (IFormFile file in uploadImage)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extensions = Path.GetExtension(file.FileName);
                    string now = DateTime.Now.ToString("yymmssfff");
                    imagesPath.Add(fileName + now + extensions);
                    string path = Path.Combine(wwwRootPath + "/img/" + folder + "/", fileName + now + extensions);
                    string thumbnailpath = Path.Combine(wwwRootPath + "/img/" + folder + "/", "thumbnail" + fileName + now + extensions);

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

        public async Task<List<string>> ImageProcessing(List<IFormFile> uploadImage, string folder, bool Delete, string DeletePath )
        {
            List<string> imagesPath = new List<string>();
            if (uploadImage.Count() > 0)
            {
                foreach (IFormFile file in uploadImage)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extensions = Path.GetExtension(file.FileName);
                    string now = DateTime.Now.ToString("yymmssfff");
                    imagesPath.Add(fileName + now + extensions);
                    string path = Path.Combine(wwwRootPath + "/img/" + folder + "/", fileName + now + extensions);
                    string thumbnailpath = Path.Combine(wwwRootPath + "/img/" + folder + "/", "thumbnail" + fileName + now + extensions);

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
            if(DeletePath != null && Delete && imagesPath.Count > 0)
            {
                var deletepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\user", DeletePath);
                var deletepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\user", "thumbnail" + DeletePath);
                if (System.IO.File.Exists(deletepath))
                {
                    System.IO.File.Delete(deletepath);
                    System.IO.File.Delete(deletepath2);
                }
            }
            
            return imagesPath;
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
