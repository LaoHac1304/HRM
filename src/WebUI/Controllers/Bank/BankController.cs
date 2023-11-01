using hrOT.Application.Banks.Queries;
using hrOT.Application.Banks;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Banks.Commands.Create;
using hrOT.Application.Banks.Commands.Update;
using hrOT.Application.Banks.Commands.Delete;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.Banks.Commands;
using Duende.IdentityServer.Extensions;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebUI.Controllers.Bank;
[Route("api/[controller]")]
[ApiController]
public class BankController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public BankController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<BankDTO>>> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllBankQuery());
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("Create")]
    [Authorize(Policy = "manager")]    
    public async Task<IActionResult> AddBank(CreateBankCommand command)
    {        
        try
        {
            var result = await _mediator.Send(command);
            return Ok("Thêm thành công");
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
    [HttpPut("Update")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> UpdateBank(UpdateBankCommand command)
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
    [HttpDelete("Delete")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> Delete(DeleteBankCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Xóa thành công");
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
