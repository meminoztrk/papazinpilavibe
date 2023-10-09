using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Report:BaseEntity
    {
        public string ReportType { get; set; }
        public string ReportText { get; set; }
        public string IsBusinessReport { get; set; }
        public int? BusinessId { get; set; }
        public Business Business { get; set; }
        public int? BusinessCommentId { get; set; }
        public BusinessComment BusinessComment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
