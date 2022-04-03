using Microsoft.AspNetCore.Mvc;
using Rise.BusinessLayer.Abstract;
using Rise.Entity.Concrete;
using Rise.ViewModels;
using Rise.ViewModels.ServiceResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.ReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("/RequestReportByLocation/{email_address}/{location}")]
        public async Task<ServiceResult<ReportViewModel>> ReceiveReportByLocation(string email_address, string location)
        {
            return await _reportService.ReceiveReportByLocation(location, email_address);
        }
        [HttpGet]
        [Route("ChangeReportStatus/{reportId}")]
        public async Task<ServiceResult<bool>> ChangeReportStatus(string reportId)
        {
            return await _reportService.ChangeReportStatus(reportId);
        }
        [HttpGet]
        [Route("/GetAllReports")]
        public async Task<ServiceResult<List<Report>>> GetAllReports()
        {
            return await _reportService.GetAllReports();
        }
    }
}
