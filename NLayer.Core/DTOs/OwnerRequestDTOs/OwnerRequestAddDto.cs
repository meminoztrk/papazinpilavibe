using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.OwnerRequestDTOs
{
    public class OwnerRequestAddDto
    {
        public int BusinessId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public string License { get; set; }
        public string Process { get; set; }
    }
}
