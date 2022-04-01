using Microsoft.AspNetCore.Mvc;

namespace Rise.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet]
        public string GetDb()
        {
            return "";
        }
    }
}
