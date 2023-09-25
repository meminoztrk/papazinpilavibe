using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessSubCommentDTOs
{
    public class BusinessSubCommentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
    }
}
