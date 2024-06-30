using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Guid = System.Guid;

namespace Focus.Business.DayStarts.Queries.GetTotalInvoicesQuery
{
   public class GetTotalInvoicesQuery : IRequest<List<TotalInvoicesLookUpModel>>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public Guid? DayId { get; set; }

        public bool IsOpeningCash { get; set; }
        public Guid? CounterId { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<GetTotalInvoicesQuery, List<TotalInvoicesLookUpModel>>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

        
            //Create logger interface variable for log error
            public readonly ILogger Logger;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<GetTotalInvoicesQuery> logger, IUserHttpContextProvider contextProvider)
            {
                Context = context;
                Logger = logger;
        
            }

            public async Task<List<TotalInvoicesLookUpModel>> Handle(GetTotalInvoicesQuery request, CancellationToken cancellationToken)
            {
                try
                {


                    //var dayStart = await Context.DayStarts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.DayId,cancellationToken);

                    var invoices = Context.Sales.AsNoTracking()
                        .Include(x=>x.SalePayments)
                        .AsQueryable();

                    var invoiceList = new List<TotalInvoicesLookUpModel>(); 
                     var saleData = invoices.Where(x => EF.Property<Guid>(x, "DayId") == request.DayId && EF.Property<Guid>(x, "CounterId") == request.CounterId).ToList();

                    foreach (var sale in saleData)
                    {
                        invoiceList.Add(new TotalInvoicesLookUpModel
                        {
                            //UserName = dayStart.DayStartUser,
                            //TotalInvoices = invoices.Count(x => EF.Property<Guid>(x, "DayId") == request.DayId && (x.InvoiceType == InvoiceType.Paid)),
                            TotalSaleReturn = invoices.Count(x => EF.Property<Guid>(x, "DayId") == request.DayId && x.IsSaleReturnPost),
                            //TotalCashInvoices = sale.SalePayments.Count(x => x.Name == "Cash"),
                            //TotalBankInvoices = sale.SalePayments.Count(x => x.Name == "Bank"),
                            //TotalCashAndBankInvoices = sale.SalePayments.Count(x => x.Name == "Bank" && x.Name == "Cash"),
                            TotalSaleReturnValue = sale.IsSaleReturn == true ? Context.Transactions.Where(x=>x.DocumentId == sale.Id).Sum(x=>x.Debit - x.Credit):0M

                        });
                    }


                    return invoiceList;
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
