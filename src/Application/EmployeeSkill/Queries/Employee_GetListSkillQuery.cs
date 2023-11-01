using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees_Skill.Queries;

public class Employee_GetListSkillQuery : IRequest<List<Skill_EmployeeDTO>>
{
    public Guid EmployeeId { get; set; }

    public Employee_GetListSkillQuery(Guid EmployeeID)
    {
        EmployeeId = EmployeeID;
    }
}

public class Employee_GetListSkillQueryHandler : IRequestHandler<Employee_GetListSkillQuery, List<Skill_EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_GetListSkillQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Skill_EmployeeDTO>> Handle(Employee_GetListSkillQuery request, CancellationToken cancellationToken)
    {
        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];


        var employee = await _context.Employees.FindAsync(request.EmployeeId);
        if(employee == null)
        {
            throw new NotFoundException(nameof(Employee), request.EmployeeId, "Mục tiêu không tồn tại.");
        }

        var query = _context.Skill_Employees
            .Include(k => k.Skill)
            .Where(s => s.EmployeeId == request.EmployeeId && s.IsDeleted == false);
        if (query == null || query.Count() == 0)
        {
            throw new NotFoundException(nameof(Skill_Employee), request.EmployeeId, "Không tìm thấy danh sách.");
        }

        var map = query.ProjectTo<Skill_EmployeeDTO>(_mapper.ConfigurationProvider);
        var list = map.ToListAsync(cancellationToken);

        return await list;

    }
}