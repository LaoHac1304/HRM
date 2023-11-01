using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.LeaveLogs.Commands.Create;
public record Employee_CreateLeaveLogCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; init; }
    public int LeaveHours { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string Reason { get; init; }
    public Guid LeaveTypeId { get; init; }
}

public class Employee_CreateLeaveLogCommandHandler : IRequestHandler<Employee_CreateLeaveLogCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_CreateLeaveLogCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(Employee_CreateLeaveLogCommand request, CancellationToken cancellationToken)
    {

        TimeSpan duration = request.EndDate - request.StartDate;
        int leaveDays = duration.Days + 1;
        int leaveHours = leaveDays * 8;

        bool ok = await CheckBenefitAsync(request.EmployeeId, leaveDays, request.LeaveTypeId,
            request.StartDate, request.EndDate, cancellationToken);

        if (!ok)
        {
            throw new Exception("Số ngày nghỉ vượt quá giới hạn");
        }

        var entity = new LeaveLog();
        entity.EmployeeId = request.EmployeeId;
        entity.LeaveTypeId = request.LeaveTypeId;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LeaveHours = request.LeaveHours;
        entity.Reason = request.Reason;
        entity.Status = LeaveLogStatus.Pending;
        entity.CreatedBy = "Employee";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Employee";

        await _context.LeaveLogs.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;

    }

    private async Task<bool> CheckBenefitAsync(Guid employeeId, int leaveDays, Guid leaveTypeId,
        DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        /*var employee = await _context.Employees
            .FindAsync(new object[] { employeeId }, cancellationToken);*/

        var leaveType = await _context.LeaveTypes
            .FindAsync(new object[] { leaveTypeId }, cancellationToken);

        DateTime startDateOfMonth = new DateTime(startDate.Year, startDate.Month, 1);
        DateTime endDateOfMonth = startDateOfMonth.AddMonths(1).AddDays(-1); ;

        var leaveLogsCount = await _context.LeaveLogs
            .AsNoTracking()
            .Where(log => log.EmployeeId == employeeId && log.LeaveTypeId == leaveTypeId
                   && log.StartDate >= startDateOfMonth && log.EndDate <= endDateOfMonth)
            .OrderBy(log => log.Status)
            .CountAsync(cancellationToken);

        return leaveDays <= (leaveType.Benefit - leaveLogsCount);
    }
}
