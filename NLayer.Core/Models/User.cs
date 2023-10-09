using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string UserPhoto { get; set; } = "defaultuser.png";
        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBusiness { get; set; }
        public bool IsUser { get; set; }
        public bool IsGoogle { get; set; }
        public string GoogleCredential { get; set; }
        public string UserId { get; set; } = Guid.NewGuid().ToString("N");
        [JsonIgnore] public string Password { get; set; }
        public ICollection<Business> Businesses { get; set; }
        public ICollection<BusinessUserImage> BusinessUserImages { get; set; }
        public ICollection<BusinessComment> BusinessComments { get; set; }
    }
}
