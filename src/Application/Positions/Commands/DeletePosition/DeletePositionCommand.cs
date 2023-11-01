using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Commands.DeletePosition;

public record DeletePositionCommand(Guid PositionId) : IRequest;

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {

         var entity = await _context.Positions
            .FindAsync(new object[] { request.PositionId }, cancellationToken);

        if (entity.IsDeleted)
        {
            throw new NotFoundException(nameof(Position), request.PositionId, "Không tìm thấy vị trí");
        }

        entity.IsDeleted = true;
        entity.LastModified = DateTime.UtcNow;
        entity.LastModifiedBy = "Admin";

        await _context.SaveChangesAsync(cancellationToken);
        Console.WriteLine(entity);

        return Unit.Value;
    }
}