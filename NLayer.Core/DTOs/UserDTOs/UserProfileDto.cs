using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.UserDTOs
{
    public class UserProfileDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserPhoto { get; set; }
        public string Location { get; set; }
        public int TotalComment { get; set; }
        public int TotalImage { get; set; }
        public DateTime Created { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBusiness { get; set; }
        public bool IsUser { get; set; }
        public bool IsGoogle { get; set; }
    }
}
