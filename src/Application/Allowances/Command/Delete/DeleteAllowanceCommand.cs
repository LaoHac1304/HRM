﻿using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Allowances.Command.Delete;

public record DeleteAllowanceCommand : IRequest
{
    public Guid Id { get; init; }
    //public bool IsDeleted { get; init; }
}

public class DeleteAllowanceCommandHandler : IRequestHandler<DeleteAllowanceCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAllowanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAllowanceCommand request, CancellationToken cancellationToken)
    {
       
            var entity = await _context.Allowances
                        .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Không tìm thấy trợ cấp có ID : {request.Id} ");
        }

        if (entity.IsDeleted)
        {
            throw new Exception("Trợ cấp này đã bị xóa");
        }

        entity.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
       
    }
}