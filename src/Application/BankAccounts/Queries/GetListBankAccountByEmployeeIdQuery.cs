using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Families;
using hrOT.Application.Families.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Queries;
public record GetListBankAccountByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<BankAccountDTO>>;

public class GetListBankAccountByEmployeeIdQueryHandler : IRequestHandler<GetListBankAccountByEmployeeIdQuery, List<BankAccountDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListBankAccountByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<BankAccountDTO>> Handle(GetListBankAccountByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var list = _context.BankAccounts
        .Where(x => x.EmployeeId == request.EmployeeId);
        if (list == null || list.Count() == 0)
        {
            throw new NotFoundException("Danh sách trống");
        }
        var map = _mapper.ProjectTo<BankAccountDTO>(list);
        var result = await map.ToListAsync(cancellationToken);
        return result;
    }
}
