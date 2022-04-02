using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.BusinessLayer.Abstract;
using Rise.Entity.Concrete;
using Rise.ViewModels.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ServiceResult<Report>> ReceiveReportByLocation(string email_address, string location)
        {
            return new ServiceResult<Report>(null,"s");
        }

        [HttpGet]
        [Route("/GetAllReports")]
        public async Task<ServiceResult<List<Report>>> GetAllReports()
        {
            return await _reportService.GetAllReports();
        }

        [HttpGet]
        [Route("ChangeReportStatus/{reportId}")]
        public async Task<ServiceResult<Report>> ChangeReportStatus(string reportId)
        {
            return new ServiceResult<Report>(null,string.Empty);
        } 


    }
}
