

using Focus.Business.Interface;
using Focus.Business.Reports.SaleInvoiceReport;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Reports.SaleInvoiceMonthWise
{
    public class SaleInvoiceReportMonthWise : IRequest<List<MonthWiseSaleLookUpModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string PaymentType { get; set; }
        public string DocumentName { get; set; }
        public string BranchId { get; set; }
        public class Handler : IRequestHandler<SaleInvoiceReportMonthWise, List<MonthWiseSaleLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<SaleInvoiceReportMonthWise> logger)
            {
                _context = context;
                _logger = logger;

            }
            public async Task<List<MonthWiseSaleLookUpModel>> Handle(SaleInvoiceReportMonthWise request, CancellationToken cancellationToken)
            {
                try
                {

                    var list=new List<MonthWiseSaleLookUpModel>();
                    if (request.DocumentName == "PurchaseMonth")
                    {
                        if (request.PaymentType == "Month")
                        {
                            

                            var salesList = _context.PurchasePosts.Include(x => x.Supplier).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.IsPurchasePost)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                    Month = x.Key.ToString(),
                                    Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                    Sales = x.Select(y => new SaleInvoiceModel()
                                    {
                                        InvoiceNo = y.RegistrationNo,
                                        Date = y.Date,
                                        CustomerName = y.Supplier.CustomerDisplayName,
                                        CustomerNameArabic = y.Supplier.ArabicName,
                                        IsSaleReturnPost = y.IsPurchaseReturn,
                                        CustomerVat=y.Supplier.VatNo,
                                        Amount = y.TotalWithOutAmount,
                                        TotalAmount = y.TotalAmount,
                                        Discount = y.DiscountAmount,
                                        VATamount = y.VatAmount
                                    }).ToList()

                            }).ToList();

                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }
                        }
                        else if (request.PaymentType == "3 Month")
                        {
                            var salesList = _context.PurchasePosts.Include(x => x.Supplier).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.IsPurchasePost)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Supplier.CustomerDisplayName,
                                    CustomerNameArabic = y.Supplier.ArabicName,
                                    IsSaleReturnPost = y.IsPurchaseReturn,
                                    Amount = y.TotalWithOutAmount,
                                    CustomerVat = y.Supplier.VatNo,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }


                        }
                        else if (request.PaymentType == "6 Month")
                        {
                            var salesList = _context.PurchasePosts.Include(x => x.Supplier).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.IsPurchasePost)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Supplier.CustomerDisplayName,
                                    CustomerNameArabic = y.Supplier.ArabicName,
                                    IsSaleReturnPost = y.IsPurchaseReturn,
                                    Amount = y.TotalWithOutAmount,
                                    CustomerVat = y.Supplier.VatNo,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }


                        }
                        else if (request.PaymentType == "Year")
                        {
                            var salesList = _context.PurchasePosts.Include(x => x.Supplier).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.IsPurchasePost)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Supplier.CustomerDisplayName,
                                    CustomerNameArabic = y.Supplier.ArabicName,
                                    IsSaleReturnPost = y.IsPurchaseReturn,
                                    Amount = y.TotalWithOutAmount,
                                    TotalAmount = y.TotalAmount,
                                    CustomerVat = y.Supplier.VatNo,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }
                        else
                        {
                            var salesList = _context.PurchasePosts.Include(x => x.Supplier).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.IsPurchasePost)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Supplier.CustomerDisplayName,
                                    CustomerNameArabic = y.Supplier.ArabicName,
                                    IsSaleReturnPost = y.IsPurchaseReturn,
                                    CustomerVat = y.Supplier.VatNo,
                                    Amount = y.TotalWithOutAmount,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }

                    }
                    else if (request.DocumentName == "DailyExpense")
                    {
                        if (request.PaymentType == "Month")
                        {


                            var salesList = _context.DailyExpenses.AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.VoucherNo,
                                    Date = y.Date,
                                    Amount = y.GrossAmount,
                                    TotalAmount = y.TotalAmount,
                                    VATamount = y.TotalVat


                                }).ToList()

                            }).ToList();

                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }


                        }
                        else if (request.PaymentType == "3 Month")
                        {
                            var salesList = _context.DailyExpenses.AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.VoucherNo,
                                    Date = y.Date,
                                    Amount = y.GrossAmount,
                                    TotalAmount = y.TotalAmount,
                                    VATamount = y.TotalVat


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }
                        else if (request.PaymentType == "6 Month")
                        {
                            var salesList = _context.DailyExpenses.AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.VoucherNo,
                                    Date = y.Date,
                                    Amount = y.GrossAmount,
                                    TotalAmount = y.TotalAmount,
                                    VATamount = y.TotalVat


                                }).ToList()

                            }).ToList();

                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }
                        else if (request.PaymentType == "Year")
                        {
                            var salesList = _context.DailyExpenses.AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved)
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.VoucherNo,
                                    Date = y.Date,
                                    Amount = y.GrossAmount,
                                    TotalAmount = y.TotalAmount,
                                    VATamount = y.TotalVat


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }
                        else
                        {
                            var salesList = _context.DailyExpenses
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved)
                                .GroupBy(x => x.Date.Month);

                            list = salesList.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.VoucherNo,
                                    Date = y.Date,
                                    Amount = y.GrossAmount,
                                    TotalAmount = y.TotalAmount,
                                    VATamount = y.TotalVat


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }

                    }

                    else
                    {
                        if (request.PaymentType == "Month")
                        {
                            var salesList = _context.Sales.Include(x=>x.Customer).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit))
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);



                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Customer.CustomerDisplayName,
                                    CustomerVat = y.Customer.VatNo,
                                    IsSaleReturnPost = y.IsSaleReturnPost,
                                    Amount = y.TotalWithOutAmount,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();

                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }
                        else if (request.PaymentType == "3 Month")
                        {
                            var salesList = _context.Sales.Include(x => x.Customer).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit))
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Customer.CustomerDisplayName,
                                    CustomerVat = y.Customer.VatNo,
                                    IsSaleReturnPost = y.IsSaleReturnPost,
                                    Amount = y.TotalWithOutAmount,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }
                        }
                        else if (request.PaymentType == "6 Month")
                        {
                            var salesList = _context.Sales.Include(x => x.Customer).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit))
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Customer.CustomerDisplayName,
                                    CustomerVat = y.Customer.VatNo,
                                    IsSaleReturnPost = y.IsSaleReturnPost,
                                    Amount = y.TotalWithOutAmount,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }
                        }
                        else if (request.PaymentType == "Year")
                        {
                            var salesList = _context.Sales.Include(x => x.Customer).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit))
                                .ToList();
                            var salesByDate = salesList.GroupBy(x => x.Date.Month);

                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Customer.CustomerDisplayName,
                                    CustomerVat = y.Customer.VatNo,
                                    IsSaleReturnPost = y.IsSaleReturnPost,
                                    Amount = y.TotalWithOutAmount,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount


                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }
                        }
                        else
                        {
                            var salesList = _context.Sales.Include(x => x.Customer).AsNoTracking()
                                .Where(x =>
                                    x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit))
                                .ToList();

                            var salesByDate = salesList.GroupBy(x => x.Date.Month);
                            list = salesByDate.Select(x => new MonthWiseSaleLookUpModel()
                            {
                                Month = x.Key.ToString(),
                                Date = Convert.ToDateTime(x.FirstOrDefault().Date),
                                Sales = x.Select(y => new SaleInvoiceModel()
                                {
                                    InvoiceNo = y.RegistrationNo,
                                    Date = y.Date,
                                    CustomerName = y.Customer.CustomerDisplayName,
                                    CustomerVat = y.Customer.VatNo,
                                    IsSaleReturnPost = y.IsSaleReturnPost,
                                    Amount = y.TotalWithOutAmount,
                                    TotalAmount = y.TotalAmount,
                                    Discount = y.DiscountAmount,
                                    VATamount = y.VatAmount

                                }).ToList()

                            }).ToList();
                            if (!string.IsNullOrEmpty(request.BranchId))
                            {
                                var branchIdList = new List<string>(request.BranchId.Split(','));
                                list = list.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                            }

                        }
                    }

                    return list.OrderBy(x=>x.Month).ToList();
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
