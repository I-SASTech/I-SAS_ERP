using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Focus.Business.Accounting;
using Focus.Business.Interface;
using Focus.Business.Reports.BalanceSheetReport;
using Focus.Business.Reports.IncomeStatementReport;
using Focus.Business.Reports.TrialBalanceReport;
using Focus.Business.Reports.CustomerLedgerReport;
using Focus.Business.Reports.ProductInventoryReport;
using Focus.Business.Reports.AdvanceBalanceSheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Focus.Business.Reports.PurchaseInvoiceReport;
using Focus.Business.Reports.VATPayableReport;
using Focus.Business.Reports.SaleInvoiceReport;
using Focus.Business.Sales.Queries;
using Focus.Business.Transactions.JVTransaction;
using Focus.Business.Dashboard.Queries.GetInventoryList;
using Focus.Business.Reports.MonthlySalesReport;
using Noble.Api.Models;
using Focus.Business.Reports.ComparisionReport;
using Focus.Business.Reports.TemporaryCashAllocation;
using Focus.Business.Reports.TemporaryCashIssueReport;
using Focus.Business.Reports.TrialBalanceAccountReport;
using Microsoft.AspNetCore.Hosting;
using Rotativa.AspNetCore;
using Focus.Business.Reports.TrialBalanceTreeReport;
using Focus.Business.Reports.CostCenterWiseAccountReport;
using Focus.Business.Reports.VatExpenseReport;
using Focus.Business.DailyExpenses.Queries.GetDailyExpenseList;
using Focus.Business.ManualAttendance;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail;
using Focus.Business.Reports.GetInvoiceSerialReport;
using Focus.Business.Reports.ProductLedgerReport;
using Focus.Business.Reports.ProductComparisionReport;
using Focus.Business.Reports.ProductWiseAccountReports;
using Focus.Business.SaleOrders.Queries.GetSaleOrderList;
using Focus.Business;
using Focus.Business.Reports.SaleInvoiceMonthWise;
using Focus.Business.Reports.VatSalePurchaseComparisonReport;

