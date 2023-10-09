using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ReportDTOs;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Core.DTOs.OwnerRequestDTOs;

namespace NLayer.API.Controllers
{
    public class OwnerRequestController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRequestService _ownerRequestService;
        public OwnerRequestController(IOwnerRequestService ownerRequestService, IMapper mapper)
        {
            _ownerRequestService = ownerRequestService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddOwnerRequest(OwnerRequestAddDto ownerRequest)
        {
            var mappedOwnerRequest = _mapper.Map<OwnerRequest>(ownerRequest);
            await _ownerRequestService.AddAsync(mappedOwnerRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
