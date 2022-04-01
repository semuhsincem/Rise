using System.Collections.Generic;

namespace Rise.BusinessLayer.Abstract
{
    public interface IReportService
    {
        void ReceiveReportByLocation(string location);
        List<string> GetAllReports();
        object GetReportDetails();

    }
}
