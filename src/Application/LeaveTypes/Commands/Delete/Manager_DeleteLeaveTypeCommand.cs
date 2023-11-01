using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.LeaveLogs.Commands.Delete;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.LeaveTypes.Commands.Delete;
public record Manager_DeleteLeaveTypeCommand : IRequest
{
    public Guid LeaveTypeId { get; init; }
}


public class Manager_DeleteLeaveTypeCommandHandler : IRequestHandler<Manager_DeleteLeaveTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public Manager_DeleteLeaveTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(Manager_DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LeaveTypes
            .FindAsync(new object[] { request.LeaveTypeId }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.LeaveTypeId);
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}
