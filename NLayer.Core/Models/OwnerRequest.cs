using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class OwnerRequest:BaseEntity
    {
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string License { get; set; }
        public string Process { get; set; }
    }
}
