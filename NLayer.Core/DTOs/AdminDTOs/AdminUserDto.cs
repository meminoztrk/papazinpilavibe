﻿using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.AdminDTOs
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        public string GuidId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string UserPhoto { get; set; }
        public string Location { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBusiness { get; set; }
        public bool IsUser { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
