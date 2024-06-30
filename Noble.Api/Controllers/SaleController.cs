using Focus.Business.Sales;
using Focus.Business.Sales.Commands.AddSaleReturn;
using Focus.Business.Sales.Commands.DeleteSale;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Business.Sales.Queries.GetSaleList;
using Focus.Business.Sales.Queries.GetSaleRegistrationNo;
using Focus.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Focus.Business.AuthorizeUser;
using Focus.Business.DispatchNotes;
using Focus.Business.DispatchNotes.Commands.AddDispatchNote;
using Focus.Business.DispatchNotes.Queries;
using Focus.Business.DispatchNotes.Queries.DispatchNoteList;
using Focus.Business.DispatchNotes.Queries.GetDispatchNoteDetails;
using Focus.Business.Interface;
using Focus.Business.PrintOptions.Commands.AddPrintOption;
using Noble.Api.Models;
using Focus.Business.PrintSettings.Queries.GetPrintSettingsDetails;
using Focus.Business.Sales.Queries.CashCustomerDetail;
using Focus.Business.Sales.Queries.CashVoucherQuery;
using Focus.Domain.Enum;
using Focus.Business.PrintSettings.Commands.AddUpdatePrintSettings;
using Focus.Business.SaleOrders.Queries.SaleList;
using Focus.Business.Sales.Queries.GetSaleDetailForEmail;
using Focus.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Sales.Queries.CashCustomerList;
using System.Collections.Generic;
using Focus.Business.Exceptions;
using Focus.Business.Sales.Commands.AddUpdateSale;
using Focus.Business.Sales.Commands.SaveVerifiedInvoice;
using Focus.Business.Common;
using Focus.Business.DefaultSettingInvoice.DefaultSettingQuery;
using Focus.Business.DefaultSettingInvoice.DefaultSattingCommand;
using Focus.Business.SaleOrders;
using Focus.Business.SaleOrders.Commands.AddSaleOrder;
using Focus.Business.SaleOrders.Queries.GetSaleOrderRegistrationNo;
using Focus.Business.SmsSetup.GsmSmsSetupCommand;
using Focus.Business.SmsSetup.GsmSmsSetupQuery;
using Focus.Business.Attachments.Commands;
using Focus.Business.CreditNotes.Commands;
using Focus.Business.CreditNotes.Queries;
using Focus.Business.CreditNotes.Model;
using Microsoft.AspNetCore.Hosting;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Focus.Business.Sales.Commands.DeleteSalePrmanently;
using Focus.Business.SaleOrders.Queries.DashboardQuery;
using Focus.Business.Sales.Commands.UpdateSale;
using Focus.Business.Sales.DocumentHistory;
using Focus.Business.SyncRecords.Commands;
using System.Security.Claims;
using System.Threading;
using static QRCoder.PayloadGenerator.SwissQrCode;
using Focus.Business.Emails.Queries;
using Focus.Business.Emails.Models;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : BaseController
    {
        private string _code;

        public readonly IApplicationDbContext Context;
        private readonly ISendEmail _SendEmail;
        public readonly IHostingEnvironment _Environment;
        //private readonly IUserHttpContextProvider _contextProvider;
        //private readonly UserManager<ApplicationUser> _userManager;
        public SaleController(IApplicationDbContext context, ISendEmail sendEmail, IHostingEnvironment environment)
        {
            Context = context;
            _SendEmail = sendEmail;
            _Environment = environment;
            //_contextProvider = contextProvider;
            //_userManager = userManager;
        }
        #region For Sale



        [Route("api/Sale/DocumentHistory")]
        [HttpGet("DocumentHistory")]
        public async Task<IActionResult> DocumentHistory(string documentName, Guid id, string currentDocument)
        {
            var result = await Mediator.Send(new DocumentHistory
            {
                Id = id,
                DocumentName = documentName,
                CurrentDocument = currentDocument,
            });
            return Ok(result);
        } 
        
        [Route("api/Sale/SendSaleEmail")]
        [HttpPost("SendSaleEmail")]
        public async Task<IActionResult> SendSaleEmail(Guid id, bool isSaleOrder, bool isCreditNote, bool isDeliveryNote, SendEmailFromListLookupModel emailCompose)
        {
            var result = await Mediator.Send(new SendSaleEmail
            {
                Id = id,
                IsSaleOrder = isSaleOrder,
                IsCreditNote = isCreditNote,
                IsDeliveryNote = isDeliveryNote,
                EmailCompose = emailCompose
            });
            return Ok(result);
        } 
        
        [Route("api/Sale/SendPurchaseEmails")]
        [HttpPost("SendPurchaseEmails")]
        public async Task<IActionResult> SendPurchaseEmails(Guid id, bool isGoodsReceive, bool isPurchaseOrder, SendEmailFromListLookupModel emailCompose)
        {
            var result = await Mediator.Send(new SendPurchaseEmails
            {
                Id = id,
                IsPurchaseOrder = isPurchaseOrder,
                IsGoodsReceive = isGoodsReceive,
                EmailCompose = emailCompose
            });
            return Ok(result);
        }


        [HttpPost("AuthorizeUser")]
        public async Task<IActionResult> AuthorizeUser(AuthorizeUserLookUp authorize)
        {
            var result = await Mediator.Send(new AuthorizeUserCheckQuery
            {
                Authorize = authorize
            });
            return Ok(result);
        }

        [Route("api/Sale/UpdateQuantity")]
        [HttpGet("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity()
        {
            UpdateInventoryQuantity();
            var stock = Context.Stocks.ToList();
            var inventories = Context.Inventories.AsNoTracking().ToList();

            foreach (var item in stock)
            {
                var opening = inventories.Where(x => x.ProductId == item.ProductId).ToList();

                var saleSum = opening.Where(x => x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut
                    || x.TransactionType == TransactionType.PurchaseReturn || x.TransactionType == TransactionType.WareHouseTransferFrom).Sum(x => x.Quantity);

                var purchaseSum = opening.Where(x => x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn
                    || x.TransactionType == TransactionType.SaleReturn || x.TransactionType == TransactionType.WareHouseTransferTo).Sum(x => x.Quantity);


                item.CurrentQuantity = purchaseSum - saleSum;
            }
            //Random rnd = new Random();
            //for (int i = 0; i < 11; i++)
            //{
            //    _code += rnd.Next(0, 9).ToString();
            //}
            //await Mediator.Send(new AddUpdateSyncRecordCommand()
            //{
            //    SyncRecordModels = Context.SyncLog(),
            //    Code = _code,
            //});
            await Context.SaveChangesAsync();


            return Ok(true);
        }
        [Route("api/Sale/OneTimeStockQuantity")]
        [HttpGet("OneTimeStockQuantity")]
        public async Task<IActionResult> OneTimeStockQuantity()
        {
            var stock = Context.Stocks.AsNoTracking().Select(x => x.ProductId).ToList();
            var products = Context.Products.AsNoTracking().ToList();
            var wareHouse = Context.Warehouses.AsNoTracking().FirstOrDefault();
            var sql = Context.Warehouses.AsNoTracking().FirstOrDefault().ToString();

            if (wareHouse != null)
            {
                var stockList = new List<Stock>();
                foreach (var item in products)

                {
                    if (!stock.Contains(item.Id))
                    {
                        stockList.Add(new Stock
                        {
                            ProductId = item.Id,
                            WareHouseId = wareHouse.Id

                        });
                    }

                }
                Context.Stocks.AddRange(stockList);
                //Random rnd = new Random();
                //for (int i = 0; i < 11; i++)
                //{
                //    _code += rnd.Next(0, 9).ToString();
                //}
                //await Mediator.Send(new AddUpdateSyncRecordCommand()
                //{
                //    SyncRecordModels = Context.SyncLog(),
                //    Code = _code,
                //});
                Context.SaveChanges();

            }


            return Ok(true);
        }

        public bool UpdateInventoryQuantity()
        {
            var inv = Context.Inventories.ToList();
            var productList = Context.Inventories.Where(x => x.CurrentQuantity < 0).Select(x => x.ProductId);

            foreach (var item in productList)
            {
                decimal total = 0;
                inv.Where(x => x.ProductId == item).ToList().ForEach(x =>
                  {
                      if (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut
                    || x.TransactionType == TransactionType.PurchaseReturn || x.TransactionType == TransactionType.WareHouseTransferFrom)
                      {
                          total = total - x.Quantity;
                          x.CurrentQuantity = total;
                      }
                      else if (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn
                    || x.TransactionType == TransactionType.SaleReturn || x.TransactionType == TransactionType.WareHouseTransferTo)
                      {
                          total = total + x.Quantity;
                          x.CurrentQuantity = total;
                      }
                  });

            }
            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = Context.SyncLog(),
                Code = _code,
            });
            Context.SaveChanges();

            return true;
        }

        [Route("api/Sale/RemoveDuplicateQuantity")]
        [HttpGet("RemoveDuplicateQuantity")]
        public bool RemoveDuplicateQuantity()
        {
            var inv = Context.Inventories.OrderBy(x => x.ProductId).ToList();

            for (int i = 0; i < inv.Count - 2; i++)
            {
                if (inv[i].DocumentId == inv[i + 1].DocumentId && inv[i].ProductId == inv[i + 1].ProductId)
                {
                    Context.Inventories.Remove(inv[i]);
                    // Context.SaveChanges();
                }
            }
            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = Context.SyncLog(),
                Code = _code,
            });
            Context.SaveChanges();

            return true;
        }


        [Route("api/Sale/TrialBalanceSettingFORSaleInvoice")]
        [HttpGet("TrialBalanceSettingFORSaleInvoice")]
        public async Task<bool> TrialBalanceSettingFORSaleInvoice(string saleNumber)
        {
            var sales = await Context.Sales
                .AsNoTracking()
                .Where(x => x.InvoiceType != InvoiceType.Hold)
                .ToListAsync();

            var account = await Context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == "42000001");

            var transactionsToUpdate = new List<Transaction>();

            foreach (var sale in sales)
            {

                var transactions = await Context.Transactions.AsNoTracking()
                    .Where(x => x.TransactionType == TransactionType.SaleInvoice && x.DocumentId == sale.Id)
                    .ToListAsync();

                var saleAccount = transactions.FirstOrDefault(x => x.AccountId == account.Id);
                var hasZeroDebitCredit = saleAccount != null && saleAccount.Debit == 0 && saleAccount.Credit == 0;

                if (transactions.Any(x => x.AccountId == account.Id))
                {
                    var transactionToUpdate = transactions.FirstOrDefault(x => x.AccountId == account.Id);
                    if (transactionToUpdate != null && hasZeroDebitCredit)
                    {
                        transactionToUpdate.Credit = sale.TotalWithOutAmount + sale.DiscountAmount;
                        transactionToUpdate.AccountId = account.Id;
                        transactionsToUpdate.Add(transactionToUpdate);
                    }
                }

            }

            Context.Transactions.UpdateRange(transactionsToUpdate);
            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            await Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = Context.SyncLog(),
                Code = _code,
            });
            await Context.SaveChangesAsync();

            return true;
        }


        [Route("api/Sale/TrialBalanceSetting")]
        [HttpGet("TrialBalanceSetting")]
        public async Task<bool> TrialBalanceSetting(string saleNumber)
        {

            var sale = await Context.Sales
                     .Include(x => x.UnRegisteredVAT)
                     .Include(x => x.SalePayments)
                     .Include(x => x.Customer)
                     .Include(x => x.SaleItems)
                     .ThenInclude(x => x.TaxRate)
                     .Include(x => x.SaleItems)
                     .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
                     .FirstOrDefaultAsync(x => x.RegistrationNo == saleNumber);

            var trans = await Context.Transactions.Where(x => x.DocumentId == sale.Id).ToListAsync();
            var documentDate = trans[0].DocumentDate;
            var approvalDate = trans[0].ApprovalDate;
            var date = trans[0].Date;
            Context.Transactions.RemoveRange(trans);
            var period = await Context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart <= sale.Date.Date && x.PeriodEnd >= sale.Date.Date);

            if (period == null)
                throw new NotFoundException("Company Submission Periods Not Found", "");
            var transactions = new List<Transaction>();

            var accounts = await Context.Accounts.Where(x =>
                x.Code == "25000001" ||
                x.Code == "42600001" ||
                x.Code == "10100001" ||
                x.Code == "10500001" ||
                x.Code == "60505001" ||
                x.Code == "42000001" ||
                x.Code == "TR-00001" ||
                x.Code == "CC-00001"
            ).ToListAsync();



            // Start New Code Here
            foreach (var item in sale.SaleItems)
            {
                if (item.ProductId != null)
                {
                    if (!item.ServiceItem)
                    {

                        //inventory Account
                        transactions.Add(new Transaction
                        {
                            Date = date,
                            DocumentDate = documentDate,
                            ApprovalDate = approvalDate,
                            ContactId = sale.CustomerId,
                            AccountId = item.Product.Category.InventoryAccountId,
                            Debit = 0,
                            Credit = Math.Abs(Math.Round(item.UnitPrice * item.Quantity, 2)),
                            Description = TransactionType.SaleInvoice.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            ProductId = item.ProductId,
                            PeriodId = period.Id
                        });

                        //cost of good sale Account
                        transactions.Add(new Transaction
                        {
                            Date = date,
                            DocumentDate = documentDate,
                            ApprovalDate = approvalDate,
                            ContactId = sale.CustomerId,
                            AccountId = item.Product.Category.COGSAccountId,
                            Credit = 0,
                            Debit = Math.Abs(Math.Round(item.UnitPrice * item.Quantity, 2)),
                            Description = TransactionType.SaleInvoice.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            ProductId = item.ProductId,
                            PeriodId = period.Id
                        });
                    }
                }



                if (!(item.ServiceItem && item.IsFree))
                {
                    //Discount Taken
                    transactions.Add(new Transaction
                    {
                        Date = date,
                        DocumentDate = documentDate,
                        ApprovalDate = approvalDate,
                        ContactId = sale.CustomerId,
                        AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                        Credit = 0,
                        Debit = Math.Abs(Math.Round((item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount * item.Quantity), 2)),
                        Description = TransactionType.SaleReturn.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id
                    });
                    //vat
                    transactions.Add(new Transaction
                    {
                        Date = date,
                        DocumentDate = documentDate,
                        ApprovalDate = approvalDate,
                        ContactId = sale.CustomerId,
                        AccountId = accounts.FirstOrDefault(x => x.Code == "25000001").Id,
                        Debit = 0,
                        Credit = Math.Abs(Math.Round((((item.UnitPrice * (item.Quantity)) - ((item.Discount != 0 ? (item.UnitPrice * (item.Quantity) * item.Discount) / 100 : item.FixDiscount * item.Quantity))) * item.TaxRate.Rate) / ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (item.TaxRate.Rate + 100) : 100), 2)),
                        Description = TransactionType.SaleInvoice.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id
                    });


                    //sale Account
                    transactions.Add(new Transaction
                    {
                        Date = date,
                        DocumentDate = documentDate,
                        ApprovalDate = approvalDate,
                        ContactId = sale.CustomerId,
                        AccountId = item.ProductId == null ? accounts.FirstOrDefault(x => x.Code == "42000001").Id : item.Product.Category.SaleAccountId,
                        Debit = 0,
                        Credit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount * item.Quantity)) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount * item.Quantity)
                            : (item.Quantity * item.UnitPrice), 2),

                        Description = TransactionType.SaleInvoice.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        ProductId = item.ProductId,
                        PeriodId = period.Id
                    });
                }


            }


            //x.Code == "10100001" || // Cash in Hand
            //x.Code == "10500001" || // Bank
            Guid terminalAccountId = default;
            Guid cashAccountId = default;
            var invoiceSetting = Context.PrintSettings.AsNoTracking().FirstOrDefault();
            if (invoiceSetting != null)
            {
                terminalAccountId = invoiceSetting.BankAccountId ?? Guid.Empty;
                cashAccountId = invoiceSetting.CashAccountId ?? Guid.Empty;
            }



            //account Receivable
            if (sale.CustomerId != null)
            {
                decimal total = (sale.SaleItems.Where(x => !x.IsFree).Sum(x => Math.Round((x.OfferQuantity == 0 ? (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : x.FixDiscount * x.Quantity)) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))
                            : (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.OfferQuantity * x.Discount) / 100 : x.FixDiscount * x.OfferQuantity)) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))), 2))) - sale.Discount;
                //For Credit Note sale Invoice
                transactions.Add(new Transaction
                {
                    Date = date,
                    DocumentDate = documentDate,
                    ApprovalDate = approvalDate,
                    ContactId = sale.CustomerId,
                    AccountId = sale.Customer.AccountId.Value,
                    //Debit = sale.SaleItems.Sum(x => Math.Round((((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : x.FixDiscount * x.Quantity)) * (sale.TaxMethod == "Inclusive" ?  0: (100 + x.TaxRate.Rate))) / (sale.TaxMethod == "Inclusive" ? (100 + x.TaxRate.Rate) : 100))), 2)),
                    Debit = total + ((sale.UnRegisteredVAT == null ? 0 : sale.UnRegisteredVAT.Rate / 100) * total),
                    Credit = 0,
                    Description = "Account Payment",
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });
                if (!sale.IsCredit)
                {
                    foreach (var payment in sale.SalePayments)
                    {
                        //For Credit Note sale Invoice
                        transactions.Add(new Transaction
                        {
                            Date = date,
                            DocumentDate = documentDate,
                            ApprovalDate = approvalDate,
                            ContactId = sale.CustomerId,
                            AccountId = sale.Customer.AccountId.Value,
                            Debit = 0,
                            Credit = payment.Received,
                            Description = TransactionType.SaleInvoice.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id
                        });
                        //For Bank Cash Note sale Invoice
                        transactions.Add(new Transaction
                        {
                            Date = date,
                            DocumentDate = documentDate,
                            ApprovalDate = approvalDate,
                            ContactId = sale.CustomerId,
                            AccountId = payment.BankCashAccountId,
                            Debit = payment.Received,
                            Credit = 0,
                            Description = TransactionType.SaleInvoice.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id
                        });
                    }
                }

            }
            else
            {
                //For walk in customer Cash sale Invoice
                foreach (var payment in sale.SalePayments)
                {
                    transactions.Add(new Transaction
                    {
                        Date = date,
                        DocumentDate = documentDate,
                        ApprovalDate = approvalDate,
                        ContactId = sale.CustomerId,
                        AccountId = payment.PaymentType == SalePaymentType.Bank ? terminalAccountId //Bank "10500001"
                                                                                : cashAccountId, //Cash
                        Debit = payment.Received - sale.Discount,
                        Credit = 0,
                        Description = TransactionType.SaleInvoice.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id
                    });
                }

                //change transaction
                transactions.Add(new Transaction
                {
                    Date = date,
                    DocumentDate = documentDate,
                    ApprovalDate = approvalDate,
                    ContactId = sale.CustomerId,
                    AccountId = cashAccountId,
                    Debit = 0,
                    Credit = sale.Change,
                    Description = TransactionType.SaleInvoice.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });
            }
            await Context.Transactions.AddRangeAsync(transactions);
            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            await Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = Context.SyncLog(),
                Code = _code,
            });
            await Context.SaveChangesAsync();
            return true;
        }


        [Route("api/Sale/RemoveDuplicateTransactions")]
        [HttpGet("RemoveDuplicateTransactions")]
        public async Task<bool> RemoveDuplicateTransactions(string purchaseNumber)
        {


            var sale = await Context.PurchasePosts
                            .AsNoTracking()
                            .Include(x => x.Supplier)
                            .Include(x => x.PurchasePostItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.PurchasePostItems)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Category)
                            .FirstOrDefaultAsync(x => x.RegistrationNo == purchaseNumber);

            var trans = await Context.Transactions.Where(x => x.DocumentId == sale.Id).ToListAsync();
            var documentDate = trans[0].DocumentDate;
            var approvalDate = trans[0].ApprovalDate;
            var date = trans[0].Date;
            using System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled);
            Context.Transactions.RemoveRange(trans);
            var period = await Context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart <= sale.Date.Date && x.PeriodEnd >= sale.Date.Date);

            if (period == null)
                throw new NotFoundException("Company Submission Periods Not Found", "");
            var transactions = new List<Transaction>();

            Guid directPurchaseAccountId = default;
            var costCenterDirectPurchase = await Context.CostCenters
                .AsNoTracking()
                .Include(x => x.Accounts)
                .FirstOrDefaultAsync(x => x.Code == "190000");
            if (costCenterDirectPurchase != null)
            {
                directPurchaseAccountId = costCenterDirectPurchase.Accounts.FirstOrDefault(x => x.Code == "19000001").Id;
            }
            else
            {
                var accountType = await Context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Assets");

                if (accountType != null)
                {
                    var newCostCenter = new CostCenter()
                    {
                        Code = "190000",
                        Name = "Direct Purchase W/O Inventory",
                        NameArabic = "Direct Purchase W/O Inventory",
                        Description = "Direct Purchase W/O Inventory",
                        IsActive = true,
                        AccountTypeId = accountType.Id
                    };
                    await Context.CostCenters.AddAsync(newCostCenter);

                    var newAccount = new Account
                    {
                        Code = "19000001",
                        Name = "Direct Purchase W/O Inventory",
                        NameArabic = "Direct Purchase W/O Inventory",
                        Description = "Direct Purchase W/O Inventory",
                        IsActive = true,
                        CostCenterId = newCostCenter.Id
                    };
                    await Context.Accounts.AddAsync(newAccount);
                    directPurchaseAccountId = newAccount.Id;
                }
            }

            foreach (var item in sale.PurchasePostItems)
            {
                if (item.ProductId != null && !item.IsService)
                {
                    //inventory Account
                    transactions.Add(new Transaction
                    {
                        Date = sale.Date,
                        DocumentDate = sale.Date,
                        ApprovalDate = approvalDate,
                        ContactId = sale.SupplierId,
                        AccountId = item.Product.Category.InventoryAccountId,
                        Credit = 0,
                        Debit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.UnitPrice * Convert.ToDecimal(item.Quantity)) - ((item.Discount != 0 ? ((item.UnitPrice * Convert.ToDecimal(item.Quantity) * item.Discount) / 100) : (item.FixDiscount * Convert.ToDecimal(item.Quantity))))) * 100) / (100 + item.TaxRate.Rate) : (item.UnitPrice * Convert.ToDecimal(item.Quantity)), 2),
                        Description = TransactionType.PurchasePost.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.PurchasePost,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        ProductId = item.ProductId,
                        PeriodId = period.Id
                    });
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    await Mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    });
                    //Save changes to database
                    await Context.SaveChangesAsync();
                }
                else
                {
                    //Direct Purchase Account
                    transactions.Add(new Transaction
                    {
                        Date = sale.Date,
                        DocumentDate = sale.Date,
                        ApprovalDate = approvalDate,
                        ContactId = sale.SupplierId,
                        AccountId = directPurchaseAccountId,
                        Credit = 0,
                        Debit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.UnitPrice * Convert.ToDecimal(item.Quantity)) - ((item.Discount != 0 ? ((item.UnitPrice * Convert.ToDecimal(item.Quantity) * item.Discount) / 100) : (item.FixDiscount * Convert.ToDecimal(item.Quantity))))) * 100) / (100 + item.TaxRate.Rate) : (item.UnitPrice * Convert.ToDecimal(item.Quantity)), 2),
                        Description = TransactionType.PurchasePost.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.PurchasePost,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        ProductId = item.ProductId,
                        PeriodId = period.Id
                    });
                }
            }
            var accounts = await Context.Accounts.Where(x => x.Code == "1300001" || x.Code == "42600001").ToListAsync();

            //vat
            transactions.Add(new Transaction
            {
                Date = sale.Date,
                DocumentDate = sale.Date,
                ApprovalDate = approvalDate,
                ContactId = sale.SupplierId,
                AccountId = accounts.FirstOrDefault(x => x.Code == "1300001").Id,
                Credit = 0,
                Debit = sale.PurchasePostItems.Sum(x => Math.Round((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) - ((x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity)))) * x.TaxRate.Rate) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? (100 + x.TaxRate.Rate) : 100), 2)),

                Description = TransactionType.PurchasePost.ToString(),
                DocumentId = sale.Id,
                TransactionType = TransactionType.PurchasePost,
                DocumentNumber = sale.RegistrationNo,
                Year = sale.Date.Year.ToString(),
                PeriodId = period.Id
            });

            //Discount  
            transactions.Add(new Transaction
            {
                Date = sale.Date,
                DocumentDate = sale.Date,
                ApprovalDate = approvalDate,
                ContactId = sale.SupplierId,
                AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                Credit = sale.PurchasePostItems.Sum(x => Math.Round((x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity)), 2)),
                Debit = 0,

                Description = TransactionType.PurchasePost.ToString(),
                DocumentId = sale.Id,
                TransactionType = TransactionType.PurchasePost,
                DocumentNumber = sale.RegistrationNo,
                Year = sale.Date.Year.ToString(),
                PeriodId = period.Id
            });

            if (sale.IsCredit)
            {
                //account Payable
                transactions.Add(new Transaction
                {
                    Date = sale.Date,
                    DocumentDate = sale.Date,
                    ApprovalDate = approvalDate,
                    ContactId = sale.SupplierId,
                    AccountId = sale.Supplier.AccountId.Value,
                    Credit = sale.PurchasePostItems.Sum(x => Math.Round((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) -  //total amount
                                (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount)) + //discount

                            ((x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) - (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity))) * x.TaxRate.Rate) / 100) : 0)
                        ), 2)),
                    Debit = 0,
                    Description = TransactionType.PurchasePost.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.PurchasePost,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });


                transactions.Add(new Transaction
                {
                    Date = sale.Date,
                    DocumentDate = sale.Date,
                    ApprovalDate = approvalDate,
                    ContactId = sale.SupplierId,
                    AccountId = sale.Supplier.AccountId.Value,
                    Debit = sale.PurchasePostItems.Where(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل").Sum(x => Math.Round(x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity), 2)),
                    Credit = 0,
                    Description = TransactionType.PurchasePost.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.PurchasePost,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });

            }
            else
            {
                var supplier = await Context.Contacts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == sale.SupplierId);

                var costCenter = await Context.CostCenters
                    .AsNoTracking()
                    .Include(x => x.Accounts)
                    .FirstOrDefaultAsync(x => x.Code == "200000");

                var cashAccount = costCenter.Accounts.FirstOrDefault(x => x.Name == "Cash Purchase");

                if (cashAccount == null)
                {
                    var newCashAccount = costCenter.Accounts.OrderBy(x => x.Code).LastOrDefault();
                    var account = new Account()
                    {
                        Code = (Convert.ToInt64(newCashAccount.Code) + 1).ToString(),
                        Name = "Cash Purchase",
                        NameArabic = "شراء نقدا",
                        Description = "Cash Purchase",
                        IsActive = true,
                        CostCenterId = costCenter.Id
                    };
                    await Context.Accounts.AddAsync(account);
                    supplier.SupplierCashAccountId = account.Id;
                    Context.Contacts.Update(supplier);
                }
                else
                {
                    if (sale.Supplier.SupplierCashAccountId == null || sale.Supplier.SupplierCashAccountId == Guid.Empty)
                    {
                        supplier.SupplierCashAccountId = cashAccount.Id;
                        Context.Contacts.Update(supplier);
                    }
                }

                //Supplier Account Credit
                transactions.Add(new Transaction
                {
                    Date = sale.Date,
                    DocumentDate = sale.Date,
                    ApprovalDate = approvalDate,
                    ContactId = sale.SupplierId,
                    AccountId = sale.Supplier.AccountId.Value,
                    Credit = sale.PurchasePostItems.Sum(x => Math.Round((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) -  //total amount
                                (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity))) + //discount

                            ((x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) - (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity))) * x.TaxRate.Rate) / 100) : 0)
                        ), 2)),
                    Debit = 0,
                    Description = TransactionType.PurchasePost.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.PurchasePost,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });

                //Supplier Account Debit
                transactions.Add(new Transaction
                {
                    Date = sale.Date,
                    DocumentDate = sale.Date,
                    ApprovalDate = approvalDate,
                    ContactId = sale.SupplierId,
                    AccountId = sale.Supplier.AccountId.Value,
                    Debit = sale.PurchasePostItems.Sum(x => Math.Round((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) -  //total amount
                                (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount)) + //discount

                            ((x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) - (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity))) * x.TaxRate.Rate) / 100) : 0)
                        ), 2)),
                    Credit = 0,
                    Description = TransactionType.PurchasePost.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.PurchasePost,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });

                //supplier account Debit Discount


                transactions.Add(new Transaction
                {
                    Date = sale.Date,
                    DocumentDate = sale.Date,
                    ApprovalDate = approvalDate,
                    ContactId = sale.SupplierId,
                    AccountId = supplier.SupplierCashAccountId,
                    Debit = sale.PurchasePostItems.Where(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل").Sum(x => Math.Round(x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity), 2)),
                    Credit = 0,
                    Description = TransactionType.PurchasePost.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.PurchasePost,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });

                //Bank/Cash Account
                transactions.Add(new Transaction
                {
                    Date = sale.Date,
                    DocumentDate = sale.Date,
                    ApprovalDate = approvalDate,
                    ContactId = sale.SupplierId,
                    AccountId = sale.BankCashAccountId.Value,
                    Credit = sale.PurchasePostItems.Sum(x => Math.Round((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) -  //total amount
                                (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount)) + //discount

                            ((x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) - (x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity))) * x.TaxRate.Rate) / 100) : 0)
                        ), 2)),
                    Debit = 0,
                    Description = TransactionType.PurchasePost.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.PurchasePost,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id
                });
            }
            await Context.Transactions.AddRangeAsync(transactions);
            Random rnd1 = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd1.Next(0, 9).ToString();
            }
            await Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = Context.SyncLog(),
                Code = _code,
            });
            await Context.SaveChangesAsync();
            scope.Complete();
            return true;
        }
        

        [Route("api/Sale/SaleAutoGenerateNo")]
        [HttpGet("SaleAutoGenerateNo")]
        [Roles("CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldTouchInvoices", "CanAddSaleReturn", "CanAddSignUpUser", "CanEditSignUpUser", "CashServiceInvoices", "CreditServiceInvoices", "CanHoldServiceInvoices")]
        public async Task<IActionResult> SaleAutoGenerateNo(string userId, string terminalId, string invoicePrefix, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetSaleRegistrationNoQuery()
            {
                UserId = userId,
                TerminalId = terminalId,
                InvoicePrefix = invoicePrefix,
                BranchId = branchId,

            });
            return Ok(autoNo);
        }

        private bool DayStart()
        {
            var dayId = User.Claims.FirstOrDefault(x => x.Type == "DayId");

            if (dayId != null && dayId.Value == "00000000-0000-0000-0000-000000000000")
            {
                return false;
            }

            return true;
        }

        #region CalculateSaleInvoice

        [Route("api/Sale/CalculateSaleInvoice")]
        [HttpGet("CalculateSaleInvoice")]
        public async Task<IActionResult> CalculateSaleInvoice()
        {
            try
            {
                var saleList = await Context.Sales
                    //.Where(x => !x.IsSaleReturnPost)
                    .OrderByDescending(x => x.RegistrationNo)
                    .Include(x => x.SaleItems)
                    .ThenInclude(x => x.Product)
                    .Include(x => x.SaleItems)
                    .ThenInclude(x => x.TaxRate)
                    .Include(x => x.Customer)
                    .ThenInclude(x => x.Account)
                    .ToListAsync();


                foreach (var invoice in saleList)
                {
                    foreach (var item in invoice.SaleItems)
                    {
                        item.TotalWithOutAmount = GrossAmount(item);
                        item.DiscountAmount = LineDiscount(item);
                        item.VatAmount = CalculateVat(item);
                        item.TotalAmount = CalculateTotal(item);
                    }

                    invoice.TotalWithOutAmount = invoice.SaleItems.Where(x => !x.IsFree).Sum(x => x.TotalWithOutAmount);
                    invoice.TotalAmount = invoice.SaleItems.Where(x => !x.IsFree).Sum(x => x.TotalAmount);
                    invoice.VatAmount = invoice.SaleItems.Where(x => !x.IsFree).Sum(x => x.VatAmount);
                    invoice.DiscountAmount = invoice.SaleItems.Where(x => !x.IsFree).Sum(x => x.DiscountAmount);
                    invoice.IsVatChange = true;
                }
                Context.Sales.UpdateRange(saleList);
                //Random rnd = new Random();
                //for (int i = 0; i < 11; i++)
                //{
                //    _code += rnd.Next(0, 9).ToString();
                //}
                //await Mediator.Send(new AddUpdateSyncRecordCommand()
                //{
                //    SyncRecordModels = Context.SyncLog(),
                //    Code = _code,
                //});
                await Context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("api/Sale/CalculateSaleOrder")]
        [HttpGet("CalculateSaleOrder")]
        public async Task<IActionResult> CalculateSaleOrder()
        {
            try
            {
                var query = await Context.SaleOrders
                    .Include(x => x.SaleOrderItems)
                    .ThenInclude(x => x.TaxRate)
                    .Include(x => x.SaleOrderItems)
                    .ThenInclude(x => x.Product)
                    .ToListAsync();

                foreach (var invoice in query)
                {
                    foreach (var item in invoice.SaleOrderItems)
                    {
                        item.TotalWithOutAmount = SaleOrderGrossAmount(item);
                        item.DiscountAmount = SaleOrderLineDiscount(item);
                        item.VatAmount = SaleOrderCalculateVat(item);
                        item.TotalAmount = SaleOrderCalculateTotal(item);
                    }

                    invoice.TotalWithOutAmount = invoice.SaleOrderItems.Sum(x => x.TotalWithOutAmount);
                    invoice.TotalAmount = invoice.SaleOrderItems.Sum(x => x.TotalAmount);
                    invoice.VatAmount = invoice.SaleOrderItems.Sum(x => x.VatAmount);
                    invoice.DiscountAmount = invoice.SaleOrderItems.Sum(x => x.DiscountAmount);
                }
                Context.SaleOrders.UpdateRange(query);
                //Random rnd = new Random();
                //for (int i = 0; i < 11; i++)
                //{
                //    _code += rnd.Next(0, 9).ToString();
                //}
                //await Mediator.Send(new AddUpdateSyncRecordCommand()
                //{
                //    SyncRecordModels = Context.SyncLog(),
                //    Code = _code,
                //});
                await Context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Route("api/Sale/CalculateDailyExpense")]
        [HttpGet("CalculateDailyExpense")]
        public async Task<IActionResult> CalculateDailyExpense()
        {
            try
            {






                var query = await Context.DailyExpenses
                    .Include(x => x.DailyExpenseDetails)
                    .ThenInclude(x => x.TaxRate)
                    .ToListAsync();

                foreach (var invoice in query)
                {


                    invoice.GrossAmount = invoice.DailyExpenseDetails
                        .Sum(x => x.Amount);
                    invoice.TotalAmount = invoice.DailyExpenseDetails
                        .Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل"
                            ? ((x.Amount * (x.TaxRate?.Rate ?? 0)) / (100)) + x.Amount
                            : x.Amount); ;
                    invoice.TotalVat = invoice.DailyExpenseDetails.Sum(x =>
                        x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل"
                            ? ((x.Amount * (x.TaxRate?.Rate ?? 0)) / (100))
                            : ((x.Amount * (x.TaxRate?.Rate ?? 0)) / (100 + (x.TaxRate?.Rate ?? 0))));
                }
                Context.DailyExpenses.UpdateRange(query);
                //Random rnd = new Random();
                //for (int i = 0; i < 11; i++)
                //{
                //    _code += rnd.Next(0, 9).ToString();
                //}
                //await Mediator.Send(new AddUpdateSyncRecordCommand()
                //{
                //    SyncRecordModels = Context.SyncLog(),
                //    Code = _code,
                //});
                await Context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        [Route("api/Sale/CalculatePurchaseInvoice")]
        [HttpGet("CalculatePurchaseInvoice")]
        public async Task<IActionResult> CalculatePurchaseInvoice()
        {
            try
            {
                var query = await Context.PurchasePosts
                    .Include(x => x.PurchasePostItems)
                    .ThenInclude(x => x.TaxRate)
                    .Include(x => x.PurchasePostItems)
                    .ThenInclude(x => x.Product)
                    .ToListAsync();

                foreach (var invoice in query)
                {
                    foreach (var item in invoice.PurchasePostItems)
                    {
                        item.TotalWithOutAmount = PurchaseInvoiceGrossAmount(item);
                        item.DiscountAmount = PurchaseInvoiceLineDiscount(item);
                        item.VatAmount = PurchaseInvoiceCalculateVat(item);
                        item.TotalAmount = PurchaseInvoiceCalculateTotal(item);
                    }

                    invoice.TotalWithOutAmount = invoice.PurchasePostItems.Sum(x => x.TotalWithOutAmount);
                    invoice.TotalAmount = invoice.PurchasePostItems.Sum(x => x.TotalAmount);
                    invoice.VatAmount = invoice.PurchasePostItems.Sum(x => x.VatAmount);
                    invoice.DiscountAmount = invoice.PurchasePostItems.Sum(x => x.DiscountAmount);
                }
                Context.PurchasePosts.UpdateRange(query);
                //Random rnd = new Random();
                //for (int i = 0; i < 11; i++)
                //{
                //    _code += rnd.Next(0, 9).ToString();
                //}
                //await Mediator.Send(new AddUpdateSyncRecordCommand()
                //{
                //    SyncRecordModels = Context.SyncLog(),
                //    Code = _code,
                //});
                await Context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region SaleInvoice
        public decimal GrossAmount(SaleItem saleItem)
        {
            decimal total;
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                total = (saleItem.IsFree ? 0 : saleItem.Quantity * saleItem.UnitPrice) * 100 / (100 + saleItem.TaxRate.Rate);
            }
            else
            {
                total = saleItem.IsFree ? 0 : saleItem.Quantity * saleItem.UnitPrice;
            }

            return total;
        }
        public decimal CalculateTotal(SaleItem saleItem)
        {
            var total = GrossAmount(saleItem);
            var vat = CalculateVat(saleItem);
            var discount = LineDiscount(saleItem);

            decimal totalAmount;
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                totalAmount = (saleItem.Discount > 0 ? saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100 : (total - saleItem.FixDiscount) * (100 + saleItem.TaxRate.Rate) / 100);
            }
            else
            {
                totalAmount = (total - discount) + vat;
            }

            return totalAmount;
        }
        public decimal CalculateVat(SaleItem saleItem)
        {
            decimal vatAmount;
            decimal discountAmount = LineDiscount(saleItem);
            decimal grossAmount = GrossAmount(saleItem);
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                vatAmount = saleItem.FixDiscount > 0 ? (grossAmount - discountAmount) * saleItem.TaxRate.Rate / 100
                    : ((saleItem.UnitPrice * saleItem.Quantity) - discountAmount) * saleItem.TaxRate.Rate / (100 + saleItem.TaxRate.Rate);
            }
            else
            {
                vatAmount = (grossAmount - discountAmount) * saleItem.TaxRate.Rate / 100;
            }

            return vatAmount;
        }
        public decimal LineDiscount(SaleItem saleItem)
        {
            var discount = saleItem.Discount == 0 ? saleItem.FixDiscount : saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100;

            return discount;
        }
        #endregion

        #region SaleOrder
        public decimal SaleOrderGrossAmount(SaleOrderItem saleItem)
        {
            decimal total;
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                total = (saleItem.IsFree ? 0 : saleItem.Quantity * saleItem.UnitPrice) * 100 / (100 + saleItem.TaxRate.Rate);
            }
            else
            {
                total = saleItem.IsFree ? 0 : saleItem.Quantity * saleItem.UnitPrice;
            }

            return total;
        }
        public decimal SaleOrderCalculateTotal(SaleOrderItem saleItem)
        {
            var total = SaleOrderGrossAmount(saleItem);
            var vat = SaleOrderCalculateVat(saleItem);
            var discount = SaleOrderLineDiscount(saleItem);

            decimal totalAmount;
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                totalAmount = (saleItem.Discount > 0 ? saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100 : (total - saleItem.FixDiscount) * (100 + saleItem.TaxRate.Rate) / 100);
            }
            else
            {
                totalAmount = (total - discount) + vat;
            }

            return totalAmount;
        }
        public decimal SaleOrderCalculateVat(SaleOrderItem saleItem)
        {
            decimal vatAmount;
            decimal discountAmount = SaleOrderLineDiscount(saleItem);
            decimal grossAmount = SaleOrderGrossAmount(saleItem);
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                vatAmount = saleItem.FixDiscount > 0 ? (grossAmount - discountAmount) * saleItem.TaxRate.Rate / 100
                    : ((saleItem.UnitPrice * saleItem.Quantity) - discountAmount) * saleItem.TaxRate.Rate / (100 + saleItem.TaxRate.Rate);
            }
            else
            {
                vatAmount = (grossAmount - discountAmount) * saleItem.TaxRate.Rate / 100;
            }

            return vatAmount;
        }
        public decimal SaleOrderLineDiscount(SaleOrderItem saleItem)
        {
            var discount = saleItem.Discount == 0 ? saleItem.FixDiscount : saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100;

            return discount;
        }
        #endregion

        #region PurchaseInvoice
        public decimal PurchaseInvoiceGrossAmount(PurchasePostItem saleItem)
        {
            decimal total;
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                total = (saleItem.Quantity * saleItem.UnitPrice) * 100 / (100 + saleItem.TaxRate.Rate);
            }
            else
            {
                total = saleItem.Quantity * saleItem.UnitPrice;
            }

            return total;
        }
        public decimal PurchaseInvoiceCalculateTotal(PurchasePostItem saleItem)
        {
            var total = PurchaseInvoiceGrossAmount(saleItem);
            var vat = PurchaseInvoiceCalculateVat(saleItem);
            var discount = PurchaseInvoiceLineDiscount(saleItem);

            decimal totalAmount;
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                totalAmount = (saleItem.Discount > 0 ? saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100 : (total - saleItem.FixDiscount) * (100 + saleItem.TaxRate.Rate) / 100);
            }
            else
            {
                totalAmount = (total - discount) + vat;
            }

            return totalAmount;
        }
        public decimal PurchaseInvoiceCalculateVat(PurchasePostItem saleItem)
        {
            decimal vatAmount;
            decimal discountAmount = PurchaseInvoiceLineDiscount(saleItem);
            decimal grossAmount = PurchaseInvoiceGrossAmount(saleItem);
            if (saleItem.TaxMethod == "Inclusive" || saleItem.TaxMethod == "شامل")
            {
                vatAmount = saleItem.FixDiscount > 0 ? (grossAmount - discountAmount) * saleItem.TaxRate.Rate / 100
                    : ((saleItem.UnitPrice * saleItem.Quantity) - discountAmount) * saleItem.TaxRate.Rate / (100 + saleItem.TaxRate.Rate);
            }
            else
            {
                vatAmount = (grossAmount - discountAmount) * saleItem.TaxRate.Rate / 100;
            }

            return vatAmount;
        }
        public decimal PurchaseInvoiceLineDiscount(PurchasePostItem saleItem)
        {
            var discount = saleItem.Discount == 0 ? saleItem.FixDiscount : saleItem.Quantity * saleItem.UnitPrice * saleItem.Discount / 100;

            return discount;
        }
        #endregion
        #endregion

        [Route("api/Sale/SaveSaleInformationReason")]
        [HttpPost("SaveSaleInformationReason")]
        public async Task<IActionResult> SaveSaleInformationReason([FromBody] UpdateSaleLookUpModel saleOrder)
        {

            {
                var id = await Mediator.Send(new UpdateSaleCommand()
                {
                    Sale = saleOrder
                });
                return Ok(id);

            }

        }



        [Route("api/Sale/SaveSaleInformation")]
        [HttpPost("SaveSaleInformation")]
        //[Roles("CashInvoices", "CreditInvoices", "CanHoldInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldTouchInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanHoldServiceInvoices")]
        //[Roles("CanCloseSaleOrder", "CanAddSaleOrder", "CanDraftSaleOrder", "CanEditSaleOrder","CanCloseSaleOrderTracking", "CanAddSaleOrderTracking", "CanDraftSaleOrderTracking", "CanEditSaleOrderTracking", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddServiceQuotation", "CanEditServiceQuotation", "CanDraftServiceQuotationCashInvoices", "CreditInvoices", "CanHoldInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldTouchInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanHoldServiceInvoices")]
        public async Task<IActionResult> SaveSaleInformation([FromBody] SaleLookupModel sale, bool invoiceWoInventory)
        {
            Message message;
            bool isUpdate = false;
            if (sale.AllowPreviousFinancialPeriod)
            {
                sale.Date = new DateTime(sale.Date.Year, sale.Date.Month, sale.Date.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
            }
            decimal total = 0;
            if (sale.SaleOrderId != null && sale.AutoPaymentVoucher == "true")
            {
                total = await Mediator.Send(new GetSaleListQueryAmount
                {
                    Id = sale.SaleOrderId,
                });
            }

            // For Sale Order
            if (sale.DocumentType == DocumentType.SaleOrder || sale.DocumentType == DocumentType.ServiceSaleOrder)
            {
                if (sale.Id == Guid.Empty)
                {
                    string autoNum = null;
                    if (sale.DocumentType != DocumentType.ServiceSaleOrder)
                    {
                        var autoNo = await Mediator.Send(new GetSaleOrderRegistrationNoQuery()
                        {
                            IsQuotation = false,
                            TerminalId = string.IsNullOrEmpty(sale.TerminalId) ? null : Guid.Parse(sale.TerminalId),
                            InvoicePrefix = sale.InvoicePrefix,
                            IsSaleOrderTracking = false,
                            BranchId = sale.BranchId,
                        });
                        autoNum = autoNo.DocumentNo;

                    }

                    message = await Mediator.Send(new AddSaleOrderCommand()
                    {
                        SaleOrder = new SaleOrderLookupModel()
                        {
                            IsQuotation = false,
                            RegistrationNo = sale.DocumentType == DocumentType.ServiceSaleOrder ? sale.RegistrationNo : autoNum,
                            ApprovalStatus = sale.InvoiceType == InvoiceType.Hold ? ApprovalStatus.Draft : ApprovalStatus.Approved,

                            IsRemove = sale.IsRemove,
                            IsEditPaidInvoice = false,
                            Date = sale.Date,
                            DispatchDate = sale.DispatchDate,
                            PickUpDate = sale.PickUpDate,
                            NoteDescription = sale.Note,
                            SaleOrderId = sale.SaleOrderId,
                            ProformaId = sale.ProformaId,
                            DeliveryChallanId = sale.DeliveryChallanId,
                            PurchaseOrderId = sale.PurchaseOrderId,
                            InquiryId = sale.InquiryId,
                            DeliveryNoteAndDate = sale.DeliveryNoteAndDate,
                            QuotationValidUpto = sale.QuotationValidUpto,
                            PerfomaValidUpto = sale.PerfomaValidUpto,
                            CustomerInquiry = sale.CustomerInquiry,
                            PoNumber = sale.PoNumber,
                            PoDate = sale.PoDate,
                            ReferredBy = sale.ReferredBy,
                            RefrenceNo = sale.RefrenceNo,
                            DoctorName = sale.DoctorName,
                            HospitalName = sale.HospitalName,
                            QuotationNo = sale.QuotationNo,
                            SaleOrderNo = sale.SaleOrderNo,
                            PeroformaInvoiceNo = sale.PeroformaInvoiceNo,
                            PurchaseOrderNo = sale.PurchaseOrderNo,
                            InquiryNo = sale.InquiryNo,
                            DeliveryId = sale.DeliveryId,
                            ShippingId = sale.ShippingId,
                            BillingId = sale.BillingId,
                            NationalId = sale.NationalId,
                            EmployeeId = sale.EmployeeId,
                            DueDate = sale.DueDate,
                            ValidityDate = sale.ValidityDate,
                            Purpose = sale.Purpose,
                            ClientPurchaseNo = sale.ClientPurchaseNo,
                            For = sale.For,

                            OneTimeDescription = sale.Description,
                            CustomerAddress = sale.CustomerAddressWalkIn,
                            Mobile = sale.Mobile,
                            LogisticId = sale.LogisticId,
                            BarCode = sale.BarCode,
                            QuotationId = sale.QuotationId,
                            WareHouseId = sale.WareHouseId,
                            CustomerId = (Guid)sale.CustomerId,
                            Refrence = sale.RefrenceNo,
                            Note = sale.TermAndCondition,
                            TotalAfterDiscount = sale.TotalAfterDiscount,
                            NoteAr = sale.TermAndConditionAr,
                            IsService = sale.IsService,
                            Description = sale.Description,
                            TaxMethod = sale.TaxMethod,
                            TaxRateId = sale.TaxRateId,
                            WarrantyLogoPath = sale.WarrantyLogoPath,
                            TerminalId = string.IsNullOrEmpty(sale.TerminalId) ? null : Guid.Parse(sale.TerminalId),
                            Discount = sale.Discount,
                            TransactionLevelDiscount = sale.TransactionLevelDiscount,
                            IsDiscountOnTransaction = sale.IsDiscountOnTransaction,
                            IsFixed = sale.IsFixed,
                            IsBeforeTax = sale.IsBeforeTax,
                            TotalAmount = sale.TotalAmount,
                            VatAmount = sale.VatAmount,
                            DiscountAmount = sale.DiscountAmount,
                            GrossAmounts = sale.GrossAmount,
                            BranchId = sale.BranchId,
                            AttachmentList = sale.AttachmentList.Select(x => new AttachmentLookUpModel
                            {
                                Id = x.Id,
                                Date = x.Date,
                                DocumentId = x.DocumentId,
                                Description = x.Description,
                                FileName = x.FileName,
                                Path = x.Path,
                            }).ToList(),
                            SaleOrderItems = sale.SaleItems.Select(x => new SaleOrderItemLookupModel
                            {
                                ProductId = x.ProductId,
                                TaxRateId = x.TaxRateId,
                                TaxMethod = x.TaxMethod,
                                UnitName = x.UnitName,
                                Discount = x.Discount,
                                FixDiscount = x.FixDiscount,
                                Quantity = x.Quantity,
                                QuantityOut = x.Quantity,
                                UnitPrice = x.UnitPrice,
                                BatchNo = x.BatchNo,
                                Description = x.Description,
                                ServiceItem = x.ServiceItem,
                                Serial = x.Serial,
                                GuaranteeDate = x.GuaranteeDate,
                                StyleNumber = x.StyleNumber,
                                IsFree = x.IsFree,
                                Scheme = x.Scheme,
                                SchemeQuantity = x.SchemeQuantity,
                                SchemePhysicalQuantity = x.SchemePhysicalQuantity,
                                TotalAmount = x.TotalAmount,
                                VatAmount = x.VatAmount,
                                DiscountAmount = x.DiscountAmount,
                                GrossAmounts = x.GrossAmount,
                            }).ToList(),

                        }
                    });

                }
                else
                {
                    {

                        message = await Mediator.Send(new AddSaleOrderCommand()
                        {
                            SaleOrder = new SaleOrderLookupModel()
                            {
                                IsRemove = sale.IsRemove,
                                IsQuotation = false,
                                Id = sale.Id,
                                NoteDescription = sale.Note,
                                IsEditPaidInvoice = sale.IsEditPaidInvoice,
                                DispatchDate = sale.DispatchDate,
                                PickUpDate = sale.PickUpDate,
                                SaleOrderId = sale.SaleOrderId,
                                ProformaId = sale.ProformaId,
                                DeliveryChallanId = sale.DeliveryChallanId,
                                PurchaseOrderId = sale.PurchaseOrderId,
                                InquiryId = sale.InquiryId,
                                DeliveryNoteAndDate = sale.DeliveryNoteAndDate,
                                QuotationValidUpto = sale.QuotationValidUpto,
                                PerfomaValidUpto = sale.PerfomaValidUpto,
                                CustomerInquiry = sale.CustomerInquiry,
                                PoNumber = sale.PoNumber,
                                PoDate = sale.PoDate,
                                TotalAfterDiscount = sale.TotalAfterDiscount,
                                ReferredBy = sale.ReferredBy,
                                RefrenceNo = sale.RefrenceNo,
                                DoctorName = sale.DoctorName,
                                HospitalName = sale.HospitalName,
                                QuotationNo = sale.QuotationNo,
                                SaleOrderNo = sale.SaleOrderNo,
                                PeroformaInvoiceNo = sale.PeroformaInvoiceNo,
                                PurchaseOrderNo = sale.PurchaseOrderNo,
                                InquiryNo = sale.InquiryNo,
                                DeliveryId = sale.DeliveryId,
                                ShippingId = sale.ShippingId,
                                BillingId = sale.BillingId,
                                NationalId = sale.NationalId,
                                EmployeeId = sale.EmployeeId,
                                DueDate = sale.DueDate,
                                ValidityDate = sale.ValidityDate,
                                Purpose = sale.Purpose,
                                ClientPurchaseNo = sale.ClientPurchaseNo,
                                For = sale.For,
                                BranchId = sale.BranchId,
                                RegistrationNo = sale.RegistrationNo,
                                ApprovalStatus = sale.InvoiceType == InvoiceType.Hold ? ApprovalStatus.Draft : ApprovalStatus.Approved,
                                Date = sale.Date,
                                OneTimeDescription = sale.Description,
                                CustomerAddress = sale.CustomerAddressWalkIn,
                                Mobile = sale.Mobile,
                                LogisticId = sale.LogisticId,
                                BarCode = sale.BarCode,
                                QuotationId = sale.QuotationId,
                                WareHouseId = sale.WareHouseId,
                                CustomerId = (Guid)sale.CustomerId,
                                Refrence = sale.RefrenceNo,
                                Note = sale.TermAndCondition,
                                NoteAr = sale.TermAndConditionAr,
                                IsService = sale.IsService,
                                Description = sale.Description,
                                TaxMethod = sale.TaxMethod,
                                TaxRateId = sale.TaxRateId,
                                WarrantyLogoPath = sale.WarrantyLogoPath,
                                TerminalId = string.IsNullOrEmpty(sale.TerminalId) ? null : Guid.Parse(sale.TerminalId),
                                Discount = sale.Discount,
                                TransactionLevelDiscount = sale.TransactionLevelDiscount,
                                IsDiscountOnTransaction = sale.IsDiscountOnTransaction,
                                IsFixed = sale.IsFixed,
                                IsBeforeTax = sale.IsBeforeTax,

                                TotalAmount = sale.TotalAmount,
                                VatAmount = sale.VatAmount,
                                DiscountAmount = sale.DiscountAmount,
                                GrossAmounts = sale.GrossAmount,
                                AttachmentList = sale.AttachmentList.Select(x => new AttachmentLookUpModel
                                {
                                    Id = x.Id,
                                    Date = x.Date,
                                    DocumentId = x.DocumentId,
                                    Description = x.Description,
                                    FileName = x.FileName,
                                    Path = x.Path,
                                }).ToList(),
                                SaleOrderItems = sale.SaleItems.Select(x => new SaleOrderItemLookupModel
                                {
                                    ProductId = x.ProductId,
                                    TaxRateId = x.TaxRateId,
                                    TaxMethod = x.TaxMethod,
                                    Discount = x.Discount,
                                    FixDiscount = x.FixDiscount,
                                    Quantity = x.Quantity,
                                    QuantityOut = x.Quantity,
                                    UnitName = x.UnitName,
                                    UnitPrice = x.UnitPrice,
                                    BatchNo = x.BatchNo,
                                    Description = x.Description,
                                    ServiceItem = x.ServiceItem,
                                    Serial = x.Serial,
                                    GuaranteeDate = x.GuaranteeDate,
                                    StyleNumber = x.StyleNumber,
                                    IsFree = x.IsFree,
                                    Scheme = x.Scheme,
                                    SchemeQuantity = x.SchemeQuantity,
                                    SchemePhysicalQuantity = x.SchemePhysicalQuantity,

                                    TotalAmount = x.TotalAmount,
                                    VatAmount = x.VatAmount,
                                    DiscountAmount = x.DiscountAmount,
                                    GrossAmounts = x.GrossAmount,
                                }).ToList(),

                            }
                        });
                        isUpdate = true;

                    }
                }
            }

            //Credit Invoice
            else if (sale.DocumentType == DocumentType.CreditNote)
            {
                var autoNo = await Mediator.Send(new GetCreditNoteAutoNo()
                {
                    TerminalId = sale.TerminalId,
                    InvoicePrefix = sale.InvoicePrefix,
                    IsCreditNote = true,
                    BranchId = sale.BranchId,
                });
                message = await Mediator.Send(new AddUpdateCreditNoteCommand()
                {
                    CreditNotes = new CreditNotesModel()
                    {
                        Code = autoNo,
                        ApprovalStatus = sale.InvoiceType == InvoiceType.Hold ? ApprovalStatus.Draft : ApprovalStatus.Approved,
                        Date = sale.Date,
                        CustomerAddress = sale.CustomerAddressWalkIn,
                        Mobile = sale.Mobile,
                        WareHouseId = sale.WareHouseId,
                        ContactId = (Guid)sale.CustomerId,
                        TaxMethod = sale.TaxMethod,
                        TaxRateId = sale.TaxRateId,
                        IsService = sale.IsService,
                        GrossAmount = sale.GrossAmount,
                        VatAmount = sale.VatAmount,
                        Amount = sale.Amount,
                        BranchId = sale.BranchId,
                        IsCreditNote = true,
                        TerminalId = string.IsNullOrEmpty(sale.TerminalId) ? null : Guid.Parse(sale.TerminalId),
                        AttachmentList = sale.AttachmentList.Select(x => new AttachmentLookUpModel
                        {
                            Id = x.Id,
                            Date = x.Date,
                            DocumentId = x.DocumentId,
                            Description = x.Description,
                            FileName = x.FileName,
                            Path = x.Path,
                        }).ToList(),
                        SaleOrderItems = sale.SaleItems.Select(x => new CreditNotesItemModel
                        {
                            ProductId = x.ProductId,
                            Description = x.Description,
                            ServiceItem = x.ServiceItem,
                            IsFree = x.IsFree,
                            TaxRateId = x.TaxRateId,
                            WareHouseId = x.WareHouseId,
                            TaxMethod = x.TaxMethod,
                            Discount = x.Discount,
                            FixDiscount = x.FixDiscount,
                            Quantity = x.Quantity,
                            UnitPrice = x.UnitPrice,
                        }).ToList(),

                    }


                });
            }

            // For Sale Quotation
            else if (sale.DocumentType == DocumentType.SaleQuotation || sale.DocumentType == DocumentType.ServiceQuotation)
            {
                if (sale.Id == Guid.Empty)
                {
                    string autoNo = null;
                    if (sale.DocumentType != DocumentType.ServiceQuotation)
                    {
                        var autoNoVal = await Mediator.Send(new GetSaleOrderRegistrationNoQuery()
                        {
                            IsQuotation = true,
                            TerminalId = string.IsNullOrEmpty(sale.TerminalId) ? null : Guid.Parse(sale.TerminalId),
                            InvoicePrefix = sale.InvoicePrefix,
                            IsSaleOrderTracking = false,
                            BranchId = sale.BranchId,
                        });
                        autoNo = autoNoVal.DocumentNo;
                    }
                    string base64String = null;
                    if (!string.IsNullOrEmpty(sale.WarrantyLogoPath) && System.IO.File.Exists(sale.WarrantyLogoPath))
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(sale.WarrantyLogoPath);
                        base64String = Convert.ToBase64String(fileBytes);
                    }
                    message = await Mediator.Send(new AddSaleOrderCommand()
                    {
                        SaleOrder = new SaleOrderLookupModel()
                        {
                            IsQuotation = true,
                            NoteDescription = sale.Note,
                            IsEditPaidInvoice = false,
                            SaleOrderId = sale.SaleOrderId,
                            ProformaId = sale.ProformaId,
                            DispatchDate = sale.DispatchDate,
                            PickUpDate = sale.PickUpDate,
                            DeliveryChallanId = sale.DeliveryChallanId,
                            PurchaseOrderId = sale.PurchaseOrderId,
                            InquiryId = sale.InquiryId,
                            DeliveryNoteAndDate = sale.DeliveryNoteAndDate,
                            QuotationValidUpto = sale.QuotationValidUpto,
                            PerfomaValidUpto = sale.PerfomaValidUpto,
                            TotalAfterDiscount = sale.TotalAfterDiscount,
                            CustomerInquiry = sale.CustomerInquiry,
                            PoNumber = sale.PoNumber,
                            PoDate = sale.PoDate,
                            ReferredBy = sale.ReferredBy,
                            RefrenceNo = sale.RefrenceNo,
                            DoctorName = sale.DoctorName,
                            HospitalName = sale.HospitalName,
                            QuotationNo = sale.QuotationNo,
                            SaleOrderNo = sale.SaleOrderNo,
                            PeroformaInvoiceNo = sale.PeroformaInvoiceNo,
                            PurchaseOrderNo = sale.PurchaseOrderNo,
                            InquiryNo = sale.InquiryNo,
                            DeliveryId = sale.DeliveryId,
                            ShippingId = sale.ShippingId,
                            BillingId = sale.BillingId,
                            NationalId = sale.NationalId,
                            EmployeeId = sale.EmployeeId,
                            DueDate = sale.DueDate,
                            ValidityDate = sale.ValidityDate,
                            Purpose = sale.Purpose,
                            ClientPurchaseNo = sale.ClientPurchaseNo,
                            For = sale.For,
                            Description = sale.Description,

                            RegistrationNo = sale.DocumentType == DocumentType.ServiceQuotation ? sale.RegistrationNo : autoNo,
                            ApprovalStatus = sale.InvoiceType == InvoiceType.Hold ? ApprovalStatus.Draft : ApprovalStatus.Approved,
                            Date = sale.Date,
                            OneTimeDescription = sale.Description,
                            CustomerAddress = sale.CustomerAddressWalkIn,
                            Mobile = sale.Mobile,
                            LogisticId = sale.LogisticId,
                            BarCode = sale.BarCode,
                            QuotationId = sale.QuotationId,
                            WareHouseId = sale.WareHouseId,
                            CustomerId = (Guid)sale.CustomerId,
                            Refrence = sale.RefrenceNo,
                            Note = sale.TermAndCondition,
                            NoteAr = sale.TermAndConditionAr,
                            IsService = sale.IsService,
                            TaxMethod = sale.TaxMethod,
                            TaxRateId = sale.TaxRateId,
                            WarrantyLogoPath = sale.WarrantyLogoPath,
                            TerminalId = string.IsNullOrEmpty(sale.TerminalId) ? null : Guid.Parse(sale.TerminalId),
                            Discount = sale.Discount,
                            TransactionLevelDiscount = sale.TransactionLevelDiscount,
                            IsDiscountOnTransaction = sale.IsDiscountOnTransaction,
                            IsFixed = sale.IsFixed,
                            IsBeforeTax = sale.IsBeforeTax,

                            TotalAmount = sale.TotalAmount,
                            VatAmount = sale.VatAmount,
                            DiscountAmount = sale.DiscountAmount,
                            GrossAmounts = sale.GrossAmount,
                            BranchId = sale.BranchId,
                            AttachmentList = sale.AttachmentList.Select(x => new AttachmentLookUpModel
                            {
                                Id = x.Id,
                                Date = x.Date,
                                DocumentId = x.DocumentId,
                                Description = x.Description,
                                FileName = x.FileName,
                                Path = x.Path,
                            }).ToList(),
                            SaleOrderItems = sale.SaleItems.Select(x => new SaleOrderItemLookupModel
                            {
                                ProductId = x.ProductId,
                                TaxRateId = x.TaxRateId,
                                UnitName = x.UnitName,
                                TaxMethod = x.TaxMethod,
                                Discount = x.Discount,
                                FixDiscount = x.FixDiscount,
                                Quantity = x.Quantity,
                                QuantityOut = x.Quantity,
                                UnitPrice = x.UnitPrice,
                                BatchNo = x.BatchNo,
                                Description = x.Description,
                                ServiceItem = x.ServiceItem,
                                Serial = x.Serial,
                                GuaranteeDate = x.GuaranteeDate,
                                StyleNumber = x.StyleNumber,
                                IsFree = x.IsFree,
                                Scheme = x.Scheme,
                                SchemeQuantity = x.SchemeQuantity,
                                SchemePhysicalQuantity = x.SchemePhysicalQuantity,

                                TotalAmount = x.TotalAmount,
                                VatAmount = x.VatAmount,
                                DiscountAmount = x.DiscountAmount,
                                GrossAmounts = x.GrossAmount,
                            }).ToList(),

                        }
                    });

                }
                else

                {
                    message = await Mediator.Send(new AddSaleOrderCommand()
                    {

                        SaleOrder = new SaleOrderLookupModel()
                        {
                            IsQuotation = true,
                            Id = sale.Id,
                            DispatchDate = sale.DispatchDate,
                            IsEditPaidInvoice = sale.IsEditPaidInvoice,
                            PickUpDate = sale.PickUpDate,
                            NoteDescription = sale.Note,
                            SaleOrderId = sale.SaleOrderId,
                            ProformaId = sale.ProformaId,
                            DeliveryChallanId = sale.DeliveryChallanId,
                            PurchaseOrderId = sale.PurchaseOrderId,
                            InquiryId = sale.InquiryId,
                            DeliveryDate = sale.DeliveryDate,
                            DeliveryNoteAndDate = sale.DeliveryNoteAndDate,
                            QuotationValidUpto = sale.QuotationValidUpto,
                            PerfomaValidUpto = sale.PerfomaValidUpto,
                            CustomerInquiry = sale.CustomerInquiry,
                            PoNumber = sale.PoNumber,
                            PoDate = sale.PoDate,
                            ReferredBy = sale.ReferredBy,
                            RefrenceNo = sale.RefrenceNo,
                            DoctorName = sale.DoctorName,
                            TotalAfterDiscount = sale.TotalAfterDiscount,
                            HospitalName = sale.HospitalName,
                            QuotationNo = sale.QuotationNo,
                            SaleOrderNo = sale.SaleOrderNo,
                            PeroformaInvoiceNo = sale.PeroformaInvoiceNo,
                            PurchaseOrderNo = sale.PurchaseOrderNo,
                            InquiryNo = sale.InquiryNo,
                            DeliveryId = sale.DeliveryId,
                            ShippingId = sale.ShippingId,
                            BillingId = sale.BillingId,
                            NationalId = sale.NationalId,
                            EmployeeId = sale.EmployeeId,
                            DueDate = sale.DueDate,
                            ValidityDate = sale.ValidityDate,
                            Purpose = sale.Purpose,
                            ClientPurchaseNo = sale.ClientPurchaseNo,
                            For = sale.For,

                            RegistrationNo = sale.RegistrationNo,
                            ApprovalStatus = sale.InvoiceType == InvoiceType.Hold ? ApprovalStatus.Draft : ApprovalStatus.Approved,
                            Date = sale.Date,
                            OneTimeDescription = sale.Description,
                            CustomerAddress = sale.CustomerAddressWalkIn,
                            Mobile = sale.Mobile,
                            LogisticId = sale.LogisticId,
                            BarCode = sale.BarCode,
                            QuotationId = sale.QuotationId,
                            WareHouseId = sale.WareHouseId,
                            CustomerId = (Guid)sale.CustomerId,
                            Refrence = sale.RefrenceNo,
                            Note = sale.TermAndCondition,
                            NoteAr = sale.TermAndConditionAr,
                            IsService = sale.IsService,
                            Description = sale.Description,
                            TaxMethod = sale.TaxMethod,
                            TaxRateId = sale.TaxRateId,
                            WarrantyLogoPath = sale.WarrantyLogoPath,
                            TerminalId = string.IsNullOrEmpty(sale.TerminalId) ? null : Guid.Parse(sale.TerminalId),
                            Discount = sale.Discount,
                            TransactionLevelDiscount = sale.TransactionLevelDiscount,
                            IsDiscountOnTransaction = sale.IsDiscountOnTransaction,
                            IsFixed = sale.IsFixed,
                            IsBeforeTax = sale.IsBeforeTax,
                            BranchId = sale.BranchId,
                            TotalAmount = sale.TotalAmount,
                            VatAmount = sale.VatAmount,
                            DiscountAmount = sale.DiscountAmount,
                            GrossAmounts = sale.GrossAmount,
                            AttachmentList = sale.AttachmentList.Select(x => new AttachmentLookUpModel
                            {
                                Id = x.Id,
                                Date = x.Date,
                                DocumentId = x.DocumentId,
                                Description = x.Description,
                                FileName = x.FileName,
                                Path = x.Path,
                            }).ToList(),
                            SaleOrderItems = sale.SaleItems.Select(x => new SaleOrderItemLookupModel
                            {
                                ProductId = x.ProductId,
                                TaxRateId = x.TaxRateId,
                                TaxMethod = x.TaxMethod,
                                Discount = x.Discount,
                                UnitName = x.UnitName,
                                FixDiscount = x.FixDiscount,
                                Quantity = x.Quantity,
                                QuantityOut = x.Quantity,
                                UnitPrice = x.UnitPrice,
                                BatchNo = x.BatchNo,
                                Description = x.Description,
                                ServiceItem = x.ServiceItem,
                                Serial = x.Serial,
                                GuaranteeDate = x.GuaranteeDate,
                                StyleNumber = x.StyleNumber,
                                IsFree = x.IsFree,
                                Scheme = x.Scheme,
                                SchemeQuantity = x.SchemeQuantity,
                                SchemePhysicalQuantity = x.SchemePhysicalQuantity,

                                TotalAmount = x.TotalAmount,
                                VatAmount = x.VatAmount,
                                DiscountAmount = x.DiscountAmount,
                                GrossAmounts = x.GrossAmount,
                            }).ToList(),

                        }
                    });
                    isUpdate = true;

                }
            }

            //Proforma Invoice
            else if (sale.DocumentType == DocumentType.ProformaInvoice || sale.DocumentType == DocumentType.ServiceProformaInvoice)
            {
                sale.InvoiceType = InvoiceType.ProformaInvoice;
                sale.DocumentType = DocumentType.ProformaInvoice;
                message = await Mediator.Send(new AddUpdateSaleCommand
                {
                    Sale = sale,
                    CounterId = sale.CounterId,
                    Payment = total,
                    InvoiceWoInventory = invoiceWoInventory
                });
            }
            
            //PO Customer
            else if (sale.DocumentType == DocumentType.PurchaseOrder || sale.DocumentType == DocumentType.PurchaseOrder)
            {
                sale.InvoiceType = InvoiceType.PurchaseOrder;
                sale.DocumentType = DocumentType.PurchaseOrder;
                message = await Mediator.Send(new AddUpdateSaleCommand
                {
                    Sale = sale,
                    CounterId = sale.CounterId,
                    Payment = total,
                    InvoiceWoInventory = invoiceWoInventory
                });
            }

            //Global Invoice
            else if (sale.DocumentType == DocumentType.GlobalInvoice)
            {
                sale.InvoiceType = InvoiceType.GlobalInvoice;
                message = await Mediator.Send(new AddUpdateSaleCommand
                {
                    Sale = sale,
                    CounterId = sale.CounterId,
                    Payment = total,
                    InvoiceWoInventory = invoiceWoInventory
                });
            }

            //Receipt Invoice
            else if (sale.DocumentType == DocumentType.ReceiptInvoice)
            {
                sale.InvoiceType = InvoiceType.ReceiptInvoice;
                message = await Mediator.Send(new AddUpdateSaleCommand
                {
                    Sale = sale,
                    CounterId = sale.CounterId,
                    Payment = total,
                    InvoiceWoInventory = invoiceWoInventory
                });
            }

            // For Sale Invoice
            else
            {
                message = await Mediator.Send(new AddUpdateSaleCommand
                {
                    Sale = sale,
                    CounterId = sale.CounterId,
                    Payment = total,
                    InvoiceWoInventory = invoiceWoInventory
                });
            }


            if (message.Id != Guid.Empty && !isUpdate)
            {
                return Ok(new { Message = message, Action = "Add" });
            }
            else if (message.Id != Guid.Empty && isUpdate)
            {
                return Ok(new { Message = message, Action = "Update" });
            }
            else
            {
                return Ok(new { Message = message, Action = "Error" });
            }

        }

        private DateTime DateTime(string v)
        {
            throw new NotImplementedException();
        }

        [Route("api/Sale/SaveSaleReturn")]
        [HttpPost("SaveSaleReturn")]
        [Roles("CanAddSaleReturn")]
        public async Task<IActionResult> SaveSaleReturn([FromBody] SaleLookupModel sale)
        {
            var counterId = User.Claims.FirstOrDefault(x => x.Type == "CounterId");

            var message = await Mediator.Send(new AddSaleReturnCommand()
            {
                Sale = sale,
                CounterId = counterId?.Value
            });

            if (message.Id != Guid.Empty)
            {
                return Ok(new { message = message, Action = "Add" });

            }
            else
            {
                return Ok(new { Message = message, Action = "Error" });
            }
        }

        [Route("api/Sale/SaveVerifiedInvoice")]
        [HttpPost("SaveVerifiedInvoice")]
        [Roles("CanAddSaleReturn")]
        public async Task<IActionResult> SaveVerifiedInvoice([FromBody] List<Guid> verifiedInvoicesList, bool status)
        {
            var message = await Mediator.Send(new SaveVerifiedInvoicesCommand()
            {
                VerifiedInvoicesList = verifiedInvoicesList,
                Status = status,
            });

            return Ok(message);
        }

        [Route("api/Sale/SaleInvoiceList")]
        [HttpGet("SaleInvoiceList")]
        //[Roles("CanViewHoldInvoices", "CanViewCreditInvoices", "CanViewPaidInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldTouchInvoices", "CanAddSaleReturn", "CanViewSaleReturn", "CanViewSaleInvoiceReport", "CanViewPaidServiceInvoices", "CanViewCreditServiceInvoices", "CanViewHoldServiceInvoices")]
        public async Task<IActionResult> SaleInvoiceList(InvoiceType status, string searchTerm, int? pageNumber, string saleHoldType, DateTime? fromDate, DateTime? toDate, DateTime? fromTime, DateTime? toTime, Guid? userId, bool isDiscountBeforeVat, bool isService, int? month, int? year, Guid? customerId, string customerType, Guid? branchId, bool isDropdown, bool isSaleReturnPost, Guid? contactId, bool isExpense, bool isPartially)
        {
            var purchaseOrder = await Mediator.Send(new GetSaleListForListingQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                Satus = status,
                IsSaleReturnPost = isSaleReturnPost,
                IsDropdown = isDropdown,
                FromDate = fromDate,
                ToDate = toDate,
                CustomerId = customerId,
                FromTime = fromTime,
                ToTime = toTime,
                UserId = userId,
                SaleHoldType = saleHoldType,
                IsService = isService,
                IsDiscountBeforeVat = isDiscountBeforeVat,
                Year = year,
                Month = month,
                CustomerType = customerType,
                BranchId = branchId,
                //TerminalId = terminalId,
                ContactId = contactId,
                IsExpense = isExpense,
                IsPartially = isPartially,

            });

            return Ok(purchaseOrder);
        }

        [Route("api/Sale/SaleList")]
        [HttpGet("SaleList")]
        [Roles("CanViewHoldInvoices", "CanViewCreditInvoices", "CanViewPaidInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanHoldTouchInvoices", "CanAddSaleReturn", "CanViewSaleReturn", "CanViewSaleInvoiceReport", "CanViewPaidServiceInvoices", "CanViewCreditServiceInvoices", "CanViewHoldServiceInvoices")]

        public async Task<IActionResult> SaleList(InvoiceType status, string searchTerm, int? pageNumber, bool isService, bool isDropdown, bool isSaleReturnPost, bool isExpense, bool isDiscountBeforeVat, DateTime? fromDate, DateTime? toDate, Guid? contactId, DateTime? fromTime, DateTime? toTime, Guid? userId, Guid? terminalId, int? month, int? year, string saleHoldType, Guid? customerId, string customerType, Guid? branchId,bool isPartially)
        {
            var purchaseOrder = await Mediator.Send(new GetSaleListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                Satus = status,
                IsSaleReturnPost = isSaleReturnPost,
                IsDropdown = isDropdown,
                IsExpense = isExpense,
                FromDate = fromDate,
                ToDate = toDate,
                ContactId = contactId,
                CustomerId = customerId,
                FromTime = fromTime,
                ToTime = toTime,
                UserId = userId,
                TerminalId = terminalId,
                SaleHoldType = saleHoldType,
                IsService = isService,
                IsDiscountBeforeVat = isDiscountBeforeVat,
                Year = year,
                Month = month,
                CustomerType = customerType,
                BranchId = branchId,
                IsPartially = isPartially,

            });

            return Ok(purchaseOrder);
        }

        [Route("api/Sale/SaleDashboardList")]
        [HttpGet("SaleDashboardList")]

        public async Task<IActionResult> SaleDashboardList(bool isService, bool isPurchase, bool isProforma, bool isPurchaseOrder, bool isPurchaseReturn, Guid? branchId)
        {
            var purchaseOrder = await Mediator.Send(new SaleListQueryForOnlySaleRecord
            {
                IsService = isService,
                IsPurchase = isPurchase,
                IsProforma = isProforma,
                IsPurchaseOrder = isPurchaseOrder,
                IsPurchaseReturn = isPurchaseReturn,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }
        [Route("api/Sale/SaleOrderDashboardList")]
        [HttpGet("SaleOrderDashboardList")]

        public async Task<IActionResult> SaleOrderDashboardList(bool isService, bool isQuotation, bool isPurchaseOrderInvoice, bool supplierQuotation, Guid? branchId)
        {
            var purchaseOrder = await Mediator.Send(new DashboardQueryForSaleOrder
            {
                IsService = isService,
                IsQuotation = isQuotation,
                IsPurchaseOrderInvoice = isPurchaseOrderInvoice,
                IsSupplierQuotation = supplierQuotation,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }



        [Route("api/Sale/SendSms")]
        [HttpGet("SendSms")]

        public async Task<IActionResult> SendSms(Guid id, bool isSendSms)
        {
            try
            {
                var sale = await Context.Sales.FirstOrDefaultAsync(s => s.Id == id);
                if (sale != null)
                {
                    sale.IsSendMsg = isSendSms;
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    await Mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    });
                    await Context.SaveChangesAsync();
                }
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Some thing went wrong");
            }

        }





        [Route("api/Sale/SaleServiceList")]
        [HttpGet("SaleServiceList")]
        [Roles("CanViewHoldServiceInvoices", "CanViewPaidServiceInvoices", "CanViewCreditServiceInvoices")]

        public async Task<IActionResult> SaleServiceList(InvoiceType status, string searchTerm, int? pageNumber, bool isService, bool isDropdown, bool isSaleReturnPost, bool isExpense, bool isDiscountBeforeVat, DateTime? fromDate, DateTime? toDate, Guid? contactId, Guid? customerId, DateTime? fromTime, DateTime? toTime, Guid? userId, Guid? terminalId)
        {
            var purchaseOrder = await Mediator.Send(new GetSaleListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                Satus = status,
                IsSaleReturnPost = isSaleReturnPost,
                IsDropdown = isDropdown,
                IsExpense = isExpense,
                FromDate = fromDate,
                ToDate = toDate,
                ContactId = contactId,
                FromTime = fromTime,
                ToTime = toTime,
                UserId = userId,
                TerminalId = terminalId,
                CustomerId = customerId,
                IsService = isService,
                IsDiscountBeforeVat = isDiscountBeforeVat,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/Sale/DeleteSale")]
        [HttpGet("DeleteSale")]
        [Roles("CanDeleteHoldInvoices", "CanHoldTouchInvoices", "CanDeleteHoldServiceInvoices")]

        public async Task<IActionResult> DeleteSale(Guid id, bool isNormal)
        {
            //if (!DayStart())
            //    throw new ApplicationException("Please start day first.");

            var message = await Mediator.Send(new DeleteSaleCommand
            {
                Id = id,
                IsNormal = isNormal
            });
            return Ok(new { message });
        }

        [Route("api/Sale/SaleDetail")]
        [HttpGet("SaleDetail")]
        [Roles("CanViewInvoiceDetail", "CanEditHoldInvoices", "CanGenerateSaleRecordPdf", "CanGenerateSaleRecordSheet", "CanSendSaleRecordEmail", "CanA4Print", "CanPosPrint", "CanDuplicateInvoices", "CashInvoices", "CreditInvoices", "CanHoldTouchInvoices", "CanAddSaleReturn", "CanViewDetailSaleReturn", "CanPrintInvoice", "CanSendSaleServiceEmail", "CanGenerateSaleServiceSheet", "CanGenerateSaleServicePdf", "CanViewServiceInvoiceDetail", "CanDuplicateServiceInvoices", "CanEditHoldServiceInvoices", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanViewCustomerBalance")]

        public async Task<IActionResult> SaleDetail(Guid id, bool isReturn, string invoiceNo, bool isFifo, int openBatch, bool colorVariants, bool deliveryChallan, bool simpleQuery, bool? isReport)
        {
            try
            {
                if (isReport == true)
                {
                    simpleQuery = true;
                }
                var sale = await Mediator.Send(new GetSaleDetailQuery()
                {
                    Id = id,
                    IsReturn = isReturn,
                    SimpleQuery = simpleQuery,
                    InvoiceNo = invoiceNo,
                    IsFifo = isFifo,
                    OpenBatch = openBatch,
                    ColorVariants = colorVariants,
                    DeliveryChallan = deliveryChallan,
                });
                if (isReport == true)
                {

                    TLVCls tlv = new TLVCls(sale.CustomerName, sale.CustomerVat == null ? "0" : sale.CustomerVat, Convert.ToDateTime(sale.Date), Convert.ToDouble(sale.TotalAmount), Convert.ToDouble(sale.VatAmount));
                    var qrValue = tlv.ToBase64();
                    QRCodeData qrCodeData;
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    {
                        qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                    }
                    var imgType = Base64QRCode.ImageType.Jpeg;
                    var qrCode = new Base64QRCode(qrCodeData);
                    string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);
                    //BarCode
                    string invoiceBarcode;
                    BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
                    Image img = barcode.Encode(BarcodeLib.TYPE.UPCA, sale.BarCode, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 45);
                    using (var ms = new MemoryStream())
                    {
                        using (var bitmap = new Bitmap(img))
                        {
                            bitmap.Save(ms, ImageFormat.Jpeg);
                            invoiceBarcode = Convert.ToBase64String(ms.GetBuffer());
                        }
                    }

                    sale.BarCode = invoiceBarcode;
                    sale.QRCode = qrCodeImageAsBase64;
                }
                return Ok(sale);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //[Route("api/Sale/SaleAmountDetail")]
        //[HttpGet("SaleAmountDetail")]
        //[Roles("Can Edit Sale Invoice as Hold", "Can Edit Sale Invoice as Paid", "Can Save Sale Invoice as Hold", "Noble Admin", "Can Save Sale Invoice as Paid", "Can View  Customer", "Can View Ware House", "Can View  Product", "Can Save Sale Return", "Can Edit  Sale Return")]

        //public async Task<IActionResult> SaleAmountDetail(Guid id)
        //{

        //    var sale = await Mediator.Send(new SaleAmountDetailQuery()
        //    {
        //        Id = id,
        //    });
        //    return Ok(sale);
        //}
        [Route("api/Sale/GetRemainingInvoiceAmount")]
        [HttpGet("GetRemainingInvoiceAmount")]
        public async Task<IActionResult> GetRemainingInvoiceAmount(Guid id)
        {
            var remainingBalance = await Mediator.Send(
                new GetRemainingInvoiceBalance
                {
                    InvoiceId = id
                });
            return Ok(remainingBalance);
        }

        [Route("api/Sale/DeleteSalePermanently")]
        [HttpGet("DeleteSalePermanently")]
        public async Task<IActionResult> DeleteSalePermanently(Guid id)
        {
            var sale = await Mediator.Send(
                new DeleteSalePermanentlyCommand
                {
                    Id = id
                });
            return Ok(sale);
        }
        #endregion

        #region For CashCustomer
        //[Route("api/Sale/SaveCashCustomer")]
        //[HttpPost("SaveCashCustomer")]
        //[Roles("Can Save Sale Invoice as Hold", "Noble Admin", "Can Save Sale Invoice as Paid", "Can View  Customer", "Can View Ware House", "Can View  Product")]
        //public async Task<IActionResult> SaveCashCustomer([FromBody] CashCustomerLookupModel cashCustomer)
        //{
        //    var id = await Mediator.Send(new AddCashCustomerCommand()
        //    {
        //        CashCustomer = cashCustomer
        //    });

        //    return Ok(new { IsSuccess = true, id });
        //}

        [Route("api/Sale/GetCashCustomer")]
        [HttpGet("GetCashCustomer")]
        [Roles("CashServiceInvoicesForWalkIn", "CashInvoicesForWalkIn", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3")]
        public async Task<IActionResult> GetCashCustomer()
        {
            var cashCustomers = await Mediator.Send(new GetCashCustomerListQuery());

            return Ok(cashCustomers);
        }
        #endregion

        //[Route("api/Sale/TouchInvoice")]
        //[HttpGet("TouchInvoice")]
        //[Roles("Can Save Touch Invoice", "Noble Admin")]

        //public IActionResult TouchInvoice()
        //{
        //    return Ok();
        //}

        #region Dispatch Note
        [Route("api/Sale/DispatchNoteAutoGenerateNo")]
        [HttpGet("DispatchNoteAutoGenerateNo")]
        [Roles("CanAddDispatchNote")]
        public async Task<IActionResult> DispatchNoteAutoGenerateNo(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetDispatchNoteRegistrationNoQuery()
            {
                BranchId = branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Sale/SaveDispatchNote")]
        [HttpPost("SaveDispatchNote")]
        [Roles("CanAddDispatchNote")]
        public async Task<IActionResult> SaveDispatchNote([FromBody] DispatchNoteLookupModel dispatchNote)
        {
            if (dispatchNote.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetDispatchNoteRegistrationNoQuery()
                {
                    BranchId = dispatchNote.BranchId
                });
                dispatchNote.RegistrationNo = autoNo;
                await Mediator.Send(new AddDispatchNoteCommand()
                {
                    DispatchNote = dispatchNote
                });

            }
            else
            {
                await Mediator.Send(new AddDispatchNoteCommand()
                {
                    DispatchNote = dispatchNote
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Sale/DispatchNoteList")]
        [HttpGet("DispatchNoteList")]
        [Roles("CanViewDispatchNote", "CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices", "CreditServiceInvoices", "CashServiceInvoices", "CanHoldServiceInvoices", "CanEditHoldServiceInvoices")]
        public async Task<IActionResult> DispatchNoteList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, Guid customerId, Guid? branchId)
        {
            var saleOrder = await Mediator.Send(new GetDispatchNoteListQuery
            {
                CustomerId = customerId,
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                BranchId = branchId,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(saleOrder);
        }

        [Route("api/Sale/DispatchNoteDetail")]
        [HttpGet("DispatchNoteDetail")]
        [Roles("CanAddDispatchNote")]
        public async Task<IActionResult> DispatchNoteDetail(Guid id)
        {
            var dispatchNotes = await Mediator.Send(new GetDispatchNoteDetailQuery()
            {
                Id = id
            });
            return Ok(dispatchNotes);
        }

        #endregion

        #region Print Option 

        [Route("api/Sale/SavePrintOption")]
        [HttpPost("SavePrintOption")]
        [Roles("CanChooseA4InvoiceType", "CanChooseThermalInvoiceType")]
        public async Task<IActionResult> SavePrintOption([FromBody] PrintOptionLookUp printOption)
        {
            {
                await Mediator.Send(new AddUpdatePrintOptionCommand
                {
                    PrintOption = printOption
                });
                return Ok(new { IsSuccess = true });
            }

        }

        #endregion
        #region PrintSetting
        [Route("api/Sale/SavePrintSetting")]
        [HttpPost("SavePrintSetting")]
        [Roles("CanChooseA4InvoiceType", "CanChooseThermalInvoiceType")]
        public async Task<IActionResult> SavePrintSetting([FromBody] PrintSettingVm printSettingVm)
        {
            if (printSettingVm.Id == Guid.Empty)
            {
                await Mediator.Send(new AddUpdatePrintSettingCommand
                {
                    Id = new Guid(),
                    WarrantyImage = printSettingVm.WarrantyImage,
                    PrintTemplate = printSettingVm.PrintTemplate,
                    TermsInAr = printSettingVm.TermsInAr,
                    ReturnDays = printSettingVm.ReturnDays,
                    TermsInEng = printSettingVm.TermsInEng,
                    PrintSize = printSettingVm.PrintSize,
                    IsActive = printSettingVm.isActive,
                    CashAccountId = printSettingVm.CashAccountId,
                    BankAccountId = printSettingVm.BankAccountId,
                    PrinterName = printSettingVm.PrinterName,
                    IsHeaderFooter = printSettingVm.IsHeaderFooter,
                    EnglishName = printSettingVm.EnglishName,
                    ArabicName = printSettingVm.ArabicName,
                    ShowBankAccount = printSettingVm.ShowBankAccount,
                    CustomerAddress = printSettingVm.CustomerAddress,
                    CustomerVat = printSettingVm.CustomerVat,
                    CustomerNumber = printSettingVm.CustomerNumber,
                    CargoName = printSettingVm.CargoName,
                    CustomerTelephone = printSettingVm.CustomerTelephone,
                    ItemPieceSize = printSettingVm.ItemPieceSize,
                    StyleNo = printSettingVm.StyleNo,
                    BlindPrint = printSettingVm.BlindPrint,
                    BankAccount1 = printSettingVm.BankAccount1,
                    BankAccount2 = printSettingVm.BankAccount2,
                    BankIcon1 = printSettingVm.BankIcon1,
                    BankIcon2 = printSettingVm.BankIcon2,
                    InvoicePrint = printSettingVm.InvoicePrint,
                    IsBlindPrint = printSettingVm.IsBlindPrint,
                    AutoPaymentVoucher = printSettingVm.AutoPaymentVoucher,
                    IsDeliveryNote = printSettingVm.IsDeliveryNote,
                    CustomerNameEn = printSettingVm.CustomerNameEn,
                    CustomerNameAr = printSettingVm.CustomerNameAr,
                    ExchangeDays = printSettingVm.ExchangeDays,
                    Bank1Id = printSettingVm.Bank1Id,
                    Bank2Id = printSettingVm.Bank2Id,
                    WelcomeLineEn = printSettingVm.WelcomeLineEn,
                    WelcomeLineAr = printSettingVm.WelcomeLineAr,
                    ClosingLineEn = printSettingVm.ClosingLineEn,
                    ClosingLineAr = printSettingVm.ClosingLineAr,
                    ContactNo = printSettingVm.ContactNo,
                    ManagementNo = printSettingVm.ManagementNo,
                    WalkInInvoiceType = printSettingVm.WalkInInvoiceType,
                    BusinessAddressEnglish = printSettingVm.BusinessAddressEnglish,
                    BusinessAddressArabic = printSettingVm.BusinessAddressArabic,
                    HeaderImage = printSettingVm.HeaderImage,
                    HeaderImage1 = printSettingVm.HeaderImage1,
                    ProposalImage = printSettingVm.ProposalImage,
                    FooterImage = printSettingVm.FooterImage,
                    TagsImages = printSettingVm.TagsImages,
                    IsQuotationPrint = printSettingVm.IsQuotationPrint,
                    TermAndConditionLength = printSettingVm.TermAndConditionLength,

                    WithSubTotal = printSettingVm.WithSubTotal,
                    ContinueWithPage = printSettingVm.ContinueWithPage,
                    SubTotalWithDashes = printSettingVm.SubTotalWithDashes,
                    BranchId = printSettingVm.BranchId,
                    DiscountTypeOption = printSettingVm.DiscountTypeOption,
                    TaxMethod = printSettingVm.TaxMethod,
                    TaxRateId = printSettingVm.TaxRateId,

                });
                return Ok(new { IsSuccess = true });
            }
            else
            {
                await Mediator.Send(new AddUpdatePrintSettingCommand
                {
                    Id = printSettingVm.Id,
                    PrintTemplate = printSettingVm.PrintTemplate,
                    TermsInAr = printSettingVm.TermsInAr,
                    ReturnDays = printSettingVm.ReturnDays,
                    TermsInEng = printSettingVm.TermsInEng,
                    PrintSize = printSettingVm.PrintSize,
                    IsActive = printSettingVm.isActive,
                    CashAccountId = printSettingVm.CashAccountId,
                    BankAccountId = printSettingVm.BankAccountId,
                    PrinterName = printSettingVm.PrinterName,
                    IsHeaderFooter = printSettingVm.IsHeaderFooter,
                    EnglishName = printSettingVm.EnglishName,
                    ArabicName = printSettingVm.ArabicName,
                    ShowBankAccount = printSettingVm.ShowBankAccount,
                    CustomerAddress = printSettingVm.CustomerAddress,
                    CustomerVat = printSettingVm.CustomerVat,
                    CustomerNumber = printSettingVm.CustomerNumber,
                    CargoName = printSettingVm.CargoName,
                    CustomerTelephone = printSettingVm.CustomerTelephone,
                    ItemPieceSize = printSettingVm.ItemPieceSize,
                    StyleNo = printSettingVm.StyleNo,
                    BlindPrint = printSettingVm.BlindPrint,
                    BankAccount1 = printSettingVm.BankAccount1,
                    BankAccount2 = printSettingVm.BankAccount2,
                    BankIcon1 = printSettingVm.BankIcon1,
                    BankIcon2 = printSettingVm.BankIcon2,
                    InvoicePrint = printSettingVm.InvoicePrint,
                    IsBlindPrint = printSettingVm.IsBlindPrint,
                    AutoPaymentVoucher = printSettingVm.AutoPaymentVoucher,
                    IsDeliveryNote = printSettingVm.IsDeliveryNote,
                    TermAndConditionLength = printSettingVm.TermAndConditionLength,
                    CustomerNameAr = printSettingVm.CustomerNameAr,
                    CustomerNameEn = printSettingVm.CustomerNameEn,
                    ExchangeDays = printSettingVm.ExchangeDays,
                    Bank1Id = printSettingVm.Bank1Id,
                    Bank2Id = printSettingVm.Bank2Id,
                    WelcomeLineEn = printSettingVm.WelcomeLineEn,
                    WelcomeLineAr = printSettingVm.WelcomeLineAr,
                    ClosingLineEn = printSettingVm.ClosingLineEn,
                    ClosingLineAr = printSettingVm.ClosingLineAr,
                    ContactNo = printSettingVm.ContactNo,
                    ManagementNo = printSettingVm.ManagementNo,
                    WalkInInvoiceType = printSettingVm.WalkInInvoiceType,
                    BusinessAddressEnglish = printSettingVm.BusinessAddressEnglish,
                    BusinessAddressArabic = printSettingVm.BusinessAddressArabic,
                    HeaderImage = printSettingVm.HeaderImage,
                    WarrantyImage = printSettingVm.WarrantyImage,
                    HeaderImage1 = printSettingVm.HeaderImage1,
                    ProposalImage = printSettingVm.ProposalImage,
                    FooterImage = printSettingVm.FooterImage,
                    TagsImages = printSettingVm.TagsImages,
                    IsQuotationPrint = printSettingVm.IsQuotationPrint,

                    WithSubTotal = printSettingVm.WithSubTotal,
                    ContinueWithPage = printSettingVm.ContinueWithPage,
                    SubTotalWithDashes = printSettingVm.SubTotalWithDashes,
                    BranchId = printSettingVm.BranchId,
                    DiscountTypeOption = printSettingVm.DiscountTypeOption,
                    TaxMethod = printSettingVm.TaxMethod,
                    TaxRateId = printSettingVm.TaxRateId,
                });
                return Ok(new { IsSuccess = true });
            }

        }
        //[Route("api/Sale/PrintSettingList")]
        //[HttpGet("PrintSettingList")]
        //public async Task<IActionResult> PrintSettingList()
        //{
        //    var printSetting = await Mediator.Send(new GetPrintSettingListQuery());
        //    return Ok(printSetting);
        //}

        [Route("api/Sale/PrintTemplateList")]
        [HttpGet("PrintTemplateList")]
        [Roles("CanChooseA4InvoiceType", "CanChooseThermalInvoiceType")]
        public IActionResult PrintTemplateList(string print)
        {
            if (print == "A4")
            {
                var printTemplateA4 = Enum.GetNames(typeof(InvoiceA4PrintTemplate)).ToList();
                return Ok(printTemplateA4);
            }

            var printTemplate = Enum.GetNames(typeof(InvoiceThermalPrintTemplate)).ToList();
            return Ok(printTemplate);
        }

        //[Route("api/Sale/PrintSettingDelete")]
        //[HttpGet("PrintSettingDelete")]
        //public async Task<IActionResult> PrintSettingDelete(Guid id)
        //{
        //    var printSettingId = await Mediator.Send(new DeletePrintSettingCommand
        //    {
        //        Id = id
        //    });
        //    return Ok(printSettingId);

        //}
        [Route("api/Sale/PrintSettingDetail")]
        [HttpGet("PrintSettingDetail")]
        //[Roles("CanPrintVatPayableReport", "CanPrintAccountLedger", "CanPrintAccountLedger", "CanPrintInvoices", "CanPrintSaleOrder", "CanPrintInvoice", "CanPrintServiceInvoices", "CanPrintSaleOrder", "CanPrintCustomerBalance", "CanPrintSupplierBalance", "CanPrintCustomerLedger", "CanPrintSupplierLedger", "CanPrintExpense", "CanPrintTouchInvoice", "CanPrintPettyCashTemplate2", "CanPrintPettyCashTemplate1", "CanPrintCPR", "CanPrintSPR", "CanChooseA4InvoiceType", "CanChooseThermalInvoiceType", "CanPrintPurchaseInvoice", "CanPrintExpenseBill", "CanViewOrderDetail", "CanViewPurchaseReturn", "CanA4Print", "CanPosPrint", "CanSendSaleEmailAsLink", "CanSendSaleEmailAsPdf", "CanPrintSaleOrder", "CanPrintInvoice", "CanA4ServicePrint", "CanPosServicePrint", "CanPrintServiceSaleOrder", "CanPrintStockIn", "CanPrintStockOut", "CanPrintAccountLedger", "User")]
        public async Task<IActionResult> PrintSettingDetail(Guid? branchId)
        {
            var printSetting = await Mediator.Send(new GetPrintSettingDetailQuery()
            {
                BranchId = branchId
            });
            return Ok(printSetting);

        }







        [Route("api/Sale/SearchCashCustomer")]
        [HttpGet("SearchCashCustomer")]
        [Roles("CashServiceInvoicesForWalkIn", "CashInvoicesForWalkIn", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3")]
        public async Task<IActionResult> SearchCashCustomer(string search)
        {
            var printSetting = await Mediator.Send(new GetCashCustomerDetailQuery()
            {
                Search = search
            });
            return Ok(printSetting);

        }
        [Route("api/Sale/VerifyCashVouch")]
        [HttpGet("VerifyCashVouch")]
        [Roles("TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3")]
        public async Task<IActionResult> VerifyCashVouch(string voucherNo)
        {
            var result = await Mediator.Send(new GetCashVoucherDetailQuery()
            {
                VoucherNo = voucherNo
            });
            return Ok(result);

        }


        [Route("api/Sale/CheckSerialExist")]
        [HttpGet("CheckSerialExist")]
        public async Task<IActionResult> CheckSerialExist(string serial, Guid productId)
        {
            var result = await Mediator.Send(new CheckSerialExistQuery()
            {
                Serial = serial,
                ProductId = productId,
            });
            return Ok(result);
        }
        #endregion


        #region ForSaleEmail
        public class EmailData
        {
            public string EmailPath { get; set; }
            public string Email { get; set; }
            public string Subject { get; set; }
            public string CompanyName { get; set; }
            public string ButtonName { get; set; }
        }
        [Route("api/Sale/SendEmail")]
        [HttpPost("SendEmail")]
        [Roles("CanSendSaleEmailAsLink")]
        public async Task<IActionResult> SendEmail([FromBody] EmailCompose emailCompose)
        {
            var sale = _SendEmail.SendInvoiceEmailLinkAsync(emailCompose);
            return Ok(sale);
        }


        [Route("api/Sale/SaleDetailForEmail")]
        [HttpGet("SaleDetailForEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> SaleDetailForEmail(Guid id, bool isReturn, string invoiceNo, bool isFifo, int openBatch, Guid companyId)
        {
            var sale = await Mediator.Send(new GetSaleDetailEmailQuery()
            {
                Id = id,
                IsReturn = isReturn,
                InvoiceNo = invoiceNo,
                IsFifo = isFifo,
                OpenBatch = openBatch,
                CompanyId = companyId
            });
            return Ok(sale);
        }
        [Route("api/Sale/TaxRateForEmail")]
        [HttpGet("TaxRateForEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> TaxRateForEmail(Guid companyId)
        {
            var sale = await Mediator.Send(new GettaxRateListForEmail()
            {
                CompanyId = companyId
            });
            return Ok(sale);
        }

        [Route("api/Sale/CompanyDetailForEmail")]
        [HttpGet("CompanyDetailForEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> CompanyDetailForEmail(Guid id)
        {

            var sale = await Mediator.Send(new CompanyDetailForEmail()
            {
                Id = id
            });
            return Ok(sale);
        }

        #endregion


        #region DiscountSetup
        [Route("api/Sale/SaveDiscountSetup")]
        [HttpPost("SaveDiscountSetup")]
        public async Task<IActionResult> SaveDiscountSetup([FromBody] DiscountSetupVm discountSetup)
        {
            var discount = await Context.DiscountSetups.FirstOrDefaultAsync();
            if (discount == null)
            {

                var data = new Focus.Domain.Entities.DiscountSetup()
                {
                    DiscountOver1Qty = discountSetup.DiscountOver1Qty,
                    DiscountOverQty = discountSetup.DiscountOverQty,
                    LineItemBeforeVat = discountSetup.LineItemBeforeVat,
                    LineItemAfterVat = discountSetup.LineItemAfterVat,
                    OverAllAfterVat = discountSetup.OverAllAfterVat,
                    OverAllBeforeVat = discountSetup.OverAllBeforeVat
                };
                await Context.DiscountSetups.AddAsync(data);
            }
            else
            {
                discount.DiscountOver1Qty = discountSetup.DiscountOver1Qty;
                discount.DiscountOverQty = discountSetup.DiscountOverQty;
                discount.LineItemBeforeVat = discountSetup.LineItemBeforeVat;
                discount.LineItemAfterVat = discountSetup.LineItemAfterVat;
                discount.OverAllAfterVat = discountSetup.OverAllAfterVat;
                discount.OverAllBeforeVat = discountSetup.OverAllBeforeVat;
            }
            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            await Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = Context.SyncLog(),
                Code = _code,
            });
            await Context.SaveChangesAsync();
            return Ok(new { IsSuccess = true });
        }
        [Route("api/Sale/GetDiscountSetup")]
        [HttpGet("GetDiscountSetup")]
        public async Task<IActionResult> GetDiscountSetup()
        {
            var discountSetup = await Context.DiscountSetups.FirstOrDefaultAsync();
            if (discountSetup == null)
            {

                return Ok(new DiscountSetupVm()
                {
                    DiscountOver1Qty = false,
                    DiscountOverQty = true,
                    LineItemBeforeVat = true,
                    LineItemAfterVat = false,
                    OverAllAfterVat = false,
                    OverAllBeforeVat = false
                });
            }
            else
            {
                var discount = new DiscountSetupVm();
                discount.DiscountOver1Qty = discountSetup.DiscountOver1Qty;
                discount.DiscountOverQty = discountSetup.DiscountOverQty;
                discount.LineItemBeforeVat = discountSetup.LineItemBeforeVat;
                discount.LineItemAfterVat = discountSetup.LineItemAfterVat;
                discount.OverAllAfterVat = discountSetup.OverAllAfterVat;
                discount.OverAllBeforeVat = discountSetup.OverAllBeforeVat;
                return Ok(discount);
            }

        }
        #endregion

        #region DefaultSetting
        [Route("api/Sale/GetDefaultSetting")]
        [HttpGet("GetDefaultSetting")]
        public async Task<IActionResult> GetDefaultSetting()
        {
            try
            {

                var sale = await Mediator.Send(new GetDefaultSettingQuery());
                return Ok(sale);


            }
            catch (Exception ex)
            {


                return Ok(ex.Message + " \n After Message \n " + ex.StackTrace);
            }
        }
        [Route("api/Sale/AddUpdateDefaultSetting")]
        [HttpPost("AddUpdateDefaultSetting")]
        public IActionResult AddUpdateDefaultSetting([FromBody] DefaultSettingModel model)
        {
            try
            {

                var sale = Mediator.Send(new AddUpdateDefaultSettingCommand()
                {
                    DefaultSettingModel = model
                });
                return Ok(sale);


            }
            catch (Exception ex)
            {


                return Ok(ex.Message + " \n After Message \n " + ex.StackTrace);
            }
        }

        #endregion



        #region GSM SMS Setup
        [Route("api/Sale/GetGsmSetting")]
        [HttpGet("GetGsmSetting")]
        public IActionResult GetGsmSetting(Guid? branchId)
        {
            try
            {
                var sale = Mediator.Send(new GetGsmSmsSetupQuery()
                {
                    BranchId = branchId
                });
                return Ok(sale);


            }
            catch (Exception ex)
            {


                return Ok(ex.Message + " \n After Message \n " + ex.StackTrace);
            }
        }
        [Route("api/Sale/AddUpdateGsmSetup")]
        [HttpPost("AddUpdateGsmSetup")]
        public IActionResult AddUpdateGsmSetup([FromBody] GsmSmsSetupModel model)
        {
            try
            {
                var sale = Mediator.Send(new AddUpdateGsmSmsSetupCommand()
                {
                    GsmSmsSetupModel = model
                });
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message + " \n After Message \n " + ex.StackTrace);
            }
        }

        #endregion

    }
}
