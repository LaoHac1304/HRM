using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Commands.Create;
public record Employee_CreateOvertimeLogCommand : IRequest<bool>
{
    public Guid EmployeeId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    //public double TotalHours { get; init; }
}

public class Employee_CreateOvertimeLogCommandHandler : IRequestHandler<Employee_CreateOvertimeLogCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public Employee_CreateOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(Employee_CreateOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var checkEmployeeId = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId && e.IsDeleted == false)
            ?? throw new Exception($"Nhân viên mang Id: {request.EmployeeId} không tồn tại.");

        var annualWorkingDays = await _context.AnnualWorkingDays
            .Where(a => a.IsDeleted == false)
            .ToListAsync();

        // Kiểm tra xem ngày hiện tại có phải là ngày lễ không
        var isHoliday = annualWorkingDays.Any(d => d.Day == request.StartDate.Date && d.TypeDate == TypeDate.Holiday);
        var holiday = annualWorkingDays.FirstOrDefault(d => d.Day.Date == request.StartDate.Date && d.TypeDate == TypeDate.Holiday);

        // Kiểm tra xem ngày hiện tại có phải là ngày cuối tuần không
        var isWeekend = annualWorkingDays.Any(d => d.Day == request.StartDate.Date && d.TypeDate == TypeDate.Weekend);
        var weekend = annualWorkingDays.FirstOrDefault(d => d.Day.Date == request.StartDate.Date && d.TypeDate == TypeDate.Weekend);

        if (isHoliday || isWeekend)
        {
            var entity = new OvertimeLog
            {
                EmployeeId = request.EmployeeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalHours = (request.EndDate - request.StartDate).TotalHours,
                Coefficients = isHoliday ? holiday!.Coefficients : weekend!.Coefficients,
                Status = OvertimeLogStatus.Pending,
                CreatedBy = "Employee",
                LastModified = DateTime.Now,
                LastModifiedBy = "Employee"
            };
            _context.OvertimeLogs.Add(entity);
        }
        else
        {
            var entity = new OvertimeLog
            {
                EmployeeId = request.EmployeeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalHours = (request.EndDate - request.StartDate).TotalHours,
                Coefficients = 1,
                Status = OvertimeLogStatus.Pending,
                CreatedBy = "Employee",
                LastModified = DateTime.Now,
                LastModifiedBy = "Employee"
            };
            _context.OvertimeLogs.Add(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}