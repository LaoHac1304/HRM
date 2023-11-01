using hrOT.Application.Common.Exceptions;
using hrOT.Application.Employees.Commands.Create;
using hrOT.Application.Employees.Commands.Delete;
using hrOT.Application.Employees.Commands.Update;
using hrOT.Application.Employees.Queries;
using hrOT.WebUI.Controllers;
using LogOT.Application.Employees;
using LogOT.Application.Employees.Commands.Create;
using LogOT.Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        //Sử dụng chính sách (policies) cho xác thực trong identity để phân quyền
        // Nó nằm ở Infrastructure/ConfigureServieces
        [Authorize(Policy = "manager")]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
            try
            {
                return await _mediator.Send(new GetAllEmployeeQuery());
            }
            catch (Exception ex)
            {               
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("Create")]
        [Authorize(Policy = "manager")]
        public async Task<IActionResult> CreateEmployee([FromForm] CreateEmployee createModel)
        {
            try
            {
                var entityId = await _mediator.Send(createModel);
                return Ok(entityId);
            }
            catch (Exception ex)
            {
                if (ex is ValidationException)
                {
                    ValidationException error = (ValidationException)ex;
                    var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                    return BadRequest(errorsDiction);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditRoleManager")]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Edit([FromForm] UpdateEmployee command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Cập nhật thành công");
            }
            catch (Exception e)
            {
                if (e is ValidationException)
                {
                    ValidationException error = (ValidationException)e;
                    var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                    return BadRequest(errorsDiction);
                }
                return BadRequest(e.Message);
            }
        }

        [HttpPut("EditRoleEmployee")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> Edit([FromForm] UpdateEmployeeRoleEmployee command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Cập nhật thành công");
            }
            catch (Exception e)
            {
                if (e is ValidationException)
                {
                    ValidationException error = (ValidationException)e;
                    var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                    return BadRequest(errorsDiction);
                }
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("[action]")]
        [Authorize(Policy = "manager")]
        public async Task<IActionResult> Delete([FromForm] DeleteEmployee command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                if (e is ValidationException)
                {
                    ValidationException error = (ValidationException)e;
                    var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                    return BadRequest(errorsDiction);
                }
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateExcel")]
        [Authorize(Policy = "manager")]
        public async Task<IActionResult> CreateEx(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    // Kiểm tra kiểu tệp tin
                    if (!IsExcelFile(file))
                    {
                        return BadRequest("Chỉ cho phép sử dụng file Excel");
                    }

                    var filePath = Path.GetTempFileName(); // Tạo một tệp tạm để lưu trữ tệp Excel
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); // Lưu tệp Excel vào tệp tạm
                    }

                    var command = new CreateEmployeeEx
                    {
                        FilePath = filePath
                    };

                    await _mediator.Send(command);
                    return Ok("Thêm thành công!");
                }
                return BadRequest("Thêm thất bại! File không được để trống.");

            }
            catch (Exception ex)
            {
                if (ex is ValidationException)
                {
                    ValidationException error = (ValidationException)ex;
                    var errorsDiction = new Dictionary<string, string[]>(error.Errors);
                    return BadRequest(errorsDiction);
                }
                return BadRequest(ex.Message);
            }
        }

        private bool IsExcelFile(IFormFile file)
        {
            // Kiểm tra phần mở rộng của tệp tin có phải là .xls hoặc .xlsx không
            var allowedExtensions = new[] { ".xls", ".xlsx" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }

        [HttpGet("GetEmployeeById")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> GetEmployee(Guid EmployeeId)
        {
            try
            {
                var query = new Employee_GetEmployeeQuery(EmployeeId);
                var employeeVm = await _mediator.Send(query);

                if (employeeVm == null)
                {
                    return BadRequest("Không tìm thấy nhân viên");
                }
                else
                {
                    return Ok(employeeVm);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UploadCV")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> UploadCV(IFormFile cvFile, Guid EmployeeId)
        {
            try
            {

                if (cvFile == null || cvFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy file");
                }
                if (!IsCVFile(cvFile))
                {
                    return BadRequest("Bạn phải sử dụng file pdf hoặc doc");
                }
                var command = new Employee_EmployeeUploadCVCommand
                {
                    CVFile = cvFile,
                    EmployeeId = EmployeeId
                };

                await _mediator.Send(command);
                return Ok("Cập nhật CV thành công");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest(ex.Message);
            }
        }
        private bool IsCVFile(IFormFile file)
        {
            // Kiểm tra phần mở rộng của tệp tin có phải là cv không
            var allowedExtensions = new[] { ".pdf", ".doc" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }


        [HttpPost("UploadImage")]
        //[Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile, Guid EmployeeId)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy hình ảnh");
                }
                if (!IsImageFile(imageFile))
                {
                    return BadRequest("Bạn phải sử dụng file hình ảnh");
                }
                var command = new UpLoadImage
                {
                    File = imageFile,
                    EmployeeId = EmployeeId
                };

                await _mediator.Send(command);

                return Ok("Cập nhật ảnh đại diện thành công");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest(ex.Message);
            }
        }

        private bool IsImageFile(IFormFile file)
        {
            // Kiểm tra phần mở rộng của tệp tin có phải là hình ảnh không
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" ,".webp" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }

        [HttpPost("UploadIdentityImage")]
        [Authorize(Policy = "employee")]
        public async Task<IActionResult> UploadIdentityImage(Guid id, IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy hình ảnh");
                }
                if (!IsImageFile(imageFile))
                {
                    return BadRequest("Bạn phải sử dụng file hình ảnh");
                }

                var command = new UploadIdentityImage
                {
                    Id = id,
                    File = imageFile
                };

                await _mediator.Send(command);

                return Ok("Cập nhật ảnh căn cước thành công");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest(ex.Message);
            }
        }
        /*
        [HttpPost("{id}/uploadDiploma")]
        [Authorize(Policy = "employee")]
        public async Task<IActionResult> UploadDiploma(Guid id, IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy hình ảnh");
                }

                var command = new UpLoadDiploma
                {
                    Id = id,
                    File = imageFile
                };

                await _mediator.Send(command);

                return Ok("Cập nhật ảnh bằng cấp thành công");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest("Lỗi cập nhật hình ảnh");
            }
        }*/
    }
}