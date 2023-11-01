using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace hrOT.Application.Employees.Commands.Create;

public class Employee_EmployeeUploadCVCommand : IRequest
{
    //public Guid Id { get; set; }
    public IFormFile CVFile { get; set; }

    public Guid EmployeeId { get; set; }
}

public class Employee_EmployeeUploadCVHandler : IRequestHandler<Employee_EmployeeUploadCVCommand>
{
    private readonly IApplicationDbContext _context;
    //private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Employee_EmployeeUploadCVHandler(IApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        //_configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Unit> Handle(Employee_EmployeeUploadCVCommand request, CancellationToken cancellationToken)
    {

        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = request.EmployeeId;
        var employee = await _context.Employees.FindAsync(employeeId);

        if (employee == null || employee.IsDeleted)
        {
            throw new NotFoundException("Không tìm thấy nhân viên");
        }

        var file = request.CVFile;
        if (file != null && file.Length > 0)
        {
            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Get the current directory
            // Specify the directory to save the CV files
            //string uploadDirectory = _configuration.GetSection("UploadDirectory").Value;
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            var relativePath = Path.Combine("Uploads\\" + employeeId + "\\CV");
            var uploadDirectory = Path.Combine(wwwrootPath, relativePath);

            //Ensure the upload directory exists
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }
            // Combine the directory and file name to get the full file path
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Save the file to the specified path
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            // Update the CVPath property of the employee
            employee.CVPath = relativePath + fileName;

            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}