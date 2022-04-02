using Newtonsoft.Json;
using RabbitMQ.Client;
using Rise.BusinessLayer.Abstract;
using Rise.DAL;
using Rise.Entity.Concrete;
using Rise.Helper.EnumHelper;
using Rise.Helper.ErrorMessages;
using Rise.ViewModels.ServiceResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
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
        /// <summary>
        /// Kullanıcı bir rapor talep ettiğinde, sistem 
        /// arkaplanda bu çalışmayı darboğaz yaratmadan sıralı bir biçimde ele alacak; rapor
        /// tamamlandığında ise kullanıcının "raporların listelendiği" endpoint üzerinden raporun
        /// durumunu "tamamlandı" olarak gözlemleyebilmesi gerekmektedir.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<ServiceResult<Report>> ReceiveReportByLocation(string location)
        {
            try
            {
                #region Added New Db Documents
                var data = await _reportDal.AddAsync(new Report()
                {
                    eReportType = EReportType.Reading,
                    Location = location,
                    RequestDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                });
                #endregion
                #region Publish To Queue
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "MckReport", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    string message = JsonConvert.SerializeObject(data);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "MckReport", basicProperties: null, body: body);
                }
                #endregion
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
