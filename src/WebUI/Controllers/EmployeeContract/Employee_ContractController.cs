using hrOT.Application.Common.Exceptions;
using hrOT.Application.EmployeeContracts;
using hrOT.Application.EmployeeContracts.Commands;
using hrOT.Application.EmployeeContracts.Commands.Add;
using hrOT.Application.EmployeeContracts.Commands.Delete;
using hrOT.Application.EmployeeContracts.Commands.Update;
using hrOT.Application.EmployeeContracts.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeContract;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Policy = "manager")]
public class Employee_ContractController : ApiControllerBase
{

    // Xuất danh sách hợp đồng
    [HttpGet("GetListByEmployeeId")]
    public async Task<IActionResult> GetListContract(Guid EmployeeID)
    {
        try
        {
            if (EmployeeID == Guid.Empty)
            {
                return BadRequest("Vui lòng nhập EmployeeId !");
            }

            var result = await Mediator.Send(new Employee_GetListContractQuery(EmployeeID));


            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("GetSeniority")]
    public async Task<IActionResult> GetSeniorityContract(Guid EmployeeID)
    {
        try
        {
            if (EmployeeID == Guid.Empty)
            {
                return BadRequest("Vui lòng nhập EmployeeId!");
            }

            var result = await Mediator.Send(new Employee_GetSeniorityContractQuery(EmployeeID));

            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    //Thêm hợp đồng cho nhân viên
    [HttpPost("CreateContract")]
    public async Task<IActionResult> CreateContract(Employee_CreateContractCommand commnad)
    {
        try
        {
            var result = await Mediator.Send(commnad);
            return Ok(result);
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

    //Cập nhật hợp đồng cho nhân viên
    [HttpPut("UpdateContract")]
    public async Task<IActionResult> UpdateContract(Employee_UpdateContractCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);
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

    // Xóa hợp đồng cho nhân viên
    [HttpDelete("DeleteContract")]
    public async Task<IActionResult> DeleteContract(Employee_DeleteContractCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok("Xóa thành công!");
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
}