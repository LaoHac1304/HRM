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
using hrOT.Application.Degrees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Families.Queries;

public record GetAllFamilyQuery : IRequest<List<FamilyDto>>;

public class GetAllDegreeQueryHandler : IRequestHandler<GetAllFamilyQuery, List<FamilyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllDegreeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FamilyDto>> Handle(GetAllFamilyQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Families
        .Where(e => e.IsDeleted == false)
        .AsNoTracking();
        if (query == null || query.Count() == 0)
        {
            throw new NotFoundException("Danh sách trống.");
        }
        var map = query.ProjectTo<FamilyDto>(_mapper.ConfigurationProvider);

        var list = map.ToListAsync(cancellationToken);
        return await list;
    }
}
