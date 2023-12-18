using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis.Repositorys;
using pis_web_api.Services;
using pis_web_api.Models.db;
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml.Style;
using System.Drawing;
namespace pis_web_api.Services
{
    public class ReportConverterToExcel
    {
        public ReportConverterToExcel() { }

        public byte[] ConvertToExcel(Report report)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Vaccination Statistics");
                int row = 1;
                decimal globalTotal = 0;
                foreach (var statisticaHolder in report.StatisticaHolders)
                {
                    decimal total = 0;
                    worksheet.Cells[row, 1].Value = statisticaHolder.LocalityName;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.Font.Size += 6;
                    row++;
                    worksheet.Cells[row, 1].Value = "Вакцина";
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 2].Value = "Цена";
                    worksheet.Cells[row, 2].Style.Font.Bold = true;
                    row++;
                    foreach (var statisticaItem in statisticaHolder)
                    {
                        worksheet.Cells[row, 1].Value = statisticaItem.VaccineName;
                        worksheet.Cells[row, 2].Value = statisticaItem.Price;
                        total += statisticaItem.Price;
                        row++;
                    }
                    globalTotal += total;
                    worksheet.Cells[row, 1].Value = "Итого:";
                    worksheet.Cells[row, 1].Style.Font.Color.SetColor(Color.Red);
                    worksheet.Cells[row, 2].Value = total;
                    worksheet.Cells[row, 2].Style.Font.Color.SetColor(Color.Red);
                    row += 2;
                }
                worksheet.Cells[row + 1, 1].Value = "Итого за все города:";
                worksheet.Cells[row + 1, 2].Value = globalTotal;
                worksheet.Columns[1].AutoFit();

                using (var memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    //var file = File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VaccinationData.xlsx");
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
