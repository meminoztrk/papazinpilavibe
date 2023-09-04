using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.UserDTOs
{
    public class UserGoogleRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsGoogle => true;
        public bool IsUser => true;
        public string GoogleCredential { get; set; }
    }
}
