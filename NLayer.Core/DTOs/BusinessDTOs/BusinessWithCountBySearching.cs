using NLayer.Core.DTOs.BusinessCommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessDTOs
{
    public class BusinessWithCountBySearching
    {
        public int BusinessCount { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public List<BusinessBySearching> BusinessesBySearching { get; set; }
    }
}
