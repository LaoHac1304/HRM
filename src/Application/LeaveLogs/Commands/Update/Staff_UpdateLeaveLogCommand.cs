using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.LeaveLogs.Commands.Update;

public record Staff_UpdateLeaveLogCommand : IRequest<string>
{
    public Guid Id { get; init; }
    public LeaveLogStatus Status { get; init; }
}
public class Staff_UpdateLeaveLogCommandHandler : IRequestHandler<Staff_UpdateLeaveLogCommand, string>
{
    private readonly IApplicationDbContext _context;

    public Staff_UpdateLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(Staff_UpdateLeaveLogCommand request, CancellationToken cancellationToken)
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
            entity.Status = request.Status;
            entity.LastModified = DateTime.Now;
            entity.LastModifiedBy = "Employee";

            await _context.SaveChangesAsync(cancellationToken);

            return "Cập nhật thành công";
        }
        catch (Exception ex)
        {
            throw new Exception("Đã xảy ra lỗi khi cập nhật Leave Log");
        }
    }
}