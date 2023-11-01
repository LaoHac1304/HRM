using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


public record CreateMultiAllowanceCommand : IRequest<Guid>
{
    //public Guid Id { get; init; }
    public List<Guid> EmployeeContractIds { get; init; }
    public Guid AllowanceTypeId { get; init; }
    public double Amount { get; init; }
}

public class CreateMultiAllowanceCommandHandler : IRequestHandler<CreateMultiAllowanceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateMultiAllowanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateMultiAllowanceCommand request, CancellationToken cancellationToken)
    {
        var empContracts = await _context.EmployeeContracts
            .Where(emp => request.EmployeeContractIds.Contains(emp.Id))
            .ToListAsync();

        if (empContracts.Count == 0)
        {
            throw new NotFoundException($"Không tìm thấy EmployeeContract!");
        }

        foreach (var empContract in empContracts)
        {
            var entity = new Allowance();
            entity.EmployeeContractId = empContract.Id;
            entity.AllowanceTypeId = request.AllowanceTypeId;
            entity.Amount = request.Amount;
            entity.CreatedBy = "Employee";
            entity.LastModified = DateTime.Now;
            entity.LastModifiedBy = "Employee";

            _context.Allowances.Add(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return empContracts[0].Id; // Trả về Id của EmployeeContract đầu tiên trong danh sách
    }
}
