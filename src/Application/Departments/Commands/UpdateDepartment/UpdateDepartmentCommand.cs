using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand : IRequest<Guid>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Departments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null || entity.IsDeleted)
        {
            throw new NotFoundException(nameof(Departments), request.Id);
            //throw new NotFoundException("Không tìm thấy phòng ban");
        }

        /*if (entity.IsDeleted)
        {
            throw new NotFoundException("Phòng ban này đã bị xóa!");
        }*/

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;

    }
}

