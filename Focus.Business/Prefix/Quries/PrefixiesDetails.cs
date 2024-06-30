using Focus.Business.Interface;
using Focus.Business.Prefix.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Prefix.Quries
{
    public class PrefixiesDetails : IRequest<PrefixiesLookupModel>
    {
        public class Handler : IRequestHandler<PrefixiesDetails, PrefixiesLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<PrefixiesDetails> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<PrefixiesLookupModel> Handle(PrefixiesDetails request, CancellationToken cancellationToken)
            {
                try
                {
                    var prefix = await _context.Prefixies.FirstOrDefaultAsync(cancellationToken);
                    if (prefix == null)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var prefixies = new Prefixies()
                        {
                            SInvoice = "SI",
                            GlobalInvoice = "GI",
                            SaleInvoiceCredit = "SIC",
                            ReceiptInvoice = "RI",
                            SaleInvoiceHold = "SIH",
                            ProformaSaleInvoice = "PSI",
                            SReturn = "SIR",
                            SOrder = "SO",
                            SQutation = "QT",
                            PInvoice = "PI",
                            PReturn = "PIR",
                            POrder = "PO",
                            SOrdeTracking = "SOT",
                            PurchaseInvoiceDraft = "PID",
                            Employee = "Em",
                            DebitNote = "DN",
                            CreditNote = "CN",
                            CustomerPayReceipt = "CP",
                            AdvanceSaleReceipt = "AR",
                            SupplierPayReceipt = "SP",
                            AdvancePurchaseReceipt = "PR",
                        };
                        await _context.Prefixies.AddAsync(prefixies, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new PrefixiesLookupModel
                        {
                            SInvoice = "SI",
                            GlobalInvoice = "GI",
                            ReceiptInvoice = "RI",
                            SaleInvoiceHold = "SIH",
                            ProformaSaleInvoice = "PSI",
                            SReturn = "SIR",
                            SOrder = "SO",
                            SQutation = "QT",
                            PInvoice = "PI",
                            PReturn = "PIR",
                            POrder = "PO",
                            PurchaseInvoiceDraft = "PID",
                            SOrdeTracking = "SOT",
                            Employee = "Em",
                            DebitNote = "DN",
                            CreditNote = "CN",
                            CustomerPayReceipt = "CP",
                            AdvanceSaleReceipt = "AR",
                            SupplierPayReceipt = "SP",
                            AdvancePurchaseReceipt = "PR",
                        };
                    }
                    else
                    {
                        return new PrefixiesLookupModel
                        {
                            Id = prefix.Id,
                            SInvoice = prefix.SInvoice,
                            SReturn = prefix.SReturn,
                            SOrder = prefix.SOrder,
                            SQutation = prefix.SQutation,
                            PInvoice = prefix.PInvoice,
                            PReturn = prefix.PReturn,
                            POrder = prefix.POrder,
                            GlobalInvoice = prefix.GlobalInvoice==null || prefix.GlobalInvoice == ""?"RI": prefix.GlobalInvoice,
                            ReceiptInvoice = prefix.ReceiptInvoice == null || prefix.ReceiptInvoice == "" ? "GI" : prefix.ReceiptInvoice,
                            SaleInvoiceHold = prefix.SaleInvoiceHold,
                            ProformaSaleInvoice = prefix.ProformaSaleInvoice,
                            PurchaseInvoiceDraft = prefix.PurchaseInvoiceDraft,
                            SOrdeTracking= prefix.SOrdeTracking,
                            Employee= prefix.Employee,
                            CreditNote= prefix.CreditNote,
                            DebitNote= prefix.DebitNote,
                            CustomerPayReceipt = prefix.CustomerPayReceipt,
                            AdvanceSaleReceipt = prefix.AdvanceSaleReceipt,
                            SupplierPayReceipt = prefix.SupplierPayReceipt,
                            AdvancePurchaseReceipt = prefix.AdvancePurchaseReceipt,

                        };
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Something went wrong " + ex.Message);
                }
            }
        }
    }
}
