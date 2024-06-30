using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ItemsBarCode.Commands
{
    public class ItemsBarCodeUpdateCommand : IRequest<Message>
    {
        public List<ProductLookUpModel> ItemsBarCodes { get; set; }
        public class Handler : IRequestHandler<ItemsBarCodeUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<ItemsBarCodeUpdateCommand> logger, IConfiguration configuration, IUserHttpContextProvider contextProvider, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _configuration = configuration;
                _contextProvider = contextProvider;
                _mediator = mediator;
            }
            public async Task<Message> Handle(ItemsBarCodeUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var productsList = new List<Product>(); 
                    var query = await _context.Products.ToListAsync(cancellationToken: cancellationToken);
                    foreach (var item in request.ItemsBarCodes)
                    {
                        var singleItem = query.FirstOrDefault(x => x.Code == item.Code);
                        if(singleItem != null)
                        {
                            if(string.IsNullOrEmpty(item.NewBarCode))
                            {
                                singleItem.BarCode = item.BarCode;
                            }
                            else
                            {
                                if(item.IsGenerated)
                                {
                                    singleItem.BarCodeType = "Generated";
                                    singleItem.BarCode = item.NewBarCode;
                                }
                                else if(!string.IsNullOrEmpty(item.NewBarCode))
                                {
                                    singleItem.BarCodeType = "Scanned";
                                    singleItem.BarCode = item.NewBarCode;
                                }
                            }

                            productsList.Add(singleItem);
                        }
                    }

                    _context.Products.UpdateRange(productsList);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        Code = _code,
                        IsServer = true
                    }, cancellationToken);

                    await _context.SaveChangesAsync();

                    return new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "Product BarCodes Updated Successfully"
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }

    }
}
