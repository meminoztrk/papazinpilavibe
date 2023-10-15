using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.AdminDTOs
{
    public class AdminDashboardDto
    {
        public int BusinessCount { get; set; }
        public int CommentCount { get; set; }
        public int UserCount { get; set; }
        public List<Chart> BusinessChart { get; set; }
        public List<Chart> CommentChart { get; set; }
        public List<Chart> UserChart { get; set; }
    }

    public class Chart
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
