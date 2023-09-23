using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessDTOs
{
    public class BusinessByUserDto
    {
        public int Id { get; set; }
        public string BusinessImage { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string Location { get; set; }
        public string Process { get; set; }
        public int Views { get; set; }
        public bool IsActive { get; set; }
    }
}
