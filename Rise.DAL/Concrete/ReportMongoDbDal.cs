using Microsoft.Extensions.Options;
using Rise.Core.Mongo;
using Rise.Entity.Concrete;
using Rise.Helper;

namespace Rise.DAL.Concrete
{
    public class ReportMongoDbDal : MongoDbRepositoryBase<Report>, IReportDal
    {
        public ReportMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
