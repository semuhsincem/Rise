using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.BusinessLayer.Abstract;
using Rise.Entity.Concrete;
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
        public async Task<Report> ReceiveReportByLocation(string email_address, string location)
        {
            return "saf";
        }

        [HttpGet]
        [Route("/GetAllReports")]
        public async Task<List<Report>> GetAllReports()
        {
            return await _reportService.GetAllReports();
        }

        [HttpGet]
        [Route("ChangeReportStatus/{reportId}")]
        public async Task<Report> ChangeReportStatus(string reportId)
        {
            return string.Empty;
        } 


    }
}
