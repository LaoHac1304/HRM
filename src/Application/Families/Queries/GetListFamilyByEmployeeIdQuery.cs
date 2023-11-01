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
using hrOT.Application.Families;
using iTextSharp.text;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Families.Queries;

public record GetListFamilyByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<FamilyDto>>;
public class GetListFamilyByEmployeeIdQueryHandler : IRequestHandler<GetListFamilyByEmployeeIdQuery, List<FamilyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListFamilyByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FamilyDto>> Handle(GetListFamilyByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Families
        .Where(x => x.EmployeeId == request.EmployeeId)
        .AsNoTracking();
        if (query == null || query.Count()==0)
        {
            throw new NotFoundException("Danh sách trống.");
        }
        var map = query.ProjectTo<FamilyDto>(_mapper.ConfigurationProvider);
        var list = map.ToListAsync(cancellationToken);      
        return await list;
    }
}
