using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands
{
    public class AddUpdateSalaryTaxSlab : IRequest<Guid>
    {
        public SalaryTaxSlabLookUpModel SalaryTaxSlab { get; set; }

        //Get all variable in entity

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateSalaryTaxSlab, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateSalaryTaxSlab> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateSalaryTaxSlab request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.SalaryTaxSlab.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var taxSalary = await Context.TaxSalaries.FindAsync(request.SalaryTaxSlab.Id);

                        Context.SalaryTaxSlabs.RemoveRange(taxSalary.SalaryTaxSlabs);


                        taxSalary.FromDate = request.SalaryTaxSlab.FromDate;
                        taxSalary.ToDate = request.SalaryTaxSlab.ToDate;

                        foreach (var taxSlabSalary in request.SalaryTaxSlab.SalaryTaxSlabList)
                        {

                            var taxSlab = new SalaryTaxSlab()
                            {
                                TaxSalaryId = taxSalary.Id,
                                IncomeFrom = taxSlabSalary.IncomeFrom,
                                IncomeTo = taxSlabSalary.IncomeTo,
                                FixedTax = taxSlabSalary.FixedTax,
                                Rate = taxSlabSalary.Rate
                            };
                            await Context.SalaryTaxSlabs.AddAsync(taxSlab, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return taxSalary.Id;

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var taxSalary = new TaxSalary
                        {
                            //TaxYear = request.TaxYear,
                            IsActive = true,
                            FromDate = request.SalaryTaxSlab.FromDate,
                            ToDate = request.SalaryTaxSlab.ToDate
                        };
                        await Context.TaxSalaries.AddAsync(taxSalary, cancellationToken);

                        foreach (var taxSlabSalary in request.SalaryTaxSlab.SalaryTaxSlabList)
                        {

                            var taxSlab = new SalaryTaxSlab()
                            {
                                TaxSalaryId = taxSalary.Id,
                                IncomeFrom = taxSlabSalary.IncomeFrom,
                                IncomeTo = taxSlabSalary.IncomeTo,
                                FixedTax = taxSlabSalary.FixedTax,
                                Rate = taxSlabSalary.Rate
                            };
                            await Context.SalaryTaxSlabs.AddAsync(taxSlab, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Add Department to database
                        await Context.SaveChangesAsync(cancellationToken);
                        return taxSalary.Id;
                    }

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
