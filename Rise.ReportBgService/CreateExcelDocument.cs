using ClosedXML.Excel;
using Newtonsoft.Json;
using RestSharp;
using Rise.ViewModels;
using Rise.ViewModels.ServiceResults;
using System;

namespace Rise.Helper
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
    public static class CreateExcelDocument
    {
        public static string CreateEx(string location)
        {

            IXLWorkbook wb = new XLWorkbook();
            IXLWorksheet ws = wb.Worksheets.Add("Reports");

            ws.Cell(1, 1).Value = "Konum Bilgisi";
            ws.Cell(1, 2).Value = "Konumda yer alan rehbere kayıtlı kişi sayısı";
            ws.Cell(1, 3).Value = "Konumda yer alan rehbere kayıtlı telefon numarası sayısı";
            var client = new RestClient("https://localhost:44366/api");
            var request = new RestRequest($"Person/GetPersonCountWithLocation/{location}");
            var response = client.GetAsync(request).Result;
            if (!string.IsNullOrEmpty(response.Content))
            {
                var excelData = JsonConvert.DeserializeObject<ServiceResult<ExcelReportViewModel>>(response.Content);

                ws.Cell(2, 1).Value = excelData.Data?.Location;
                ws.Cell(2, 2).Value = excelData.Data?.PersonCount;
                ws.Cell(2, 3).Value = excelData.Data?.PhoneCount;
            }

            var excelName = Guid.NewGuid();
            string file = $"Temp\\{excelName}.xlsx";
            wb.SaveAs(file);
            return file;
        }
    }
}
