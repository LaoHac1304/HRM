using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.Employees.Queries;
public record Employee_GetEmployeeQuery : IRequest<EmployeeVm>
{
    public Guid EmployeeId { get; set; }

    public Employee_GetEmployeeQuery(Guid employeeId)
    {
        EmployeeId = employeeId;
    }
}

public class Employee_GetEmployeeQueryHandler : IRequestHandler<Employee_GetEmployeeQuery, EmployeeVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_GetEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EmployeeVm> Handle(Employee_GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = request.EmployeeId;
        var employee = await _context.Employees.FindAsync(employeeId);

        if (employee == null || employee.IsDeleted)
        {
            throw new NotFoundException("Không tìm được nhân viên.");
        }

        var employeeVm = _context.Employees
            .Where(e => e.Id == employeeId)
            .ProjectTo<EmployeeVm>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        return employeeVm;
    }
}
