using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.CompaniesOption.Commands.AddUpdateOption
{
    public class AddUpdateOptionCommand : IRequest<bool>
    {
        public CompanyOptionLookUp CompanyOption { get; set; }

        public class Handler : IRequestHandler<AddUpdateOptionCommand, bool>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateOptionCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<bool> Handle(AddUpdateOptionCommand request, CancellationToken cancellationToken)
            {

                try
                {

                    var companyOptions = await Context.CompanyOptions.FirstOrDefaultAsync(x=>x.LocationId == request.CompanyOption.LocationId && x.Label==request.CompanyOption.Label,cancellationToken: cancellationToken);


                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (companyOptions != null)
                    {
                        companyOptions.OptionValue = request.CompanyOption.OptionValue;
                        companyOptions.Value = request.CompanyOption.Value;
                        companyOptions.LocationId = request.CompanyOption.LocationId;
                    }
                    else
                    {
                        //Add New
                        var companyOption = new CompanyOption()
                        {
                            LocationId = request.CompanyOption.LocationId,
                            Label = request.CompanyOption.Label,
                            Value = request.CompanyOption.Value,
                            OptionValue = request.CompanyOption.OptionValue,
                        };
                        await Context.CompanyOptions.AddAsync(companyOption, cancellationToken);

                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    //Add Department to database
                    await Context.SaveChangesAsync(cancellationToken);

                    return true;

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
