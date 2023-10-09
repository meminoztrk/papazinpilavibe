using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BusinessDTOs;
using NLayer.Core.DTOs.ReportDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Helper;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    public class ReportController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(ReportAddDto report)
        {
            var mappedReport = _mapper.Map<Report>(report);
            await _reportService.AddAsync(mappedReport);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
