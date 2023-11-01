using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Positions.Commands.CreatePosition;

public record CreatePositionCommand : IRequest<string>
{
    public Guid DepartmentId { get; set; }
    public string? Name { get; set; }
}

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreatePositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        // check if the department name existed
        var tmp = _context.Positions.FirstOrDefault(pos => pos.DepartmentId == request.DepartmentId && pos.Name.Equals(request.Name));
        if (tmp != null) { throw new ArgumentException($"Đã tồn tại Department với tên \"{request.Name}\"");  }

        var entity = new Position()
        {
            DepartmentId = request.DepartmentId,
            Name = request.Name
        };

        _context.Positions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return "Thêm thành công";
    }
}