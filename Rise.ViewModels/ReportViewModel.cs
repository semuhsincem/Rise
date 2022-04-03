using Rise.Helper.EnumHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rise.ViewModels
{
    public class ReportViewModel
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public DateTime RequestDate { get; set; }
        public EReportType eReportType { get; set; }
        public string EmailAddress { get; set; }

    }
}
