using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Commands.Update;

public record Staff_UpdateOvertimeLogCommand : IRequest<string>
{
    public Guid Id { get; init; }
    public OvertimeLogStatus Status { get; init; }
}

public class Staff_UpdateOvertimeLogCommandHandler : IRequestHandler<Staff_UpdateOvertimeLogCommand, string>
{
    private readonly IApplicationDbContext _context;

    public Staff_UpdateOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(Staff_UpdateOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLogs
            .FirstOrDefaultAsync(o => o.Id == request.Id && o.IsDeleted == false)
            ?? throw new NotFoundException($"OvertimeLog Id: {request.Id} không tồn tại.");

        entity.Status = request.Status;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Staff";

        await _context.SaveChangesAsync(cancellationToken);

        return "Cập nhật thành công";
    }
}