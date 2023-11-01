using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace hrOT.Application.Banks.Commands.Create;
public record CreateBankCommand : IRequest<Guid>
{
    public string BankName { get; set; }
    public string Description { get; set; }
    //public BankCommandDTO BankDTO { get; set; }

    /*public CreateBankCommand(BankCommandDTO bankDTO)
    {
        BankDTO = bankDTO;
    }*/
}
public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateBankCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        /*var entity = new Bank();
        entity.BankName = request.BankDTO.BankName;
        entity.Description = request.BankDTO.Description;
        entity.CreatedBy = "test";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        _context.Banks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;*/
        
            var bank = new Bank
            {
                Id = new Guid(),
                BankName = request.BankName,
                Description = request.Description
            };

            await _context.Banks.AddAsync(bank);
            await _context.SaveChangesAsync(cancellationToken);
            return bank.Id;
        
    }
}
