using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class BusinessComment:BaseEntity
    {
        public int? BusinessId { get; set; }
        public Business Business { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public string CommentType { get; set; }
        public ICollection<BusinessUserImage> BusinessUserImages { get; set; }
        public ICollection<BusinessSubComment> BusinessSubComments { get; set; }
    }
}
