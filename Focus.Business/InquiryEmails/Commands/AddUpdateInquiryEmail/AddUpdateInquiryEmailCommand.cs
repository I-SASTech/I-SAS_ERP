using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryEmails.Queries.GetInquiryEmailDetails;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryEmails.Commands.AddUpdateInquiryEmail
{
    public class AddUpdateInquiryEmailCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public InquiryEmailDetailsLookUpModel EmailDetails { get; set; }
       

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInquiryEmailCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateInquiryEmailCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateInquiryEmailCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var inquiryEmail = new InquiryEmail()
                    {
                        Subject = request.EmailDetails.Subject,
                        IsReceived = request.EmailDetails.IsReceived,
                        Description = request.EmailDetails.Description,
                        InquiryId = request.EmailDetails.InquiryId,
                        DateTime = DateTime.Now
                    };

                    //Add Department to database
                    await Context.InquiryEmails.AddAsync(inquiryEmail, cancellationToken);
                    var emailCcList = new List<InquiryEmailCc>();
                    foreach (var emailCc in request.EmailDetails.InquiryEmailCcDetails)
                    {
                        emailCcList.Add(new InquiryEmailCc()
                        {
                            Email = emailCc.Email,
                            InquiryEmailId = inquiryEmail.Id
                        });
                    }

                    await Context.InquiryEmailCcs.AddRangeAsync(emailCcList, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return inquiryEmail.Id;

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
