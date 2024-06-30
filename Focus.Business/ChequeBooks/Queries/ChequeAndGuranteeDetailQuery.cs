using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.ChequeBooks.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Focus.Business.ChequeBooks.Queries
{
    public class ChequeAndGuranteeDetailQuery : IRequest<List<ChequeBookItemLookUpModel>>
    {
        public Guid BankId { get; set; }

        public class Handler : IRequestHandler<ChequeAndGuranteeDetailQuery, List<ChequeBookItemLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ChequeAndGuranteeDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<ChequeBookItemLookUpModel>> Handle(ChequeAndGuranteeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var chequebook = await _context.ChequeBookItems.AsNoTracking()
                        .Where(x=>x.BankId==request.BankId).ToListAsync();
                    var chequebookList = new List<ChequeBookItemLookUpModel>();

                    if (chequebook != null)
                    {
                        foreach (var item in chequebook)
                        {
                            chequebookList.Add(new ChequeBookItemLookUpModel()
                            {
                                Id = item.Id,
                                IsCashed = item.IsCashed,
                                IsPaid = item.IsPaid,
                                Code = item.Code,
                                IsActive = item.IsActive,
                                ReservedAccount = item.ReservedAccount,
                                BankNo = item.BankNo,
                                IsBlock = item.IsBlock,
                                Date = item.Date,
                                IssuedToName = item.IssuedToName,
                                IssuedTo = item.IssuedTo,
                                Remarks = item.Remarks,
                                ShortDetail = item.ShortDetail,
                                Amount = item.Amount,
                                BookNo = item.BookNo,
                                ChequeDate = item.ChequeDate,
                                SerialNo = item.SerialNo,
                                ChequeNo =item.ChequeNo,
                                BankId=item.BankId,
                                ValidityDate=item.ValidityDate,
                                ChequeTypes = item.ChequeType == 0 ? "" : item.ChequeType.ToString(),
                                StatusTypes = item.StatusType == 0 ? "" : item.StatusType.ToString(),
                                CashTypes = item.CashType == 0 ? "" : item.CashType.ToString(),
                                ChequeBookId =item.ChequeBookId,
                                AlertDate=item.AlertDate,
                                StatusDate=item.StatusDate,

                            });
                        }

                        return chequebookList;
                    }
                    
                    throw new NotFoundException(nameof(chequebook), request.BankId);
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
