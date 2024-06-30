using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.ChequeBooks.Commands;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ChequeBooks.Queries
{
    public class ChequeBookListQuery : PagedRequest, IRequest<PagedResult<List<ChequeBookLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<ChequeBookListQuery, PagedResult<List<ChequeBookLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ChequeBookListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ChequeBookLookUpModel>>> Handle(ChequeBookListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var query = _context.ChequeBooks.AsNoTracking()
                        .Include(x=>x.Bank)
                        .Include(x=>x.GetChequeBookItems)
                        .Where(x=>x.BankId==request.Id)
                        .AsQueryable();

                    if (request.IsDropDown)
                    {
                        query = query.Where(x => x.IsActive);
                        var chequebook = new List<ChequeBookLookUpModel>();
                        foreach (var item in query)
                        {
                            chequebook.Add(new ChequeBookLookUpModel()
                            {
                                Id = item.Id,
                                IsActive = item.IsActive,
                                BankNo = item.BankNo,
                                NoOfCheques = item.NoOfCheques,
                                StartingNo = item.StartingNo,
                                IsBlock = item.IsBlock,
                                Blocked = item.GetChequeBookItems.Count(x=>x.IsBlock),
                                LastNo = item.LastNo,
                                Date = item.Date,
                                Dates = item.Date.ToString("dd/MM/yyyy"),
                                Used = item.Used,
                                Remaining = item.Remaining,
                                Reason = item.Reason,
                                BookNo = item.BookNo,
                            });
                        }

                        return new PagedResult<List<ChequeBookLookUpModel>>
                        {
                            Results = chequebook,
                        };
                    }

                 


                    var list = new List<ChequeBookLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new ChequeBookLookUpModel()
                        {
                            Id = item.Id,
                            IsActive = item.IsActive,
                            BankNo = item.BankNo,
                            NoOfCheques = item.NoOfCheques,
                            StartingNo = item.StartingNo,
                            IsBlock = item.IsBlock,
                            LastNo = item.LastNo,
                            Date = item.Date,
                            UsedCheck = item.GetChequeBookItems.Count(x => x.IsUsed),
                            Blocked = item.GetChequeBookItems.Count(x => x.IsBlock),
                            Dates = item.Date.ToString("dd/MM/yyyy"),
                            Used = item.Used,
                            Remaining = item.Remaining,
                            Reason = item.Reason,
                            BookNo = item.BookNo,
                        });
                    }

                    return new PagedResult<List<ChequeBookLookUpModel>>
                    {
                        Results = list
                    };





                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
