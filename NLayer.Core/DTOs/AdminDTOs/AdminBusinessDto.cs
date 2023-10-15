using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.AdminDTOs
{
    public class AdminBusinessDto
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Process { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
