using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Departments.Queries.GetDepartment;

public record GetListDepartmentQuery : IRequest<List<DepartmentDTO>>;

public class GetListDepartmentQueryHandler : IRequestHandler<GetListDepartmentQuery, List<DepartmentDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListDepartmentQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DepartmentDTO>> Handle(GetListDepartmentQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Departments.Where(d => d.IsDeleted == false).AsNoTracking();
        if (query == null || query.Count() == 0)
        {
            throw new NotFoundException(nameof(Departments));
        }

        var map = query.ProjectTo<DepartmentDTO>(_mapper.ConfigurationProvider);

        var list = map.ToListAsync(cancellationToken);

        return await list;
    }
}