using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Experiences.Queries;

public class Employee_GetListExperienceQuery : IRequest<List<ExperienceDTO>>
{
    public Guid EmployeeId { get; set; }

    public Employee_GetListExperienceQuery(Guid employeeId)
    {
        EmployeeId = employeeId;
    }
}

public class Employee_GetListExperienceQueryHandler : IRequestHandler<Employee_GetListExperienceQuery, List<ExperienceDTO>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_GetListExperienceQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)

    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<ExperienceDTO>> Handle(Employee_GetListExperienceQuery request, CancellationToken cancellationToken)
    {
        var employeeId = request.EmployeeId;
        var employee = await _context.Employees.FindAsync(employeeId);
        if (employee == null) { throw new NotFoundException(nameof(Employee), request.EmployeeId, "Nhân viên không tồn tại"); }
        else
        {
            var list = await _context.Experiences
                .Where(exp => exp.EmployeeId.Equals(employeeId) && exp.IsDeleted == false)
                .ProjectTo<ExperienceDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list.Count == 0 ? throw new NotFoundException("Danh sách trống!") : list;
        }
    }
}