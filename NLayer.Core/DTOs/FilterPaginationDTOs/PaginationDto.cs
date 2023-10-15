using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.FilterPaginationDTOs
{
    public class PaginationDto
    {
        public int Current { get; set; } = 1;
        public int PageSize { get; set; } = 1;
    }
}
