using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using hrOT.Application.Common.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace hrOT.Application.Degrees.Queries;

public record GetListDegreeByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<DegreeDto>>
{
}

public class GetListDegreeByEmployeeIdQueryHandler : IRequestHandler<GetListDegreeByEmployeeIdQuery, List<DegreeDto>>
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

    public async Task<List<DegreeDto>> Handle(GetListDegreeByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        // Lấy Id từ cookie
        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        // var employeeId = Guid.Parse(employeeIdCookie);

        var result = await _context.Degrees
             .Where(x => x.EmployeeId == request.EmployeeId)
             .ProjectTo<DegreeDto>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
        if (result == null || result.Count() == 0)
        {
            throw new NotFoundException(nameof(Degrees), request.EmployeeId, "Không tìm thấy danh sách.");
        }
        return result;
       
    }
}