using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.ProductionModule.SampleRequests.Commands
{
   public class AddSampleRequestCommand : IRequest<Guid>
    {
        public SampleRequestLookupModel sampleRequest { get; set; }
        public class Handler : IRequestHandler<AddSampleRequestCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddSampleRequestCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddSampleRequestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.sampleRequest.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var sampleRequest = new Domain.Entities.SampleRequest()
                        {
                            Code = request.sampleRequest.Code,
                            Date = request.sampleRequest.Date,
                            ProductId = request.sampleRequest.ProductId,
                            NoOfSampleRequests = request.sampleRequest.NoOfSampleRequests,
                            CustomerId = request.sampleRequest.CustomerId,
                            ReferredBy = request.sampleRequest.ReferredBy,
                            RequestGenerated = request.sampleRequest.RequestGenerated,
                            DueDate = request.sampleRequest.DueDate,
                            ApprovalStatus = request.sampleRequest.ApprovalStatus,
                            SampleRequestItems = request.sampleRequest.SampleRequestItems.Select(x =>
                                new SampleRequestItem()
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    HighQuantity = x.HighQuantity,
                                    Description = x.Description,
                                    UnitPerPack = x.UnitPerPack,
                                    SampleRequestId = x.SampleRequestId

                                }).ToList()
                        };

                        await _context.SampleRequests.AddAsync(sampleRequest, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=sampleRequest.Code
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return sampleRequest.Id;
                    }
                    else
                    {
                        var purchase = await _context.SampleRequests
                          .FindAsync(request.sampleRequest.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        purchase.Code = request.sampleRequest.Code;
                        purchase.Date = request.sampleRequest.Date;
                        purchase.ProductId = request.sampleRequest.ProductId;
                        purchase.NoOfSampleRequests = request.sampleRequest.NoOfSampleRequests;
                        purchase.CustomerId = request.sampleRequest.CustomerId;
                        purchase.ReferredBy = request.sampleRequest.ReferredBy;
                        purchase.ApprovalStatus = request.sampleRequest.ApprovalStatus;
                        purchase.RequestGenerated = request.sampleRequest.RequestGenerated;
                        purchase.DueDate = request.sampleRequest.DueDate;

                        purchase.SampleRequestItems = request.sampleRequest.SampleRequestItems.Select(x =>
                                                       new SampleRequestItem()
                                                       {
                                                           ProductId = x.ProductId,
                                                           Quantity = x.Quantity,
                                                           UnitPerPack = x.UnitPerPack,
                                                           Description = x.Description,
                                                           HighQuantity = x.HighQuantity,
                                                           SampleRequestId = x.SampleRequestId
                                                       }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=purchase.Code
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return purchase.Id;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
