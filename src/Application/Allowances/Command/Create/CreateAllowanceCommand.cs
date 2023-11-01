using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace hrOT.Application.Allowances.Command.Create;

public record CreateAllowanceCommand : IRequest<Guid>
{
    //public Guid Id { get; init; }
    public Guid EmployeeContractId { get; init; }
    public Guid AllowanceTypeId { get; init; }
    public double Amount { get; init; }
}

public class CreateAllowanceCommandHandler : IRequestHandler<CreateAllowanceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAllowanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAllowanceCommand request, CancellationToken cancellationToken)
    {

        var empContract = await _context.EmployeeContracts
            .FirstOrDefaultAsync(emp => emp.Id == request.EmployeeContractId);

        if (empContract == null) // không dùng empContract.count() == 0 vì nó dùng firstordefault
        {
            //throw new NotFoundException(nameof(EmployeeContracts), request.EmployeeContractId, "Không tìm thấy EmployeeContractId");
            throw new NotFoundException("Không tìm thấy EmployeeContractId");
        }
        var allowanceType = await _context.AllowanceTypes
            .FirstOrDefaultAsync(a => a.Id == request.AllowanceTypeId);

        if (allowanceType == null) // không dùng allowanceType.count() == 0 vì nó dùng firstordefault
        {
            //throw new NotFoundException(nameof(AllowanceTypes), request.AllowanceTypeId, "Không tìm thấy AllowanceTypeId");
            throw new NotFoundException("Không tìm thấy AllowanceTypeId");
        }

        var entity = new Allowance();
        entity.EmployeeContractId = request.EmployeeContractId;
        entity.AllowanceTypeId = request.AllowanceTypeId;
        entity.Amount = request.Amount;
        entity.CreatedBy = "Employee";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Employee";

        _context.Allowances.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;

    }
}