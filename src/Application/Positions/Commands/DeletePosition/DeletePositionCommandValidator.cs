using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Commands.DeletePosition;
public class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(n => n.PositionId)
            .NotEmpty().WithMessage("PositionIdkhông được bỏ trống")
            .MustAsync(ExistAsync).WithMessage("PositionId không tồn tại");
    }
    private async Task<bool> ExistAsync(Guid PositionId, CancellationToken cancellationToken)
    {
        var Exists = await _context.Positions.AnyAsync(e => e.Id == PositionId, cancellationToken);
        return Exists;
    }

}