using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class FavoriteComment:BaseEntity
    {
        public int BusinessCommentId { get; set; }
        public BusinessComment BusinessComment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
