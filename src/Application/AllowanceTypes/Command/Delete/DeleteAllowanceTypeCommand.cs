using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.AllowanceTypes.Command.Delete;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.AllowanceTypes.Command.Delete;
public record DeleteAllowanceTypeCommand : IRequest
{
    public Guid Id { get; init; }
}

public class DeleteAllowanceTypeCommandHandler : IRequestHandler<DeleteAllowanceTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAllowanceTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAllowanceTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.AllowanceTypes
                    .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Không tìm thấy loại trợ cấp có ID : {request.Id} ");
        }

        if (entity.IsDeleted)
        {
            throw new Exception("Loại trợ cấp này đã bị xóa");
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
