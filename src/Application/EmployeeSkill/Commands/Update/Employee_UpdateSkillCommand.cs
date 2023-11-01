using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Update;

public class Employee_UpdateSkillCommand : IRequest<Guid>
{

    public Guid SkillId { get; init; }
    public Guid EmployeeId { get; init; }
    public string Level { get; init; }

}

public class Employee_UpdateSkillCommandHandler : IRequestHandler<Employee_UpdateSkillCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_UpdateSkillCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> Handle(Employee_UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        #region Đây là đoạn code có vẻ thừa: vì trong bảng này chỉ cần sai ID của employee hay skill cũng là ko tìm thấy nhân viên với kĩ năng đó rồi.
        var skill = await _context.Skills
        .Where(s => s.Id == request.SkillId && s.IsDeleted == false)
        .FirstOrDefaultAsync();
        if (skill == null)
        {
            throw new NotFoundException(nameof(Skill), request.SkillId, "ID kỹ năng không tồn tại trong danh sách kỹ năng!");
        }
        //if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
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


        empSkill.Level = request.Level;
        /*empSkill.SkillId = request.SkillId;*/

        _context.Skill_Employees.Update(empSkill);
        await _context.SaveChangesAsync(cancellationToken);
        return empSkill.Id;


    }
}