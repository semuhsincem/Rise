using Rise.Entity.Concrete;
using Rise.ViewModels;
using Rise.ViewModels.ServiceResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.BusinessLayer.Abstract
{
    public interface IReportService
    {
        Task<ServiceResult<ReportViewModel>> ReceiveReportByLocation(string location,string email);
        Task<ServiceResult<List<Report>>> GetAllReports();
        Task<ServiceResult<Report>> GetReportDetails(string id);
        Task<ServiceResult<bool>> ChangeReportStatus(string id);

    }
}
