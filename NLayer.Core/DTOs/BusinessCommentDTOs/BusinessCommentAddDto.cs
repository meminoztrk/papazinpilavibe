using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessCommentDTOs
{
    public class BusinessCommentAddDto
    {
        public int UserId { get; set; }
        public string GuidId { get; set; }
        public int BusinessId { get; set; }
        public string Comment { get; set; }
        public string CommentType { get; set; }
        public double Rate { get; set; }
        public List<IFormFile> UploadedImages { get; set; }
    }
}
