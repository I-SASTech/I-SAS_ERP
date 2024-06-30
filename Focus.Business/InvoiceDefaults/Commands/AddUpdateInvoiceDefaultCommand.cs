using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.InvoiceDefaults.Commands
{
    public class AddUpdateInvoiceDefaultCommand: IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public bool IsSalePrice { get; set; }
        public bool IsCustomerPrice { get; set; }
        public bool IsSalePriceLabel { get; set; }
        public bool IsCustomerPriceLabel { get; set; }

        //Create Handler class that inherit from IRequestHandler interface
        public class Handler : IRequestHandler<AddUpdateInvoiceDefaultCommand, Guid>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateInvoiceDefaultCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateInvoiceDefaultCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var invoiceDefaultDetail1 = await Context.InvoiceDefaults.FirstOrDefaultAsync();

                   
                    if (invoiceDefaultDetail1!=null)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        invoiceDefaultDetail1.IsSalePrice = request.IsSalePrice;
                        invoiceDefaultDetail1.IsCustomerPrice = request.IsCustomerPrice;
                        invoiceDefaultDetail1.IsSalePriceLabel = request.IsSalePriceLabel;
                        invoiceDefaultDetail1.IsCustomerPriceLabel = request.IsCustomerPriceLabel;

                        //Save changes to database
                        Context.InvoiceDefaults.Update(invoiceDefaultDetail1);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        Context.SaveChanges();
                        return invoiceDefaultDetail1.Id;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var invoiceDefaultDetail = new InvoiceDefault
                        {
                            IsSalePrice = request.IsSalePrice,
                            IsCustomerPrice = request.IsCustomerPrice,
                            IsSalePriceLabel = request.IsSalePriceLabel,
                            IsCustomerPriceLabel = request.IsCustomerPriceLabel

                        };

                         Context.InvoiceDefaults.Add(invoiceDefaultDetail);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        Context.SaveChanges();
                        return invoiceDefaultDetail.Id;

                    }
                    
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Department Name Already Exist");
                }
            }
        }
    }
}
