using Rise.Core.Mongo;
using Rise.Helper.EnumHelper;
using System;

namespace Rise.Entity.Concrete
{
    public class Report : MongoDbEntity
    {
        public string Location { get; set; }
        public DateTime RequestDate { get; set; }
        public EReportType eReportType { get; set; }
    }
}
