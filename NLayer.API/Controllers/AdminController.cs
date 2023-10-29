using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AdminDTOs;
using NLayer.Core.DTOs.FilterPaginationDTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;
using System.Linq;
using System.Reflection;

namespace NLayer.API.Controllers
{
    public class AdminController : CustomBaseController
    {
        private readonly IBusinessService businessService;
        private readonly IUserService userService;
        private readonly IBusinessCommentService businessCommentService;

        public AdminController(IBusinessService businessService, IUserService userService, IBusinessCommentService businessCommentService)
        {
            this.businessService = businessService;
            this.userService = userService;
            this.businessCommentService = businessCommentService;
        }

        [HttpGet("[action]")]
        public IActionResult GetDashboard()
        {
            AdminDashboardDto adminDashboard = new AdminDashboardDto();

            var businesses = businessService.Where(x=>!x.IsDeleted).ToList();
            List<Chart> businessChart = new List<Chart>();
            for (int i = 4; i >= 0; i--)
            {
                Chart chart = new Chart();
                if (i == 0)
                {
                    chart.Name = "Bugün";
                    chart.Value = businesses.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));
                }
                else if (i == 1)
                {
                    chart.Name = "Dün";
                    chart.Value = businesses.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
                }
                else
                {
                    chart.Name = $"{i} Gün Önce";
                    chart.Value = businesses.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.AddDays(-1*i).ToString("dd.MM.yyyy"));
                }
                businessChart.Add(chart);
            }
            adminDashboard.BusinessCount = businesses.Count();
            adminDashboard.BusinessChart = businessChart;


            var comments = businessCommentService.Where(x => !x.IsDeleted).ToList();
            List<Chart> commentsChart = new List<Chart>();
            for (int i = 4; i >= 0; i--)
            {
                Chart chart = new Chart();
                if (i == 0)
                {
                    chart.Name = "Bugün";
                    chart.Value = comments.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));
                }
                else if (i == 1)
                {
                    chart.Name = "Dün";
                    chart.Value = comments.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
                }
                else
                {
                    chart.Name = $"{i} Gün Önce";
                    chart.Value = comments.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.AddDays(-1 * i).ToString("dd.MM.yyyy"));
                }
                commentsChart.Add(chart);
            }
            adminDashboard.CommentCount = comments.Count();
            adminDashboard.CommentChart = commentsChart;


            var users = userService.Where(x => !x.IsDeleted).ToList();
            List<Chart> userChart = new List<Chart>();
            for (int i = 4; i >= 0; i--)
            {
                Chart chart = new Chart();
                if (i == 0)
                {
                    chart.Name = "Bugün";
                    chart.Value = users.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));
                }
                else if (i == 1)
                {
                    chart.Name = "Dün";
                    chart.Value = users.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
                }
                else
                {
                    chart.Name = $"{i} Gün Önce";
                    chart.Value = users.Count(x => x.CreatedDate.ToString("dd.MM.yyyy") == DateTime.Now.AddDays(-1 * i).ToString("dd.MM.yyyy"));
                }
                userChart.Add(chart);
            }
            adminDashboard.UserCount = users.Count();
            adminDashboard.UserChart = userChart;


            return CreateActionResult(CustomResponseDto<AdminDashboardDto>.Success(200, adminDashboard));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBusinessesWithUser([FromBody]FilterPaginationDto paginationFilter)
        {       
            return CreateActionResult(await businessService.GetBusinessesWithUser(paginationFilter));
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> UpdateBusinessPatch(int id, JsonPatchDocument entity)
        {
            await businessService.UpdatePatchAsync(id, entity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetReviews([FromBody] FilterPaginationDto paginationFilter)
        {
            return CreateActionResult(await businessCommentService.GetCommentsWithUser(paginationFilter));
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> UpdateReviewsPatch(int id, JsonPatchDocument entity)
        {
            await businessCommentService.UpdatePatchAsync(id, entity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetUsers([FromBody] FilterPaginationDto paginationFilter)
        {
            return CreateActionResult(await userService.GetUsers(paginationFilter));
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> UpdateUserPatch(int id, JsonPatchDocument entity)
        {
            await userService.UpdatePatchAsync(id, entity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
