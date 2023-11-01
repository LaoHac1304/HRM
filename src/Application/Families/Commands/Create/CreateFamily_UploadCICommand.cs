using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees.Commands.Create;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace hrOT.Application.Families.Commands.Create;
public record CreateFamily_UploadCICommand : IRequest<Unit>
{
    public Guid FamilyId { get; set; }
    public string? CitizenIdentificationNumber { get; set; }
    public DateTime? CreatedDateCI { get; set; }
    public string? PlaceForCI { get; set; }
    public IFormFile PhotoCIOnTheFront { get; set; }
    public IFormFile PhotoCIOnTheBack { get; set; }
}

public class CreateFamily_UploadCICommandHandler : IRequestHandler<CreateFamily_UploadCICommand, Unit>
{
    private readonly IApplicationDbContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateFamily_UploadCICommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Unit> Handle(CreateFamily_UploadCICommand request, CancellationToken cancellationToken)
    {
        var family = await _context.Families.FindAsync(request.FamilyId);
        if (family == null || family.IsDeleted)
        {
            throw new NotFoundException(nameof(Families), request.FamilyId);
        }

        family.CitizenIdentificationNumber = request.CitizenIdentificationNumber;
        family.CreatedDateCI = request.CreatedDateCI;
        family.PlaceForCI = request.PlaceForCI;

        // Lấy đường dẫn tuyệt đối của thư mục wwwroot
        string wwwrootPath = _webHostEnvironment.WebRootPath;
        string relativePath = Path.Combine("Uploads", family.EmployeeId.ToString(), "FamilyIdentityImages", family.Id.ToString());
        string webpFolderPath = Path.Combine(wwwrootPath, relativePath);

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

        family.PhotoCIOnTheFront = relativePath + "/" + photoCIOnTheFrontName;
        family.PhotoCIOnTheBack = relativePath + "/" + photoCIOnTheBackName;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
