using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.AdminDTOs
{
    public class AdminBaseDto<T> where T : class
    {
        public int ItemCount { get; set; }
        public List<T> Items { get; set; }
    }
}
