using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees.Commands.Update
{
    public record UploadIdentityImage : IRequest
    {
        public Guid Id { get; set; }
        public bool IsBackFace { get; set; }
        public required IFormFile File { get; set; }
    }

    public class UploadIdentityImageHandler : IRequestHandler<UploadIdentityImage>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        [Obsolete]
        private readonly IHostingEnvironment _environment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadIdentityImageHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _environment = environment;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Unit> Handle(UploadIdentityImage request, CancellationToken cancellationToken)
        {

            var entity = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if(request.File == null)
            {
                throw new BadHttpRequestException("File is null");
            }

            if (entity == null)
            {
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            // Lấy đường dẫn tuyệt đối của thư mục wwwroot
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string relativePath = Path.Combine("Uploads\\" + request.Id + "\\IdentityImages\\");
            string webpFolderPath = Path.Combine(wwwrootPath, relativePath);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(webpFolderPath))
            {
                Directory.CreateDirectory(webpFolderPath);
            }

            // Định nghĩa đường dẫn và tên file cho hình ảnh sau khi chuyển đổi sang định dạng WebP
            string webpImageFileName = $"{Guid.NewGuid()}.webp";
            string webpImagePath = Path.Combine(webpFolderPath, webpImageFileName);

            // Chuyển đổi hình ảnh sang định dạng WebP
            using (Image image = await Image.LoadAsync(request.File.OpenReadStream(), cancellationToken))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 600),
                    Mode = ResizeMode.Max
                }));

                // Lưu hình ảnh dưới dạng định dạng WebP
                await image.SaveAsync(webpImagePath, cancellationToken);
            }

            if(request.IsBackFace)
            {
                entity.PhotoCIOnTheBack = relativePath + webpImageFileName;
            }
            else
            {
                entity.PhotoCIOnTheFront = relativePath + webpImageFileName;
            }

            //entity.IdentityImage = "/IdentityImages/" + webpImageFileName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
