using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class BusinessImage:BaseEntity
    {
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public string Image { get; set; }
    }
}
