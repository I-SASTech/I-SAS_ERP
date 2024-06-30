using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.DayStarts.Queries.GetOpeninigBalance
{
    public class GetBalanceOfWholeDay : IRequest<OpeningBalanceLookUpModel>
    {

        public class Handler : IRequestHandler<GetBalanceOfWholeDay, OpeningBalanceLookUpModel>
        {

            public readonly IApplicationDbContext Context;


            public readonly ILogger Logger;
            public readonly IMediator _Mediator;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<GetBalanceOfWholeDay> logger,
                IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _Mediator = mediator;
            }

            public async Task<OpeningBalanceLookUpModel> Handle(GetBalanceOfWholeDay request, CancellationToken cancellationToken)
            {
                try
                {
                    //Opening balance 
                    var opening = new OpeningBalanceLookUpModel();
                    var dayStart = Context.DayStarts.FirstOrDefault(x => x.IsDayStart && x.IsActive);

                    var lookUpModel = new List<OpeningBalanceLookUpModel>();

                    if (dayStart != null)
                    {
                        var terminalList = Context.DayStarts.Where(x => x.DayStartId == dayStart.Id).ToList();
                        foreach (var terminal in terminalList)
                        {
                            var result = await _Mediator.Send(new GetOpeningBalanceQuery()
                            {
                                Id = terminal.CounterId,
                                IsOpeningCash = false
                            });
                            lookUpModel.Add(result);
                        }
                    }
                    

                    var model = new OpeningBalanceLookUpModel()
                    {
                        OpeningCash = lookUpModel.Sum(X=>X.OpeningBalance),
                        CashInHand = lookUpModel.Sum(x => x.CashInHand) - lookUpModel.Sum(X => X.OpeningBalance),
                        Bank = lookUpModel.Sum(x => x.Bank),
                        DraftExpense = lookUpModel.Sum(x => x.DraftExpense),
                        PostExpense = lookUpModel.Sum(x => x.PostExpense),
                    };

                    

                    return model;

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
        }
    }
}
