using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchasePosts.Commands.PurchasePostCosting
{
    public class AddPurchasePostCosting : IRequest<Message>
    {
        public PurchasePostLookupModel PurchasePost { get; set; }
        public decimal Payments { get; set; }

        public class Handler : IRequestHandler<AddPurchasePostCosting, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddPurchasePostCosting> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddPurchasePostCosting request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var savePurchasePost = await _context.PurchasePosts.FindAsync(request.PurchasePost.Id);

                    if (savePurchasePost == null)
                        throw new ApplicationException("Purchase invoice not found.");

                    var totalExpense = savePurchasePost.PurchasePostExpenses.Sum(x => x.Amount);

                    var productId = request.PurchasePost.PurchasePostItems.Select(x => x.ProductId);

                    //var products = _context.Products
                    //    .AsNoTracking()
                    //    .Include(x => x.Category)
                    //    .Where(x=> productId.Contains(x.Id))
                    //    .AsQueryable();



                    //var stocks = _context.Stocks.AsNoTracking().AsQueryable();
                    var transactions = new Collection<Domain.Entities.Transaction>();
                    var ledgerTransactions = new Collection<LedgerTransaction>();
                    decimal cogsAccount = 0;
                    var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");

                    using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                    var autoNumber = _context.Inventories.OrderByDescending(x => x.AutoNumbering).FirstOrDefault(x => x.AutoNumbering != 0)?.AutoNumbering;
                    var stockList = await _context.Stocks.Where(x => x.WareHouseId == request.PurchasePost.WareHouseId).ToListAsync(cancellationToken: cancellationToken);
                    var productList = await _context.Products.ToListAsync(cancellationToken: cancellationToken);
                    var taxRates = await _context.TaxRates.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    foreach (var item in request.PurchasePost.PurchasePostItems)
                    {
                        
                        var tax = taxRates.FirstOrDefault(x => x.Id == item.TaxRateId);
                        var product = productList.FirstOrDefault(x => x.Id == item.ProductId);
                        if (product == null)
                            throw new NotFoundException("Product Not Found", "");

                        product.SalePrice = item.SalePrice;

                        var stock = stockList.FirstOrDefault(x => x.ProductId == item.ProductId);
                        if (stock == null)
                        {
                            var newStock = new Stock()
                            {
                                ProductId = item.ProductId.Value,
                                WareHouseId = request.PurchasePost.WareHouseId,
                                BranchId= request.PurchasePost.BranchId,
                            };
                            newStock.CurrentQuantity += Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity);
                            newStock.AveragePrice = Math.Round(item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل" ? (item.UnitPrice * 100) / (100 + tax.Rate) : item.UnitPrice, 2);
                            newStock.CurrentStockValue = Math.Round(newStock.CurrentQuantity * newStock.AveragePrice, 2);
                            

                            await _context.Stocks.AddAsync(newStock, cancellationToken);

                            stock = newStock;
                        }
                        else
                        {
                            var qty = stock.CurrentQuantity;
                            stock.CurrentQuantity += Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity);

                            stock.AveragePrice = Math.Round(((stock.AveragePrice * qty) + ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل" ? item.UnitPrice * 100 / (100 + tax.Rate) : item.UnitPrice) * item.Quantity)) / stock.CurrentQuantity, 2);
                            stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                            stock.BranchId = request.PurchasePost.BranchId;
                        }

                        var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                        {
                            ProductId = item.ProductId.Value,
                            StockId = stock.Id,
                            WareHouseId = request.PurchasePost.WareHouseId
                        }, cancellationToken);

                        var inv = new Inventory()
                        {
                            Date = DateTime.UtcNow,
                            DocumentNumber = savePurchasePost.RegistrationNo,
                            DocumentId = savePurchasePost.Id,
                            Quantity = Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity),
                            SalePrice = item.Product.SalePrice,
                            Price = Math.Round(item.UnitExpense, 2),
                            ProductId = item.ProductId.Value,
                            StockId = stock.Id,
                            WareHouseId = stock.WareHouseId,
                            WarrantyDate = item.GuaranteeDate,
                            Serial = item.SerialNo,
                            TransactionType = TransactionType.PurchasePost,
                            AveragePrice = currentInventory.CurrentQuantity == 0 ? Math.Round(item.UnitExpense, 2) : Math.Round((currentInventory.AveragePrice + item.UnitExpense) / 2, 2),
                            ExpiryDate = item.ExpiryDate ?? currentInventory.ExpiryDate,
                            CurrentQuantity = currentInventory.CurrentQuantity + Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity),
                            CurrentStockValue = (currentInventory.CurrentQuantity + Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity)) * (currentInventory.CurrentQuantity == 0 ? Math.Round(item.UnitExpense, 2) : Math.Round((currentInventory.AveragePrice + item.UnitExpense) / 2, 2)),
                            BatchNumber = item.BatchNo,
                            IsActive = true,
                            IsOpen = true,
                            AutoNumbering = (int)(autoNumber == null ? 1 : ++autoNumber),
                            RemainingQuantity = Convert.ToDecimal(item.Quantity),
                            BranchId = request.PurchasePost.BranchId,
                        };
                        await _context.Inventories.AddAsync(inv, cancellationToken);

                        //account COGS
                        cogsAccount += (item.UnitExpense - item.UnitPrice) * item.Quantity;
                        if (isPos)
                        {
                            ledgerTransactions.Add(new LedgerTransaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.PurchasePost.Date,
                                ApprovalDate = DateTime.UtcNow,
                                ContactId = savePurchasePost.SupplierId,
                                AccountId = product.CogsAccountId,
                                Credit = 0,
                                Debit = (item.UnitExpense - item.UnitPrice) * item.Quantity,
                                Description = TransactionType.PurchasePost.ToString(),
                                DocumentId = savePurchasePost.Id,
                                TransactionType = TransactionType.PurchasePost,
                                DocumentNumber = savePurchasePost.RegistrationNo,
                                Year = savePurchasePost.Date.Year.ToString(),
                                BranchId = request.PurchasePost.BranchId,
                            });
                        }


                        //Save changes to database
                        //await _context.SaveChangesAsync(cancellationToken);
                    }



                    var accounts = await _context.Accounts.Where(x => x.Code == "1300001" || x.Code == "60500101" || x.Code == "60000101").ToListAsync(cancellationToken);



                    //transactions.Add(new Domain.Entities.Transaction
                    //{
                    //    Date = DateTime.UtcNow,
                    //    DocumentDate = request.PurchasePost.Date,
                    //    ApprovalDate = DateTime.UtcNow,
                    //    ContactId = savePurchasePost.SupplierId,
                    //    AccountId = accounts.FirstOrDefault(x => x.Code == "60000101").Id,
                    //    Credit = 0,
                    //    Debit = cogsAccount,
                    //    Description = TransactionType.PurchasePost.ToString(),
                    //    DocumentId = savePurchasePost.Id,
                    //    TransactionType = TransactionType.PurchasePost,
                    //    DocumentNumber = savePurchasePost.RegistrationNo,
                    //    BranchId = request.PurchasePost.BranchId,
                    //    Year = savePurchasePost.Date.Year.ToString(),
                    //});

                    //account Expense
                    //transactions.Add(new Domain.Entities.Transaction
                    //{
                    //    Date = DateTime.UtcNow,
                    //    DocumentDate = request.PurchasePost.Date,
                    //    ApprovalDate = DateTime.UtcNow,
                    //    ContactId = savePurchasePost.SupplierId,
                    //    AccountId = accounts.FirstOrDefault(x => x.Code == "60500101").Id,
                    //    Credit = totalExpense,
                    //    Debit = 0,
                    //    Description = TransactionType.PurchasePost.ToString(),
                    //    DocumentId = savePurchasePost.Id,
                    //    TransactionType = TransactionType.PurchasePost,
                    //    DocumentNumber = savePurchasePost.RegistrationNo,
                    //    Year = savePurchasePost.Date.Year.ToString(),
                    //    BranchId = request.PurchasePost.BranchId,
                    //});



                    savePurchasePost.IsCost = true;

                    await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                    await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                        BranchId = request.PurchasePost.BranchId,
                    }, cancellationToken);

                    //Save changes to database
                    await _context.SaveChangesAsync(cancellationToken);

                    scope.Complete();


                    var message = new Message
                    {
                        Id = savePurchasePost.Id,
                        IsAddUpdate = "Data has been added successfully"
                    };
                    return message;

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
