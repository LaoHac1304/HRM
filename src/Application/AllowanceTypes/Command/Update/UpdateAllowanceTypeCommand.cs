using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Allowances.Command.Update;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;

namespace hrOT.Application.AllowanceTypes.Command.Update;
public record UpdateAllowanceTypeCommand : IRequest<Guid>
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Boolean IsPayTax { get; init; }
    public string Description { get; init; }
    public string Eligibility_Criteria { get; init; }
    public string Requirements { get; init; }
}

public class UpdateAllowanceTypeCommandHandler : IRequestHandler<UpdateAllowanceTypeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateAllowanceTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateAllowanceTypeCommand request, CancellationToken cancellationToken)
    {
            var entity = await _context.AllowanceTypes
                        .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"Không tìm thấy loại trợ cấp có ID : {request.Id} ");
            }

            if (entity.IsDeleted)
            {
                throw new Exception("Loại trợ cấp này đã bị xóa");
            }

        entity.Name = request.Name;
            entity.IsPayTax = request.IsPayTax;
            entity.Description = request.Description;
            entity.Eligibility_Criteria = request.Eligibility_Criteria;
            entity.Requirements = request.Requirements;

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
    }
}
