using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Commands.Add;

public class CreateSkillCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    //public SkillCommandDTO SkillDTO { get; set; }

    /*public CreateSkillCommand(SkillCommandDTO skillDTO)
    {
        SkillDTO = skillDTO;
    }*/
}

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var existSkill = await _context.Skills
            .FirstOrDefaultAsync(s => s.SkillName == request.Name);

        if (existSkill != null)
        {
            //throw new NotFoundException(nameof(Skills), request.Id);
            throw new NotFoundException("Kỹ năng đã tồn tại");
        }
        var skill = new Skill();
        skill.Id = new Guid();
        skill.SkillName = request.Name;
        skill.Skill_Description = request.Description;

        await _context.Skills.AddAsync(skill);
        await _context.SaveChangesAsync(cancellationToken);
        return skill.Id;
    }
}