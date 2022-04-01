using Rise.BusinessLayer.Abstract;
using Rise.DAL;
using Rise.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.BusinessLayer.Concrete
{
    public class ReportManager : IReportService
    {
        private readonly IReportDal _reportDal;
        public ReportManager(IReportDal reportDal)
        {
            _reportDal = reportDal;
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _reportDal.GetAll(null);
        }

        public async Task<Report> GetReportDetails(string id)
        {
            return await _reportDal.GetAsync(x => x.Id == id);
        }

        public async Task<Report> ReceiveReportByLocation(string location)
        {
            return await _reportDal.GetAsync(x => x.Location == location);
        }
    }
}
