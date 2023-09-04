using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string SehirIlceMahalleAdi { get;set; }
        public int UstID { get; set; }
        public string minlongitude { get; set; }
        public string minlatitude { get; set; }
        public string maxlongitude { get; set; }
        public string maxlatitude { get; set; }
        public string MahalleID { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
