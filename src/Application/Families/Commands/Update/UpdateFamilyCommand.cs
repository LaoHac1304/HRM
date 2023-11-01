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
public record UpdateFamilyCommand : IRequest<Guid>
{
    public Guid FamilyId { get; init; }
    //public Guid EmployeeId { get; init; }
    public string? Name { get; init; }
    public DateTime DateOfBirth { get; init; }
    public Relationship Relationship { get; init; }
    public Boolean IsDependent { get; set; }
    public string? CitizenIdentificationNumber { get; set; }
    public DateTime? CreatedDateCI { get; set; }
    public string? PlaceForCI { get; set; }
    public IFormFile? PhotoCIOnTheFront { get; set; }
    public IFormFile? PhotoCIOnTheBack { get; set; }
}

public class UpdateFamilyCommandHandler : IRequestHandler<UpdateFamilyCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public UpdateFamilyCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Guid> Handle(UpdateFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Families
        .Where(x => x.Id == request.FamilyId).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Nhân viên không tổn tại!");
        }
        entity.DateOfBirth = request.DateOfBirth;
        entity.Relationship = request.Relationship;
        entity.Name = request.Name;
        entity.IsDependent = request.IsDependent;
        entity.CitizenIdentificationNumber = request.CitizenIdentificationNumber;
        entity.CreatedDateCI = request.CreatedDateCI;
        entity.PlaceForCI = request.PlaceForCI;

        // Lấy đường dẫn tuyệt đối của thư mục wwwroot
        string wwwrootPath = _webHostEnvironment.WebRootPath;
        string relativePath = Path.Combine("Uploads", entity.EmployeeId.ToString(), "FamilyIdentityImages", entity.Id.ToString());
        string webpFolderPath = Path.Combine(wwwrootPath, relativePath);

        // Nếu tồn tại thư mục được tạo trước đó xóa nó và tạo lại cái mới
        if (Directory.Exists(webpFolderPath))
        {
            Directory.Delete(webpFolderPath, true);
        }

        // Tạo thư mục nếu chưa tồn tại
        if (!Directory.Exists(webpFolderPath))
        {
            Directory.CreateDirectory(webpFolderPath);
        }

        // Định nghĩa đường dẫn và tên file cho hình ảnh sau khi chuyển đổi sang định dạng WebP
        string photoCIOnTheFrontName = $"{Guid.NewGuid()}.webp";
        string photoCIOnTheBackName = $"{Guid.NewGuid()}.webp";
        string photoCIOnTheFrontPath = Path.Combine(webpFolderPath, photoCIOnTheFrontName);
        string photoCIOnTheBackPath = Path.Combine(webpFolderPath, photoCIOnTheBackName);

        // Chuyển đổi PhotoCIOnTheFront hình ảnh sang định dạng WebP
        using (Image frontCIImage = await Image.LoadAsync(request.PhotoCIOnTheFront.OpenReadStream(), cancellationToken))
        {
            frontCIImage.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(800, 600),
                Mode = ResizeMode.Max
            }));

            // Lưu hình ảnh dưới dạng định dạng WebP
            await frontCIImage.SaveAsync(photoCIOnTheFrontPath, cancellationToken);
        }

        // Chuyển đổi PhotoCIOnTheBack hình ảnh sang định dạng WebP
        using (Image backCIImage = await Image.LoadAsync(request.PhotoCIOnTheBack.OpenReadStream(), cancellationToken))
        {
            backCIImage.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(800, 600),
                Mode = ResizeMode.Max
            }));

            // Lưu hình ảnh dưới dạng định dạng WebP
            await backCIImage.SaveAsync(photoCIOnTheBackPath, cancellationToken);
        }

        entity.PhotoCIOnTheFront = relativePath + "/" + photoCIOnTheFrontName;
        entity.PhotoCIOnTheBack = relativePath + "/" + photoCIOnTheBackName;
        entity.LastModified = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
