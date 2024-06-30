using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.ChequeBooks.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ChequeBooks.ChequeAndGurantee
{
    public class GetChequeGuranteeDetailQuery : IRequest<ChequeBookItemLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetChequeGuranteeDetailQuery, ChequeBookItemLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetChequeGuranteeDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ChequeBookItemLookUpModel> Handle(GetChequeGuranteeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var chequebook = await _context.ChequeBookItems.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == request.Id);
                    
                    if (chequebook != null)
                    {
                        var attachments = _context.Attachments
                      .AsNoTracking()
                      .Where(x => x.DocumentId == chequebook.Id)
                      .AsQueryable();

                        var attachmentList = await attachments.Select(x => new AttachmentLookUpModel
                        {
                            Id = x.Id,
                            DocumentId = x.DocumentId,
                            Date = x.Date,
                            Description = x.Description,
                            FileName = x.FileName,
                            Path = x.Path
                        }).ToListAsync(cancellationToken: cancellationToken);
                        return new ChequeBookItemLookUpModel()
                        {
                            Id = chequebook.Id,
                            IsPaid = chequebook.IsPaid,
                            IsCashed = chequebook.IsCashed,
                            Code = chequebook.Code,
                            BookNo = chequebook.BookNo,
                            SerialNo = chequebook.SerialNo,
                            BankNo = chequebook.BankNo,
                            ChequeNo = chequebook.ChequeNo,
                            IsActive = chequebook.IsActive,
                            IsBlock = chequebook.IsBlock,
                            Date = chequebook.Date,
                            BankId = chequebook.BankId,
                            ReservedAccount = chequebook.ReservedAccount,
                            ChequeDate = chequebook.ChequeDate,
                            AlertDate = chequebook.AlertDate,
                            StatusDate = chequebook.StatusDate,
                            ValidityDate = chequebook.ValidityDate,
                            IssuedTo = chequebook.IssuedTo,
                            Remarks = chequebook.Remarks,
                            IssuerAccount = chequebook.IssuerAccount,
                            IssuedToName = chequebook.IssuedToName,
                            ShortDetail = chequebook.ShortDetail,
                            Amount = chequebook.Amount,
                            ChequeTypeDate = chequebook.ChequeTypeDate,
                            ChequeType = chequebook.ChequeType,
                            ChequeTypes = chequebook.ChequeType == 0 ? "" : chequebook.ChequeType.ToString(),
                            StatusType = chequebook.StatusType,
                            StatusTypes = chequebook.StatusType == 0 ? "" : chequebook.StatusType.ToString(),
                            CashType = chequebook.CashType,
                            CashTypes = chequebook.CashType == 0 ? "" : chequebook.CashType.ToString(),
                            ChequeBookId = chequebook.ChequeBookId,
                            AttachmentList = attachmentList,


                        };
                    }
                    throw new NotFoundException(nameof(chequebook), request.Id);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
