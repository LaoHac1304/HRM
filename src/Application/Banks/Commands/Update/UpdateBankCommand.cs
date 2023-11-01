using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace hrOT.Application.Banks.Commands.Update;
public record UpdateBankCommand : IRequest<string>
{
    public Guid Id { get; init; }
    public string BankName { get; init; }
    public string Description { get; init; }
    
}
public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateBankCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
    {
        
        var bank = await _context.Banks
               .FindAsync(new object[] { request.Id }, cancellationToken);

        if (bank == null)
        {
            throw new NotFoundException($"Không tìm thấy tài khoảng ngân hàng có ID : {request.Id} ");
        }

        if (bank.IsDeleted)
        {
            throw new Exception("Tài khoản ngân hàng này đã bị xóa");
        }


        var tempBankName = bank.BankName;

        bank.BankName = request.BankName;
        bank.Description = request.Description;

        _context.Banks.Update(bank);
        await _context.SaveChangesAsync(cancellationToken);

        #region đoạn cần chuyển vào command validator thay vì ở đây
        var existBankNameCheck = await _context.Banks
                .Where(s => s.BankName == request.BankName)
                .CountAsync();

            if (existBankNameCheck > 1)
            {
                bank.BankName = tempBankName;
                _context.Banks.Update(bank);
                await _context.SaveChangesAsync(cancellationToken);

                throw new Exception("Tên ngân hàng đã tồn tại");
            }
        #endregion

        return "Cập nhật thành công";
        
    }
}
