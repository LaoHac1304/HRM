using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Commands.Delete;

public class DeleteSkillCommand : IRequest<Unit>
{
    public Guid SkillId { get; set; }

    public DeleteSkillCommand(Guid skillId)
    {
        SkillId = skillId;
    }
}

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
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

        skill.IsDeleted = true;
        _context.Skills.Update(skill);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}