﻿using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public async Task<CustomResponseDto<UserProfileDto>> GetUserProfileByUserId(string userid)
        {
            var user = await _userRepository.GetUserProfileByUserId(userid);
            return CustomResponseDto<UserProfileDto>.Success(200, user);
        }

        public bool UniqueEmail(string text)
        {
            bool s = _userRepository.UniqueUsername(text);
            return s;
        }
        public bool UniqueUsername(string text)
        {
            bool s = _userRepository.UniqueUsername(text);
            return s;
        }
    }
}
