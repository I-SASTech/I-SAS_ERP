using DevExpress.Data;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace Noble.Report.Reports.ReportManagementModule.SalePurchase
{
    public partial class SalePurchaseReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var companyId = Request.QueryString["CompanyId"];
                var branchId = Request.QueryString["BranchId"];


                var myData = GetToken.TokenValue(Guid.Parse(companyId));
                Session["CompanyId"] = myData.Where(x => x.TokenName == "ServerAddress").Select(x => x.CompanyId).FirstOrDefault();
                Session["ServerAddress"] = myData.Where(x => x.Token == "ServerAddress").Select(x => x.TokenName).FirstOrDefault();
                Session["Sales"] = myData.Where(x => x.TokenName == "Sales").Select(x => x.Token).FirstOrDefault();
                Session["Purchase"] = myData.Where(x => x.TokenName == "Purchase").Select(x => x.Token).FirstOrDefault();
                Session["Reporting"] = myData.Where(x => x.TokenName == "Reporting").Select(x => x.Token).FirstOrDefault();


                var formName = Request.QueryString["formName"];
                string Print = Request.QueryString["Print"] == null ? "" : Request.QueryString["Print"];
                var serverAddress = Session["ServerAddress"].ToString() == null ? null : Session["ServerAddress"].ToString();
                var printSetting = GetPrintSetting.PrintDetails(branchId,Session["Sales"].ToString(), serverAddress);
                string customerId = Request.QueryString["customerId"] == null ? "" : Request.QueryString["customerId"];
                var companyInfo = GetCompanyInfo.GetCompanyInfodetials(companyId, Session["Sales"].ToString(), serverAddress);

                if (formName == "Sale" || formName == "Purchase")
                {
                    // Company / GetContactOpeningBalanceList
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string Invoice = Request.QueryString["Invoice"] == null ? "" : Request.QueryString["Invoice"];
                    var saleDtl = IncoiceReport.IncoiceReportDtl(Invoice, fromDate, toDate, formName, Session["Reporting"].ToString(), serverAddress, branchId, customerId);

                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.SalePurchaseReport.Sale_PurchaseInvoice(companyInfo, saleDtl, fromDate, toDate, formName);
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "Invoice Report";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("InvoiceNo");
                        dt.Columns.Add("Date");
                        if (formName == "Sale")
                        {
                            dt.Columns.Add("CustomerName");
                        }
                        else
                        {
                            dt.Columns.Add("SupplierName");
                        }
                        dt.Columns.Add("GrossAmount");
                        dt.Columns.Add("Discount");
                        dt.Columns.Add("NetSale");
                        dt.Columns.Add("Vat");
                        dt.Columns.Add("Total");
                        DataRow row;
                        int i = 1;
                        if (formName == "Sale")
                        {
                            foreach (var item in saleDtl)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["InvoiceNo"] = item.InvoiceNo;
                                row["Date"] = item.Date.ToString("dd MMMM yyyy");
                                row["CustomerName"] = item.CustomerName;
                                row["GrossAmount"] = item.TotalWithOutAmount.ToString("N2");
                                row["Discount"] = item.Discount.ToString("N2");
                                row["NetSale"] = (item.TotalWithOutAmount + item.Discount).ToString();
                                row["Vat"] = item.VATamount.ToString("N2");
                                row["Total"] = item.TotalAmount.ToString("N2");
                                dt.Rows.Add(row);
                            }
                        }
                        else
                        {
                            foreach (var item in saleDtl)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["InvoiceNo"] = item.InvoiceNo;
                                row["Date"] = item.Date.ToString("dd MMMM yyyy");
                                row["SupplierName"] = item.EnglishName;
                                row["GrossAmount"] = item.Amount.ToString("N2");
                                row["Discount"] = item.Discount.ToString("N2");
                                row["NetSale"] = (item.Amount + item.Discount).ToString();
                                row["Vat"] = item.VATamount.ToString("N2");
                                row["Total"] = item.TotalAmount.ToString("N2");
                                dt.Rows.Add(row);
                            }
                        }
                        //ASPxSummaryItem aSPxSummary = new ASPxSummaryItem();
                        //aSPxSummary.ShowInColumn = "Total";
                        //aSPxSummary.FieldName = "Total";Width="*"
                        //aSPxSummary.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        //ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.SettingsPager.PageSize = 23;
                        ASPxGridView1.Columns["#"].Width = 40;
                        if (formName == "Sale")
                        {
                            ASPxGridView1.Columns["CustomerName"].Width = 580;
                        }
                        else
                        {
                            ASPxGridView1.Columns["SupplierName"].Width = 580;
                        }

                        ASPxGridView1.Columns["InvoiceNo"].Width = 150;
                        ASPxGridView1.Columns["Discount"].Width = 100;
                        ASPxGridView1.Columns["Date"].Width = 150;
                        ASPxGridView1.Columns["GrossAmount"].Width = 100;
                        ASPxGridView1.Columns["Vat"].Width = 100;
                        ASPxGridView1.Columns["NetSale"].Width = 100;
                        ASPxGridView1.Columns["Total"].Width = 100;

                        ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Vat"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Vat"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["NetSale"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["NetSale"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Discount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Discount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["GrossAmount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["GrossAmount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Total = new ASPxSummaryItem();
                        Total.FieldName = "Total";
                        Total.DisplayFormat = "{0:#,0.00}";
                        Total.SummaryType = SummaryItemType.Sum;
                        Total.ShowInColumn = "Total";
                        ASPxGridView1.TotalSummary.Add(Total);
                        ASPxSummaryItem Total_Vat = new ASPxSummaryItem();
                        Total_Vat.FieldName = "Vat";
                        Total_Vat.DisplayFormat = "{0:#,0.00}";
                        Total_Vat.SummaryType = SummaryItemType.Sum;
                        Total_Vat.ShowInColumn = "Vat";
                        ASPxGridView1.TotalSummary.Add(Total_Vat);
                        ASPxSummaryItem Net_Total = new ASPxSummaryItem();
                        Net_Total.FieldName = "NetSale";
                        Net_Total.DisplayFormat = "{0:#,0.00}";
                        Net_Total.SummaryType = SummaryItemType.Sum;
                        Net_Total.ShowInColumn = "NetSale";
                        ASPxGridView1.TotalSummary.Add(Net_Total);
                        ASPxSummaryItem Discount = new ASPxSummaryItem();
                        Discount.FieldName = "Discount";
                        Discount.DisplayFormat = "{0:#,0.00}";
                        Discount.SummaryType = SummaryItemType.Sum;
                        Discount.ShowInColumn = "Discount";
                        ASPxGridView1.TotalSummary.Add(Discount);

                        ASPxSummaryItem Gross_Total = new ASPxSummaryItem();
                        Gross_Total.FieldName = "GrossAmount";
                        Gross_Total.DisplayFormat = "{0:#,0.00}";
                        Gross_Total.SummaryType = SummaryItemType.Sum;
                        Gross_Total.ShowInColumn = "GrossAmount";
                        ASPxGridView1.TotalSummary.Add(Gross_Total);
                        ASPxGridView1.Settings.ColumnMinWidth = 30;

                    }

                }
                else if (formName == "MonthlySalesComparisionReport")
                {

                    string year = Request.QueryString["year"] == null ? "" : Request.QueryString["year"];
                    string month = Request.QueryString["month"] == null ? "" : Request.QueryString["month"];
                    if (year != "undefined" && month != "undefined")
                    {
                        var comparison = GetsalePurchaseComparison.GetsalePurchaseComparisonDtl(month, year, Session["Reporting"].ToString(), serverAddress, branchId);

                        if (Print == "true")
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport report1 = new Noble.Report.Reports.Reports.SalePurchaseReport.MonthlySalesComparisionReport(companyInfo, comparison, month, year);
                            ASPxWebDocumentViewer1.OpenReport(report1);

                        }
                        else
                        {
                            ASPxWebDocumentViewer1.Visible = false;
                            ASPxGridView1.Visible = true;
                            var dt = new DataTable();
                            dt.Columns.Add("#");
                            dt.Columns.Add("Date");
                            dt.Columns.Add("TotalPurchase");
                            dt.Columns.Add("Discount");
                            dt.Columns.Add("Return");
                            dt.Columns.Add("Total");
                            dt.Columns.Add("Tax");
                            dt.Columns.Add("NetAmount");
                            dt.Columns.Add("Cash");
                            dt.Columns.Add("Bank");
                            dt.Columns.Add("Credit");
                            dt.Columns.Add("SaleDate");
                            dt.Columns.Add("TotalSale");
                            dt.Columns.Add("SaleDiscount");
                            dt.Columns.Add("ReturnSale");
                            dt.Columns.Add("SaleTotal");
                            dt.Columns.Add("SaleTax");
                            dt.Columns.Add("NetSaleAmount");
                            dt.Columns.Add("CashSale");
                            dt.Columns.Add("BankSale");
                            dt.Columns.Add("CreditSale");
                            DataRow row;
                            int i = 1;

                            foreach (var item in comparison)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["Date"] = item.PurchaseDate.ToString("dd MMMM yyyy");
                                row["TotalPurchase"] = item.GrossPurchaseAmount.ToString("N2");
                                row["Discount"] = item.PurchaseDiscount.ToString("N2");
                                row["Return"] = item.PurchaseTotalReturns.ToString("N2");
                                row["Total"] = (item.GrossPurchaseAmount - item.PurchaseTotalReturns - item.PurchaseDiscount).ToString("N2");
                                row["Tax"] = item.PurchaseTotalTax.ToString("N2");
                                row["NetAmount"] = ((item.GrossAmount - item.PurchaseTotalReturns - item.PurchaseDiscount) + item.PurchaseTotalTax).ToString("N2");
                                row["Cash"] = item.PurchaseCash.ToString("N2");
                                row["Bank"] = item.PurchaseBank.ToString("N2");
                                row["Credit"] = item.PurchaseCredit.ToString("N2");

                                row["SaleDate"] = item.Date.ToString("dd MMMM yyyy");
                                row["TotalSale"] = (item.GrossAmount).ToString("N2");
                                row["SaleDiscount"] = item.Discount.ToString("N2");
                                row["ReturnSale"] = item.TotalReturns.ToString("N2");
                                row["SaleTotal"] = (item.GrossAmount - item.TotalReturns - item.Discount).ToString("N2");
                                row["SaleTax"] = item.TotalTax.ToString("N2");
                                row["NetSaleAmount"] = ((item.GrossAmount - item.TotalReturns - item.Discount) + item.TotalTax).ToString("N2");
                                row["CashSale"] = item.Cash.ToString("N2");
                                row["BankSale"] = item.Bank.ToString("N2");
                                row["CreditSale"] = item.Credit.ToString("N2");
                                dt.Rows.Add(row);
                            }

                            GridViewBandColumn bandPurchase = new GridViewBandColumn();
                            bandPurchase.Caption = "Purchase";
                            bandPurchase.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                            //#eaf0f9
                            bandPurchase.HeaderStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                            bandPurchase.HeaderStyle.Font.Bold = true;
                            bandPurchase.HeaderStyle.ForeColor = Color.Black;
                            GridViewBandColumn bandSale = new GridViewBandColumn();
                            bandSale.Caption = "Sale";
                            bandSale.HeaderStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                            bandSale.HeaderStyle.ForeColor = Color.Black;
                            bandSale.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                            bandSale.HeaderStyle.Font.Bold = true;
                            // Add the bands to the grid view
                            ASPxGridView1.Columns.Add(bandPurchase);
                            ASPxGridView1.Columns.Add(bandSale);
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (j < 11)
                                {
                                    GridViewDataTextColumn column = new GridViewDataTextColumn();
                                    column.FieldName = dt.Columns[j].ColumnName;
                                    column.Caption = dt.Columns[j].ColumnName;
                                    bandPurchase.Columns.Add(column);
                                }
                                else
                                {
                                    GridViewDataTextColumn column = new GridViewDataTextColumn();
                                    column.FieldName = dt.Columns[j].ColumnName;
                                    column.Caption = dt.Columns[j].ColumnName;
                                    bandSale.Columns.Add(column);
                                }
                            }
                            ASPxGridView1.Columns["#"].Width = 40;
                            ASPxGridView1.DataSource = dt;
                            ASPxGridView1.DataBind();
                            ASPxGridView1.Columns["TotalPurchase"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["TotalPurchase"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Discount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Discount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Return"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Return"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Tax"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Tax"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["NetAmount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["NetAmount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Cash"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Cash"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Bank"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Bank"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Credit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["Credit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["TotalSale"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["TotalSale"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["SaleDiscount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["SaleDiscount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["ReturnSale"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["ReturnSale"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["SaleTotal"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["SaleTotal"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["SaleTax"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["SaleTax"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["NetSaleAmount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["NetSaleAmount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["CashSale"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["CashSale"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["BankSale"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["BankSale"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["CreditSale"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns["CreditSale"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.TotalSummary.Clear();
                            ASPxSummaryItem TotalPurchase = new ASPxSummaryItem();
                            TotalPurchase.FieldName = "TotalPurchase";
                            TotalPurchase.DisplayFormat = "{0:#,0.00}";
                            TotalPurchase.SummaryType = SummaryItemType.Sum;
                            TotalPurchase.ShowInColumn = "TotalPurchase";
                            ASPxGridView1.TotalSummary.Add(TotalPurchase);
                            ASPxSummaryItem Discount = new ASPxSummaryItem();
                            Discount.FieldName = "Discount";
                            Discount.DisplayFormat = "{0:#,0.00}";
                            Discount.SummaryType = SummaryItemType.Sum;
                            Discount.ShowInColumn = "Discount";
                            ASPxGridView1.TotalSummary.Add(Discount);
                            ASPxSummaryItem Return = new ASPxSummaryItem();
                            Return.FieldName = "Return";
                            Return.DisplayFormat = "{0:#,0.00}";
                            Return.SummaryType = SummaryItemType.Sum;
                            Return.ShowInColumn = "Return";
                            ASPxGridView1.TotalSummary.Add(Return);
                            ASPxSummaryItem Total = new ASPxSummaryItem();
                            Total.FieldName = "Total";
                            Total.DisplayFormat = "{0:#,0.00}";
                            Total.SummaryType = SummaryItemType.Sum;
                            Total.ShowInColumn = "Total";
                            ASPxGridView1.TotalSummary.Add(Total);
                            ASPxSummaryItem Tax = new ASPxSummaryItem();
                            Tax.FieldName = "Tax";
                            Tax.DisplayFormat = "{0:#,0.00}";
                            Tax.SummaryType = SummaryItemType.Sum;
                            Tax.ShowInColumn = "Tax";
                            ASPxGridView1.TotalSummary.Add(Tax);
                            ASPxSummaryItem NetAmount = new ASPxSummaryItem();
                            NetAmount.FieldName = "NetAmount";
                            NetAmount.DisplayFormat = "{0:#,0.00}";
                            NetAmount.SummaryType = SummaryItemType.Sum;
                            NetAmount.ShowInColumn = "NetAmount";
                            ASPxGridView1.TotalSummary.Add(NetAmount);
                            ASPxSummaryItem Cash = new ASPxSummaryItem();
                            Cash.FieldName = "Cash";
                            Cash.DisplayFormat = "{0:#,0.00}";
                            Cash.SummaryType = SummaryItemType.Sum;
                            Cash.ShowInColumn = "Cash";
                            ASPxGridView1.TotalSummary.Add(Cash);
                            ASPxSummaryItem Bank = new ASPxSummaryItem();
                            Bank.FieldName = "Bank";
                            Bank.DisplayFormat = "{0:#,0.00}";
                            Bank.SummaryType = SummaryItemType.Sum;
                            Bank.ShowInColumn = "Bank";
                            ASPxGridView1.TotalSummary.Add(Bank);
                            ASPxSummaryItem Credit = new ASPxSummaryItem();
                            Credit.FieldName = "Credit";
                            Credit.DisplayFormat = "{0:#,0.00}";
                            Credit.SummaryType = SummaryItemType.Sum;
                            Credit.ShowInColumn = "Credit";
                            ASPxGridView1.TotalSummary.Add(Credit);
                            ASPxSummaryItem TotalSale = new ASPxSummaryItem();
                            TotalSale.FieldName = "TotalSale";
                            TotalSale.DisplayFormat = "{0:#,0.00}";
                            TotalSale.SummaryType = SummaryItemType.Sum;
                            TotalSale.ShowInColumn = "TotalSale";
                            ASPxGridView1.TotalSummary.Add(TotalSale);
                            ASPxSummaryItem SaleDiscount = new ASPxSummaryItem();
                            SaleDiscount.FieldName = "SaleDiscount";
                            SaleDiscount.DisplayFormat = "{0:#,0.00}";
                            SaleDiscount.SummaryType = SummaryItemType.Sum;
                            SaleDiscount.ShowInColumn = "SaleDiscount";
                            ASPxGridView1.TotalSummary.Add(SaleDiscount);
                            ASPxSummaryItem ReturnSale = new ASPxSummaryItem();
                            ReturnSale.FieldName = "ReturnSale";
                            ReturnSale.DisplayFormat = "{0:#,0.00}";
                            ReturnSale.SummaryType = SummaryItemType.Sum;
                            ReturnSale.ShowInColumn = "ReturnSale";
                            ASPxGridView1.TotalSummary.Add(ReturnSale);
                            ASPxSummaryItem SaleTotal = new ASPxSummaryItem();
                            SaleTotal.FieldName = "SaleTotal";
                            SaleTotal.DisplayFormat = "{0:#,0.00}";
                            SaleTotal.SummaryType = SummaryItemType.Sum;
                            SaleTotal.ShowInColumn = "SaleTotal";
                            ASPxGridView1.TotalSummary.Add(SaleTotal);
                            ASPxSummaryItem SaleTax = new ASPxSummaryItem();
                            SaleTax.FieldName = "SaleTax";
                            SaleTax.DisplayFormat = "{0:#,0.00}";
                            SaleTax.SummaryType = SummaryItemType.Sum;
                            SaleTax.ShowInColumn = "SaleTax";
                            ASPxGridView1.TotalSummary.Add(SaleTax);
                            ASPxSummaryItem NetSaleAmount = new ASPxSummaryItem();
                            NetSaleAmount.FieldName = "NetSaleAmount";
                            NetSaleAmount.DisplayFormat = "{0:#,0.00}";
                            NetSaleAmount.SummaryType = SummaryItemType.Sum;
                            NetSaleAmount.ShowInColumn = "NetSaleAmount";
                            ASPxGridView1.TotalSummary.Add(NetSaleAmount);
                            ASPxSummaryItem CashSale = new ASPxSummaryItem();
                            CashSale.FieldName = "CashSale";
                            CashSale.DisplayFormat = "{0:#,0.00}";
                            CashSale.SummaryType = SummaryItemType.Sum;
                            CashSale.ShowInColumn = "CashSale";
                            ASPxGridView1.TotalSummary.Add(CashSale);
                            ASPxSummaryItem BankSale = new ASPxSummaryItem();
                            BankSale.FieldName = "BankSale";
                            BankSale.DisplayFormat = "{0:#,0.00}";
                            BankSale.SummaryType = SummaryItemType.Sum;
                            BankSale.ShowInColumn = "BankSale";
                            ASPxGridView1.TotalSummary.Add(BankSale);
                            ASPxSummaryItem CreditSale = new ASPxSummaryItem();
                            CreditSale.FieldName = "CreditSale";
                            CreditSale.DisplayFormat = "{0:#,0.00}";
                            CreditSale.SummaryType = SummaryItemType.Sum;
                            CreditSale.ShowInColumn = "CreditSale";
                            ASPxGridView1.TotalSummary.Add(CreditSale);
                        }
                    }
                }
                else if (formName == "VatExpenseReport")
                {
                    // Company / GetContactOpeningBalanceList
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    var dailyEcpense = GetVatExpenseReport.GetVatExpenseDtl(fromDate, toDate, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.SalePurchaseReport.VatExpenseReport(companyInfo, dailyEcpense, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "CustomerLedgerReport";
                        ASPxGridView1.Visible = false;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("date");
                        dt.Columns.Add("VocherNo");
                        dt.Columns.Add("GrossValue");
                        dt.Columns.Add("Vat");
                        dt.Columns.Add("Total");
                        DataRow row;
                        int i = 1;
                        foreach (var item in dailyEcpense)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["date"] = Convert.ToDateTime(item.Date).ToString("dd MMMM yyyy");
                            row["VocherNo"] = item.VoucherNo;
                            row["GrossValue"] = item.GrossAmount.ToString("N2");
                            row["Vat"] = item.TotalVat.ToString("N2");
                            row["Total"] = item.TotalAmount.ToString("N2");
                            dt.Rows.Add(row);
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["date"].Width = 525;
                        ASPxGridView1.Columns["VocherNo"].Width = 310;
                        ASPxGridView1.Columns["Total"].Width = 240;
                        ASPxGridView1.Columns["Vat"].Width = 205;
                        ASPxGridView1.Columns["GrossValue"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["GrossValue"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Vat"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Vat"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem GrossValue = new ASPxSummaryItem();
                        GrossValue.FieldName = "GrossValue";
                        GrossValue.DisplayFormat = "{0:#,0.00}";
                        GrossValue.SummaryType = SummaryItemType.Sum;
                        GrossValue.ShowInColumn = "GrossValue";
                        ASPxGridView1.TotalSummary.Add(GrossValue);
                        ASPxSummaryItem VatTotal = new ASPxSummaryItem();
                        VatTotal.FieldName = "Vat";
                        VatTotal.DisplayFormat = "{0:#,0.00}";
                        VatTotal.SummaryType = SummaryItemType.Sum;
                        VatTotal.ShowInColumn = "Vat";
                        ASPxGridView1.TotalSummary.Add(VatTotal);
                        ASPxSummaryItem Total = new ASPxSummaryItem();
                        Total.FieldName = "Total";
                        Total.DisplayFormat = "{0:#,0.00}";
                        Total.SummaryType = SummaryItemType.Sum;
                        Total.ShowInColumn = "Total";
                        ASPxGridView1.TotalSummary.Add(Total);

                    }


                }
                else if (formName == "DailyExpenseReport")
                {
                    // Company / GetContactOpeningBalanceList
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string paymentTypr = Request.QueryString["paymentType"] == null ? "" : Request.QueryString["paymentType"];
                    var dailyExpense = GetDailyExpense.GetDailyExpenseDtl(fromDate, toDate, paymentTypr, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.SalePurchaseReport.DailyExpenseReport(companyInfo, dailyExpense, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "DailyExpenseReport " + DateTime.Now;
                        ASPxGridView1.Visible = false;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("Month");
                        dt.Columns.Add("date");
                        dt.Columns.Add("VocherNo");
                        dt.Columns.Add("GrossValue");
                        dt.Columns.Add("Vat");
                        dt.Columns.Add("Total");
                        DataRow row;
                        int i = 0;
                        foreach (var item in dailyExpense)
                        {
                            foreach (var item2 in item.Sales)
                            {
                                row = dt.NewRow();
                                row["Month"] = item.Date.ToString("MMMM yyyy");
                                row["date"] = Convert.ToDateTime(item2.Date).ToString("dd MMMM yyyy");
                                row["VocherNo"] = item2.CustomerVat;
                                row["GrossValue"] = item2.Amount.ToString("N2");
                                row["Vat"] = item2.VATamount.ToString("N2");
                                row["Total"] = item2.TotalAmount.ToString("N2");
                                dt.Rows.Add(row);
                            }
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["Month"]);
                        ASPxGridView1.ExpandAll();
                        ASPxGridView1.Columns["date"].Width = 445;
                        ASPxGridView1.Columns["VocherNo"].Width = 435;
                        ASPxGridView1.Columns["Total"].Width = 205;
                        ASPxGridView1.Columns["Vat"].Width = 205;
                        ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Vat"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Vat"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem GrossValue = new ASPxSummaryItem();
                        GrossValue.FieldName = "GrossValue";
                        GrossValue.DisplayFormat = "{0:#,0.00}";
                        GrossValue.SummaryType = SummaryItemType.Sum;
                        GrossValue.ShowInColumn = "GrossValue";
                        ASPxGridView1.TotalSummary.Add(GrossValue);
                        ASPxSummaryItem VatTotal = new ASPxSummaryItem();
                        VatTotal.FieldName = "Vat";
                        VatTotal.DisplayFormat = "{0:#,0.00}";
                        VatTotal.SummaryType = SummaryItemType.Sum;
                        VatTotal.ShowInColumn = "Vat";
                        ASPxGridView1.TotalSummary.Add(VatTotal);
                        ASPxSummaryItem Total = new ASPxSummaryItem();
                        Total.FieldName = "Total";
                        Total.DisplayFormat = "{0:#,0.00}";
                        Total.SummaryType = SummaryItemType.Sum;
                        Total.ShowInColumn = "Total";
                        ASPxGridView1.TotalSummary.Add(Total);

                    }
                }
                else if (formName == "AdvanceSaleInvoice")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];

                    string invoiceType = Request.QueryString["invoiceType"] == null ? "" : Request.QueryString["invoiceType"];
                    var Sale = GetAdvanceSaleInvoice.GetAdvanceSaleInvoiceDtl(fromDate, toDate, numberOfPeriods, compareWith, customerId, invoiceType, Session["Reporting"].ToString(), serverAddress);
                    if (compareWith != null && compareWith != "" && compareWith != "null")
                    {
                        var Accounts = GetAllAccounts.GetAllAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        //XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalance.AdvanceTrialBalanceComparison(companyInfo, trailBalance, compareWith, numberOfPeriods);
                        //ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        //ComparisonBalanceSheetReport.DisplayName = "Advance Trail Balance Report" + DateTime.Now;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.SalePurchaseReport.AdvanceSalePurchase.AdvanceSaleInvoice(companyInfo, Sale, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Advance Sale Invoice" + DateTime.Now;
                    }
                }
                else if (formName == "SaleInvoiceSummary" || formName == "PurchaseInvoiceSummary")
                {
                    string invoiceType = Request.QueryString["invoiceType"] == null ? "" : Request.QueryString["invoiceType"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    if (formName == "SaleInvoiceSummary")
                    {
                        var SaleRecord = GetSaleInvoiceSummary.GetSaleInvoiceSummaryDtl(invoiceType, numberOfPeriods, compareWith, Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.SalePurchaseReport.AdvanceSaleInvoiceReport.AdvanceSaleInvoiceReport(companyInfo, SaleRecord, compareWith, numberOfPeriods);
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Advance Sale Invoice Report" + DateTime.Now;
                    }
                    else if (formName == "PurchaseInvoiceSummary")
                    {
                        var PurchaseRecord = GetPurchaseInvoiceSummary.GetPurchaseInvoiceSummaryDtl(invoiceType, numberOfPeriods, compareWith, Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.SalePurchaseReport.AdvancePurchaseInvoiceReport.PurchaseInvoiceSummary(companyInfo, PurchaseRecord, compareWith, numberOfPeriods);
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Advance Sale Invoice Report" + DateTime.Now;
                    }


                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}