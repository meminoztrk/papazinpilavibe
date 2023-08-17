using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Stock : BaseEntity
    {
        public string Brand { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Critical { get; set; }
        public string Unit { get; set; }
        public string Explain { get; set; }
        public string Image { get; set; }
    }
}
