using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class BusinessFAQ:BaseEntity
    {
        public int? BusinessId { get; set; }
        public Business Business { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
