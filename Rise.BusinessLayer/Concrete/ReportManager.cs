using Rise.BusinessLayer.Abstract;
using Rise.DAL;
using Rise.Entity.Concrete;
using Rise.Helper.EnumHelper;
using Rise.Helper.ErrorMessages;
using Rise.ViewModels.ServiceResults;
using System;
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

        public async Task<ServiceResult<bool>> ChangeReportStatus(string id)
        {
            try
            {
                var res = await _reportDal.GetAsync(x => x.Id == id);
                if (res == null)
                    return new ServiceResult<bool>(false, ServiceMessages.ReportWasNotFound, false);
                else
                {
                    res.eReportType = EReportType.Completed;
                    await _reportDal.UpdateAsync(id, res);
                    return new ServiceResult<bool>(true, ServiceMessages.Success);
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<bool>(false, ServiceMessages.AnErrorOccured, false);
            }
        }

        public async Task<ServiceResult<List<Report>>> GetAllReports()
        {
            try
            {
                var data = await _reportDal.GetAll(null);
                return new ServiceResult<List<Report>>(data, ServiceMessages.Success);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<List<Report>>(null, ServiceMessages.AnErrorOccured, false);
            }
        }

        public async Task<ServiceResult<Report>> GetReportDetails(string id)
        {
            try
            {
                var data = await _reportDal.GetAsync(x => x.Id == id);
                return new ServiceResult<Report>(data, ServiceMessages.Success);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<Report>(null, ServiceMessages.AnErrorOccured, false);
            }
        }

        public async Task<ServiceResult<Report>> ReceiveReportByLocation(string location)
        {
            try
            {
                var data = await _reportDal.GetAsync(x => x.Location == location);
                return new ServiceResult<Report>(data, ServiceMessages.Success);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<Report>(null, ServiceMessages.AnErrorOccured, false);
            } 
        }
    }
}
