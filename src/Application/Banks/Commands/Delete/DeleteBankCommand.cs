﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Banks.Commands.Delete;
public record DeleteBankCommand() : IRequest
{
    public Guid Id { get; init; }
}
public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand>
{
    private readonly IApplicationDbContext _context;


    public DeleteBankCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Banks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null || entity.IsDeleted)
        {
            throw new NotFoundException("Không tồn tại dữ liệu.");

        }      

        entity.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
       
    }
}
