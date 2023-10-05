using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessDTOs
{
    public class BusinessBySearching
    {
        public int Id { get; set; }
        public string BusinessImage { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public int TotalComment { get; set; }
        public int SearchCommentCount { get; set; }
        public string SearchComment { get; set; }
        public double Rate { get; set; }
        public string Location { get; set; }
        public string FoodTypes { get; set; }
        public string BusinessProps { get; set; }
        public string BusinessServices { get; set; }

    }
}
