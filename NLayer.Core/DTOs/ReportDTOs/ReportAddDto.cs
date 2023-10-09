using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ReportDTOs
{
    public class ReportAddDto
    {
        public string ReportType { get; set; }
        public string ReportText { get; set; }
        public string IsBusinessReport { get; set; }
        public int? BusinessId { get; set; }
        public int? BusinessCommentId { get; set; }
        public int UserId { get; set; }

    }
}
