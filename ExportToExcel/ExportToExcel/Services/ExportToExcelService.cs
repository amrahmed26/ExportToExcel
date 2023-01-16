using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Aelgak_WebApp.Services
{
    public class ExportToExcelService<T> where T : class
    {
        static ExportToExcelService<T> exportToExcelService;
        private ExportToExcelService()
        {

        }
        public async static Task<ExportToExcelService<T>> GetInstance()
        {
            if (exportToExcelService == null)
                return exportToExcelService= new ();
            else 
                return exportToExcelService;    
        }
        public async Task<byte[]> ExporttoExcel<T>(List<T> table, string filename)
        {
           ExcelPackage package = new ExcelPackage();
           ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(filename);
            worksheet.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return package.GetAsByteArray();
        }
    }
}
