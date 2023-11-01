using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.TimeAttendanceLogs.Commands.Update;
public record UpdateTimeAttendanceLog : IRequest<string>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class UpdateTimeAttendanceLogHandler : IRequestHandler<UpdateTimeAttendanceLog, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateTimeAttendanceLogHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UpdateTimeAttendanceLog request, CancellationToken cancellationToken)
    {
        // Lấy danh sách ngày làm việc hàng năm
        var annualWorkingDays = await _context.AnnualWorkingDays
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        // Lấy danh sách lịch sử chấm công
        var timeAttendanceLogs = await _context.TimeAttendanceLogs
            .Where(x => !x.IsDeleted
                && x.StartTime >= request.StartDate
                && x.EndTime <= request.EndDate)
            .OrderBy(x => x.StartTime)
            .ToListAsync(cancellationToken);

        // Lấy loại nghỉ phép hàng năm
        var leaveTypeNormal = await _context.LeaveTypes
            .FirstOrDefaultAsync(l => l.Name == "Nghỉ phép hàng năm")
            ?? throw new Exception("Tên loại nghỉ phép đã thay đổi, vui lòng cập nhật lại");

        // Kiểm tra xem danh sách ngày làm việc hàng năm có rỗng không
        if (annualWorkingDays.Count == 0)
        {
            throw new Exception("Vui lòng cập nhật danh sách ngày làm việc hàng năm");
        }

        // Kiểm tra xem danh sách lịch sử chấm công có rỗng không
        if (timeAttendanceLogs.Count == 0)
        {
            throw new Exception($"Lịch sử chấm công không tồn tại trong khoảng ngày" +
                $" {request.StartDate.Date.ToShortDateString()}" +
                $" - {request.EndDate.Date.ToShortDateString()} ");
        }

        List<DateTime> dayNotWorkInMonth = new();
        var checkMonth = false;

        foreach (var log in timeAttendanceLogs)
        {
            // Kiểm tra xem đã tính toán chấm công cho log này chưa
            var isCalculated = await _context.LeaveLogs
                .AnyAsync(l => l.EmployeeId == log.EmployeeId
                    && l.StartDate.Month == log.StartTime.Month
                    && l.Status == LeaveLogStatus.Cancelled);

            if (!isCalculated)
            {
                var totalDaysInMonth = DateTime.DaysInMonth(log.StartTime.Year, log.StartTime.Month);
                var maxMonthIndex = 0;

                if (!checkMonth)
                {
                    // Tạo danh sách các ngày không làm việc trong tháng
                    for (int i = 0; i < totalDaysInMonth; i++)
                    {
                        DateTime date = new DateTime(log.StartTime.Year, log.StartTime.Month, i + 1);
                        maxMonthIndex = date.Month;
                        dayNotWorkInMonth.Add(date);
                    }
                }

                // Lọc các log được chấm công trong danh sách những này không làm việc trong tháng
                var filterLog = timeAttendanceLogs
                    .Where(t => dayNotWorkInMonth
                        .Any(d => d == t.StartTime.Date))
                    .ToList();

                // Xóa các ngày đã được chấm công khỏi danh sách
                foreach (var filter in filterLog)
                {
                    dayNotWorkInMonth.Remove(filter.StartTime.Date);
                }

                // Lọc ra các ngày trong phạm vi
                var filterInRange = dayNotWorkInMonth
                    .Where(d => d >= request.StartDate.Date
                        && d <= request.EndDate.Date)
                    .ToList();

                if (!checkMonth)
                {
                    // Tạo log nghỉ phép cho các ngày không làm việc trong phạm vi
                    for (int i = 0; i < filterInRange.Count; i++)
                    {
                        var leavelog = new LeaveLog
                        {
                            LeaveTypeId = leaveTypeNormal.Id,
                            EmployeeId = log.EmployeeId,
                            StartDate = filterInRange[i].Date.AddHours(8),
                            EndDate = filterInRange[i].Date.AddHours(17),
                            LeaveHours = 8,
                            Reason = "Nghỉ không phép",
                            Status = LeaveLogStatus.Cancelled,
                            CreatedBy = "Admin",
                            LastModified = DateTime.Now,
                            LastModifiedBy = "Admin"
                        };

                        maxMonthIndex = leavelog.StartDate.Month;

                        _context.LeaveLogs.Add(leavelog);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }

                // Lọc các ngày đã được chấp thuận
                var approvedLog = await _context.LeaveLogs
                    .Where(l => l.Status == LeaveLogStatus.Approved)
                    .ToListAsync();
                if (approvedLog.Count > 0)
                {
                    foreach (var item in approvedLog)
                    {
                        bool isDuplicated = _context.LeaveLogs.Any(l => l.StartDate.Date == item.StartDate.Date);
                        if (isDuplicated)
                        {
                            var duplicatedDay = _context.LeaveLogs
                                .FirstOrDefault(l => l.StartDate.Date == item.StartDate.Date
                                && l.Status == LeaveLogStatus.Cancelled);

                            if (duplicatedDay != null)
                            {
                                _context.LeaveLogs.Remove(duplicatedDay);
                            }
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Xóa các ngày đã được chấm công không làm việc trong tháng
                dayNotWorkInMonth.RemoveAll(d => d.Date.Month == maxMonthIndex);
                checkMonth = log.StartTime.Month == maxMonthIndex;
            }
        }

        //==================================================================================================================================================
        //===============================================================================================================================
        //===========================================================================================================
        //======================================================================================

        //Tính châm công theo 3 loại ngày

        foreach (var log in timeAttendanceLogs)
        {
            // Giờ bắt đầu làm việc tiêu chuẩn là 8 giờ sáng
            var validHourWork = log.StartTime.Date.AddHours(8);

            // So sánh giờ trong log và giờ bắt đầu làm việc tiêu chuẩn
            var isValidHour = DateTime.Compare(validHourWork, log.StartTime);

            //tính số giờ làm việc trong 1 ngày
            //nếu nó đi làm trước 12h trưa tức là nó sẽ có nghỉ ăn cơm trưa thì phải trừ 1 tiếng đi nếu k tính cả thời gian nó nghỉ ăn cơm trưa công ty lỗ 1 tiếng
            var testend = log.StartTime;
            var teststart = log.EndTime;
            var testdate = log.EndTime - log.StartTime;
            var logDuration = (log.EndTime - log.StartTime).TotalHours;
            if (log.StartTime.TimeOfDay < new TimeSpan(12, 0, 0))
            {
                logDuration -= 1;
            }

            // Tổng số giờ làm việc
            log.Ducation = logDuration;
            var durationWorkHour = log.Ducation;

            // Kiểm tra xem ngày hiện tại có phải là ngày lễ không
            var isHoliday = annualWorkingDays.Any(d => d.Day == log.StartTime.Date && d.TypeDate == TypeDate.Holiday);
            var holiday = annualWorkingDays.FirstOrDefault(d => d.Day.Date == log.StartTime.Date && d.TypeDate == TypeDate.Holiday);

            // Kiểm tra xem ngày hiện tại có phải là ngày cuối tuần không
            var isWeekend = annualWorkingDays.Any(d => d.Day == log.StartTime.Date && d.TypeDate == TypeDate.Weekend);
            var weekend = annualWorkingDays.FirstOrDefault(d => d.Day.Date == log.StartTime.Date && d.TypeDate == TypeDate.Weekend);

            var isExistLeaveLog = await _context.LeaveLogs.AnyAsync(l => l.StartDate.Date == log.StartTime.Date);

            if (isHoliday || isWeekend)
            {
                // Nếu là ngày lễ hoặc ngày cuối tuần, tính là tăng ca
                var entity = new OvertimeLog
                {
                    EmployeeId = log.EmployeeId,
                    StartDate = log.StartTime,
                    EndDate = log.EndTime,
                    Coefficients = isHoliday ? holiday!.Coefficients : weekend!.Coefficients,
                    TotalHours = durationWorkHour,
                    Status = OvertimeLogStatus.Pending,
                    CreatedBy = "Admin",
                    LastModified = DateTime.Now,
                    LastModifiedBy = "Admin"
                };

                var checkExist = await _context.OvertimeLogs
                    .AnyAsync(l => l.StartDate == entity.StartDate && l.EndDate == entity.EndDate);

                if (!checkExist)
                {
                    _context.OvertimeLogs.Add(entity);
                }
            }
            // Nếu không phải là ngày lễ hoặc ngày cuối tuần (ngày thường)
            else
            {
                // Nếu đi trễ sau giờ làm việc tiêu chuẩn, lưu số giờ bị thiếu vào leavelog
                if (isValidHour < 0)
                {
                    var entity = new LeaveLog
                    {
                        LeaveTypeId = leaveTypeNormal.Id,
                        EmployeeId = log.EmployeeId,
                        StartDate = log.StartTime,
                        EndDate = log.EndTime,
                        LeaveHours = (log.StartTime - validHourWork).TotalHours,
                        Reason = "Đi trễ",
                        Status = LeaveLogStatus.Cancelled,
                        CreatedBy = "Admin",
                        LastModified = DateTime.Now,
                        LastModifiedBy = "Admin"
                    };

                        var checkExist = await _context.LeaveLogs
                        .AnyAsync(l => l.StartDate == entity.StartDate && l.EndDate == entity.EndDate);

                    if (!checkExist)
                    {
                        _context.LeaveLogs.Add(entity);
                    }
                }

                //// Nếu làm ít hơn 8 tiếng, lưu số giờ bị thiếu vào leavelog
                //if (durationWorkHour < 8)
                //{
                //    var entity = new LeaveLog
                //    {
                //        LeaveTypeId = leaveTypeNormal.Id,
                //        EmployeeId = log.EmployeeId,
                //        StartDate = log.StartTime,
                //        EndDate = log.EndTime,
                //        LeaveHours = (8 - log.Ducation),
                //        Reason = "Về sớm không phép",
                //        Status = LeaveLogStatus.Cancelled,
                //        CreatedBy = "Admin",
                //        LastModified = DateTime.Now,
                //        LastModifiedBy = "Admin"
                //    };

                //    var checkExist = await _context.LeaveLogs
                //        .AnyAsync(l => l.StartDate == entity.StartDate && l.EndDate == entity.EndDate);

                //    if (!checkExist)
                //    {
                //        _context.LeaveLogs.Add(entity);
                //    }
                //}


                // Nếu làm nhiều hơn 8 tiếng, lưu số giờ thừa vào overtimelog
                if (durationWorkHour > 8)
                {
                    
                    var entity = new OvertimeLog
                    {
                        EmployeeId = log.EmployeeId,
                        StartDate = log.StartTime.Date.AddHours(17),
                        EndDate = log.EndTime.Date.AddHours(17 + (durationWorkHour - 8)),
                        Coefficients = 1.5,
                        TotalHours = durationWorkHour - 8,
                        Status = OvertimeLogStatus.Pending,
                        CreatedBy = "Admin",
                        LastModified = DateTime.Now,
                        LastModifiedBy = "Admin"
                    };

                    var checkExist = await _context.OvertimeLogs
                        .AnyAsync(l => l.StartDate == entity.StartDate && l.EndDate == entity.EndDate);

                    if (!checkExist)
                    {
                        _context.OvertimeLogs.Add(entity);
                    }
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return "Tính chấm công thành công";
    }
}