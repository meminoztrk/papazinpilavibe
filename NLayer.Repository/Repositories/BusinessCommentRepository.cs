﻿using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class BusinessCommentRepository : GenericRepository<BusinessComment>, IBusinessCommentRepository
    {
        public BusinessCommentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
