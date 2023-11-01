using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Commands.Update;
public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateSkillCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.SkillName)
                   .NotEmpty().WithMessage("Tên kĩ năng không được để trống.");
                   //.MustAsync(BeUniqueName).WithMessage("Tên kĩ năng đã tồn tại.");

        RuleFor(v => v.Skill_Description)
            .NotEmpty().WithMessage("Mô tả kĩ năng không được để trống.");
    }

    /*private async Task<bool> BeUniqueName(UpdateSkillCommand updateSkillCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Skills
            .Where(s => s.SkillName == updateSkillCommand.SkillDTO.SkillName && s.IsDeleted == false)
            .AllAsync(s => s.SkillName != arg1, arg2);
    }*/
}