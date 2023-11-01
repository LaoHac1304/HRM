using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Update;

public class Employee_UpdateSkillCommandValidator : AbstractValidator<Employee_UpdateSkillCommand>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateSkillCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.EmployeeId)
            .NotEmpty().WithMessage("ID nhân viên không được để trống.");
        RuleFor(v => v.SkillId)
            .NotEmpty().WithMessage("ID kỹ năng không được để trống.");
        RuleFor(v => v.Level)
            .NotEmpty().WithMessage("Cấp bậc không được để trống.")
            .NotNull().WithMessage("Cấp bậc không được để trống.");
    }

    /*private async Task<bool> BeUniqueName(Employee_UpdateSkillCommand employee_CreateSkillCommand, Guid arg1, CancellationToken arg2)
    {
        var result = await _context.Skill_Employees
            .Where(s => s.Skill.Id == employee_CreateSkillCommand.SkillId && s.IsDeleted == false)
            .AllAsync(s => s.Skill.Id != arg1, arg2);
        return result;
    }*/
}