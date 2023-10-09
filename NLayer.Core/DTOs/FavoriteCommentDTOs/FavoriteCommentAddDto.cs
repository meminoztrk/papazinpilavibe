using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.FavoriteCommentDTOs
{
    public class FavoriteCommentAddDto
    {
        public int BusinessCommentId { get; set; }
        public int UserId { get; set; }
    }
}
