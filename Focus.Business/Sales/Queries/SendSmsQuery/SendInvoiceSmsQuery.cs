
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleList;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using UnitM = MediatR.Unit;
using Microsoft.EntityFrameworkCore;
using System.IO.Ports;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DocumentFormat.OpenXml.Office.Word;

namespace Focus.Business.Sales.Queries.SendSmsQuery
{
    public class SendInvoiceSmsQuery : IRequest<UnitM>
    {

        public Guid Id { get; set; }


        public class Handler : IRequestHandler<SendInvoiceSmsQuery, UnitM>
        {
            private readonly IApplicationDbContext _context;
            public IConfiguration Configuration { get; }
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<SendInvoiceSmsQuery> logger, IConfiguration configuration)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
            }

            public async Task<UnitM> Handle(SendInvoiceSmsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    _context.DisableTenantFilter = true;
                    //var counterId = _contextProvider.GetCounterId();

                    var saleList = _context.Sales
                        .AsNoTracking()
                        .Include(x => x.TaxRate)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.Customer)
                        .Include(x => x.CashCustomer)
                        .Include(x => x.SalePayments)
                        .Where(x => x.IsSendMsg && !x.IsMsgSended && (x.InvoiceType == InvoiceType.Paid || x.InvoiceType == InvoiceType.Credit));

                    var updateRecord = string.Empty;
                    var invoiceList = new List<SaleListLookupModel>();
                    foreach (var invoice in saleList)
                    {
                        var mobileNumber = invoice.CashCustomerId != null ? invoice.CashCustomer.Mobile : invoice.Customer.ContactNo1;
                        if (!string.IsNullOrEmpty(mobileNumber))
                        {
                            var customerName = invoice.CashCustomerId != null ? invoice.CashCustomer.Name : invoice.Customer.EnglishName;
                            var port = _context.GsmSmsSetups.FirstOrDefault()?.ComPort;
                            var NetAmount = !invoice.IsDiscountOnTransaction ? (((invoice.SaleItems.Count(x => x.IsFree == false) > 0 ?
                            invoice.SaleItems.Where(x => x.IsFree == false).Sum(x => (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount)) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))) * (100 + x.TaxRate.Rate) / 100) :
                            (x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100))) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100)))) : 0) + invoice.Discount + invoice.UnRegisteredVAtAmount))
                            : Math.Round(invoice.SaleItems.Where(x => !x.IsFree).Sum(x => x.UnitPrice * x.Quantity) + ((invoice.TaxMethod == "Exclusive" || invoice.TaxMethod == "غير شامل") ? CalculateTotalVat(invoice.SaleItems.ToList(), invoice.IsBeforeTax, invoice.IsFixed, invoice.IsDiscountOnTransaction, invoice.TransactionLevelDiscount) : 0) - CalculateTransactionLevelDiscount(invoice.SaleItems.ToList(), invoice.IsBeforeTax, invoice.IsFixed, invoice.IsDiscountOnTransaction, invoice.TransactionLevelDiscount, invoice.TaxMethod) + invoice.Discount + invoice.UnRegisteredVAtAmount, 2);

                            var receivedAmt = invoice.SalePayments.Where(x => x.Name != "Credit").Sum(x => x.Received);
                            var smsDetail = "Dear " + customerName + ", \r\nYour account has been debited with order Ref No:" + invoice.RegistrationNo + " Dated: " + invoice.Date.ToString("d")
                                + "\r\nRef No:" + invoice.RegistrationNo + " Amount Due=Rs. " + Math.Round(NetAmount, 2) + " Payment=Rs. " + Math.Round(receivedAmt, 2);
                            //+ "\r\nYour Total Balance Including Ref No:" + invoice.RegistrationNo + ", upto " + (invoice.DueDate==null?invoice.Date.ToString("d"): invoice.DueDate.Value.ToString("d")) + " is Rs. " + Math.Round(NetAmount - receivedAmt,2);


                            SerialPort sp = new SerialPort();
                            sp.PortName = port;
                            sp.Open();
                            sp.WriteLine("AT" + Environment.NewLine);

                            Thread.Sleep(100);
                            sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
                            Thread.Sleep(100);
                            sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
                            Thread.Sleep(100);
                            sp.WriteLine("AT+CMGS=\"" + mobileNumber + "\"" + Environment.NewLine);
                            Thread.Sleep(100);

                            sp.WriteLine(smsDetail + Environment.NewLine);
                            Thread.Sleep(200);



                            sp.Write(new byte[] { 26 }, 0, 1);
                            Thread.Sleep(100);
                            var response = sp.ReadExisting();
                            if (response.Contains("Error"))
                                _logger.LogError(response);
                            sp.Close();
                            Thread.Sleep(100);

                            if (!string.IsNullOrEmpty(response))
                            {
                                updateRecord += "update Sales set IsMsgSended = 1 where Id = '" + invoice.Id + "'; ";
                            }
                        }
                    }
                    _context.DisableTenantFilter = false;
                    
                    
                    if (!string.IsNullOrEmpty(updateRecord))
                    {
                        using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                        {
                            connection1.Open();
                            var cmd = new SqlCommand(updateRecord, connection1);
                            cmd.ExecuteNonQuery();
                            connection1.Close();
                        }
                    }
                    return UnitM.Value;
                }
                catch (Exception exception)
                {
                    _context.DisableTenantFilter = false;
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }

            public decimal CalculateTotalVat(ICollection<SaleItem> invoice, bool IsBeforeTax, bool IsFixed, bool IsDiscountOnTransaction, decimal TransactionLevelDiscount)
            {
                var sumQuantity = invoice.Sum(product => product.IsFree ? 0 : product.Quantity);
                var total = invoice.Sum(prod => (prod.IsFree ? 0 : ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (IsBeforeTax && !IsFixed && IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * TransactionLevelDiscount) / 100) : (IsBeforeTax && IsFixed && IsDiscountOnTransaction ? (TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100)));

                return total;
            }
            public decimal CalculateTransactionLevelDiscount(ICollection<SaleItem> sale, bool IsBeforeTax, bool IsFixed, bool IsDiscountOnTransaction, decimal TransactionLevelDiscount, string TaxMethod)
            {
                var sumQuantity = sale.Sum(product => product.IsFree ? 0 : product.Quantity);
                var total = (IsBeforeTax && IsDiscountOnTransaction) ? (TaxMethod == "Inclusive" || TaxMethod == "شامل") ? (TransactionLevelDiscount * (sale.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity))) / 100) : (IsFixed ? TransactionLevelDiscount : TransactionLevelDiscount * sale.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (IsFixed ? TransactionLevelDiscount : TransactionLevelDiscount * (sale.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((TaxMethod == "Inclusive" || TaxMethod == "شامل") ? 0 : CalculateTotalVat(sale, IsBeforeTax, IsFixed, IsDiscountOnTransaction, TransactionLevelDiscount))) / 100);

                return total;
            }
        }
    }
}
