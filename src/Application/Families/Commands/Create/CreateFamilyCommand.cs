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
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Families.Commands.Create;
public record CreateFamilyCommand : IRequest<string>
{
    public Guid EmployeeId { get; init; }
    public string? Name { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public Relationship Relationship { get; init; }
    public Boolean IsDependent { get; set; }
    public string? CitizenIdentificationNumber { get; set; }
    public DateTime? CreatedDateCI { get; set; }
    public string? PlaceForCI { get; set; }
    public IFormFile PhotoCIOnTheFront { get; set; }
    public IFormFile PhotoCIOnTheBack { get; set; }
}

public class CreateFamilyCommandHandler : IRequestHandler<CreateFamilyCommand, string>
{
    private readonly IApplicationDbContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateFamilyCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> Handle(CreateFamilyCommand request, CancellationToken cancellationToken)
    {
        //if (request.EmployeeId != null)
        //{
        //    var position = await _context.Employees.FindAsync(request.EmployeeId);
        //    if (position == null)
        //    {
        //        throw new NotFoundException(nameof(Employee), request.EmployeeId);
        //    }
        //}
        var employeeId = request.EmployeeId;

        var employee = await _context.Employees
            .Include(a => a.ApplicationUser)
            .Where(e => e.Id == request.EmployeeId && e.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (employee == null) { return "Id nhân viên không tồn tại"; }
        //if (employee.IsDeleted) { return "Nhân viên này đã bị xóa"; }

        var entity = new Family()
        {
            EmployeeId = employeeId,
            DateOfBirth = request.DateOfBirth,
            Name = request.Name,
            Relationship = request.Relationship,
            IsDependent = request.IsDependent,
            CitizenIdentificationNumber = request.CitizenIdentificationNumber,
            CreatedDateCI = request.CreatedDateCI,
            PlaceForCI = request.PlaceForCI
        };

        // Lấy đường dẫn tuyệt đối của thư mục wwwroot
        string wwwrootPath = _webHostEnvironment.WebRootPath;
        string relativePath = Path.Combine("Uploads", request.EmployeeId.ToString(), "FamilyIdentityImages", entity.Id.ToString());
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

        entity.PhotoCIOnTheFront = relativePath + "/" + photoCIOnTheFrontName;
        entity.PhotoCIOnTheBack = relativePath + "/" + photoCIOnTheBackName;

        _context.Families.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return "Thêm thành công";
    }
}
