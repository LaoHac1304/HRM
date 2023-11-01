using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace hrOT.Application.Allowances.Command.Update;

public record UpdateAllowanceCommand : IRequest<Guid>
{
    public Guid Id { get; init; }
    public Guid EmployeeContractId { get; init; }
    public Guid AllowanceTypeId { get; init; }
    public double Amount { get; init; }
}

public class UpdateAllowanceCommandHandler : IRequestHandler<UpdateAllowanceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateAllowanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateAllowanceCommand request, CancellationToken cancellationToken)
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

        entity.EmployeeContractId = request.EmployeeContractId;
        entity.AllowanceTypeId = request.AllowanceTypeId;
        entity.Amount = request.Amount;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
        
    }
}