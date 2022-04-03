namespace Rise.ViewModels
{
    public class ExcelReportViewModel
    {
        public int PersonCount { get; set; }
        public string Location { get; set; }
        public int PhoneCount { get; set; }
        public ExcelReportViewModel(string location)
        {
            Location = location;
        }
        public ExcelReportViewModel()
        {

        }
    }
}
