using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees.Commands.Delete;
public record DeleteIdentityImage : IRequest<string>
{
    public Guid EmployeeId { get; init; }
    public bool IsBackFace { get; init; }
}

public class DeleteIdentityImageHandler : IRequestHandler<DeleteIdentityImage, string>
{

    private readonly IApplicationDbContext _context;

    public DeleteIdentityImageHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(DeleteIdentityImage request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Employee), request.EmployeeId , "Nhân viên không tồn tại");
        }

        if(request.IsBackFace)
        {
            entity.PhotoCIOnTheBack = null;
        }
        else
        {
            entity.PhotoCIOnTheFront = null;
        }

        _context.Employees.Update(entity);
       await _context.SaveChangesAsync(cancellationToken);
        return "Xóa thành công";
    }
}