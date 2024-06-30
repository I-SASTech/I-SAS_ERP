using System;
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
    public class GetChequeBookDetailQuery : IRequest<ChequeBookLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetChequeBookDetailQuery, ChequeBookLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetChequeBookDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ChequeBookLookUpModel> Handle(GetChequeBookDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var chequebook = await _context.ChequeBooks.AsNoTracking()
                        .Include(x=>x.GetChequeBookItems)
                        .FirstOrDefaultAsync(x=>x.Id==request.Id);

                    if (chequebook != null)
                    {
                        return new ChequeBookLookUpModel()
                        {
                            Id = chequebook.Id,
                            IsActive = chequebook.IsActive,
                            BankNo = chequebook.BankNo,
                            NoOfCheques = chequebook.NoOfCheques,
                            StartingNo = chequebook.StartingNo,
                            IsBlock = chequebook.IsBlock,
                            LastNo = chequebook.LastNo,
                            Date = chequebook.Date,
                            Dates = chequebook.Date.ToString("dd/MM/yyyy"),
                            Used = chequebook.Used,
                            Remaining = chequebook.Remaining,
                            Reason = chequebook.Reason,
                            BookNo = chequebook.BookNo,
                            ChequeBookItems = chequebook.GetChequeBookItems.Select(x =>
                              new ChequeBookItemLookUpModel()
                              {
                                  Id = x.Id,
                                  BookNo = x.BookNo,
                                  SerialNo = x.SerialNo,
                                  ChequeNo = x.ChequeNo,
                                  BankId = x.BankId,
                                  IsActive = x.IsActive,
                                  IsBlock = x.IsBlock,
                                  Date = x.Date,
                                  ChequeBookId = x.ChequeBookId
                              }).ToList(),
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
