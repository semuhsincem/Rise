using Rise.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.BusinessLayer.Abstract
{
    public interface IReportService
    {
        Task<Report> ReceiveReportByLocation(string location);
        Task<List<Report>> GetAllReports();
        Task<Report> GetReportDetails(string id);

    }
}
