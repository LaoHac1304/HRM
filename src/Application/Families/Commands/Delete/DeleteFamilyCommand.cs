using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees.Commands.Delete;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Families.Commands.Delete;

public record DeleteFamilyCommand : IRequest
{
    public Guid FamilyId { get; init; }
}


public class DeleteFamilyCommandHandler : IRequestHandler<DeleteFamilyCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteFamilyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Families
        .FindAsync(new object[] { request.FamilyId }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Không tìm thấy Family Id: {request.FamilyId}");
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}