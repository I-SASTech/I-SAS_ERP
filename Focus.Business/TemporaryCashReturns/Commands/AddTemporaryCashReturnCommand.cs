using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashReturns.Commands
{
    public class AddTemporaryCashReturnCommand : IRequest<Guid>
    {
        public TemporaryCashReturnLookUp TemporaryCashReturn { get; set; }

        public class Handler : IRequestHandler<AddTemporaryCashReturnCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddTemporaryCashReturnCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddTemporaryCashReturnCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.TemporaryCashReturn.Id == Guid.Empty)
                    {
                        string registrationNo;
                        var autoCode = await _context.TemporaryCashReturns.OrderBy(x => x.RegistrationNo).LastOrDefaultAsync(cancellationToken: cancellationToken);
                        if (autoCode != null)
                        {
                            registrationNo= GenerateNewCode(autoCode.RegistrationNo);
                        }
                        else
                        {
                            registrationNo = GenerateCodeFirstTime();
                        }

                        var temporaryCashReturn = new TemporaryCashReturn()
                        {
                            RegistrationNo = registrationNo,
                            Date = request.TemporaryCashReturn.Date,
                            TemporaryCashIssueId = request.TemporaryCashReturn.TemporaryCashIssueId,
                            Amount = request.TemporaryCashReturn.Amount,
                            UserId = request.TemporaryCashReturn.UserId,
                            IsCashRequesterUser = request.TemporaryCashReturn.IsCashRequesterUser,
                            Description = request.TemporaryCashReturn.Description,
                        };

                        await _context.TemporaryCashReturns.AddAsync(temporaryCashReturn, cancellationToken);

                        
                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                           DocumentNo=temporaryCashReturn.RegistrationNo,
                            Code = _code,
                        }, cancellationToken);
                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();
                        return temporaryCashReturn.Id;
                    }

                    return Guid.Empty;


                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
            public string GenerateCodeFirstTime()
            {
                return "TCR-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(4);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "TCR-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
