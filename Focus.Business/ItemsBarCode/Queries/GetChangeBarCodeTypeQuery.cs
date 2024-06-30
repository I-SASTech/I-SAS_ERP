using Focus.Business.Brands.Commands.DeleteBrand;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Products.Commands.DeleteProduct;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ItemsBarCode.Queries
{
    public class GetChangeBarCodeTypeQuery : IRequest<Message>
    {
        public class Handler : IRequestHandler<GetChangeBarCodeTypeQuery, Message>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            public readonly IApplicationDbContext _context;

            //Create object of logger for all type of message show
            public readonly ILogger _logger;

            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<GetChangeBarCodeTypeQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(GetChangeBarCodeTypeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = await _context.Products.ToListAsync();

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    foreach (var item in products)
                    {
                        if(string.IsNullOrEmpty(item.BarCode))
                        {
                            item.BarCodeType = "None";
                        }
                        else
                        {
                            item.BarCodeType = "Scanned";
                        }
                    }

                    _context.Products.UpdateRange(products);


                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        Code = _code,
                        IsServer = true
                    }, cancellationToken);

                    await _context.SaveChangesAsync();

                    return new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "Item Barcode Type Changed Successfully."
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
