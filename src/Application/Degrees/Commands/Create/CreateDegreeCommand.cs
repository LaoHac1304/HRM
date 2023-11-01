using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.EmployeeContracts.Commands.Add;
using hrOT.Application.OvertimeLogs.Commands.Create;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using hrOT.Application.Degrees.Commands.Update;

namespace hrOT.Application.Degrees.Commands.Create;

public record CreateDegreeCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; init; }
    public string Name { get; init; }
    public DegreeStatus Status { get; init; }
    public TypeOfDegree Type { get; set; }
    public IFormFile? Photo { get; init; }
}

public class CreateDegreeCommandHandler : IRequestHandler<CreateDegreeCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateDegreeCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Guid> Handle(CreateDegreeCommand request, CancellationToken cancellationToken)
    {
        var validateEmpId = await _context.Employees.FindAsync(new object[] { request.EmployeeId }, cancellationToken);
        if (validateEmpId == null)
        {
            throw new NotFoundException("Employee ID không hợp lệ");
        }
        else
        {
            var existdegree = await _context.Degrees
            .FirstOrDefaultAsync(d => d.EmployeeId == request.EmployeeId && d.Name == request.Name);
            if (existdegree == null)
            {
                var entity = new Degree();
                entity.EmployeeId = request.EmployeeId;
                entity.Name = request.Name;
                entity.CreatedBy = "test";
                entity.LastModified = DateTime.Now;
                entity.LastModifiedBy = "test";
                entity.Status = request.Status;
                entity.Type = request.Type;
                if (request.Photo != null)
                    entity.Photo = UploadFile(request, cancellationToken).Result;
                _context.Degrees.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
            throw new Exception("Bằng cấp này của nhân viên đã tồn tại");
        }

    }
    private async Task<string> UploadFile(CreateDegreeCommand request, CancellationToken cancellationToken)
    {

        var file = request.Photo;
        if (file != null && file.Length > 0)
        {
            // Generate a unique file name
            var newFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string relativePath = Path.Combine("Uploads\\" + request.EmployeeId + "\\Degrees\\");
            string webpFolderPath = Path.Combine(wwwrootPath, relativePath);

            if (!Directory.Exists(webpFolderPath))
            {
                Directory.CreateDirectory(webpFolderPath);

            }

            string webpImageFileName = $"{Guid.NewGuid()}.webp";
            string webpImagePath = Path.Combine(webpFolderPath, webpImageFileName);

            using (Image image = await Image.LoadAsync(request.Photo.OpenReadStream(), cancellationToken))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 600),
                    Mode = ResizeMode.Max
                }));

                // Lưu hình ảnh dưới dạng định dạng WebP
                await image.SaveAsync(webpImagePath, cancellationToken);
            }
            // Save the file to the specified path
            // Update the ContractPath property of the employee
            return await Task.FromResult(relativePath + webpImageFileName);
        }

        return null;
    }
}
