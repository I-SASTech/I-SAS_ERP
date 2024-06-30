using Focus.Business.BarCodeSetup.Models;
using Focus.Business.Interface;
using Focus.Business.ListOrdringSetup.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BarCodeSetup.Queries
{
    public class GetBarCodeSetupQuery : IRequest<BarCodeSetupLookupModel>
    {
        public class Handler : IRequestHandler<GetBarCodeSetupQuery, BarCodeSetupLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<GetBarCodeSetupQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<BarCodeSetupLookupModel> Handle(GetBarCodeSetupQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var barCodeSetupCount = _context.ListOrderSetups.Count(x => x.DocumentName == "BarCode");



                    if (barCodeSetupCount == 0)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var list = new List<ListOrderSetup>();
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Item Code",
                            Name = "Item Code",
                            Sequence = 1,
                            DocumentName = "BarCode"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Item Name (English)",
                            Name = "Item Name (English)",
                            Sequence = 2,
                            DocumentName = "BarCode"
                        });
                        
                        await _context.ListOrderSetups.AddRangeAsync(list);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,

                        }, cancellationToken);

                        await _context.SaveChangesAsync();

                    }
                    
                    var barCodeSetupList = await _context.ListOrderSetups.Where(x => x.DocumentName == "BarCode").Select(x => new ListOrderSetupLookupModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        DisplayName = x.DisplayName,
                        DocumentName = x.DocumentName,
                        Sequence = x.Sequence,
                    }).ToListAsync();


                    return new BarCodeSetupLookupModel
                    {
                        ItemViewSetupList = barCodeSetupList,
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
