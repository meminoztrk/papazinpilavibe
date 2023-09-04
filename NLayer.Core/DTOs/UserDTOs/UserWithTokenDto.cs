using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.UserDTOs
{
    public class UserWithTokenDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserPhoto { get; set; }
        public int? ProvinceId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBusiness { get; set; }
        public bool IsUser { get; set; }
        public bool IsGoogle { get; set; }
        public string Token { get; set; }
    }
}
