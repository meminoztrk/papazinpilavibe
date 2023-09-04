using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.UserDTOs
{
    public class UserGoogleLoginDto
    {
        public string Email { get; set; }
        public string GoogleCredential { get; set; }
    }
}
