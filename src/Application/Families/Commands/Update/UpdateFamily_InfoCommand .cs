using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees.Commands.Update;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Families.Commands.Update;
public record UpdateFamily_InfoCommand : IRequest<bool>
{
    public Guid FamilyId { get; init; }
    public string? Name { get; init; }
    public DateTime DateOfBirth { get; init; }
    public Relationship Relationship { get; init; }
    public Boolean IsDependent { get; set; }
}

public class UpdateFamily_InfoCommandHandler : IRequestHandler<UpdateFamily_InfoCommand, bool>
{
    private readonly IApplicationDbContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public UpdateFamily_InfoCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> Handle(UpdateFamily_InfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Families
        .Where(x => x.Id == request.FamilyId).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException(nameof(Family), request.FamilyId);
        }
        entity.DateOfBirth = request.DateOfBirth;
        entity.Relationship = request.Relationship;
        entity.Name = request.Name;
        entity.IsDependent = request.IsDependent;

        entity.LastModified = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
