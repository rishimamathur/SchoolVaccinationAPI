using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolVaccinationAPI.Data;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SchoolVaccinationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("vaccinations")]
        public async Task<IActionResult> GetVaccinationReport(
            string? vaccineName,
            string? studentName,
            DateTime? fromDate,
            DateTime? toDate,
            int page = 1,
            int pageSize = 10)
        {
            var query = from sv in _context.StudentVaccinations
                        join s in _context.Students on sv.Student_ID equals s.Student_ID
                        join v in _context.Vaccinations on sv.Vaccine_ID equals v.Vaccine_ID
                        orderby s.Student_ID
                        select new
                        {
                            s.Student_ID,
                            s.First_Name,
                            s.Last_Name,
                            s.Class,
                            v.Vaccine_Name,
                            sv.Vaccinated_On
                        };

            if (!string.IsNullOrEmpty(vaccineName))
                query = query.Where(x => x.Vaccine_Name.Contains(vaccineName));

            if (!string.IsNullOrEmpty(studentName))
                query = query.Where(x => x.First_Name.Contains(studentName) || x.Last_Name.Contains(studentName));

            if (fromDate.HasValue)
                query = query.Where(x => x.Vaccinated_On >= fromDate);

            if (toDate.HasValue)
                query = query.Where(x => x.Vaccinated_On <= toDate);

            var totalCount = await query.CountAsync();

            var results = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { totalCount, page, pageSize, results });
        }

        [HttpGet("vaccinations/download")]
        public async Task<IActionResult> DownloadReport(string format = "csv")
        {
            var data = await (from sv in _context.StudentVaccinations
                              join s in _context.Students on sv.Student_ID equals s.Student_ID
                              join v in _context.Vaccinations on sv.Vaccine_ID equals v.Vaccine_ID
                              select new
                              {
                                  s.Student_ID,
                                  StudentName = s.First_Name + " " + s.Last_Name,
                                  s.Class,
                                  v.Vaccine_Name,
                                  sv.Vaccinated_On
                              }).ToListAsync();

            var fileNameBase = $"VaccinationReport_{DateTime.Now:yyyyMMddHHmmss}";
            byte[] bytes;
            string mimeType;
            string fileName;

            try
            {
                if (format.ToLower() == "excel")
                {
                    using var stream = new MemoryStream();
                    using (var spreadsheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
                    {
                        var workbookPart = spreadsheetDocument.AddWorkbookPart();
                        workbookPart.Workbook = new Workbook();

                        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        var sheetData = new SheetData();

                        var headerRow = new Row();
                        headerRow.Append(
                            CreateCell("Student ID"),
                            CreateCell("Student Name"),
                            CreateCell("Class"),
                            CreateCell("Vaccine Name"),
                            CreateCell("Vaccinated On")
                        );
                        sheetData.AppendChild(headerRow);

                        foreach (var item in data)
                        {
                            var row = new Row();
                            row.Append(
                                CreateCell(item.Student_ID.ToString()),
                                CreateCell(item.StudentName),
                                CreateCell(item.Class),
                                CreateCell(item.Vaccine_Name),
                                CreateCell(item.Vaccinated_On.ToString("yyyy-MM-dd"))
                            );
                            sheetData.AppendChild(row);
                        }

                        worksheetPart.Worksheet = new Worksheet(sheetData);

                        var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                        sheets.Append(new Sheet
                        {
                            Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                            SheetId = 1,
                            Name = "Vaccination Report"
                        });

                        workbookPart.Workbook.Save();
                    }

                    stream.Position = 0;
                    bytes = stream.ToArray();
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = fileNameBase + ".xlsx";
                }
                else if (format.ToLower() == "pdf")
                {
                    using var stream = new MemoryStream();
                    var doc = new Document(PageSize.A4, 10, 10, 10, 10);
                    PdfWriter.GetInstance(doc, stream);
                    doc.Open();

                    var table = new PdfPTable(5) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 1.5f, 3, 1.5f, 2.5f, 2 });

                    var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    table.AddCell(new Phrase("Student ID", boldFont));
                    table.AddCell(new Phrase("Student Name", boldFont));
                    table.AddCell(new Phrase("Class", boldFont));
                    table.AddCell(new Phrase("Vaccine Name", boldFont));
                    table.AddCell(new Phrase("Vaccinated On", boldFont));

                    foreach (var item in data)
                    {
                        table.AddCell(item.Student_ID.ToString());
                        table.AddCell(item.StudentName);
                        table.AddCell(item.Class);
                        table.AddCell(item.Vaccine_Name);
                        table.AddCell(item.Vaccinated_On.ToString("yyyy-MM-dd"));
                    }

                    doc.Add(table);
                    doc.Close();

                    bytes = stream.ToArray();
                    mimeType = "application/pdf";
                    fileName = fileNameBase + ".pdf";
                }
                else
                {
                    var csv = new StringBuilder();
                    csv.AppendLine("StudentID,StudentName,Class,VaccineName,VaccinatedOn");

                    foreach (var d in data)
                    {
                        csv.AppendLine($"{d.Student_ID},{d.StudentName},{d.Class},{d.Vaccine_Name},{d.Vaccinated_On:yyyy-MM-dd}");
                    }

                    bytes = Encoding.UTF8.GetBytes(csv.ToString());
                    mimeType = "text/csv";
                    fileName = fileNameBase + ".csv";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return File(bytes, mimeType, fileName);
        }

        private Cell CreateCell(string value)
        {
            return new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(value)
            };
        }
    }
}
