using hrOT.Application.Common.Exceptions;
using hrOT.Application.EmployeeExperience.Commands;
using hrOT.Application.EmployeeExperience.Commands.Add;
using hrOT.Application.EmployeeExperience.Commands.Delete;
using hrOT.Application.Experiences.Commands;
using hrOT.Application.Experiences.Queries;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeExperience;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Policy = "ManagerOrStaff")]
public class Employee_ExperienceController : ApiControllerBase
{
    // Xuất danh sách
    [HttpGet("GetListExperience")]
    public async Task<IActionResult> GetListExperience(Guid EmployeeId)
    {
        try
        {
            if (EmployeeId == Guid.Empty)
            {
                return BadRequest("Vui lòng nhập EmployeeId!");
            }

            var result = await Mediator
            .Send(new Employee_GetListExperienceQuery(EmployeeId));

            return Ok(result);
        }catch (Exception ex)
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

    // Khởi tạo
    [HttpPost("CreateExperience")]
    public async Task<IActionResult> CreateExperience([FromForm] ExperienceCommandDTO experienceDTO, Guid EmployeeId)
    {
        try
        {
            if (EmployeeId == Guid.Empty)
            {
                return BadRequest("Vui lòng nhập ExperienceID !");
            }

            var result = await Mediator
            .Send(new Employee_ExperienceCreateCommand(EmployeeId, experienceDTO));

            return Ok(result);
        } catch (Exception ex)
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

    // Update
    [HttpPut("UpdateExperience")]
    public async Task<IActionResult> UpdateExperience(Guid experienceID, [FromForm] ExperienceCommandDTO experienceDTO)
    {
        //if (EmployeeID == Guid.Empty)
        //{
        //    return BadRequest("Vui lòng nhập EmployeeId !");
        //}
        try
        {
            if (experienceID == Guid.Empty)
            {
                return BadRequest("Vui lòng nhập ExperienceID !");
            }

            var result = await Mediator
                .Send(new Employee_ExperienceUpdateCommand(experienceID, experienceDTO));

            return Ok(result);
        }catch (Exception ex)
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

    // Xóa
    [HttpDelete("DeleteExperience")]
    public async Task<IActionResult> DeleteExperience(Guid experienceID)
    {
        try
        {
            if (experienceID == Guid.Empty)
            {
                return BadRequest("Vui lòng nhập ExperienceID !");
            }

            var result = await Mediator
                .Send(new Employee_ExperienceDeleteCommand(experienceID));

            return Ok(result);
        }catch (Exception ex)
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
}