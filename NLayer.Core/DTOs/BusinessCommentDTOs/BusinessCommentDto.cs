﻿using NLayer.Core.DTOs.BusinessSubCommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessCommentDTOs
{
    public class BusinessCommentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserImage { get; set; }
        public int TotalComment { get; set; }
        public int BusinessId { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public string CommentType { get; set; }
        public DateTime Created { get; set; }
        public List<string> Images { get; set; }
        public List<int> LikedUsers { get; set; }
        public List<BusinessSubCommentDto> SubComments { get; set; }

    }
}
