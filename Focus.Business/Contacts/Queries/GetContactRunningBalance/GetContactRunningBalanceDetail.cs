using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Contacts.Queries.GetContactDetails;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Focus.Business.Contacts.Queries.GetContactRunningBalance
{
    public class GetContactRunningBalanceDetail : IRequest<decimal>
    {
        public Guid AccountId { get; set; }
        public DateTime? Date { get; set; }

        public class Handler : IRequestHandler<GetContactRunningBalanceDetail, decimal>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetContactDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<decimal> Handle(GetContactRunningBalanceDetail request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Date != null)
                    {
                        return _context.Transactions.Where(x => x.AccountId == request.AccountId && x.Date < request.Date).Sum(x => x.Debit - x.Credit);

                    }
                    else
                    {

                        return _context.Transactions.Where(x => x.AccountId == request.AccountId).Sum(x => x.Debit - x.Credit);

                    }
                    //var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);

                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
        }
    }
}
