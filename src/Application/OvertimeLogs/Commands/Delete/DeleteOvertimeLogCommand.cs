using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Commands.Delete;

public record DeleteOvertimeLogCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    //public bool IsDeleted { get; init; }
}

public class DeleteOvertimeLogCommandHandler : IRequestHandler<DeleteOvertimeLogCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLogs
            .FirstOrDefaultAsync(o => o.Id == request.Id && o.IsDeleted == false)
            ?? throw new NotFoundException($"OvertimeLog Id: {request.Id} không tồn tại.");

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}