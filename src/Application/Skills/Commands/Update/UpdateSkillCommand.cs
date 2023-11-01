using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Commands.Update;

public class UpdateSkillCommand : IRequest<Guid>
{
    public Guid SkillId { get; set; }
    public string? SkillName { get; set; }
    public string? Skill_Description { get; set; }
    /*public SkillCommandDTO SkillDTO { get; set; }

    public UpdateSkillCommand(Guid skillId, SkillCommandDTO skillDTO)
    {
        SkillId = skillId;
        SkillDTO = skillDTO;
    }*/
}

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateSkillCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();

        if (skill == null || skill.IsDeleted)
        {
            throw new NotFoundException(nameof(Skills), request.SkillId);
        }

        /*if (skill.IsDeleted)
        {
            throw new InvalidOperationException("Kĩ năng này đã bị xóa!");
        }*/
            //var tempSkillName = skill.SkillName;

            skill.SkillName = request.SkillName;
            skill.Skill_Description = request.Skill_Description;

            _context.Skills.Update(skill);
            await _context.SaveChangesAsync(cancellationToken);

        return skill.Id;
    }
}