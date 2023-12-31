﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class BusinessCommentService : Service<BusinessComment>, IBusinessCommentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBusinessCommentRepository _businessCommentRepository;
        private readonly IBusinessUserImageService _businessUserImageService;
        private readonly IMapper _mapper;
        private readonly CustomImageProcessing _customImageProcessing;

        public BusinessCommentService(IGenericRepository<BusinessComment> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IBusinessUserImageService businessUserImageService, IMapper mapper, CustomImageProcessing customImageProcessing, IBusinessCommentRepository businessCommentRepository) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _customImageProcessing = customImageProcessing;
            _businessUserImageService = businessUserImageService;
            _businessCommentRepository = businessCommentRepository;
        }

        public async Task<CustomResponseDto<NoContentDto>> AddComment(BusinessCommentAddDto businessComment)
        {
            businessComment.UserId = _userRepository.Where(x => x.UserId == businessComment.GuidId).FirstOrDefault().Id;

            if(await _businessCommentRepository.AnyAsync(x=>x.BusinessId == businessComment.BusinessId && x.UserId == businessComment.UserId))
            {
                return CustomResponseDto<NoContentDto>.Fail(200,"Daha önce bu işletmeyi zaten yorumladınız!");
            }

            var mappedBusinessComment = _mapper.Map<BusinessComment>(businessComment);

            var addedBusinessComment = await AddAsync(mappedBusinessComment);

            if (businessComment.UploadedImages != null && businessComment.UploadedImages.Count() > 0)
            {
                var businessImageUpload = await _customImageProcessing.ImageProcessing(businessComment.UploadedImages, "business");
                var selected = businessImageUpload.Select(x => new BusinessUserImage { Image = x, UserId = addedBusinessComment.UserId, BusinessId = addedBusinessComment.BusinessId, BusinessCommentId = addedBusinessComment.Id }).ToList();
                await _businessUserImageService.AddRangeAsync(selected);
            }

            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<CustomResponseDto<AdminBaseDto<AdminBusinessCommentDto>>> GetCommentsWithUser(FilterPaginationDto filterPagination)
        {
            var busienssComment = await _businessCommentRepository.GetCommentsWithUser(filterPagination);
            return CustomResponseDto<AdminBaseDto<AdminBusinessCommentDto>>.Success(200, busienssComment);
        }

        public async Task<CustomResponseDto<BusinessCommentByUserDto>> GetUserCommentById(int id)
        {
            var userComment = await _businessCommentRepository.GetUserCommentById(id);
            return CustomResponseDto<BusinessCommentByUserDto>.Success(200, userComment);
        }

        public async Task<CustomResponseDto<List<BusinessCommentByUserDto>>> GetUserComments(string userid, int page)
        {
            var userComment = await _businessCommentRepository.GetUserComments(userid, page);
            return CustomResponseDto<List<BusinessCommentByUserDto>>.Success(200, userComment);
        }
    }
}
