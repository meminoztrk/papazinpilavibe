using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Business:BaseEntity
    {
        public string BusinessName { get; set; }
        public string MinHeader { get; set; }
        public string About { get; set; }
        public string FoodTypes { get; set; }
        public string BusinessProps { get; set; }
        public string BusinessServices { get; set; }
        public string Adress { get; set; }
        public string Process { get; set; }
        public string Phone { get; set; }
        public string MapIframe { get; set; }
        public string BusinessLicense { get; set; }
        public string CommentTypes { get; set; }
        public string BusinessType { get; set; }
        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public bool HasCourier { get; set; }
        public string Website { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Mo { get; set; }
        public string Tu { get; set; }
        public string We { get; set; }
        public string Th { get; set; }
        public string Fr { get; set; }
        public string Sa { get; set; }
        public string Su { get; set; }
        public int Views { get; set; } = 0;
        public ICollection<BusinessImage> BusinessImages { get; set; }
        public ICollection<BusinessComment> BusinessComments { get; set; }
        public ICollection<FavoriteBusiness> FavoriteBusinesses { get; set; }
    }
}
