using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.QuotationTemplates.Commands.AddQuotationTemplate
{
    public class AddUpdateQuotationTemplateCommand : IRequest<Message>
    {
        //Get all variable in entity
        public QuotationTemplateLookUp QuotationTemplate { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateQuotationTemplateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateQuotationTemplateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Message> Handle(AddUpdateQuotationTemplateCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.QuotationTemplate.Id != Guid.Empty)
                    {
                        var quotationTemplates = await _context.QuotationTemplates.FindAsync(request.QuotationTemplate.Id);

                        if (quotationTemplates == null)
                            throw new NotFoundException("Good Receive", request.QuotationTemplate.RegistrationNo);


                        quotationTemplates.IsService = request.QuotationTemplate.IsService;
                        quotationTemplates.Date = request.QuotationTemplate.Date;
                        quotationTemplates.TemplateName = request.QuotationTemplate.TemplateName;
                        quotationTemplates.Description = request.QuotationTemplate.Description;
                        quotationTemplates.RegistrationNo = request.QuotationTemplate.RegistrationNo;
                        quotationTemplates.ApprovalStatus = request.QuotationTemplate.ApprovalStatus;

                        _context.QuotationTemplateItems.RemoveRange(quotationTemplates.QuotationTemplateItems);
                        quotationTemplates.QuotationTemplateItems = request.QuotationTemplate.QuotationTemplateItems.Select(x =>
                            new QuotationTemplateItem()
                            {

                                ProductId = x.ProductId,
                                Quantity = x.Quantity,
                                UnitPrice = Math.Round(x.UnitPrice, 2),

                            }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            DocumentNo = quotationTemplates.RegistrationNo,
                            Code = _code,
                        }, cancellationToken);




                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = quotationTemplates.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;



                    }
                    else
                    {
                        var quotationTemplate = new QuotationTemplate
                        {
                            Date = request.QuotationTemplate.Date,
                            IsService = request.QuotationTemplate.IsService,
                            RegistrationNo = request.QuotationTemplate.RegistrationNo,
                            Description = request.QuotationTemplate.Description,
                            TemplateName = request.QuotationTemplate.TemplateName,
                            ApprovalStatus = request.QuotationTemplate.ApprovalStatus,
                            QuotationTemplateItems = request.QuotationTemplate.QuotationTemplateItems.Select(x =>
                              new QuotationTemplateItem()
                              {
                                  ProductId = x.ProductId,
                                  UnitPrice = x.UnitPrice,
                                  Quantity = x.Quantity,
                              }).ToList(),



                        };
                        //Add Department to database
                        await _context.QuotationTemplates.AddAsync(quotationTemplate, cancellationToken);
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            DocumentNo = quotationTemplate.RegistrationNo,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = quotationTemplate.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                        
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
