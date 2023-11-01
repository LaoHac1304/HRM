
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;


namespace hrOT.Application.Departments.Commands.DeleteDepartment;

public record DeleteDepartmentCommand(Guid Id) : IRequest;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Departments
                .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null || entity.IsDeleted)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        /*if (entity.IsDeleted)
        {
            throw new Exception("Phòng ban này đã bị xóa!");
        }*/

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

