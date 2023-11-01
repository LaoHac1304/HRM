using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Add;

public class Employee_CreateSkillCommand : IRequest<Guid>
{
    public Guid SkillId { get; init; }
    public Guid EmployeeId { get; init; }
    public string Level { get; init; }

}

public class Employee_CreateSkillCommandHandler : IRequestHandler<Employee_CreateSkillCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_CreateSkillCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> Handle(Employee_CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
        .Where(s => s.Id == request.SkillId && s.IsDeleted == false)
        .FirstOrDefaultAsync();

        if (skill == null)
        {
            throw new NotFoundException(nameof(Skill), request.SkillId, "ID kỹ năng không tồn tại trong danh sách kỹ năng!");
        }
        //if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        var employee = await _context.Employees
            .Where(e => e.Id == request.EmployeeId && e.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (employee == null)
        {
            throw new NotFoundException(nameof(Employee), request.EmployeeId, "ID nhân viên không tồn tại trong danh sách nhân viên!");
        }

        var empSkill = await _context.Skill_Employees
        .Where(es => es.EmployeeId == request.EmployeeId && es.SkillId == request.SkillId && !es.IsDeleted)
        .FirstOrDefaultAsync();
        if (empSkill != null)
        {
            throw new Exception( $"Đã tồn tại nhân viên (ID:{request.EmployeeId}) sở hữu kỹ năng (ID:{request.SkillId})");
        }

        var empskill = new Skill_Employee
        {
            Id = Guid.NewGuid(),
            Level = request.Level,
            EmployeeId = request.EmployeeId,
            SkillId = request.SkillId
        };

        await _context.Skill_Employees.AddAsync(empskill);
        await _context.SaveChangesAsync(cancellationToken);
        return empskill.Id;
    }

}