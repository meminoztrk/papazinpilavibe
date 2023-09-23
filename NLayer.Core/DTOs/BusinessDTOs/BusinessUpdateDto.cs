using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessDTOs
{
    public class BusinessUpdateDto
    {
        public string BusinessName { get; set; }
        public string MinHeader { get; set; }
        public string About { get; set; }
        public string FoodTypes { get; set; }
        public string BusinessProps { get; set; }
        public string BusinessServices { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string MapIframe { get; set; }
        public string BusinessLicense { get; set; }
        public string CommentTypes { get; set; }
        public string BusinessType { get; set; }
        public int ProvinceId { get; set; }
        public string GuidId { get; set; }
        public string Website { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Mo { get; set; }
        public string Tu { get; set; }
        public string We { get; set; }
        public string Th { get; set; }
        public string Fr { get; set; }
        public string Sa { get; set; }
        public string Su { get; set; }
        public List<IFormFile> UploadedImages { get; set; }
        public List<IFormFile> UploadedLicense { get; set; }
    }
}
