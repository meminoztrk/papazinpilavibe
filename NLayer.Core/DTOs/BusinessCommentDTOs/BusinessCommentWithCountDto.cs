using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessCommentDTOs
{
    public class BusinessCommentWithCountDto
    {
        public int CommentCount { get; set; }
        public List<BusinessCommentDto> BusinessComments { get; set; }
    }
}
