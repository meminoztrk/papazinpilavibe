using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.AdminDTOs
{
    public class AdminBusinessCommentDto
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string FullName { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public string CommentType { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