using Focus.Business.CostCenters.Queries;
using Focus.Business.YearlyPeriods.Queries;
using Focus.Business.Reports.AdvanceTrialBalanceReport;
using Focus.Business.Reports.AdvanceTrialBalanceAccountReport.Queries;
using Focus.Business.Reports.AdvanceCustomerLedgerReport.Queries;
using Focus.Business.Reports.AdvanceInventoryItemReport.Queries;
using Focus.Business.Reports.AdvanceQuantityWiseInventoryItemReport.Queries;
using Focus.Business.Reports.AdvanceStockReport.Queries;
using Focus.Business.Reports.GetAdvanceSalesInvoice.Queries;
using Focus.Business.Reports.SaleInvoiceSummaryReport.Queries;
using Focus.Business.Reports.PurchaseInvoiceSummaryReport.Queries;
using Focus.Business.Reports.ProductSummaryReport.Queries;
using Focus.Business.Reports.CustomerAdvancesReport;
using Focus.Business.Reports.AdvanceContactLedger;
using Focus.Business.Reports.VatReturnReport;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        private readonly ICompanyComponent _companyComponent;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISendEmail _SendEmail;
        public ReportController(ICompanyComponent companyComponent, IHostingEnvironment hostingEnvironment, ISendEmail sendEmail)
        {
            _companyComponent = companyComponent;
            _hostingEnvironment = hostingEnvironment;
            _SendEmail = sendEmail;
        }
        [Route("api/Report/GetAdvanceStockReport")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetAdvanceStockReport")]
        public async Task<IActionResult> GetAdvanceStockReport(DateTime FromDate, DateTime ToDate, string compareWith, string numberOfPeriods, Guid productId, string branchId)
        {
            var list = await Mediator.Send(new GetAdvanceStockReportQuery()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                ProductId = productId,
                BranchId = branchId,
            });
            return Ok(list);
        }
        [Route("api/Report/TrialBalanceReportAsync")]
        [Microsoft.AspNetCore.Mvc.HttpGet("TrialBalanceReportAsync")]
        [Roles("CanViewTrialBalance")]
        public async Task<IActionResult> TrialBalanceReportAsync(DateTime FromDate, DateTime ToDate,string branchId)
        {
            var list = await Mediator.Send(new GetTrialBalanceQuery()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                BranchId = branchId,
            });
            return Ok(list);
        }

        [Route("api/Report/GetSalesInvoice")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetSalesInvoice")]
        public async Task<IActionResult> GetSalesInvoice(DateTime FromDate, DateTime ToDate, string compareWith, string numberOfPeriods, string invoiceType, string customerId, string branchId)
        {
            var list = await Mediator.Send(new GetSalesInvoiceQuery()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                InvoiceType = invoiceType,
                CustomerId = customerId,
                BranchId = branchId,
            });
            return Ok(list);
        } 
        [Route("api/Report/GetSalesInvoiceSummary")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetSalesInvoiceSummary")]
        public async Task<IActionResult> GetSalesInvoiceSummary(string compareWith, string numberOfPeriods, string invoiceType, string branchId)
        {
            var list = await Mediator.Send(new GetSaleInvoiceSummaryQuery()
            {
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                InvoiceType = invoiceType,
                BranchId = branchId
            });
            return Ok(list);
        } 
        [Route("api/Report/GetPurchaseInvocieSummary")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetPurchaseInvocieSummary")]
        public async Task<IActionResult> GetPurchaseInvocieSummary(string compareWith, string numberOfPeriods, string invoiceType, string branchId)
        {
            var list = await Mediator.Send(new GetPurchaseInvocieSummaryQuery()
            {
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                InvoiceType = invoiceType,
                BranchId = branchId
            });
            return Ok(list);
        } 
        [Route("api/Report/GetProductSummary")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetProductSummary")]
        public async Task<IActionResult> GetProductSummary(string compareWith, string numberOfPeriods, string productId, string branchId)
        {
            var list = await Mediator.Send(new GetProductSummaryQuery()
            {
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                ProductId = productId,
                BranchId = branchId
            });
            return Ok(list);
        } 
        [Route("api/Report/GetAdvanceTrialBalanceReport")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetAdvanceTrialBalanceReport")]
        [Roles("CanViewTrialBalance")]
        public async Task<IActionResult> GetAdvanceTrialBalanceReport(DateTime FromDate, DateTime ToDate, string compareWith, string numberOfPeriods, string branchId)
        {
            var list = await Mediator.Send(new GetAdvanceTrialBalanceReportQuery()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                BranchId = branchId
            });
            return Ok(list);
        }
        [Route("api/Report/GetAdvanceInventoryItem")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetAdvanceInventoryItem")]
        public async Task<IActionResult> GetAdvanceInventoryItem(DateTime FromDate, DateTime ToDate, string compareWith, string numberOfPeriods, Guid productId, string branchId)
        {
            var list = await Mediator.Send(new GetAdvanceInventoryItemReport()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                ProductId = productId,
                BranchId = branchId
            });
            return Ok(list);
        }
        [Route("api/Report/GetAdvanceQuantityWiseInventoryItem")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetAdvanceQuantityWiseInventoryItem")]
        public async Task<IActionResult> GetAdvanceQuantityWiseInventoryItem(DateTime FromDate, DateTime ToDate, string compareWith, string numberOfPeriods, Guid productId, string branchId)
        {
            var list = await Mediator.Send(new GetAdvanceQuantityWiseInventoryItem()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                ProductId = productId,
                BranchId = branchId
            });
            return Ok(list);
        }

        [Route("api/Report/VatSalePurchaseComparisionReport")]
        [HttpGet("VatSalePurchaseComparisionReport")]
        public async Task<IActionResult> VatSalePurchaseComparisionReport(DateTime fromDate, DateTime toDate, string paymentType, string documentName, string branchId)
        {
            var list = await Mediator.Send(new VatSalePurchaseComparisionReport()
            {
                FromDate = fromDate,
                ToDate = toDate,
                PaymentType = paymentType,
                DocumentName = documentName,
                BranchId = branchId,
            });
            return Ok(list);
        }

        [Route("api/Report/VatReturnReport")]
        [HttpGet("VatReturnReport")]
        public async Task<IActionResult> VatReturnReport(DateTime fromDate, DateTime toDate, string paymentType, string documentName, string branchId)
        {
            var list = await Mediator.Send(new VatReturnReportQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
                DocumentName = documentName,
                BranchId = branchId,
            });
            return Ok(list);
        }




        [Route("api/Report/SaleInvoiceReportMonthWise")]
        [HttpGet("SaleInvoiceReportMonthWise")]
        public async Task<IActionResult> SaleInvoiceReportMonthWise(DateTime fromDate, DateTime toDate, string paymentType, string documentName, string branchId)
        {
            var list = await Mediator.Send(new SaleInvoiceReportMonthWise()
            {
                FromDate = fromDate,
                ToDate = toDate,
                PaymentType = paymentType,
                DocumentName = documentName,
                BranchId = branchId,
            });
            return Ok(list);
        }
        [Route("api/Report/CustomerAdvancesReport")]
        [HttpGet("CustomerAdvancesReport")]
        public async Task<IActionResult> CustomerAdvancesReport(DateTime fromDate, DateTime toDate, Guid? contactId, string documentName, Guid? accountId)
        {
            var list = await Mediator.Send(new GetCustomerAdvancesQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
                ContactId = contactId,
                DocumentName = documentName,
                AccountId = accountId,
            });
            return Ok(list);
        }
        [Route("api/Report/CustomerAdvanceSummaryReport")]
        [HttpGet("CustomerAdvanceSummaryReport")]
        public async Task<IActionResult> CustomerAdvanceSummaryReport(DateTime fromDate, DateTime toDate, string documentName, bool IsCustomer)
        {
            var list = await Mediator.Send(new AdvanceContactLedger()
            {
                IsCustomer = IsCustomer,
                FromDate = fromDate,
                ToDate = toDate,
                DocumentName = documentName,
            });
            return Ok(list);
        }
        [Route("api/Report/TrialBalanceAccountReportAsync")]
        [HttpGet("TrialBalanceAccountReportAsync")]
        [Roles("CanViewAccountLedger")]
        public async Task<IActionResult> TrialBalanceAccountReportAsync(DateTime fromDate, DateTime toDate,string branchId)
        {
            var list = await Mediator.Send(new GetTrialBalanceAccountReport()
            {
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId
            });
            return Ok(list);
        }
        [Route("api/Report/GetAdvanceTrialBalanceAccount")]
        [HttpGet("GetAdvanceTrialBalanceAccount")]
        [Roles("CanViewAccountLedger")]
        public async Task<IActionResult> GetAdvanceTrialBalanceAccount(DateTime fromDate, DateTime toDate, string compareWith, string numberOfPeriods,string branchId)
        {
            var list = await Mediator.Send(new GetAdvanceTrialBalanceAccountReport()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                BranchId = branchId
            });
            return Ok(list);
        }
        [Route("api/Report/GetAllAccounts")]
        [HttpGet("GetAllAccounts")]
        [Roles("CanViewAccountLedger")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var list = await Mediator.Send(new GetAllAccountsList()
            {
            });
            return Ok(list);
        }
        [Route("api/Report/TrialBalanceTreeReport")]
        [HttpGet("TrialBalanceTreeReport")]
        [Roles("CanViewAccountLedger")]
        public async Task<IActionResult> TrialBalanceTreeReport(DateTime fromDate, DateTime toDate,string branchId)
        {
            var list = await Mediator.Send(new TrialBalanceTreeReport()
            {
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId
            });
            return Ok(list);
        }

        [Route("api/Report/PrintPdf")]
        [HttpPost("PrintPdf")]
        public async Task<IActionResult> PrintPdf([FromForm] HtmlLookUp htmlData)
        {
            var viewPdf = new ViewAsPdf(htmlData)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 2, Bottom = 3, Right = 2, Top = 3 }
            };
            var pdfBytes = await viewPdf.BuildFile(ControllerContext);
            return File(pdfBytes, "application/pdf");
        }

        [Route("api/Report/VatExpenseReport")]
        [HttpGet("VatExpenseReport")]
        public async Task<IActionResult> VatExpenseReport(DateTime fromDate, DateTime toDate, string branchId)
        {
            var list = await Mediator.Send(new VatExpenseReport()
            {
                BranchId = branchId,
                FromDate = fromDate,
                ToDate = toDate
            });
            return Ok(list);
        }

        [Route("api/Report/SupplierPurchaseReport")]
        [HttpGet("SupplierPurchaseReport")]
        public async Task<IActionResult> SupplierPurchaseReport(Guid supplierId, bool isMultiUnit, DateTime fromDate, DateTime toDate, string branchId)
        {
            var list = await Mediator.Send(new SupplierPurchaseReportQuery()
            {
                BranchId = branchId,
                SupplierId = supplierId,
                IsMultiUnit = isMultiUnit,
                FromDate = fromDate,
                ToDate = toDate
            });
            return Ok(list);
        }

        [Route("api/Report/ProductComparisonReports")]
        [HttpGet("ProductComparisonReports")]
        public async Task<IActionResult> ProductComparisonReports(string searchTerm, DateTime fromDate, DateTime toDate,string branchId)
        {
            var list = await Mediator.Send(new ProductComparisonReport()
            {
                SearchTerm = searchTerm,
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId
            });
            return Ok(list);
        }
        [HttpGet("ProductWiseAccountReportCsv")]
        [Route("api/Report/ProductWiseAccountReportCsv")]
        public async Task<IActionResult> ProductWiseAccountReportCsv(string language, DateTime fromDate, DateTime toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var resultQuery = await Mediator.Send(new ProductComparisonReport
            {
                FromDate = fromDate,
                ToDate = toDate
            });




            {
                var reportName = language == "en" ? "Product Wise Account Reports" : "تقارير حساب المنتج الحكيم";


                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate.ToString("d");
                    to = "To Date : " + toDate.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate.ToString("d") + " حتى تاريخه";
                }




                var i = 1;
                var j = 1;
                var sb = new StringBuilder();





                foreach (var stock in resultQuery)
                {
                    List<string> columnHeaders = new List<string>();

                    string value = "";
                    if (language == "en")
                    {
                        value = "\n" + "Product Name:" + "," + stock.ProductName;

                    }
                    else
                    {
                        value = "\n" + "Product Name:" + "," + stock.ProductName;

                    }

                    if (language == "en")
                    {
                        columnHeaders.Add("Inventory");
                        columnHeaders.Add(" ");
                        columnHeaders.Add("Cost of Goods Sale");
                        columnHeaders.Add(" ");
                        columnHeaders.Add("Sale");
                        columnHeaders.Add(" ");
                    }
                    else
                    {
                        columnHeaders.Add("جرد");
                        columnHeaders.Add(" ");
                        columnHeaders.Add("عميل بيع البضائع");
                        columnHeaders.Add(" ");
                        columnHeaders.Add("أُوكَازيُون");
                        columnHeaders.Add(" ");

                    }

                    if (language == "en")
                    {
                        columnHeaders.Add("Opening Balance");
                        columnHeaders.Add("Closing  Balance");
                        columnHeaders.Add("Opening Balance");
                        columnHeaders.Add("Closing Balance");
                        columnHeaders.Add("Opening Balance");
                        columnHeaders.Add("Closing Balance");
                    }
                    else
                    {
                        columnHeaders.Add("جرد");
                        columnHeaders.Add(" ");
                        columnHeaders.Add("عميل بيع البضائع");
                        columnHeaders.Add(" ");
                        columnHeaders.Add("أُوكَازيُون");
                        columnHeaders.Add(" ");

                    }


                    sb.AppendLine(value);
                    string header = "Inventory" + "," + "," + "Cost of Goods Sale" + "," + "," + "Sale" + "," + "," + "\n";
                    string header1 = "Opening Balance" + "," + "Closing Balance" + "," + "Opening Balance" + "," + "Closing Balance" + "," + "Opening Balance" + "," + "Closing Balance" + "," + "\n";
                    string productValues = (stock.IOpeningBalance > 0 ? "Dr:" + Math.Abs(stock.IOpeningBalance) : "Cr:" + Math.Abs(stock.IOpeningBalance).ToString()) + "," + (stock.IClosingingBalance > 0 ? "Dr:" + Math.Abs(stock.IClosingingBalance) : "Cr:" + Math.Abs(stock.IClosingingBalance).ToString()) + "," + (stock.GSOpeningBalance > 0 ? "Dr:" + Math.Abs(stock.GSOpeningBalance) : "Cr:" + Math.Abs(stock.GSOpeningBalance).ToString()) + "," + (stock.GSClosingingBalance > 0 ? "Dr:" + Math.Abs(stock.GSClosingingBalance) : "Cr:" + Math.Abs(stock.GSClosingingBalance).ToString()) + "," + (stock.SOpeningBalance > 0 ? "Dr:" + Math.Abs(stock.SOpeningBalance) : "Cr:" + Math.Abs(stock.SOpeningBalance).ToString()) + "," + (stock.SClosingingBalance > 0 ? "Dr:" + Math.Abs(stock.SClosingingBalance) : "Cr:" + Math.Abs(stock.SClosingingBalance).ToString()) + "," + "\n" + "\n";
                    sb.Append(header);
                    sb.Append(header1);
                    sb.Append(productValues);
                }

                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                     $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                     $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "," + from + "," + to)}\r\n" +
                     $"{sb} ");
                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{sb} ");
                }






                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"ProductWiseAccountReport.csv");
            }

        }

        [Route("api/Report/CostCenterWiseAccountQuery")]
        [HttpGet("CostCenterWiseAccountQuery")]
        public async Task<IActionResult> CostCenterWiseAccountQuery(string branchId)
        {
            var list = await Mediator.Send(new CostCenterWiseAccountQuery()
            {
                BranchId = branchId,
            });
            return Ok(list);
        }

        [Route("api/Report/BalanceSheet")]
        [Microsoft.AspNetCore.Mvc.HttpGet("BalanceSheet")]
        [Roles("CanViewBalanceSheetReport")]
        public async Task<IActionResult> BalanceSheet(DateTime FromDate, DateTime ToDate, string branchId,string compareWith, string numberOfPeriods)
        {
            var incomeStatementList = await Mediator.Send(new GetIncomeStatementQuery()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                BranchId = branchId,

            });
            var list = await Mediator.Send(new GetBalanceSheetQuery()
            {
                FromDate = FromDate,
                ToDate = ToDate,
                BranchId = branchId,
            });
            var expnese = incomeStatementList.Expenses.Sum(x => x.Amount);
            var income = incomeStatementList.Income.Sum(x => x.Amount);

            var asset = list.Asset.Sum(x => x.Amount);
            var liability = list.Liability.Sum(x => x.Amount);
            var equity = list.Equity.Sum(x => x.Amount);

            list.YearlyIncome = Math.Abs(income) - expnese;

            //  list.YearlyIncome = Math.Abs(list.YearlyIncome);
            return Ok(list);
        }

        [Route("api/Report/AdvanceBalanceSheet")]
        [Microsoft.AspNetCore.Mvc.HttpGet("AdvanceBalanceSheet")]
        [Roles("CanViewBalanceSheetReport")]
        public async Task<IActionResult> AdvanceBalanceSheet(DateTime fromDate, DateTime toDate, string compareWith, string numberOfPeriods,string branchId)
        {
            var list = await Mediator.Send(new GetAdvanceBalanceSheetQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                BranchId = branchId

            });
            if (compareWith == null)
            {
                var incomeStatementList = await Mediator.Send(new GetIncomeSheetQuery()
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    CompareWith = compareWith,
                    NumberOfPeriods = numberOfPeriods
                });

                var expnese = incomeStatementList.Expense.Sum(x => x.Amount);
                var income = incomeStatementList.Income.Sum(x => x.Amount);
                var asset = list.Asset.Sum(x => x.Amount);
                var liability = list.Liability.Sum(x => x.Amount);
                var equity = list.Equity.Sum(x => x.Amount);

                list.YearlyIncome = Math.Abs(income) - expnese;
                list.TotalAssets = asset;
                list.TotalLiabilities = liability;
                list.TotalEquities = equity;
            }


            return Ok(list);
        }
        [Route("api/Report/GetIncomeSheetQuery")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetIncomeSheetQuery")]
        public async Task<IActionResult> GetIncomeSheetQuery(DateTime fromDate, DateTime toDate, string compareWith, string numberOfPeriods)
        {
            var list = await Mediator.Send(new GetIncomeSheetQuery()
            {
                FromDate = fromDate,
                ToDate = toDate,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods
            });

            return Ok(list);
        }
        [Route("api/Report/GetCostCenterList")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetCostCenterList")]
        public async Task<IActionResult> GetCostCenterList(bool isList,string branchId)
        {
            var list = await Mediator.Send(new GetCostCenterListQuery()
            {
                IsList = isList,
                BranchId = branchId
            });

            return Ok(list);
        }
        [Route("api/Report/GetYearlyPeriodList")]
        [Microsoft.AspNetCore.Mvc.HttpGet("GetYearlyPeriodList")]
        public async Task<IActionResult> GetYearlyPeriodList()
        {
            var list = await Mediator.Send(new GetYearlyPeriodListQuery() { });

            return Ok(list);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("IncomeStatement")]
        [Roles("CanViewIncomeStatementReport")]
        public async Task<IActionResult> IncomeStatement(DateTime FromDate, DateTime ToDate)
        {
            var list = await Mediator.Send(new GetIncomeStatementQuery()
            {
                FromDate = FromDate,
                ToDate = ToDate
            });
            return Ok(list);
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("CustomerLedger")]
        public async Task<IActionResult> CustomerLedger(DateTime FromDate, DateTime ToDate, bool IsCustomer, bool formName, string compareWith, string numberOfPeriods,string branchId)
        {

            var list = await Mediator.Send(new GetAdvanceCustomerLedgerQuery()
            {
                IsCustomer = IsCustomer,
                FromDate = FromDate,
                ToDate = ToDate,
                FormName = formName,
                CompareWith = compareWith,
                NumberOfPeriods = numberOfPeriods,
                BranchId = branchId
            });
            return Ok(list);
        }
        [HttpGet("VatPayable")]
        [Roles("CanViewVatPayableReport")]
        public async Task<IActionResult> VatPayable(DateTime FromDate, DateTime ToDate, string branchId)
        {
            var list = await Mediator.Send(new GetVatPayableQuery()
            {
                BranchId = branchId,
                FromDate = FromDate,
                ToDate = ToDate
            });
            return Ok(list);

        }
        [Microsoft.AspNetCore.Mvc.HttpGet("MedicalReport")]
        [AllowAnonymous]
        public async Task<IActionResult> MedicalReport(DateTime fromDate, DateTime toDate, Guid categoryId)
        {

            var list = await Mediator.Send(new MedicalReportQuery()
            {
                FromTime = fromDate,
                ToTime = toDate,
                CategoryId = categoryId
            });
            return Ok(list);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("ProductLedgerReport")]
        [AllowAnonymous]
        public async Task<IActionResult> ProductLedgerReport(DateTime fromDate, DateTime toDate, Guid productId, string branchId)
        {
            var list = await Mediator.Send(new ProductLedgerReport()
            {
                BranchId = branchId,
                FromDate = fromDate,
                ToDate = toDate,
                ProductId = productId
            });
            return Ok(list);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("ProductComparisionReport")]
        [AllowAnonymous]
        public async Task<IActionResult> ProductComparisionReport(DateTime fromDate, DateTime toDate, string branchId)
        {

            var list = await Mediator.Send(new ProductComparisionReport()
            {
                BranchId = branchId,
                FromDate = fromDate,
                ToDate = toDate,
            });
            return Ok(list);
        }


        [HttpGet("SaleInvoice")]
        [Roles("CanViewSaleInvoiceReport")]
        public async Task<IActionResult> SaleInvoiceReport(DateTime FromDate, DateTime toDate, string branchId, string customerId)
        {
            var list = await Mediator.Send(new GetSaleInvoiceQuery()
            {
                FromDate = FromDate,
                ToDate = toDate,
                BranchId = branchId,
                CustomerId = customerId
            });
            return Ok(list);
        }
        [HttpGet("MonthlySaleReport")]
        [Roles("CanViewSaleInvoiceReport")]
        public async Task<IActionResult> MonthlySaleReport(int year, int month, DateTime? from, DateTime? to)
        {
            var list = await Mediator.Send(new GetMonthlySaleQuery()
            {
                Year = year,
                Month = month
            });
            return Ok(list);
        }
        [HttpGet("MonthlyComparisionSaleReport")]
        [Roles("CanViewSaleInvoiceReport")]
        public async Task<IActionResult> MonthlyComparisionSaleReport(int year, int month, DateTime? from, DateTime? to, string branchId)
        {
            var list = await Mediator.Send(new GetMonthlyComparisionReportQuery()
            {
                Year = year,
                Month = month,
                BranchId = branchId,
            });
            return Ok(list);
        }
        [HttpGet("MonthlyPurchaseReport")]
        [Roles("CanViewPurchaseInvoiceReport")]
        public async Task<IActionResult> MonthlyPurchaseReport(int year, int month, DateTime? from, DateTime? to)
        {
            var list = await Mediator.Send(new GetMonthlyPurchaseQuery()
            {
                Year = year,
                Month = month
            });
            return Ok(list);
        }

        [HttpPost("SaleInvoiceCsv")]
        [Roles("CanViewSaleInvoiceReport")]
        public async Task<IActionResult> SaleInvoiceCsv([FromBody] List<SaleInvoiceModel> invoiceList, string language, DateTime? fromDate, DateTime? toDate, Guid companyId, string type)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = "";
            decimal grossAmount = 0;
            decimal discountAmount = 0;
            decimal amountwithDiscount = 0;
            decimal vaTamount = 0;
            decimal totalAmount = 0;
            if (type == "Both" || type == "كلاهما")
            {
                reportName = language == "en" ? "Sale Invoice And Sale Return Report: " : "فاتورة البيع وتقرير إرجاع البيع";
                grossAmount = invoiceList.Where(x => x.SaleInvoiceId == null).Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount) - invoiceList.Where(x => x.SaleInvoiceId != null).Sum(x => Math.Abs(x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount));
                discountAmount = invoiceList.Where(x => x.SaleInvoiceId == null).Sum(x => x.Discount) - invoiceList.Where(x => x.SaleInvoiceId != null).Sum(x => Math.Abs(x.Discount));
                amountwithDiscount = invoiceList.Where(x => x.SaleInvoiceId == null).Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount) - invoiceList.Where(x => x.SaleInvoiceId != null).Sum(x => Math.Abs(x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount));
                vaTamount = invoiceList.Where(x => x.SaleInvoiceId == null).Sum(x => x.VATamount) - invoiceList.Where(x => x.SaleInvoiceId != null).Sum(x => Math.Abs(x.VATamount));
                totalAmount = invoiceList.Where(x => x.SaleInvoiceId == null).Sum(x => x.TotalAmount) - invoiceList.Where(x => x.SaleInvoiceId != null).Sum(x => Math.Abs(x.TotalAmount));
            }
            if (type == "Sale Invoice" || type == "فاتورة البيع")
            {
                reportName = language == "en" ? "Sale Invoice Report: " : "تقرير فاتورة البيع";
                invoiceList = invoiceList.Where(x => x.SaleInvoiceId == null).ToList();

                grossAmount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount);
                discountAmount = invoiceList.Sum(x => x.Discount);
                amountwithDiscount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount);
                vaTamount = invoiceList.Sum(x => x.VATamount);
                totalAmount = invoiceList.Sum(x => x.TotalAmount);
            }
            if (type == "Sale Return" || type == "عودة البيع")
            {
                reportName = language == "en" ? "Sale Return Report: " : "تقرير إرجاع المبيعات";
                invoiceList = invoiceList.Where(x => x.SaleInvoiceId != null).ToList();

                grossAmount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount);
                discountAmount = invoiceList.Sum(x => x.Discount);
                amountwithDiscount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount);
                vaTamount = invoiceList.Sum(x => x.VATamount);
                totalAmount = invoiceList.Sum(x => x.TotalAmount);
            }

            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }
            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Invoice No");
                columnHeaders.Add("Date");
                columnHeaders.Add("Customer Name");
                columnHeaders.Add("Gross value");
                columnHeaders.Add("Discount Amount");
                columnHeaders.Add("Net Sale Amount");
                columnHeaders.Add("VAT Amount");
                columnHeaders.Add("Total Amount");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("الرقم العرض");
                columnHeaders.Add("التاريخ");
                columnHeaders.Add("اسم الزبون");
                columnHeaders.Add("القيمة اإلجمالية");
                columnHeaders.Add("مقدار الخصم");
                columnHeaders.Add("مبلغ البيع الصافي");
                columnHeaders.Add("قيمة الضريبة");
                columnHeaders.Add("المبلغ اإلجمالي");
            }



            var i = 1;
            var sb = new StringBuilder();

            foreach (var saleInvoice in invoiceList)
            {
                var name = language == "en" ? !string.IsNullOrEmpty(saleInvoice.CustomerName) ? saleInvoice.CustomerName
                    : saleInvoice.CustomerNameArabic == null || saleInvoice.CustomerName == "" ? "Walk-In" : saleInvoice.CustomerNameArabic
                    : !string.IsNullOrEmpty(saleInvoice.CustomerNameArabic) ? saleInvoice.CustomerNameArabic
                    : string.IsNullOrEmpty(saleInvoice.CustomerName) ? "Walk-In" : saleInvoice.CustomerName;

                string value = i++ + "," + saleInvoice.InvoiceNo + "," + saleInvoice.Date.ToString("d") + "," + name + "," + Math.Round(saleInvoice.TaxMethod == "Inclusive" || saleInvoice.TaxMethod == "شامل" ? saleInvoice.Amount - saleInvoice.VATamount : saleInvoice.Amount, 2) + "," + Math.Round(saleInvoice.Discount, 2) + "," + Math.Round(saleInvoice.TaxMethod == "Inclusive" || saleInvoice.TaxMethod == "شامل" ? saleInvoice.AmountwithDiscount - saleInvoice.VATamount : saleInvoice.AmountwithDiscount, 2) + "," + Math.Round(saleInvoice.VATamount, 2) + "," + Math.Round(saleInvoice.TotalAmount, 2);

                sb.AppendLine(value);
            }




            byte[] buffer;
            if (language == "en")
            {

                buffer = Encoding.UTF8.GetBytes(
                $"{string.Join(",", company.NameEnglish + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + "," + "," + reportName + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "," + "," + "," + from + "," + to)}\r\n" +
                $"{string.Join(",", columnHeaders)}\r\n " +
                $"{sb}\r\n " +
                $"{string.Join(",", "," + "," + "," + "Total" + "," + Math.Round(grossAmount, 2) + "," + Math.Round(discountAmount, 2) + "," + Math.Round(amountwithDiscount, 2) + "," + Math.Round(vaTamount, 2) + "," + Math.Round(totalAmount, 2))}");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
               $"{string.Join(",", company.NameEnglish + "," + "," + "," + "," + "," + "," + "," + "," + company.NameArabic)}\r\n" +
               $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + "," + "," + "," + "," + "," + company.CategoryArabic)}\r\n" +
               $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + "," + "," + reportName + "," + "," + "," + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
               $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + "," + "," + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
               $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + "," + "," + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
               $"{string.Join(",", "," + "," + "," + from + "," + to)}\r\n" +
               $"{string.Join(",", columnHeaders)}\r\n " +
               $"{sb}\r\n " +
               $"{string.Join(",", "," + "," + "," + "Total" + "," + Math.Round(grossAmount, 2) + "," + Math.Round(discountAmount, 2) + "," + Math.Round(amountwithDiscount, 2) + "," + Math.Round(vaTamount, 2) + "," + Math.Round(totalAmount, 2))}");
            }



            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"SaleReport.csv");
        }

        [HttpPost("PurchaseInvoiceCsv")]
        [Roles("CanViewPurchaseInvoiceReport")]
        public async Task<IActionResult> PurchaseInvoiceCsv([FromBody] List<SaleInvoiceModel> invoiceList, string language, DateTime? fromDate, DateTime? toDate, Guid companyId, string type)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = "";
            decimal grossAmount = 0;
            decimal discountAmount = 0;
            decimal amountwithDiscount = 0;
            decimal vaTamount = 0;
            decimal totalAmount = 0;
            if (type == "Both" || type == "كلاهما")
            {
                reportName = language == "en" ? "Purchase Invoice And Purchase Return Report: " : "فاتورة الشراء وتقرير إرجاع الشراء";
                grossAmount = invoiceList.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount) - invoiceList.Where(x => x.PurchaseInvoiceId != null).Sum(x => Math.Abs(x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount));
                discountAmount = invoiceList.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.Discount) - invoiceList.Where(x => x.PurchaseInvoiceId != null).Sum(x => Math.Abs(x.Discount));
                amountwithDiscount = invoiceList.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount) - invoiceList.Where(x => x.PurchaseInvoiceId != null).Sum(x => Math.Abs(x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount));
                vaTamount = invoiceList.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.VATamount) - invoiceList.Where(x => x.PurchaseInvoiceId != null).Sum(x => Math.Abs(x.VATamount));
                totalAmount = invoiceList.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.TotalAmount) - invoiceList.Where(x => x.PurchaseInvoiceId != null).Sum(x => Math.Abs(x.TotalAmount));
            }
            if (type == "Purchase Invoice" || type == "فاتورة الشراء")
            {
                reportName = language == "en" ? "Purchase Invoice Report: " : "تقرير فاتورة الشراء";
                invoiceList = invoiceList.Where(x => x.PurchaseInvoiceId == null).ToList();

                grossAmount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount);
                discountAmount = invoiceList.Sum(x => x.Discount);
                amountwithDiscount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount);
                vaTamount = invoiceList.Sum(x => x.VATamount);
                totalAmount = invoiceList.Sum(x => x.TotalAmount);
            }
            if (type == "Purchase Return" || type == "عودة شراء")
            {
                reportName = language == "en" ? "Purchase Return Report: " : "تقرير إرجاع الشراء";
                invoiceList = invoiceList.Where(x => x.PurchaseInvoiceId != null).ToList();

                grossAmount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.Amount - x.VATamount : x.Amount);
                discountAmount = invoiceList.Sum(x => x.Discount);
                amountwithDiscount = invoiceList.Sum(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل" ? x.AmountwithDiscount - x.VATamount : x.AmountwithDiscount);
                vaTamount = invoiceList.Sum(x => x.VATamount);
                totalAmount = invoiceList.Sum(x => x.TotalAmount);
            }

            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }

            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Invoice No");
                columnHeaders.Add("Date");
                columnHeaders.Add("Supplier Name");
                columnHeaders.Add("Gross value");
                columnHeaders.Add("Discount Amount");
                columnHeaders.Add("Net Sale Amount");
                columnHeaders.Add("VAT Amount");
                columnHeaders.Add("Total Amount");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("الرقم العرض");
                columnHeaders.Add("التاريخ");
                columnHeaders.Add("اسم المورد");
                columnHeaders.Add("القيمة اإلجمالية");
                columnHeaders.Add("مقدار الخصم");
                columnHeaders.Add("مبلغ البيع الصافي");
                columnHeaders.Add("قيمة الضريبة");
                columnHeaders.Add("المبلغ اإلجمالي");
            }



            var i = 1;
            var sb = new StringBuilder();

            foreach (var saleInvoice in invoiceList)
            {
                var name = language == "en" ? !string.IsNullOrEmpty(saleInvoice.CustomerName) ? saleInvoice.CustomerName
                        : saleInvoice.CustomerNameArabic == null || saleInvoice.CustomerName == "" ? "Walk-In" : saleInvoice.CustomerNameArabic
                        : !string.IsNullOrEmpty(saleInvoice.CustomerNameArabic) ? saleInvoice.CustomerNameArabic
                        : string.IsNullOrEmpty(saleInvoice.CustomerName) ? "Walk-In" : saleInvoice.CustomerName;

                string value = i++ + "," + saleInvoice.InvoiceNo + "," + saleInvoice.Date.ToString("d") + "," + name + "," + Math.Round(saleInvoice.TaxMethod == "Inclusive" || saleInvoice.TaxMethod == "شامل" ? saleInvoice.Amount - saleInvoice.VATamount : saleInvoice.Amount, 2) + "," + Math.Round(saleInvoice.Discount, 2) + "," + Math.Round(saleInvoice.TaxMethod == "Inclusive" || saleInvoice.TaxMethod == "شامل" ? saleInvoice.AmountwithDiscount - saleInvoice.VATamount : saleInvoice.AmountwithDiscount, 2) + "," + Math.Round(saleInvoice.VATamount, 2) + "," + Math.Round(saleInvoice.TotalAmount, 2);

                sb.AppendLine(value);
            }


            byte[] buffer;
            if (language == "en")
            {
                buffer = Encoding.UTF8.GetBytes(
             $"{string.Join(",", company.NameEnglish + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
             $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
             $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + "," + "," + reportName + "," + "," + "," + ",")}\r\n" +
             $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
             $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
             $"{string.Join(",", "," + "," + "," + from + "," + to)}\r\n" +
             $"{string.Join(",", columnHeaders)}\r\n " +
             $"{sb}\r\n " +
             $"{string.Join(",", "," + "," + "," + "Total" + "," + Math.Round(grossAmount, 2) + "," + Math.Round(discountAmount, 2) + "," + Math.Round(amountwithDiscount, 2) + "," + Math.Round(vaTamount, 2) + "," + Math.Round(totalAmount, 2))}");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
               $"{string.Join(",", company.NameEnglish + "," + "," + "," + "," + "," + "," + "," + "," + company.NameArabic)}\r\n" +
               $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + "," + "," + "," + "," + "," + company.CategoryArabic)}\r\n" +
               $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + "," + "," + reportName + "," + "," + "," + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
               $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + "," + "," + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
               $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + "," + "," + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
               $"{string.Join(",", "," + "," + "," + from + "," + to)}\r\n" +
               $"{string.Join(",", columnHeaders)}\r\n " +
               $"{sb}\r\n " +
               $"{string.Join(",", "," + "," + "," + "Total" + "," + Math.Round(grossAmount, 2) + "," + Math.Round(discountAmount, 2) + "," + Math.Round(amountwithDiscount, 2) + "," + Math.Round(vaTamount, 2) + "," + Math.Round(totalAmount, 2))}");
            }



            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"SaleReport.csv");
        }

        [HttpPost("VatExpenseCsv")]
        [AllowAnonymous]
        public async Task<IActionResult> VatExpenseCsv([FromBody] List<DailyExpenseLookUpModel> invoiceList, string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "VAT Expense Report: " : "تقرير مصروفات ضريبة القيمة المضافة";


            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }

            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Date");
                columnHeaders.Add("Expense No");
                columnHeaders.Add("Gross Amount");
                columnHeaders.Add("VAT Amount");
                columnHeaders.Add("Total Amount");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("تاريخ");
                columnHeaders.Add("رقم المصاريف");
                columnHeaders.Add("المبلغ الإجمالي");
                columnHeaders.Add("قيمة الضريبة");
                columnHeaders.Add("المبلغ الإجمالي");
            }


            decimal grossAmount = 0;
            decimal vaTamount = 0;
            decimal totalAmount = 0;
            var i = 1;
            var j = 1;
            grossAmount = Math.Round((invoiceList.Sum(x => x.GrossAmount)), 2);
            vaTamount = Math.Round((invoiceList.Sum(x => x.TotalVat)), 2);
            totalAmount = Math.Round((invoiceList.Sum(x => x.TotalAmount)), 2);

            var sb = new StringBuilder();

            foreach (var vat in invoiceList)
            {
                string value = i++ + "," + vat.Date + "," + vat.VoucherNo + "," + Math.Round(vat.GrossAmount, 2) + "," + Math.Round(vat.TotalVat, 2) + "," + Math.Round(vat.TotalAmount, 2);
                sb.AppendLine(value);
            }
            byte[] buffer;
            if (language == "en")
            {

                buffer = Encoding.UTF8.GetBytes(
                $"{string.Join(",", company.NameEnglish + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + "," + "," + reportName + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                $"{string.Join(",", "," + "," + "," + from + "," + to)}\r\n" +
                $"{string.Join(",", columnHeaders)}\r\n " +
                $"{sb}\r\n " +
                $"{string.Join(",", ",", "," + Math.Round(grossAmount, 2) + "," + Math.Round(vaTamount, 2) + "," + Math.Round(totalAmount, 2))}");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
                 $"{string.Join(",", company.NameEnglish + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + "," + "," + reportName + "," + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + "," + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", "," + "," + "," + from + "," + to)}\r\n" +
                 $"{string.Join(",", columnHeaders)}\r\n " +
                 $"{sb}\r\n " +
                 $"{string.Join(",", ",", "," + Math.Round(grossAmount, 2) + "," + Math.Round(vaTamount, 2) + "," + Math.Round(totalAmount, 2))}");
            }




            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"VATExpenserEPORT.csv");
        }



        [HttpPost("ManualListQuery")]
        public async Task<IActionResult> ManualListQuery([FromBody] List<ManualAttendenceLookUpModel> employeelist, string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "Attendence Report: " : "Attendence Report";


            var from = "";
            var to = "";

            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }


            List<string> columnHeaders = new List<string>();

            {
                columnHeaders.Add("#");
                columnHeaders.Add("Employee Name");
                columnHeaders.Add("Date");
                columnHeaders.Add("Check In");
                columnHeaders.Add("Check Out");
                columnHeaders.Add("Total Hour");
                columnHeaders.Add("Working %");
            }




            var i = 1;
            var j = 1;
            var vatPaidSb = new StringBuilder();
            var vatPayableSb = new StringBuilder();

            foreach (var vat in employeelist)
            {
                if (vat.CheckType == "Week Holiday")
                {
                    string value = "," + "," + "," + "," + "," + "Week Holiday(" + vat.Description + ")" + "," + "Working: 100%";
                    vatPaidSb.AppendLine(value);
                }
                else if (vat.CheckType == "Guested Holiday")
                {
                    string value = "," + "," + "," + "," + "," + "Guested Holiday(" + vat.Description + ")" + "," + "Working: 100%";
                    vatPaidSb.AppendLine(value);
                }
                else if (vat.CheckType == "On Leave")
                {
                    string value = i++ + "," + vat.EmployeeName + "," + "," + "," + "," + "On Leave" + "," + "Working: 100%";
                    vatPaidSb.AppendLine(value);
                }
                else if (vat.IsAbsent)
                {
                    string value = i++ + "," + vat.EmployeeName + "," + "," + "," + "," + "Absent" + "," + "Working: 0%";
                    vatPaidSb.AppendLine(value);
                }
                else
                {
                    string value = i++ + "," + vat.EmployeeName + "," + vat.Date + "," + vat.CheckIn + "," + vat.CheckOut + "," + vat.TotalHour + "," + Math.Round(vat.WorkingPercentage) + "%";
                    vatPaidSb.AppendLine(value);
                }

            }


            var totalHour = Math.Abs(employeelist.Sum(x => x.TotalHour));
            //int totalMinute = Decimal.ToInt32(Math.Abs(employeelist.Sum(x => x.TotalMinute))/60) ;
            //var officeMinute = Decimal.ToInt32(Math.Abs(employeelist.Sum(x => x.OfficeMinute))/60);
            var totalWorkingHour = Math.Abs(employeelist.Sum(x => x.OfficeHour));
            var total = Math.Abs(((totalHour) / (totalWorkingHour)) * 100);


            byte[] buffer;


            buffer = Encoding.UTF8.GetBytes(
                            $"{string.Join(",", company.NameEnglish + "," + ",")}\r\n" +
                            $"{string.Join(",", company.CategoryEnglish + "," + ",")}\r\n" +
                            $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                            $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                            $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                            $"{string.Join(",", "," + from + "," + to)}\r\n" +
                            $"{string.Join(",", "")}\r\n " +
                            $"{string.Join(",", columnHeaders)}\r\n " +

                            $"{vatPaidSb} " +
                            $"{string.Join(",", "," + "," + "" + "," + "")}\r\n" +
                            $"{string.Join(",", "," + "," + "Total Working %" + "," + Math.Round(Math.Abs(total), 2) + "%")}");







            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"AttendenceCSV.csv");
        }





        [HttpPost("VatPayableCsv")]
        [Roles("CanViewVatPayableReport")]
        public async Task<IActionResult> VatPayableCsv([FromBody] VatPayableListModel vatPayableList, string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "Vat Payable Report: " : "تقرير ضريبة القيمة المضافة المستحقة الدفع";


            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }

            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Code");
                columnHeaders.Add("Name");
                columnHeaders.Add("Amount");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("	الــكــود");
                columnHeaders.Add("اسم");
                columnHeaders.Add("مقدار");
            }



            var i = 1;
            var j = 1;
            var vatPaidSb = new StringBuilder();
            var vatPayableSb = new StringBuilder();

            foreach (var vat in vatPayableList.VatPaid)
            {
                string value = i++ + "," + vat.Code + "," + (language == "en" ? vat.Name : vat.NameArabic) + "," + Math.Abs(vat.Amount);
                vatPaidSb.AppendLine(value);
            }

            foreach (var vat in vatPayableList.VatPayable)
            {
                string value = j++ + "," + vat.Code + "," + (language == "en" ? vat.Name : vat.NameArabic) + "," + Math.Abs(vat.Amount);
                vatPayableSb.AppendLine(value);
            }

            var vatPayable = Math.Abs(vatPayableList.VatPayable.Sum(x => x.Amount)) - Math.Abs(vatPayableList.VatPaid.Sum(x => x.Amount));

            byte[] buffer;
            if (language == "en")
            {
                buffer = Encoding.UTF8.GetBytes(
                                $"{string.Join(",", company.NameEnglish + "," + ",")}\r\n" +
                                $"{string.Join(",", company.CategoryEnglish + "," + ",")}\r\n" +
                                $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                                $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                                $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                                $"{string.Join(",", "," + from + "," + to)}\r\n" +
                                $"{string.Join(",", "")}\r\n " +
                                $"{string.Join(",", "VAT Paid")}\r\n " +
                                $"{string.Join(",", columnHeaders)}\r\n " +
                                $"{vatPaidSb} " +
                                $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(vatPayableList.VatPaid.Sum(x => x.Amount)))}\r\n " +
                                $"{string.Join(",", (language == "en" ? "VAT Payable" : "الرقم الضريبي المستحقة"))}\r\n " +
                                $"{vatPayableSb} " +
                                $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(vatPayableList.VatPayable.Sum(x => x.Amount)))}\r\n" +
                                $"{string.Join(",", "," + "," + "" + "," + "")}\r\n" +
                                $"{string.Join(",", "," + "," + (language == "en" ? "VAT Payable" : "	الرقم الضريبي المستحقة") + "," + Math.Abs(vatPayable))}");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
               $"{string.Join(",", company.NameEnglish + "," + ",")}\r\n" +
               $"{string.Join(",", company.CategoryEnglish + "," + ",")}\r\n" +
               $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
               $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "," + from + "," + to)}\r\n" +
               $"{string.Join(",", "")}\r\n " +
               $"{string.Join(",", (language == "en" ? "VAT Paid" : "دفع ضريبة القيمة المضافة"))}\r\n " +
               $"{string.Join(",", columnHeaders)}\r\n " +
               $"{vatPaidSb} " +
               $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(vatPayableList.VatPaid.Sum(x => x.Amount)))}\r\n " +
               $"{string.Join(",", (language == "en" ? "VAT Payable" : "الرقم الضريبي المستحقة"))}\r\n " +
               $"{vatPayableSb} " +
               $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(vatPayableList.VatPayable.Sum(x => x.Amount)))}\r\n" +
               $"{string.Join(",", "," + "," + "" + "," + "")}\r\n" +
               $"{string.Join(",", "," + "," + (language == "en" ? "VAT Payable" : "	الرقم الضريبي المستحقة") + "," + Math.Abs(vatPayable))}");
            }





            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"VatPayableReport.csv");
        }
        [HttpGet("BalanceSheetCsv")]
        [Roles("CanViewBalanceSheetReport")]
        public async Task<IActionResult> BalanceSheetCsv(string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var incomeStatementList = await Mediator.Send(new GetIncomeStatementQuery()
            {
                FromDate = fromDate.Value,
                ToDate = toDate.Value
            });
            var list = await Mediator.Send(new GetBalanceSheetQuery()
            {
                FromDate = fromDate.Value,
                ToDate = toDate.Value
            });
            var expnese = incomeStatementList.Expenses.Sum(x => x.Amount);
            var income = incomeStatementList.Income.Sum(x => x.Amount);
            list.YearlyIncome = Math.Abs(income) - expnese;



            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "Balance Sheet Report: " : "تقرير الميزانية العمومية";


            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }

            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Code");
                columnHeaders.Add("Account");
                columnHeaders.Add("Amount");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("	الــكــود");
                columnHeaders.Add("الحساب");
                columnHeaders.Add("مقدار");
            }



            var i = 1;
            var j = 1;
            var totalAssets = new StringBuilder();
            var totalLiabilites = new StringBuilder();
            var totalEquity = new StringBuilder();

            foreach (var vat in list.Asset)
            {
                string value = i++ + "," + vat.Code + "," + (language == "en" ? vat.CostCenter : vat.CostCenterArabic) + "," + Math.Abs(vat.Amount);
                totalAssets.AppendLine(value);
            }

            var asset = list.Asset.Sum(x => x.Amount);
            foreach (var vat in list.Liability)
            {
                string value = j++ + "," + vat.Code + "," + (language == "en" ? vat.CostCenter : vat.CostCenterArabic) + "," + Math.Abs(vat.Amount);
                totalLiabilites.AppendLine(value);
            }
            var liablity = list.Liability.Sum(x => x.Amount);
            foreach (var vat in list.Equity)
            {
                string value = j++ + "," + vat.Code + "," + (language == "en" ? vat.CostCenter : vat.CostCenterArabic) + "," + Math.Abs(vat.Amount);
                totalEquity.AppendLine(value);
            }
            var equity = list.YearlyIncome + list.Equity.Sum(x => x.Amount);
            var totalEquityAndLiability = equity + liablity;


            byte[] buffer;
            if (language == "en")
            {
                buffer = Encoding.UTF8.GetBytes(
                              $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                              $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                              $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                              $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                              $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                              $"{string.Join(",", "," + from + "," + to)}\r\n" +
                              $"{string.Join(",", "")}\r\n " +
                              $"{string.Join(",", (language == "en" ? "Assets" : "أصول"))}\r\n " +
                              $"{string.Join(",", columnHeaders)}\r\n " +
                              $"{totalAssets} " +
                              $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(asset))}\r\n " +
                              $"{string.Join(",", (language == "en" ? "Liabilities" : "المطلوبات"))}\r\n " +
                              $"{totalLiabilites} " +
                              $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(liablity))}\r\n" +
                              $"{string.Join(",", (language == "en" ? "Equity" : "عدالة"))}\r\n " +
                              $"{totalEquity} " +
                              $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(equity))}\r\n" +
                              $"{string.Join(",", "," + "," + "" + "," + "")}\r\n" +
                              $"{string.Join(",", "," + "Total Liabilities and Equity" + "," + Math.Abs(totalEquityAndLiability) + "," + "	Total Assets" + "," + Math.Abs(asset))}");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
              $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
              $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
              $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
              $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
              $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
              $"{string.Join(",", "," + from + "," + to)}\r\n" +
              $"{string.Join(",", "")}\r\n " +
              $"{string.Join(",", (language == "en" ? "Assets" : "أصول"))}\r\n " +
              $"{string.Join(",", columnHeaders)}\r\n " +
              $"{totalAssets} " +
              $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(asset))}\r\n " +
              $"{string.Join(",", (language == "en" ? "Liabilities" : "المطلوبات"))}\r\n " +
              $"{totalLiabilites} " +
              $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(liablity))}\r\n" +
              $"{string.Join(",", (language == "en" ? "Equity" : "عدالة"))}\r\n " +
              $"{totalEquity} " +
              $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(equity))}\r\n" +
              $"{string.Join(",", "," + "," + "" + "," + "")}\r\n" +
              $"{string.Join(",", "," + "Total Liabilities and Equity" + "," + Math.Abs(totalEquityAndLiability) + "," + "	Total Assets" + "," + Math.Abs(asset))}");
            }





            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"VatPayableReport.csv");
        }
        [HttpGet("IncomeStatementCsv")]
        [Roles("CanViewIncomeStatementReport")]
        public async Task<IActionResult> IncomeStatementCsv(string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var incomeStatementList = await Mediator.Send(new GetIncomeStatementQuery()
            {
                FromDate = fromDate.Value,
                ToDate = toDate.Value
            });

            var expnese = incomeStatementList.Expenses.Sum(x => x.Amount);
            var income = incomeStatementList.Income.Sum(x => x.Amount);



            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "Income Statement Report: " : "تقرير بيان الدخل";


            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }

            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Code");
                columnHeaders.Add("Account");
                columnHeaders.Add("Amount");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("	الــكــود");
                columnHeaders.Add("الحساب");
                columnHeaders.Add("مقدار");
            }



            var i = 1;
            var j = 1;
            var totalAssets = new StringBuilder();
            var totalLiabilites = new StringBuilder();
            var totalEquity = new StringBuilder();

            foreach (var vat in incomeStatementList.Income)
            {
                string value = i++ + "," + vat.Code + "," + (language == "en" ? vat.CostCenter : vat.CostCenterArabic) + "," + Math.Abs(vat.Amount);
                totalAssets.AppendLine(value);
            }

            foreach (var vat in incomeStatementList.Expenses)
            {
                string value = j++ + "," + vat.Code + "," + (language == "en" ? vat.CostCenter : vat.CostCenterArabic) + "," + Math.Abs(vat.Amount);
                totalLiabilites.AppendLine(value);
            }

            var netIncome = Math.Abs(income) - Math.Abs(expnese);


            byte[] buffer;
            if (language == "en")
            {

                buffer = Encoding.UTF8.GetBytes(
                  $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                  $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                  $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                  $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                  $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                  $"{string.Join(",", "," + from + "," + to)}\r\n" +
                  $"{string.Join(",", "")}\r\n " +
                  $"{string.Join(",", (language == "en" ? "Revenue" : "إيرادات"))}\r\n " +
                  $"{string.Join(",", columnHeaders)}\r\n " +
                  $"{totalAssets} " +
                  $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(income))}\r\n " +
                  $"{string.Join(",", (language == "en" ? "Expenses" : "نفقات"))}\r\n " +
                  $"{totalLiabilites} " +
                  $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(expnese))}\r\n" +
                  $"{string.Join(",", "," + "," + "" + "," + "")}\r\n" +
                  $"{string.Join(",", "," + "Net Income" + "," + Math.Abs(netIncome))}");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
                $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                $"{string.Join(",", "," + from + "," + to)}\r\n" +
                $"{string.Join(",", "")}\r\n " +
                $"{string.Join(",", (language == "en" ? "Revenue" : "إيرادات"))}\r\n " +
                $"{string.Join(",", columnHeaders)}\r\n " +
                $"{totalAssets} " +
                $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(income))}\r\n " +
                $"{string.Join(",", (language == "en" ? "Expenses" : "نفقات"))}\r\n " +
                $"{totalLiabilites} " +
                $"{string.Join(",", "," + "," + (language == "en" ? "Total" : "مجموع") + "," + Math.Abs(expnese))}\r\n" +
                $"{string.Join(",", "," + "," + "" + "," + "")}\r\n" +
                $"{string.Join(",", "," + "Net Income" + "," + Math.Abs(netIncome))}");
            }



            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"IncomeStatementReport.csv");
        }

        [HttpPost("InventoryStockCsv")]
        [Route("api/Report/InventoryStockCsv")]
        [Roles("CanViewStockReport")]
        public async Task<IActionResult> InventoryStockCsv([FromBody] List<InventoryLookUpModel> inventoryList, string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "Inventory Stock Report: " : "تقرير المخزون المخزون";

            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }


            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Product");
                columnHeaders.Add("Opening");
                columnHeaders.Add("Quantity In");
                columnHeaders.Add("Quantity Out");
                columnHeaders.Add("Balance");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("	منتج");
                columnHeaders.Add("افتتاح");
                columnHeaders.Add("الكمية في");
                columnHeaders.Add("كمية خارج");
                columnHeaders.Add("الرصيد");
            }



            var i = 1;
            var j = 1;
            var sb = new StringBuilder();

            foreach (var stock in inventoryList)
            {




                string value = i++ + "," + (language == "en" ? stock.ProductName : stock.ProductNameArabic) + "," + Math.Abs(stock.Opening) + "," + Math.Abs(stock.QuantityIn) + "," + Math.Abs(stock.QuantityOut) + "," + (stock.Balance);
                sb.AppendLine(value);
            }

            byte[] buffer;
            if (language == "en")
            {

                buffer = Encoding.UTF8.GetBytes(
               $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
               $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
               $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "," + from + "," + to)}\r\n" +
               $"{string.Join(",", columnHeaders)}\r\n " +
               $"{sb} ");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
              $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
              $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
              $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo)}\r\n" +
              $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo)}\r\n" +
              $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo)}\r\n" +
              $"{string.Join(",", "," + from + "," + to)}\r\n" +
              $"{string.Join(",", columnHeaders)}\r\n " +
              $"{sb} ");
            }





            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"StockInventoryReport.csv");
        }



        [HttpPost("SaleOrderTrackingReport")]
        [Route("api/Report/SaleOrderTrackingReport")]
        public async Task<IActionResult> SaleOrderTrackingReport([FromBody] List<SaleOrderLookUpModel> saleOrderTrackinglist, string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "Inventory Stock Report: " : "تقرير المخزون المخزون";

            var from = "";
            var to = "";
            if (language == "en")
            {
                from = "From Date : " + fromDate?.ToString("d");
                to = "To Date : " + toDate?.ToString("d");
            }
            else if (language == "ar")
            {
                from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
            }


            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Sale Order Tracking No");
                columnHeaders.Add("Employee Name");
                columnHeaders.Add("Product English Name");
                columnHeaders.Add("Product Arabic Name");
                columnHeaders.Add("Product Code");
                columnHeaders.Add("Quantity");
                columnHeaders.Add("UnitPrice");
                columnHeaders.Add("Total");
            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("	منتج");
                columnHeaders.Add("افتتاح");
                columnHeaders.Add("الكمية في");
                columnHeaders.Add("كمية خارج");
                columnHeaders.Add("الرصيد");
            }



            var i = 1;
            var j = 1;
            var sb = new StringBuilder();

            foreach (var item in saleOrderTrackinglist)
            {

                foreach (var stock in item.SaleOrderItem)
                {
                    string value = i++ + "," + item.RegistrationNumber + "," + item.EmployeeName + "," + stock.ProductNameEn + "," + stock.ProductNameAr + "," + stock.Code + "," + stock.Quantity + "," + stock.UnitPrice + "," + stock.Total;
                    sb.AppendLine(value);
                }

            }

            byte[] buffer;
            if (language == "en")
            {

                buffer = Encoding.UTF8.GetBytes(
               $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
               $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
               $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
               $"{string.Join(",", "," + from + "," + to)}\r\n" +
               $"{string.Join(",", columnHeaders)}\r\n " +
               $"{sb} ");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
              $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
              $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
              $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo)}\r\n" +
              $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo)}\r\n" +
              $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo)}\r\n" +
              $"{string.Join(",", "," + from + "," + to)}\r\n" +
              $"{string.Join(",", columnHeaders)}\r\n " +
              $"{sb} ");
            }





            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"StockInventoryReport.csv");
        }




        [HttpPost("SalePurchaseComparisionReport")]
        [Route("api/Report/SalePurchaseComparisionReport")]
        [Roles("CanViewSaleInvoiceReport")]
        public async Task<IActionResult> SalePurchaseComparisionReport([FromBody] List<MonthlyComparisionReportModel> inventoryList, string language, string month, int year, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var reportName = language == "en" ? "Monthly Sales Purchase Comparision Report " : "تقرير مقارنة شراء المبيعات الشهرية";




            List<string> columnHeaders = new List<string>();
            if (language == "en")
            {
                columnHeaders.Add("#");
                columnHeaders.Add("Date");

                columnHeaders.Add("Total Purchases");
                columnHeaders.Add("Discount");
                columnHeaders.Add("Purchase Return");
                columnHeaders.Add("Total");
                columnHeaders.Add("Tax");
                columnHeaders.Add("Net Sale");
                columnHeaders.Add("Cash");
                columnHeaders.Add("Bank");
                columnHeaders.Add("Credit");
                columnHeaders.Add("  ");
                columnHeaders.Add("Date");

                columnHeaders.Add("Total Sales");
                columnHeaders.Add("Discount");
                columnHeaders.Add("Sale Return");
                columnHeaders.Add("Total");
                columnHeaders.Add("Tax");
                columnHeaders.Add("Net Sale");
                columnHeaders.Add("Cash");
                columnHeaders.Add("Bank");
                columnHeaders.Add("Credit");

            }
            else
            {
                columnHeaders.Add("#");
                columnHeaders.Add("	تاريخ");

                columnHeaders.Add("إجمالي المشتريات");
                columnHeaders.Add("خصم");
                columnHeaders.Add("عودة شراء");
                columnHeaders.Add("المجموع");
                columnHeaders.Add("ضريبة");
                columnHeaders.Add("صافي البيع");
                columnHeaders.Add("نقدي");
                columnHeaders.Add("بنك");
                columnHeaders.Add("الإئتمان");
                columnHeaders.Add("  ");
                columnHeaders.Add("	تاريخ");

                columnHeaders.Add("إجمالي المبيعات");
                columnHeaders.Add("خصم");
                columnHeaders.Add("عودة البيع");
                columnHeaders.Add("المجموع");
                columnHeaders.Add("ضريبة");
                columnHeaders.Add("صافي البيع");
                columnHeaders.Add("نقدي");
                columnHeaders.Add("بنك");
                columnHeaders.Add("الإئتمان");
            }



            var i = 1;
            var j = 1;
            var sb = new StringBuilder();

            foreach (var stock in inventoryList)
            {




                string value = i++ + "," + (stock.PurchaseDate.ToString("d") + "," + Math.Abs(stock.GrossPurchaseAmount) + "," + Math.Abs(stock.PurchaseDiscount) + "," + Math.Abs(stock.PurchaseTotalReturns) + "," + Math.Abs(stock.GrossPurchaseAmount - stock.PurchaseTotalReturns - stock.PurchaseDiscount) + "," + Math.Abs(stock.PurchaseTotalTax) + "," + Math.Abs((stock.GrossPurchaseAmount - stock.PurchaseTotalReturns - stock.PurchaseDiscount) - stock.PurchaseTotalTax) + "," + Math.Abs(stock.PurchaseCash) + "," + Math.Abs(stock.PurchaseBank) + "," + Math.Abs(stock.PurchaseCredit) + "," + "," + (stock.Date.ToString("d")) + "," + "," + Math.Abs(stock.GrossAmount) + "," + Math.Abs(stock.Discount) + "," + Math.Abs(stock.TotalReturns) + "," + Math.Abs(stock.GrossAmount - stock.TotalReturns - stock.Discount) + "," + Math.Abs(stock.TotalTax) + "," + Math.Abs((stock.GrossAmount - stock.TotalReturns - stock.Discount) + stock.TotalTax) + Math.Abs(stock.Cash) + "," + Math.Abs(stock.Bank) + "," + Math.Abs(stock.Credit));
                sb.AppendLine(value);
            }


            var cashAmount = inventoryList.Sum(x => x.Cash);
            var bankAmount = inventoryList.Sum(x => x.Bank);
            var creditAmount = inventoryList.Sum(x => x.Credit);
            var discount = inventoryList.Sum(x => x.Discount);
            var totalReturns = inventoryList.Sum(x => x.TotalReturns);
            var totalTax = inventoryList.Sum(x => x.TotalTax);
            var totalAmount = inventoryList.Sum(c => c.GrossAmount);
            var total = inventoryList.Sum(c => c.GrossAmount - c.TotalReturns - c.Discount);
            var NetAmount = inventoryList.Sum(c => (c.GrossAmount - c.TotalReturns - c.Discount) + c.TotalTax);
            var purchasecashAmount = inventoryList.Sum(x => x.PurchaseCash);
            var purchasebankAmount = inventoryList.Sum(x => x.PurchaseBank);
            var purchasecreditAmount = inventoryList.Sum(x => x.PurchaseCredit);
            var purchasediscount = inventoryList.Sum(x => x.PurchaseDiscount);
            var purchasetotalReturns = inventoryList.Sum(x => x.PurchaseTotalReturns);
            var purchasetotalTax = inventoryList.Sum(x => x.PurchaseTotalTax);
            var purchasetotalAmount = inventoryList.Sum(c => c.GrossPurchaseAmount);
            var purchasetotal = inventoryList.Sum(c => c.GrossPurchaseAmount - c.PurchaseTotalReturns - c.PurchaseDiscount);
            var purchaseNetAmount = inventoryList.Sum(c => (c.GrossPurchaseAmount - c.PurchaseTotalReturns - c.PurchaseDiscount) + c.PurchaseTotalTax);

            byte[] buffer;
            if (language == "en")
            {

                buffer = Encoding.UTF8.GetBytes(
            $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
            $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
            $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
            $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + ",")}\r\n" +
            $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
            $"{string.Join(",", "," + "," + "," + "," + "," + "," + "," + "," + "," + month + " " + year + " " + "Monthly Sales Purchase Comparision Report")}\r\n" +
             $"{string.Join(",", "," + "Purchase Record" + "," + "," + "," + "," + "," + "," + "," + "," + "," + "," + "," + "Sale Record")}\r\n" +

            $"{string.Join(",", columnHeaders)}\r\n " +
            $"{sb}\r\n " +
            $"{string.Join(",", "," + "Total" + "," + "," + Math.Round(purchasediscount, 2) + "," + Math.Round(purchasetotalReturns, 2) + "," + Math.Round(purchasetotalTax, 2) + "," + Math.Round(purchasetotalAmount, 2) + "," + Math.Round(purchasetotal, 2) + "," + Math.Round(purchaseNetAmount, 2) + Math.Round(purchasecashAmount, 2) + "," + Math.Round(purchasebankAmount, 2) + "," + Math.Round(purchasecreditAmount, 2) + "," + "," + "," + Math.Round(discount, 2) + "," + Math.Round(totalReturns, 2) + "," + Math.Round(totalTax, 2) + "," + Math.Round(totalAmount, 2) + "," + Math.Round(total, 2) + "," + Math.Round(NetAmount, 2) + "," + Math.Round(cashAmount, 2) + "," + Math.Round(bankAmount, 2) + "," + Math.Round(creditAmount, 2))}");
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(
              $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
              $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
              $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
              $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
              $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
              $"{string.Join(",", "," + "," + "," + "," + "," + "," + "," + "," + "," + month + " " + year + " " + "Monthly Sales Purchase Comparision Report")}\r\n" +
               $"{string.Join(",", "," + "Purchase Record" + "," + "," + "," + "," + "," + "," + "," + "," + "," + "," + "," + "Sale Record")}\r\n" +

              $"{string.Join(",", columnHeaders)}\r\n " +
              $"{sb}\r\n " +
              $"{string.Join(",", "," + "Total" + "," + "," + Math.Round(purchasediscount, 2) + "," + Math.Round(purchasetotalReturns, 2) + "," + Math.Round(purchasetotalTax, 2) + "," + Math.Round(purchasetotalAmount, 2) + "," + Math.Round(purchasetotal, 2) + "," + Math.Round(purchaseNetAmount, 2) + Math.Round(purchasecashAmount, 2) + "," + Math.Round(purchasebankAmount, 2) + "," + Math.Round(purchasecreditAmount, 2) + "," + "," + "," + Math.Round(discount, 2) + "," + Math.Round(totalReturns, 2) + "," + Math.Round(totalTax, 2) + "," + Math.Round(totalAmount, 2) + "," + Math.Round(total, 2) + "," + Math.Round(NetAmount, 2) + "," + Math.Round(cashAmount, 2) + "," + Math.Round(bankAmount, 2) + "," + Math.Round(creditAmount, 2))}");

            }






            var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

            return File(data, "text/csv", $"ComparisionReport.csv");
        }




        [HttpPost("CustomerBalanceCsv")]
        [Route("api/Report/CustomerBalanceCsv")]
        [Roles("CanViewCustomerBalance", "CanViewSupplierBalance")]
        public async Task<IActionResult> CustomerBalanceCsv([FromBody] CustomerBalanceReportVm ledger, string language, DateTime? fromDate, DateTime? toDate, Guid companyId, string formName)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            if (formName == "Customer")
            {
                var reportName = language == "en" ? "Customer Balance Report: " : "تقرير رصيد العميل";


                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }

                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Document Code");
                    columnHeaders.Add("Date");
                    columnHeaders.Add("Transaction Type");
                    columnHeaders.Add("Description");
                    columnHeaders.Add("Debit");
                    columnHeaders.Add("Credit");
                    columnHeaders.Add("Balance");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("	رمز الوثيقة");
                    columnHeaders.Add("تاريخ");
                    columnHeaders.Add("نوع المعاملة");
                    columnHeaders.Add("وصف");
                    columnHeaders.Add("مدين");
                    columnHeaders.Add("الإئتمان");
                    columnHeaders.Add("الرصيد");
                }



                var i = 1;
                var j = 1;
                var sb = new StringBuilder();

                foreach (var stock in ledger.ContactList)
                {




                    string value = i++ + "," + stock.DocumentNumber + "," + stock.Date + "," + stock.TransactionType + "," + stock.Description + "," + Math.Abs(stock.DebitAmount) + "," + Math.Abs(stock.CreditAmount) + "," + Math.Abs(stock.OpeningBalance);
                    sb.AppendLine(value);
                }

                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                 $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                 $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                 $"{string.Join(",", "," + from + "," + to)}\r\n" +
                 $"{string.Join(",", "," + "Opening Balance" + "," + (ledger.OpeningBalance > 0 ? "Dr" + Math.Abs(ledger.OpeningBalance).ToString("0.00") : "Cr" + Math.Abs(ledger.OpeningBalance).ToString("0.00")))}\r\n" +
                 $"{string.Join(",", columnHeaders)}\r\n " +
                 $"{sb}\r\n " +
                 $"{string.Join(",", "," + "," + "," + "Closing Balance" + "," + Math.Round(ledger.TotalBalance, 2) + "," + Math.Round(ledger.TotalDebit, 2) + "," + Math.Round(ledger.TotalCredit, 2) + "," + (ledger.RunningBalance > 0 ? "Dr" + "   " + Math.Abs(ledger.RunningBalance) : "Cr" + "   " + Math.Abs(ledger.RunningBalance)))}");
                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                  $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                  $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                  $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                  $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                  $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                  $"{string.Join(",", "," + from + "," + to)}\r\n" +
                  $"{string.Join(",", "," + "Opening Balance" + "," + (ledger.OpeningBalance > 0 ? "Dr" + Math.Abs(ledger.OpeningBalance).ToString("0.00") : "Cr" + Math.Abs(ledger.OpeningBalance).ToString("0.00")))}\r\n" +
                  $"{string.Join(",", columnHeaders)}\r\n " +
                  $"{sb}\r\n " +
                  $"{string.Join(",", "," + "," + "," + "Closing Balance" + "," + Math.Round(ledger.TotalBalance, 2) + "," + Math.Round(ledger.TotalDebit, 2) + "," + Math.Round(ledger.TotalCredit, 2) + "," + (ledger.RunningBalance > 0 ? "Dr" + "   " + Math.Abs(ledger.RunningBalance) : "Cr" + "   " + Math.Abs(ledger.RunningBalance)))}");
                }






                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"CustomerBalanceReport.csv");
            }
            else
            {
                var reportName = language == "en" ? "Supplier Balance Report: " : "تقرير رصيد المورد ";

                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }

                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Document Code");
                    columnHeaders.Add("Date");
                    columnHeaders.Add("Transaction Type");
                    columnHeaders.Add("Description");
                    columnHeaders.Add("Debit");
                    columnHeaders.Add("Credit");
                    columnHeaders.Add("Balance");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("	رمز الوثيقة");
                    columnHeaders.Add("تاريخ");
                    columnHeaders.Add("نوع المعاملة");
                    columnHeaders.Add("وصف");
                    columnHeaders.Add("مدين");
                    columnHeaders.Add("الإئتمان");
                    columnHeaders.Add("الرصيد");
                }


                var i = 1;
                var j = 1;
                var sb = new StringBuilder();

                foreach (var stock in ledger.ContactList)
                {




                    string value = i++ + "," + stock.DocumentNumber + "," + stock.Date + "," + stock.TransactionType + "," + stock.Description + "," + Math.Abs(stock.DebitAmount) + "," + Math.Abs(stock.CreditAmount) + "," + Math.Abs(stock.OpeningBalance);
                    sb.AppendLine(value);
                }


                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                    $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                    $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", "," + from + "," + to)}\r\n" +
                    $"{string.Join(",", "," + "Opening Balance" + "," + (ledger.OpeningBalance > 0 ? "Dr" + Math.Abs(ledger.OpeningBalance).ToString("0.00") : "Cr" + Math.Abs(ledger.OpeningBalance).ToString("0.00")))}\r\n" +
                    $"{string.Join(",", columnHeaders)}\r\n " +
                    $"{sb}\r\n " +
                    $"{string.Join(",", "," + "," + "," + "Closing Balance" + "," + Math.Round(ledger.TotalBalance, 2) + "," + Math.Round(ledger.TotalDebit, 2) + "," + Math.Round(ledger.TotalCredit, 2) + "," + (ledger.RunningBalance > 0 ? "Dr" + "   " + Math.Abs(ledger.RunningBalance) : "Cr" + "   " + Math.Abs(ledger.RunningBalance)))}");


                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                    $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                    $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                    $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                    $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                    $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                    $"{string.Join(",", "," + from + "," + to)}\r\n" +
                    $"{string.Join(",", "," + "Opening Balance" + "," + (ledger.OpeningBalance > 0 ? "Dr" + Math.Abs(ledger.OpeningBalance).ToString("0.00") : "Cr" + Math.Abs(ledger.OpeningBalance).ToString("0.00")))}\r\n" +
                    $"{string.Join(",", columnHeaders)}\r\n " +
                    $"{sb}\r\n " +
                    $"{string.Join(",", "," + "," + "," + "Closing Balance" + "," + Math.Round(ledger.TotalBalance, 2) + "," + Math.Round(ledger.TotalDebit, 2) + "," + Math.Round(ledger.TotalCredit, 2) + "," + (ledger.RunningBalance > 0 ? "Dr" + "   " + Math.Abs(ledger.RunningBalance) : "Cr" + "   " + Math.Abs(ledger.RunningBalance)))}");


                }





                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"SupplierBalanceReport.csv");

            }

        }





        [HttpPost("CustomerLadgerCsv")]
        [Route("api/Report/CustomerLadgerCsv")]
        [Roles("CanViewCustomerLedger", "CanViewSupplieLedger")]
        public async Task<IActionResult> CustomerLadgerCsv([FromBody] List<CustomerLedgerModel> contactList, string language, DateTime? fromDate, DateTime? toDate, Guid companyId, string formName)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            if (formName == "Customer")
            {
                var reportName = language == "en" ? "Customer Ledger Report " : "تقرير دفتر الأستاذ العميل";

                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }

                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Code");
                    columnHeaders.Add("Name");
                    columnHeaders.Add("Account Code");
                    columnHeaders.Add("Vat No.");
                    columnHeaders.Add("Amount");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("شفرة");
                    columnHeaders.Add("اسم");
                    columnHeaders.Add("رمز الحساب");
                    columnHeaders.Add("ضريبة القيمة المضافة لا");
                    columnHeaders.Add("مقدار");

                }
                var totalDebit = contactList.Sum(x => x.Amount > 0 ? x.Amount : 0);
                var totalCredit = contactList.Sum(x => x.Amount <= 0 ? x.Amount * -1 : 0);

                var i = 1;
                var j = 1;
                var sb = new StringBuilder();

                foreach (var stock in contactList)
                {




                    string value = i++ + "," + stock.ContactCode + "," + (language == "en") + "," + stock.AccountCode + "," + stock.VatNo + "," + (stock.Amount > 0 ? "Dr" + (Math.Abs(stock.Amount)).ToString("0.00") : "Cr" + Math.Abs(stock.Amount).ToString("0.00"));
                    sb.AppendLine(value);
                }

                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{string.Join(",", columnHeaders)}\r\n " +
                   $"{sb}\r\n " +
                   $"{string.Join(",", "," + "Total Debit" + "," + Math.Round(totalDebit, 2) + "," + "Total Credit" + Math.Round(totalCredit, 2) + "," + "Total " + Math.Round(totalDebit - totalCredit, 2))}");

                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{string.Join(",", columnHeaders)}\r\n " +
                   $"{sb}\r\n " +
                   $"{string.Join(",", "," + "Total Debit" + "," + Math.Round(totalDebit, 2) + "," + "Total Credit" + Math.Round(totalCredit, 2) + "," + "Total " + Math.Round(totalDebit - totalCredit, 2))}");

                }





                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"CustomerLedgerReport.csv");
            }
            else
            {
                var reportName = language == "en" ? "Supplier Ledger Report " : "تقرير دفتر الأستاذ المورد";

                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }

                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Code");
                    columnHeaders.Add("Name");
                    columnHeaders.Add("Account Code");
                    columnHeaders.Add("Vat No.");
                    columnHeaders.Add("Amount");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("شفرة");
                    columnHeaders.Add("اسم");
                    columnHeaders.Add("رمز الحساب");
                    columnHeaders.Add("ضريبة القيمة المضافة لا");
                    columnHeaders.Add("مقدار");

                }


                var totalDebit = contactList.Sum(x => x.Amount > 0 ? x.Amount : 0);
                var totalCredit = contactList.Sum(x => x.Amount <= 0 ? x.Amount : 0);
                var i = 1;
                var j = 1;
                var sb = new StringBuilder();

                foreach (var stock in contactList)
                {




                    string value = i++ + "," + stock.ContactCode + "," + (language == "en") + "," + stock.AccountCode + "," + stock.VatNo + "," + (stock.Amount > 0 ? "Dr" + (Math.Abs(stock.Amount)).ToString("0.00") : "Cr" + Math.Abs(stock.Amount).ToString("0.00"));
                    sb.AppendLine(value);
                }

                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{string.Join(",", columnHeaders)}\r\n " +
                   $"{sb}\r\n " +
                   $"{string.Join(",", "," + "Total Debit" + "," + Math.Round(totalDebit, 2) + "," + "Total Credit" + Math.Round(totalCredit, 2) + "," + "Total " + Math.Round(totalCredit + totalDebit, 2))}");

                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{string.Join(",", columnHeaders)}\r\n " +
                   $"{sb}\r\n " +
                   $"{string.Join(",", "," + "Total Debit" + "," + Math.Round(totalDebit, 2) + "," + "Total Credit" + Math.Round(totalCredit, 2) + "," + "Total " + Math.Round(totalCredit + totalDebit, 2))}");

                }






                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"SupplierLedgerReport.csv");

            }

        }





        [HttpPost("AccountLadgerCsv")]
        [Route("api/Report/AccountLadgerCsv")]
        [Roles("CanViewAccountLedger")]
        public async Task<IActionResult> AccountLadgerCsv([FromBody] AccountLedgerReportLookUp ledger, string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            {
                var reportName = language == "en" ? "Account Ledger Report " : "تقرير دفتر الأستاذ";
                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }

                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Date");
                    columnHeaders.Add("Document Code");
                    columnHeaders.Add("Narration");
                    columnHeaders.Add("Debit");
                    columnHeaders.Add("Credit");
                    columnHeaders.Add("Balance");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("رمز الوثيقة");
                    columnHeaders.Add("السرد");
                    columnHeaders.Add("مدين");
                    columnHeaders.Add("الإئتمان");
                    columnHeaders.Add("الرصيد");

                }

                string TransactionType(string transactionType)
                {
                    if (transactionType == "StockOut") return "Stock Out";
                    else if (transactionType == "JournalVoucher") return "Journal Voucher";
                    else if (transactionType == "BankPay") return "Bank Pay";
                    else if (transactionType == "ExpenseVoucher") return "Expense Voucher";
                    else if (transactionType == "Expense") return "Expense";
                    else if (transactionType == "CashPay") return "Cash Pay";
                    else if (transactionType == "BankReceipt") return "Bank Receipt";
                    else if (transactionType == "StockIn") return "Stock In";
                    else if (transactionType == "SaleInvoice") return "Sale Invoice";
                    else if (transactionType == "PurchaseReturn") return "PurchaseReturn";
                    else if (transactionType == "PurchasePost") return "Purchase";
                    else if (transactionType == "CashReceipt") return "Cash Receipt";
                    else
                    {
                        return transactionType;
                    }
                }


                var i = 1;
                var j = 1;
                var sb = new StringBuilder();

                foreach (var stock in ledger.TransactionList)
                {




                    string value = i++ + "," + stock.Date + "," + stock.DocumentNumber + "," + TransactionType(stock.Description) + "," + Math.Abs(stock.DebitAmount) + "," + Math.Abs(stock.CreditAmount) + "," + (stock.OpeningBalance > 0 ? "Dr" + (Math.Abs(stock.OpeningBalance)).ToString("0.00") : "Cr" + Math.Abs(stock.OpeningBalance).ToString("0.00"));
                    sb.AppendLine(value);
                }

                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                     $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                     $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "," + from + "," + to)}\r\n" +
                     $"{string.Join(",", "," + "Opening Balance" + "," + (ledger.OpeningBalance > 0 ? "Dr" + Math.Abs(ledger.OpeningBalance).ToString("0.00") : "Cr" + Math.Abs(ledger.OpeningBalance).ToString("0.00")))}\r\n" +
                     $"{string.Join(",", columnHeaders)}\r\n " +
                     $"{sb}\r\n " +
                     $"{string.Join(",", ",", "," + "Closing Balance" + "," + Math.Round(ledger.TotalDebit, 2) + "," + Math.Round(ledger.TotalCredit, 2) + "," + (ledger.RunningBalance > 0 ? "Dr" + "   " + Math.Abs(ledger.RunningBalance) : "Cr" + "   " + Math.Abs(ledger.RunningBalance)))}");

                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{string.Join(",", "," + "Opening Balance" + "," + (ledger.OpeningBalance > 0 ? "Dr" + Math.Abs(ledger.OpeningBalance).ToString("0.00") : "Cr" + Math.Abs(ledger.OpeningBalance).ToString("0.00")))}\r\n" +
                   $"{string.Join(",", columnHeaders)}\r\n " +
                   $"{sb}\r\n " +
                   $"{string.Join(",", ",", "," + "Closing Balance" + "," + Math.Round(ledger.TotalDebit, 2) + "," + Math.Round(ledger.TotalCredit, 2) + "," + (ledger.RunningBalance > 0 ? "Dr" + "   " + Math.Abs(ledger.RunningBalance) : "Cr" + "   " + Math.Abs(ledger.RunningBalance)))}");

                }





                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"AccountLedgerReport.csv");

            }

        }




        [HttpPost("TrialBalanceAccountCsv")]
        [Route("api/Report/TrialBalanceAccountCsv")]
        [Roles("CanViewAccountLedger")]
        public async Task<IActionResult> TrialBalanceAccountCsv([FromBody] List<TrialBalanceAccountReportLookup> transactionDetail, string language, DateTime? fromDate, DateTime? toDate, Guid companyId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            {
                var reportName = language == "en" ? "Trial Balance Report " : "تقرير ميزان المراجعة";


                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }


                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Code");
                    columnHeaders.Add("Name");
                    columnHeaders.Add("Cost Center");
                    columnHeaders.Add("Account Type");
                    columnHeaders.Add("Debit");
                    columnHeaders.Add("Credit");
                    columnHeaders.Add("Balance");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("شفرة");
                    columnHeaders.Add("اسم");
                    columnHeaders.Add("مركز تقييم الكلفة");
                    columnHeaders.Add("نوع الحساب");
                    columnHeaders.Add("مدين");
                    columnHeaders.Add("الإئتمان");
                    columnHeaders.Add("الرصيد");
                }



                var i = 1;
                var j = 1;
                var sb = new StringBuilder();

                foreach (var stock in transactionDetail)
                {




                    string value = i++ + "," + stock.AccountCode + "," + stock.AccountName + "," + stock.CostCenter + "," + stock.AccountType + "," + Math.Abs(stock.Debit) + "," + Math.Abs(stock.Credit) + "," + (stock.Total > 0 ? "Dr " + (Math.Abs(stock.Total)).ToString("0.00") : "Cr " + Math.Abs(stock.Total).ToString("0.00"));
                    sb.AppendLine(value);
                }

                var totalDebit = transactionDetail.Sum(x => x.Debit);
                var totalCredit = transactionDetail.Sum(x => x.Credit);
                var total = transactionDetail.Sum(x => x.Debit - x.Credit);



                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                    $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                    $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                    $"{string.Join(",", "," + from + "," + to)}\r\n" +
                    $"{string.Join(",", columnHeaders)}\r\n " +
                    $"{sb}\r\n " +
                    $"{string.Join(",", ",", ",", "," + Math.Round(totalDebit, 2) + "," + Math.Round(totalCredit, 2) + "," + (total > 0 ? "Dr " + (Math.Abs(total)).ToString("0.00") : "Cr " + Math.Abs(total).ToString("0.00")))}");

                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{string.Join(",", columnHeaders)}\r\n " +
                   $"{sb}\r\n " +
                   $"{string.Join(",", ",", ",", "," + Math.Round(totalDebit, 2) + "," + Math.Round(totalCredit, 2) + "," + (total > 0 ? "Dr " + (Math.Abs(total)).ToString("0.00") : "Cr " + Math.Abs(total).ToString("0.00")))}");


                }
                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"TrailBalanceReport.csv");

            }

        }



        [HttpPost("TrialBalanceCsv")]
        [Route("api/Report/TrialBalanceCsv")]
        [Roles("CanViewTrialBalance")]
        public async Task<IActionResult> TrialBalanceCsv([FromBody] List<TrialBalanceModel> accounts, string language, DateTime? fromDate, DateTime? toDate, Guid companyId, string formName)
        {
            var company = _companyComponent.GetCompanyById(companyId);


            {
                var reportName = language == "en" ? "Trial Balance Report " : "تقرير ميزان المراجعة";


                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }

                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Code");
                    columnHeaders.Add("Name");
                    columnHeaders.Add("Debit");
                    columnHeaders.Add("Credit");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("شفرة");
                    columnHeaders.Add("اسم");
                    columnHeaders.Add("مدين");
                    columnHeaders.Add("الإئتمان");

                }

                var totalDebit = accounts.Sum(x => x.Debit < 0 ? x.Debit * -1 : x.Debit);
                var totalCredit = accounts.Sum(x => x.Credit < 0 ? x.Credit * -1 : x.Credit);

                var i = 1;
                var j = 1;
                var sb = new StringBuilder();

                foreach (var stock in accounts)
                {




                    string value = i++ + "," + stock.Code + "," + (language == "en" ? stock.Name : stock.NameArabic) + "," + Math.Abs(stock.Debit) + "," + Math.Abs(stock.Credit);
                    sb.AppendLine(value);
                }


                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{string.Join(",", columnHeaders)}\r\n " +
                   $"{sb}\r\n " +
                   $"{string.Join(",", ",", "," + "Total Debit" + Math.Round(totalDebit, 2) + "," + "Total Credit" + Math.Round(totalCredit, 2))}");


                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                    $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                    $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                    $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                    $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                    $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                    $"{string.Join(",", "," + from + "," + to)}\r\n" +
                    $"{string.Join(",", columnHeaders)}\r\n " +
                    $"{sb}\r\n " +
                    $"{string.Join(",", ",", "," + "Total Debit" + Math.Round(totalDebit, 2) + "," + "Total Credit" + Math.Round(totalCredit, 2))}");


                }



                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"TrialBalanceReport.csv");
            }

        }
        [HttpGet("ProductInventoryCsv")]
        [Route("api/Report/ProductInventory")]
        [Roles("CanViewProductInventoryReport")]
        public async Task<IActionResult> ProductInventoryCsv(string language, DateTime? fromDate, DateTime? toDate, Guid companyId, Guid wareHouseId)
        {
            var company = _companyComponent.GetCompanyById(companyId);

            var resultQuery = await Mediator.Send(new ProductInventoryReport
            {
                WareHouseId = wareHouseId,
                FromDate = fromDate,
                ToDate = toDate,
            });




            {
                var reportName = language == "en" ? "Product Inventory Report " : "تقرير جرد المنتج";


                var from = "";
                var to = "";
                if (language == "en")
                {
                    from = "From Date : " + fromDate?.ToString("d");
                    to = "To Date : " + toDate?.ToString("d");
                }
                else if (language == "ar")
                {
                    from = "From Date : " + fromDate?.ToString("d") + " من التاريخ";
                    to = "To Date : " + toDate?.ToString("d") + " حتى تاريخه";
                }

                List<string> columnHeaders = new List<string>();
                if (language == "en")
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("Date");
                    columnHeaders.Add("Product");
                    columnHeaders.Add("Transaction Type");
                    columnHeaders.Add("Price");
                    columnHeaders.Add("Average Price");
                    columnHeaders.Add("Quantity");
                    columnHeaders.Add("Current Quantity");
                    columnHeaders.Add(" Sale Price");
                }
                else
                {
                    columnHeaders.Add("#");
                    columnHeaders.Add("تاريخ");
                    columnHeaders.Add("المنتج");
                    columnHeaders.Add("نوع المعاملة");
                    columnHeaders.Add("السعر");
                    columnHeaders.Add("الكمية الحالية");
                    columnHeaders.Add("سعر البيع");

                }



                var i = 1;
                var j = 1;
                var sb = new StringBuilder();





                foreach (var stock in resultQuery)
                {


                    string value = "";
                    if (language == "en")
                    {
                        value = "Product Name:" + "," + stock.ProductName;

                    }
                    else
                    {
                        value = "Product Name:" + "," + (stock.ProductName + stock.ProductNameAr);

                    }


                    sb.AppendLine(value);
                    string header = "#" + "," + "Date" + "," + "Product" + "," + "Transaction Type" + "," + "Price" + "," +
                                    "Average Price" + "," + "Quantity" + "," + "Current Quantity" + "," + "Sale Price" + "\n";
                    sb.Append(header);
                    var n = 0;
                    foreach (var inventory in stock.ProductInventoryReport)
                    {
                        string inventoryValues = n++ + "," + inventory.Date + "," + inventory.ProductName + "," + inventory.TransactionType + "," + inventory.Price + "," + inventory.AveragePrice + "," + (inventory.TransactionType == "SaleReturn" ? "+" + inventory.Quantity : inventory.TransactionType == "StockIn" ? "+" + inventory.Quantity : inventory.TransactionType == "StockOut" ? "-" + inventory.Quantity : inventory.TransactionType == "StockIn" ? "+" + inventory.Quantity : inventory.TransactionType == "SaleInvoice" ? "-" + inventory.Quantity : inventory.TransactionType == "PurchaseReturn" ? "-" + inventory.Quantity : inventory.TransactionType == "PurchasePost" ? "+" + inventory.Quantity : "-" + inventory.Quantity) + "," + inventory.CurrentQuantity + "," + inventory.SalePrice;
                        sb.AppendLine(inventoryValues);
                    }
                }

                byte[] buffer;
                if (language == "en")
                {

                    buffer = Encoding.UTF8.GetBytes(
                     $"{string.Join(",", company.NameEnglish + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", company.CategoryEnglish + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + ",")}\r\n" +
                     $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + ",")}\r\n" +
                     $"{string.Join(",", "," + from + "," + to)}\r\n" +
                     $"{sb} ");
                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(
                   $"{string.Join(",", company.NameEnglish + "," + "," + "," + company.NameArabic)}\r\n" +
                   $"{string.Join(",", company.CategoryEnglish + "," + "," + "," + company.CategoryArabic)}\r\n" +
                   $"{string.Join(",", "VAT No.: " + company.VatRegistrationNo + "," + "," + reportName + "," + company.VatRegistrationNo + "رقم ضريبة القيمة المضافة")}\r\n" +
                   $"{string.Join(",", "Cr No.: " + company.CompanyRegNo + "," + "," + "," + company.CompanyRegNo + "رقم السجل التجاري")}\r\n" +
                   $"{string.Join(",", "Tel.: " + company.PhoneNo + "," + "," + "," + company.PhoneNo + "هاتف")}\r\n" +
                   $"{string.Join(",", "," + from + "," + to)}\r\n" +
                   $"{sb} ");
                }






                var data = Encoding.UTF8.GetPreamble().Concat(buffer).ToArray();

                return File(data, "text/csv", $"ProductInventoryReport.csv");
            }

        }


        [HttpGet("GetTransactionList")]
        [Roles("CanViewDayWiseTransactions")]
        public async Task<IActionResult> GetTransactionList(DateTime date)
        {

            var list = await Mediator.Send(new GetDateWiseTransactionList()
            {
                Date = date
            });
            return Ok(list);
        }
        [Route("api/Report/GetInvoiceSerialReport")]
        [HttpGet("GetInvoiceSerialReport")]
        public async Task<IActionResult> GetInvoiceSerialReport(DateTime fromDate, DateTime toDate, string serialNumber, string branchId)
        {

            if (!string.IsNullOrEmpty(serialNumber))
            {
                var list = await Mediator.Send(new GetInvoiceSerialReportQuery()
                {
                    BranchId = branchId,
                    FromDate = fromDate,
                    ToDate = toDate,
                    SerialNumber = serialNumber,

                });
                return Ok(list);
            }
            return Ok();
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("PurchaseInvoice")]
        [Roles("CanViewPurchaseInvoiceReport")]
        public async Task<IActionResult> PurchaseInvoice(DateTime FromDate, DateTime ToDate, string branchId, string customerId)
        {
            var list = await Mediator.Send(new GetPurchaseInvoiceQuery()
            {
                BranchId = branchId,
                FromDate = FromDate,
                ToDate = ToDate,
                CustomerId = customerId
            });
            return Ok(list);
        }


        [Route("api/Report/GetBankTransactionQuery")]
        [HttpGet("GetBankTransactionQuery")]
        [Roles("CanViewBanTransaction")]
        public async Task<IActionResult> GetBankTransactionQuery(DateTime fromDate, DateTime toDate, Guid accountId)
        {
            var region = await Mediator.Send(new GetBankTransactionQuery
            {
                AccountId = accountId,
                FromDate = fromDate,
                ToDate = toDate,

            });
            return Ok(region);
        }
        [Route("api/Report/ProductInventoryReport")]
        [HttpGet("ProductInventoryReport")]
        [Roles("CanViewProductInventoryReport")]
        public async Task<IActionResult> ProductInventoryReport(DateTime fromDate, DateTime toDate, Guid wareHouseId, string branchId)
        {
            var region = await Mediator.Send(new ProductInventoryReport
            {
                BranchId = branchId,
                WareHouseId = wareHouseId,
                FromDate = fromDate,
                ToDate = toDate,

            });
            return Ok(region);
        }

        [Route("api/Report/TemporaryCashAllocationList")]
        [HttpGet("TemporaryCashAllocationList")]
        [Roles("CanPrintTCAllocationReport")]
        public async Task<IActionResult> TemporaryCashAllocationList(int month, int year)
        {
            var paymentVoucher = await Mediator.Send(new TemporaryCashAllocationReportQuery
            {
                Month = month,
                Year = year,
            });
            return Ok(paymentVoucher);
        }

        [Route("api/Report/TemporaryCashIssueList")]
        [HttpGet("TemporaryCashIssueList")]
        [Roles("CanViewTCAllocationReport")]
        public async Task<IActionResult> TemporaryCashIssueList(int month, int year, Guid employeeId)
        {
            var paymentVoucher = await Mediator.Send(new TemporaryCashIssueReportQuery
            {
                Month = month,
                Year = year,
                EmployeeId = employeeId,
            });
            return Ok(paymentVoucher);
        }
        public static class Tempsave
        {
            public static string report { get; set; }
        }
        [Route("api/Report/DownloadReport")]
        [HttpGet("DownloadReport")]
        public async Task<ActionResult> DownloadReport(string Report)
        {
            Tempsave.report = Report;
            return File(Report, "application/pdf");
        }


        [Route("api/Report/SendEmailPdf")]
        [HttpPost("SendEmailPdf")]
        public async Task<IActionResult> SendEmailPdf([FromBody] EmailCompose emailCompose)
        {
            var viewPdf = new ViewAsPdf(emailCompose)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 2, Bottom = 3, Right = 2, Top = 3 }
            };
            emailCompose.PdfBytes = await viewPdf.BuildFile(ControllerContext);
            var sale = _SendEmail.SendInvoiceEmailPdfAsync(emailCompose);
            return Ok(sale);
        }

    }
}
