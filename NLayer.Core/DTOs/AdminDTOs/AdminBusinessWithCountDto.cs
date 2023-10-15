using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.AdminDTOs
{
    public class AdminBusinessWithCountDto
    {
        public List<AdminBusinessDto> AdminBusiness { get; set; }
        public int BusinessCount { get; set; }
    }
}
