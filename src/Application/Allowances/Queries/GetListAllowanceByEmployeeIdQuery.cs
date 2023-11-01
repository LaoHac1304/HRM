using System.Reflection.Metadata;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Allowances.Queries;

public record GetListAllowanceByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<AllowanceDto>>
{
}

public class GetListDegreeByEmployeeIdQueryHandler : IRequestHandler<GetListAllowanceByEmployeeIdQuery, List<AllowanceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetListDegreeByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<AllowanceDto>> Handle(GetListAllowanceByEmployeeIdQuery request, CancellationToken cancellationToken)
    {

        var query = _context.Allowances.Where(b => b.EmployeeContract.Employee.Id == request.EmployeeId)
            .AsNoTracking();
        if (query == null || query.Count() == 0)
        {
            throw new NotFoundException("Danh sách trống.");
        }

        var map = query.ProjectTo<AllowanceDto>(_mapper.ConfigurationProvider);

        var list = map.ToListAsync(cancellationToken);

        return await list;

    }
}