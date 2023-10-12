using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.UserDTOs
{
    public class UserResetPasswordDto
    {
        public string Password { get; set; }
        public string UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
