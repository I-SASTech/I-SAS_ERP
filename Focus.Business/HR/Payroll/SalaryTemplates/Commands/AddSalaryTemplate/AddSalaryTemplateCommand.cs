using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.SalaryTemplates.Commands.AddSalaryTemplate
{
    public class AddUpdateSalaryTemplateCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public SalaryTemplateLookUp SalaryTemplate { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateSalaryTemplateCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateSalaryTemplateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateSalaryTemplateCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.SalaryTemplate.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var salaryTemplates =  Context.SalaryTemplates.AsNoTracking()
                            .Include(x => x.SalaryContributions)
                            .Include(x => x.SalaryDeductions)
                            .Include(x => x.SalaryAllowances)
                            .FirstOrDefault(x => x.Id == request.SalaryTemplate.Id);

                        if (salaryTemplates == null)
                            throw new NotFoundException("Salary Template", request.SalaryTemplate.StructureName);


                        salaryTemplates.Code = request.SalaryTemplate.Code;
                            salaryTemplates.StructureName = request.SalaryTemplate.StructureName;

                        //Salary Allowances
                            Context.SalaryAllowances.RemoveRange(salaryTemplates.SalaryAllowances);
                            foreach (var x in request.SalaryTemplate.SalaryAllowances)
                            {
                               
                                    var salaryAllowance = new SalaryAllowance()
                                    {
                                       AllowanceId = x.Id,
                                       SalaryTemplateId = request.SalaryTemplate.Id,
                                       AmountType = x.AmountType,
                                       TaxPlan = x.TaxPlan,
                                       Amount = x.Amount,
                                    };
                                    await Context.SalaryAllowances.AddAsync(salaryAllowance, cancellationToken);

                            }
                            
                            //Salary Deduction
                            Context.SalaryDeductions.RemoveRange(salaryTemplates.SalaryDeductions);
                            foreach (var x in request.SalaryTemplate.SalaryDeductions)
                            {
                               
                                    var salaryDeduction = new SalaryDeduction()
                                    {
                                       DeductionId = x.Id,
                                       SalaryTemplateId = request.SalaryTemplate.Id,
                                        AmountType = x.AmountType,
                                       TaxPlan = x.TaxPlan,
                                       Amount = x.Amount,
                                    };
                                    await Context.SalaryDeductions.AddAsync(salaryDeduction, cancellationToken);

                            }
                            
                            
                            //Salary Contribution
                            Context.SalaryContributions.RemoveRange(salaryTemplates.SalaryContributions);
                            foreach (var x in request.SalaryTemplate.SalaryContributions)
                            {
                               
                                    var salaryContribution = new SalaryContribution()
                                    {
                                       ContributionId = x.Id,
                                       SalaryTemplateId = request.SalaryTemplate.Id,
                                        AmountType = x.AmountType,
                                       Amount = x.Amount,
                                    };
                                    await Context.SalaryContributions.AddAsync(salaryContribution, cancellationToken);

                            }

                        Context.SalaryTemplates.Update(salaryTemplates);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                           
                        
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                            return salaryTemplates.Id;
                        

                    }
                    else
                    {

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var salaryTemplate = new SalaryTemplate
                        {
                          Code = request.SalaryTemplate.Code,
                          StructureName = request.SalaryTemplate.StructureName,
                          SalaryAllowances = request.SalaryTemplate.SalaryAllowances.Select(x =>
                              new SalaryAllowance()
                              {
                                  AllowanceId = x.Id,
                                  AmountType = x.AmountType,
                                  TaxPlan = x.TaxPlan,
                                  Amount = x.Amount,


                              }).ToList(),
                          SalaryDeductions = request.SalaryTemplate.SalaryDeductions.Select(x =>
                              new SalaryDeduction()
                              {
                                  DeductionId = x.Id,
                                  AmountType = x.AmountType,
                                  TaxPlan = x.TaxPlan,
                                  Amount = x.Amount,


                              }).ToList(),
                          SalaryContributions = request.SalaryTemplate.SalaryContributions.Select(x =>
                              new SalaryContribution()
                              {
                                  ContributionId = x.Id,
                                  AmountType = x.AmountType,
                                  Amount = x.Amount,


                              }).ToList(),
                        };
                        //Add Department to database
                        await Context.SalaryTemplates.AddAsync(salaryTemplate, cancellationToken);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);
                        return salaryTemplate.Id;



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
