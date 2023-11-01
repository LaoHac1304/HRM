using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Queries;
public class GetAllBankAccountQuery : IRequest<List<BankAccountDTO>>
{
}
public class GetAllBankAccountQueryHandler : IRequestHandler<GetAllBankAccountQuery, List<BankAccountDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllBankAccountQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<BankAccountDTO>> Handle(GetAllBankAccountQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.BankAccounts
                .Where(e => e.IsDeleted == false)
                .ProjectTo<BankAccountDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            if (list == null || list.Count == 0)
            {
                throw new NotFoundException("Danh sách trống.");
            }
            return list;
        
    }
}
