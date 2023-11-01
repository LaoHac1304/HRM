using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees.Commands.Create;
using hrOT.Application.Degrees.Commands.Update;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.Degrees.Commands.Update;
public record UpdateDegreeCommand : IRequest<string>
{
    public Guid Id { get; init; }

    public string Name { get; init; }
    public DegreeStatus Status { get; init; }
    public TypeOfDegree Type { get; set; }
    public IFormFile? Photo { get; init; }

}

public class UpdateDegreeCommandHandler : IRequestHandler<UpdateDegreeCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public UpdateDegreeCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> Handle(UpdateDegreeCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Degrees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null || entity.IsDeleted)
        {
            throw new NotFoundException(nameof(Degrees), request.Id);
        }
        entity.Name = request.Name;
        entity.Status = request.Status;
        entity.Type = request.Type;
        if(request.Photo != null) { 
            entity.Photo = await UploadFile(request.Photo, entity.EmployeeId, cancellationToken);
        }
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id.ToString();

    }
    private async Task<string> UploadFile(IFormFile photo,Guid employeeId , CancellationToken cancellationToken)
    {

        var file = photo;
        if (file != null && file.Length > 0)
        {
            // Generate a unique file name
            var newFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string relativePath = Path.Combine("Uploads\\" + employeeId + "\\Degrees\\");
            string webpFolderPath = Path.Combine(wwwrootPath, relativePath);

            if (!Directory.Exists(webpFolderPath))
            {
                Directory.CreateDirectory(webpFolderPath);

            }

            string webpImageFileName = $"{Guid.NewGuid()}.webp";
            string webpImagePath = Path.Combine(webpFolderPath, webpImageFileName);

            using (Image image = await Image.LoadAsync(photo.OpenReadStream(), cancellationToken))
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