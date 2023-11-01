using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.AllowanceTypes.Command.Create;
public record CreateAllowanceTypeCommand : IRequest<Guid>
{
    //public Guid Id { get; init; }
    public string Name { get; init; }
    public Boolean IsPayTax { get; init; }
    public string Description { get; init; }
    public string Eligibility_Criteria { get; init; }
    public string Requirements { get; init; }
}

public class CreateAllowanceTypeCommandHandler : IRequestHandler<CreateAllowanceTypeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAllowanceTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAllowanceTypeCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _context.AllowanceTypes
            .AnyAsync(a => a.Name == request.Name && a.IsDeleted == false);
        if (isExist) { throw new Exception("Loại phụ cấp đã tồn tại"); }

        var entity = new AllowanceType
        {
            Id = new Guid(),
            Name = request.Name,
            IsPayTax = request.IsPayTax,
            Description = request.Description,
            Eligibility_Criteria = request.Eligibility_Criteria,
            Requirements = request.Requirements,
            CreatedBy = "Employee",
            LastModified = DateTime.Now,
            LastModifiedBy = "Employee"
        };

        _context.AllowanceTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}