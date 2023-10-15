using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.FilterPaginationDTOs
{
    public class FilterDto
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
