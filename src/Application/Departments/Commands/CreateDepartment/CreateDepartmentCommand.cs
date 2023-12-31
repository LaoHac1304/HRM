﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Departments.Commands.CreateDepartment;
public record CreateDepartmentCommand : IRequest<Guid>
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var existDepartment = await _context.Departments
            .FirstOrDefaultAsync(d => d.Name == request.Name);
        if (existDepartment != null)
        {
            throw new NotFoundException("Phòng ban đã tồn tại");
        }
        var entity = new Department();
        entity.Name = request.Name;
        entity.Description = request.Description;

        _context.Departments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;

    }
}
