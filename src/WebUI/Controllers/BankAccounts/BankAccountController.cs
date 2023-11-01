using hrOT.Application.BankAccounts.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.BankAccounts.Commands;
using hrOT.Application.BankAccounts.Commands.Create;
using hrOT.Application.BankAccounts.Commands.Update;
using hrOT.Application.BankAccounts.Commands.Delete;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using hrOT.Application.BankAccounts;
using hrOT.Domain.Entities;
using Duende.IdentityServer.Extensions;
using ValidationException = hrOT.Application.Common.Exceptions.ValidationException;
using hrOT.Application.Families.Queries;
using hrOT.Application.Families;

namespace WebUI.Controllers.BankAccounts;
[Route("api/[controller]")]
[ApiController]
public class BankAccountController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public BankAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<BankAccountDTO>>> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllBankAccountQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("Create")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> Create(CreateBankAccountCommand command)
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
    public async Task<ActionResult> UpdateStatus(UpdateBankAccountCommand command)
    {
        
        try
        {
            var result = await _mediator.Send(command);
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
    public async Task<ActionResult> Delete([FromForm] DeleteBankAccountCommand command)
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
