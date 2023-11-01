using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.LeaveLogs.Commands.Update;

public record Employee_UpdateLeaveLogCommand : IRequest<string>
{
    public Guid Id { get; init; }
    //public Guid EmployeeId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int LeaveHours { get; init; }
    public string Reason { get; init; }
    public Guid LeaveTypeId { get; init; }
    //public LeaveLogStatus Status { get; init; }
}
public class Employee_UpdateLeaveLogCommandHandler : IRequestHandler<Employee_UpdateLeaveLogCommand, string>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(Employee_UpdateLeaveLogCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.LeaveLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("Id không tồn tại");
        }
        else if (entity.IsDeleted)
        {
            throw new InvalidOperationException("Log nghỉ phép này đã bị xóa");
        }
        try
        {
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.LeaveHours = request.LeaveHours;
            entity.Reason = request.Reason;
            entity.Status = LeaveLogStatus.Pending;
            entity.LeaveTypeId = request.LeaveTypeId;

            await _context.SaveChangesAsync(cancellationToken);

            return "Cập nhật thành công";
        }
        catch (Exception ex)
        {
            throw new Exception("Đã xảy ra lỗi khi cập nhật Leave Log");
        }
    }
}
