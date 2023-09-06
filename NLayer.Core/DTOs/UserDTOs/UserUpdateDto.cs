using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.UserDTOs
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ProvinceId { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
