using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessDTOs
{
    public class BusinessFavoriteDto
    {
        public int Id { get; set; }
        public string BusinessImage { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public int TotalComment { get; set; }
        public double Rate { get; set; }
        public string Location { get; set; }
        public string FoodTypes { get; set; }
        public string BusinessProps { get; set; }
        public string BusinessServices { get; set; }
        public DateTime Created { get; set; }

        public List<int> LikedUsers { get; set; }

    }
}
