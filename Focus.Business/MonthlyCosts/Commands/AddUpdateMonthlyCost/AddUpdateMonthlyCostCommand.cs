using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.MonthlyCosts.Commands.AddUpdateMonthlyCost
{
    public class AddUpdateMonthlyCostCommand : IRequest<Guid>
    {
        //Get all variable in entity
       

        public MonthlyCostLookUpModel monthlyCost { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateMonthlyCostCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateMonthlyCostCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateMonthlyCostCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.monthlyCost.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        //Data already exist ; Update data
                        //get data from database using id

                        var monthlyCosts =  Context.MonthlyCosts.AsNoTracking().Include(x => x.MonthlyCostItems)
                        .FirstOrDefault(x => x.Id == request.monthlyCost.Id);

                        monthlyCosts.MonthlyRent = request.monthlyCost.MonthlyRent;
                        monthlyCosts.MonthlySaleries = request.monthlyCost.MonthlySaleries;
                        monthlyCosts.MonthlyUtilityBills = request.monthlyCost.MonthlyUtilityBills;
                        monthlyCosts.MonthlyGovtFees = request.monthlyCost.MonthlyGovtFees;
                        monthlyCosts.MonthlyGovtZakat = request.monthlyCost.MonthlyGovtZakat;
                        monthlyCosts.GovtFeeForLabour = request.monthlyCost.GovtFeeForLabour;
                        Context.MonthlyCostItems.RemoveRange(monthlyCosts.MonthlyCostItems);
                        monthlyCosts.BranchId = request.monthlyCost.BranchId;
                        monthlyCosts.MonthlyCostItems = request.monthlyCost.MonthlyCostItems.Select(x =>
                               new MonthlyCostItem()
                               {
                                   Description = x.Description,
                                   MonthlyCosts = x.MonthlyCosts,
                                   YearlyCost = x.YearlyCost,
                                   Daily = x.Daily,
                                   Total = x.Total,

                               }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        // Return Department id after successfully updating data
                        return monthlyCosts.Id;
                        //Check Department exist

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var monthlyCost = new MonthlyCost
                        {
                            MonthlyRent = request.monthlyCost.MonthlyRent,
                            Date = request.monthlyCost.Date,
                            MonthlySaleries = request.monthlyCost.MonthlySaleries,
                            MonthlyUtilityBills = request.monthlyCost.MonthlyUtilityBills,
                            MonthlyGovtFees = request.monthlyCost.MonthlyGovtFees,
                            MonthlyGovtZakat = request.monthlyCost.MonthlyGovtZakat,
                            GovtFeeForLabour = request.monthlyCost.GovtFeeForLabour,
                            BranchId = request.monthlyCost.BranchId,
                            MonthlyCostItems = request.monthlyCost.MonthlyCostItems.Select(x =>
                                new MonthlyCostItem()
                                {
                                    Description = x.Description,
                                    MonthlyCosts = x.MonthlyCosts,
                                    YearlyCost = x.YearlyCost,
                                    Daily = x.Daily,
                                    Total = x.Total,

                                }).ToList()
                        };

                        //Add Department to database
                        await Context.MonthlyCosts.AddAsync(monthlyCost, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);
                        await Context.SaveChangesAsync(cancellationToken);
                        return monthlyCost.Id;
                    }



                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Department Name Already Exist");
                }
            }
        }
    }
}
