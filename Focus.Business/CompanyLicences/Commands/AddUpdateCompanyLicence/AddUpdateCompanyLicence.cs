using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Business.Exceptions;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.CompanyLicences.Commands.AddUpdateCompanyLicence
{
    public class AddUpdateCompanyLicence : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfTransactions { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyType CompanyType { get; set; }

        public class Handler : IRequestHandler<AddUpdateCompanyLicence, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateCompanyLicence> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateCompanyLicence request, CancellationToken cancellationToken)
            {

                try
                {
                 
                    if (request.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var companyLicence = new CompanyLicence()
                        {
                            CompanyId = request.CompanyId,
                            CompanyType = request.CompanyType,
                            FromDate = request.FromDate,
                            ToDate = request.ToDate,
                            NumberOfUsers = request.NumberOfUsers,
                            NoOfTransactionsAllow = request.NumberOfTransactions,
                            IsActive = request.IsActive,
                            IsBlock = request.IsBlock
                        };

                      
                        await Context.CompanyLicences.AddAsync(companyLicence, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return companyLicence.Id;
                    }
                    else
                    {
                        var companyLicence = await Context.CompanyLicences.FindAsync(request.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (companyLicence == null)
                            throw new NotFoundException("Company Licnece", request.Id);

                        companyLicence.CompanyId = request.CompanyId;
                        companyLicence.CompanyType = request.CompanyType;
                        companyLicence.FromDate = request.FromDate;
                        companyLicence.ToDate = request.ToDate;
                        companyLicence.NumberOfUsers = request.NumberOfUsers;
                        companyLicence.NoOfTransactionsAllow = request.NumberOfTransactions;
                        companyLicence.IsActive = request.IsActive;
                        companyLicence.IsBlock = request.IsBlock;
                        companyLicence.Id = Guid.Empty;
                        //for tracking company licence history we will add here
                        await Context.CompanyLicences.AddAsync(companyLicence, cancellationToken);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);
                        return companyLicence.CompanyId;
                    }
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Something went wrong");
                }
                finally
                {
                    Context.DisableTenantFilter = false;
                }
            }
        }
    }
}
