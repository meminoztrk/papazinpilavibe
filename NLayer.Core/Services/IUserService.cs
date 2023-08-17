﻿using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IUserService:IService<User>
    {
        bool UniqueEmail(string text);
        bool UniqueUsername(string text);
        User GetByUsername(string username);
    }
}
