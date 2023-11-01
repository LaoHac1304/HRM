using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees.Commands.Create;
using hrOT.Application.Employees;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.Families.Commands.Create;
public record CreateFamily_UploadInfoCommand : IRequest<string>
{
    public Guid EmployeeId { get; set; }
    public string? Name { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public Relationship Relationship { get; init; }
    public Boolean IsDependent { get; set; }
}

public class CreateFamily_UploadInfoCommandHandler : IRequestHandler<CreateFamily_UploadInfoCommand, string>
{
    private readonly IApplicationDbContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateFamily_UploadInfoCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> Handle(CreateFamily_UploadInfoCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(request.EmployeeId);

        if (employee == null || employee.IsDeleted)
        {
            throw new NotFoundException(nameof(Employee), request.EmployeeId);
        }

        var entity = new Family()
        {
            EmployeeId = request.EmployeeId,
            DateOfBirth = request.DateOfBirth,
            Name = request.Name,
            Relationship = request.Relationship,
            IsDependent = request.IsDependent
        };

        _context.Families.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id.ToString();
    }
}
