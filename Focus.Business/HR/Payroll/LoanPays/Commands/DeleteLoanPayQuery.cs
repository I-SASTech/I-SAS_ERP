using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.LoanPays.Commands
{
   public class DeleteLoanPayQuery : IRequest<Guid>
   {
       public Guid Id { get; set; }

       public class Handler : IRequestHandler<DeleteLoanPayQuery, Guid>
       {
           //Create an object of IApplicationDbContext for delete the data from database
           public readonly IApplicationDbContext _context;

           //Create object of logger for all type of message show
           public readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<DeleteLoanPayQuery> logger, IMediator mediator)
            {
               _context = context;
               _logger = logger;
                _mediator = mediator;
           }
           public async Task<Guid> Handle(DeleteLoanPayQuery request, CancellationToken cancellationToken)
           {
               //Get Data from database in specific id which we want to delete
               var loanPay = await _context.LoanPays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken); 
              

                try
               {
                   if (loanPay != null)
                   {
                        var loanPayment = await _context.LoanPayments.FirstOrDefaultAsync(x => x.Id == loanPay.LoanPaymentId, cancellationToken);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (loanPayment != null)
                       {
                           loanPayment.RemainingLoan = loanPayment.RemainingLoan + loanPay.Amount;
                           loanPayment.Payment = loanPayment.Payment + loanPay.Amount;

                            _context.LoanPayments.Update(loanPayment);
                       }
                       _context.LoanPays.Remove(loanPay);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                   }
                   return request.Id;
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
