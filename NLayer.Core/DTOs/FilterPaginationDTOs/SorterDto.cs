using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.FilterPaginationDTOs
{
    public class SorterDto
    {
        public string Field { get; set; }
        public string Order { get; set; } = "descend";
    }
}
