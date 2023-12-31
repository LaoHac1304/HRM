﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.Degrees.Commands.Delete;

public record DeleteDegreeCommand : IRequest
{
    public Guid DegreeId { get; init; }
    //public bool IsDeleted { get; init; }

}
public class DeleteOvertimeLogCommandHandler : IRequestHandler<DeleteDegreeCommand>
{
    private readonly IApplicationDbContext _context;


    public DeleteOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<Unit> Handle(DeleteDegreeCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Degrees.FindAsync(new object[] { request.DegreeId }, cancellationToken);

        if (entity == null || entity.IsDeleted)
        {
            throw new NotFoundException(nameof(Degree), request.DegreeId, "Không tìm thấy bằng cấp");
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}