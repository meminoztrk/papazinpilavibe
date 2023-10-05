using NLayer.Core.DTOs.BusinessSubCommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessCommentDTOs
{
    public class BusinessCommentByUserDto
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string BusinessImage { get; set; }
        public string BusinessName { get; set; }
        public string Location { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public string CommentType { get; set; }
        public DateTime Created { get; set; }
        public List<string> Images { get; set; }
        public List<BusinessSubCommentDto> SubComments { get; set; }
    }
}
