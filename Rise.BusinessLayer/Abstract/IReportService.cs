using Rise.Entity.Concrete;
using Rise.ViewModels.ServiceResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.BusinessLayer.Abstract
{
    public interface IReportService
    {
        Task<ServiceResult<Report>> ReceiveReportByLocation(string location);
        Task<ServiceResult<List<Report>>> GetAllReports();
        Task<ServiceResult<Report>> GetReportDetails(string id);
        Task<ServiceResult<bool>> ChangeReportStatus(string id);

    }
}
