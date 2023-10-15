using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.About;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.BusinessCommentDTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using NLayer.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class BusinessService : Service<Business>, IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IBusinessImageService _businessImageService;
        private readonly IUserRepository _userRepository;
        private readonly CustomImageProcessing _customImageProcessing;
        private readonly IMapper _mapper;
        public BusinessService(IGenericRepository<Business> repository, IUnitOfWork unitOfWork, IBusinessRepository businessRepository, IMapper mapper, IUserRepository userRepository, IBusinessImageService businessImageService, CustomImageProcessing customImageProcessing) : base(repository, unitOfWork)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _businessImageService = businessImageService;
            _customImageProcessing = customImageProcessing;
        }

        public async Task<CustomResponseDto<NoContentDto>> AddBusiness(BusinessAddDto business)
        {
            business.UserId = _userRepository.Where(x => x.UserId == business.GuidId).FirstOrDefault().Id;

            var mappedBusiness = _mapper.Map<Business>(business);

            if(business.UploadedLicense != null && business.UploadedLicense.Count() == 1)
            {
                var licenseImageUpload = await _customImageProcessing.ImageProcessing(business.UploadedLicense, "license");
                mappedBusiness.BusinessLicense = licenseImageUpload.FirstOrDefault();
            }
            else
            {
                mappedBusiness.BusinessLicense = "defaultlicense.png";
            }


            var addedBusiness = await AddAsync(mappedBusiness);

            if(business.UploadedImages != null && business.UploadedImages.Count() > 0)
            {
                var businessImageUpload = await _customImageProcessing.ImageProcessing(business.UploadedImages, "business");
                var selected = businessImageUpload.Select(x => new BusinessImage { Image = x, BusinessId = addedBusiness.Id }).ToList();
                await _businessImageService.AddRangeAsync(selected);
            }        

            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateBusiness(int id, BusinessUpdateDto business)
        {
            var getBusiness = _businessRepository.Where(x => x.Id == id).FirstOrDefault();
            await _customImageProcessing.BusinessUpdateImageProcessing(id, business.UploadedImages);
            var imageList = await _customImageProcessing.ImageProcessing(business.UploadedLicense, "license", getBusiness.BusinessLicense != "defaultlicense.png", getBusiness.BusinessLicense);
            business.BusinessLicense = imageList.Count() > 0 ? imageList.FirstOrDefault() : business.BusinessLicense;
            var updatedBusiness = _mapper.Map<BusinessUpdateDto, Business>(business, getBusiness);
                 

            await UpdateAsync(updatedBusiness);

            return CustomResponseDto<NoContentDto>.Success(200);
        }


        public async Task<CustomResponseDto<List<BusinessByUserDto>>> GetBusinessesByUserId(string userId)
        {
            int id = _userRepository.Where(x => x.UserId == userId).FirstOrDefault().Id;

            var businessByUser = await _businessRepository.GetBusinessesWithIncludeByUserId(id);

            return CustomResponseDto<List<BusinessByUserDto>>.Success(200, businessByUser);
        }

        public async Task<CustomResponseDto<BusinessDto>> GetBusinessById(int id)
        {
            var businessById = await _businessRepository.GetBusinessWithIncludeById(id);
            return CustomResponseDto<BusinessDto>.Success(200, businessById);
        }

        public async Task<CustomResponseDto<BusinessWithCommentDto>> GetBusinessesWithCommentById(int id)
        {
            var businessWithCommentById = await _businessRepository.GetBusinessesWithCommentById(id);
            return CustomResponseDto<BusinessWithCommentDto>.Success(200, businessWithCommentById);
        }

        public async Task<CustomResponseDto<BusinessCommentWithCountDto>> GetBusinessCommentsWithPaginationById(int id, int page, int take, bool isAsc, string commentType, int rate, string search)
        {
            var businessCommentsWithPagination = await _businessRepository.GetBusinessCommentsWithPaginationById(id,page,take, isAsc, commentType,rate, search);
            return CustomResponseDto<BusinessCommentWithCountDto>.Success(200, businessCommentsWithPagination);
        }

        public async Task<CustomResponseDto<BusinessWithCountBySearching>> GetBusinessWithCountBySearching(int page, int take, int provinceId, bool isMostReview, string search)
        {
            var businessWithCountBySearching = await _businessRepository.GetBusinessWithCountBySearching(page, take, provinceId, isMostReview, search);
            return CustomResponseDto<BusinessWithCountBySearching>.Success(200, businessWithCountBySearching);
        }

        public async Task<CustomResponseDto<AdminBusinessWithCountDto>> GetBusinessesWithUser(FilterPaginationDto paginationFilter)
        {
            return CustomResponseDto<AdminBusinessWithCountDto>.Success(200, await _businessRepository.GetBusinessesWithUser(paginationFilter));
        }
    }
}
