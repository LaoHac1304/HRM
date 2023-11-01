using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;

namespace hrOT.Application.LeaveLogs.Commands.Delete;

public record DeleteLeaveLogCommand : IRequest<string>
{
    public Guid Id { get; init; }
    //public bool IsDeleted { get; init; }

    public DeleteLeaveLogCommand(Guid id)
    {
        Id = id;
    }
}
public class DeleteLeaveLogCommandHandler : IRequestHandler<DeleteLeaveLogCommand, string>
{
    private readonly IApplicationDbContext _context;

    public DeleteLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(DeleteLeaveLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LeaveLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("Id không tồn tại");
        }
        if (entity.IsDeleted)
        {
            throw new InvalidOperationException("Log nghỉ phép này đã bị xóa");
        }

        entity.IsDeleted = true;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);

            return "Xóa thành công";
        }
        catch (Exception ex)
        {
            throw new Exception("Đã xảy ra lỗi khi xóa Leave Log");
        }
    }
}
