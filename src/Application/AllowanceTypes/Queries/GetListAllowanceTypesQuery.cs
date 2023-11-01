using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Allowances.Queries;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.AllowanceTypes.Queries;
public record GetListAllowanceTypesQuery : IRequest<List<AllowanceTypesDTO>>;

public class GetListAllowanceTypesQueryHandler : IRequestHandler<GetListAllowanceTypesQuery, List<AllowanceTypesDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListAllowanceTypesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AllowanceTypesDTO>> Handle(GetListAllowanceTypesQuery request, CancellationToken cancellationToken)
    {
       var query = _context.AllowanceTypes
                .Where(o => o.IsDeleted == false)
                .AsNoTracking();
        if (query == null || query.Count() == 0)
        {
            throw new NotFoundException("Danh sách trống.");
        }
        var map = query.ProjectTo<AllowanceTypesDTO>(_mapper.ConfigurationProvider);
        var list = map.ToListAsync(cancellationToken);
        return await list;
    }
}
