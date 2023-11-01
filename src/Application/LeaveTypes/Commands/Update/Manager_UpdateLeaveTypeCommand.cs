using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.LeaveLogs.Commands.Update;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.LeaveTypes.Commands.Update;
public record Manager_UpdateLeaveTypeCommand : IRequest<Guid>
{
    public Guid LeaveTypeId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Benefit { get; init; } // Number of leave days
    public bool IsReward { get; init; }

}


public class Manager_UpdateLeaveTypeCommandHandler : IRequestHandler<Manager_UpdateLeaveTypeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public Manager_UpdateLeaveTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(Manager_UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.LeaveTypes
            .FindAsync(new object[] { request.LeaveTypeId }, cancellationToken);

        if (entity == null || entity.IsDeleted == true)
        {
            throw new NotFoundException(nameof(LeaveLog), request.LeaveTypeId, "Không tìm thấy loại nghỉ phép");
        }

        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Manager";

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Benefit = request.Benefit;
        entity.IsReward = request.IsReward;

        await _context.SaveChangesAsync(cancellationToken);

        return request.LeaveTypeId;
    }
}
