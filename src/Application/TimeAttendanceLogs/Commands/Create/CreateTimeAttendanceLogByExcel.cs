﻿
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace hrOT.Application.TimeAttendanceLogs.Commands.Create;
public record CreateTimeAttendanceLogByExcel : IRequest<string>
{
    public string FilePath { get; set; }
}
public class CreateTimeAttendanceLogByExcelHandler : IRequestHandler<CreateTimeAttendanceLogByExcel, string>
{
    private readonly IApplicationDbContext _context;

    public CreateTimeAttendanceLogByExcelHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateTimeAttendanceLogByExcel request, CancellationToken cancellationToken)
    {
        var filePath = request.FilePath;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The specified file does not exist.", filePath);
        }


        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {

            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = 1;
            int currentRow = 2;

            while (worksheet.Cells[currentRow, 1].Value != null)
            {
                rowCount++;
                currentRow++;
            }
            var timeAttendanceLog = new List<TimeAttendanceLog>();

            for (int row = 2; row <= rowCount; row++)
            {
                var employeeId = worksheet.Cells[row, 1].GetValue<string>();
                var employeeIdGuid = Guid.Parse(employeeId);
                var employee = await _context.Employees
                    .Where(x => x.Id == employeeIdGuid)
                    .FirstOrDefaultAsync(cancellationToken);
                if (employee == null)
                {
                    continue;
                }
                var startTime = worksheet.Cells[row, 2].GetValue<DateTime>();
                var endTime = worksheet.Cells[row, 3].GetValue<DateTime>();

                var logExists = await _context.TimeAttendanceLogs
                    .AnyAsync(log => log.EmployeeId == Guid.Parse(employeeId) && log.StartTime == startTime && log.EndTime == endTime);

                if (logExists)
                {
                    // Bỏ qua dòng dữ liệu đã tồn tại và tiếp tục với dòng dữ liệu tiếp theo
                    continue;
                }

                var log = new TimeAttendanceLog
                {
                    EmployeeId = Guid.Parse(employeeId),
                    StartTime = startTime,
                    EndTime = endTime
                };

                timeAttendanceLog.Add(log);
            }

            await _context.TimeAttendanceLogs.AddRangeAsync(timeAttendanceLog);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return "Thêm thành công";
    }

}
