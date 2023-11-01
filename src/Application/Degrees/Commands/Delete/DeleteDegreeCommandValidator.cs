using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Degrees.Commands.Delete;

public class DeleteDegreeCommandValidator : AbstractValidator<DeleteDegreeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDegreeCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(query => query.DegreeId)
            .NotEmpty().WithMessage("Id không được bỏ trống");
    }

    private async Task<bool> ExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Degrees.AnyAsync(e => e.Id == Id, cancellationToken);
        return employeeExists;
    }
}