using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Delete;

public class Employee_DeleteSkillCommand : IRequest<Unit>
{
    public Guid SkillId { get; set; }
    public Guid EmployeeId { get; set; }

    public Employee_DeleteSkillCommand(Guid EmployeeID, Guid SkillID)
    {
        SkillId = SkillID;
        EmployeeId = EmployeeID;
    }
}

public class Employee_DeleteSkillCommandHandler : IRequestHandler<Employee_DeleteSkillCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_DeleteSkillCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(Employee_DeleteSkillCommand request, CancellationToken cancellationToken)
    {

        /*var skill = await _context.Skills
        .Where(s => s.Id == request.SkilLId && s.IsDeleted == false)
        .FirstOrDefaultAsync();
        if (skill == null) { throw new NotFoundException("Kỹ năng không tồn tại!"); }
        //if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa"; }

        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = request.EmployeeId;

        var empSkill = await _context.Skill_Employees
            .Where(es => es.EmployeeId == employeeId
            && es.SkillId == request.SkilLId)
            .FirstOrDefaultAsync();
        if (empSkill == null) { return "Id nhân viên không tồn tại"; }
        if (empSkill.IsDeleted) { return "Kĩ năng này của nhân viên đã bị xóa"; }

        empSkill.IsDeleted = true;

        _context.Skill_Employees.Update(empSkill);
        await _context.SaveChangesAsync(cancellationToken);
        return "Xóa thành công";*/ //khúc này code cũ cringe với báo lỗi lung tung ở đoạn Skill Employee
        #region Đây là đoạn code có vẻ thừa: Nội dung tương tự phương thức Update
        var skill = await _context.Skills
        .Where(s => s.Id == request.SkillId && s.IsDeleted == false)
        .FirstOrDefaultAsync();
        if (skill == null)
        {
            throw new NotFoundException(nameof(Skill), request.SkillId, "ID kỹ năng không tồn tại trong danh sách kỹ năng!");
        }
        var employee = await _context.Employees
               .Where(e => e.Id == request.EmployeeId && e.IsDeleted == false)
               .FirstOrDefaultAsync();
        if (employee == null)
        {
            throw new NotFoundException(nameof(Employee), request.EmployeeId, "ID nhân viên không tồn tại trong danh sách nhân viên!");
        }
        #endregion
        var empSkill = await _context.Skill_Employees
            .Where(es => es.EmployeeId == request.EmployeeId && es.SkillId == request.SkillId && !es.IsDeleted)
            .FirstOrDefaultAsync();
        if (empSkill == null)
        {
            throw new NotFoundException(nameof(Skill_Employee), $"Không tìm thấy nhân viên (ID:{request.EmployeeId}) sở hữu kỹ năng (ID:{request.SkillId})");
        }

        empSkill.IsDeleted = true;

        _context.Skill_Employees.Update(empSkill);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}