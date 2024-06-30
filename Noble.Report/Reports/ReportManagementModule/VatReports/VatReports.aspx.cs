using DevExpress.Data;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Noble.Report.NobleReportServices;

namespace Noble.Report.Reports.ReportManagementModule.VatReports
{
    public partial class VatReports : System.Web.UI.Page
    {
        string[] monthNames = new string[]{"","January", "February", "March", "April", "May", "June",
                        "July", "August", "September", "October", "November", "December"};
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

                if (formName == "VatPayableReport")
                {

                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    var VatDtl = GetVatDtl.VatDtl(fromDate, toDate, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.VATReport.VatReport(companyInfo, VatDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "VatPayableReport";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("Code");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("Status");

                        DataRow row;
                        int i = 1;
                        foreach (var item in VatDtl.VatPayable)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["Code"] = item.Code;
                            row["Name"] = item.Name;
                            row["Amount"] = item.Amount; /*< 0 ? (item.Amount * -1).ToString("N2") : item.Amount.ToString("N2");*/
                            row["Status"] = "Vat Payable";
                            dt.Rows.Add(row);
                        }

                        var dt1 = new DataTable();
                        dt1.Columns.Add("#");
                        dt1.Columns.Add("Code");
                        dt1.Columns.Add("Name");
                        dt1.Columns.Add("Amount");
                        dt1.Columns.Add("Status");
                        DataRow row1;
                        foreach (var item in VatDtl.VatPaid)
                        {
                            row1 = dt1.NewRow();
                            row1["#"] = i++;
                            row1["Code"] = item.Code;
                            row1["Name"] = item.Name;
                            row1["Amount"] = item.Amount;/* < 0 ? (item.Amount * -1).ToString("N2") : item.Amount.ToString("N2");*/
                            row1["Status"] = "Vat Paid";
                            dt1.Rows.Add(row1);
                        }
                        dt.Merge(dt1);
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["Status"]);
                        ASPxGridView1.ExpandAll();
                        ASPxGridView1.Columns["#"].Width = 60;
                        ASPxGridView1.Columns["Code"].Width = 150;
                        ASPxGridView1.Columns["Name"].Width = 400;
                        ASPxGridView1.Columns["Amount"].Width = 445;
                        ASPxGridView1.Columns["Amount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Amount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleAlways;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Amount = new ASPxSummaryItem();
                        Amount.FieldName = "Amount";
                        Amount.DisplayFormat = "{0:#,0.00}";
                        Amount.SummaryType = SummaryItemType.Sum;
                        Amount.ShowInColumn = "Amount";
                        ASPxGridView1.TotalSummary.Add(Amount);
                        ASPxGridView1.GroupSummary.Clear();
                        ASPxSummaryItem GAmount = new ASPxSummaryItem();
                        GAmount.ShowInGroupFooterColumn = "Amount";
                        GAmount.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        GAmount.DisplayFormat = "{0:#,0.00}";
                        GAmount.FieldName = "Amount";
                        ASPxGridView1.GroupSummary.Add(GAmount);
                    }

                }

                else if (formName == "VatReturnReport")
                {

                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string language = Request.QueryString["toDate"] == null ? "" : Request.QueryString["language"];
                    string currency = Request.QueryString["toDate"] == null ? "" : Request.QueryString["currency"];

                    var VatDtl = GetVatReturnReport.VatDtl(fromDate, toDate, Session["Reporting"].ToString(), serverAddress, branchId);
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        if(language=="ar")
                        {
                            XtraReport incomeStatementRpt = new Reports.VATReport.VatReturnReportArabic(companyInfo, VatDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), currency);
                            ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                            incomeStatementRpt.DisplayName = "VATReturn";
                        }
                        else
                        {
                           


                            XtraReport incomeStatementRpt = new Reports.VATReport.SalePurchaseVatReturnReport(companyInfo, VatDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), currency);
                            ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                            incomeStatementRpt.DisplayName = "VATReturn";
                        }

                       
                        
                       
                    }


                }


                else if (formName == "VatMonth")
                {

                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string PaymentType = Request.QueryString["PaymentType"] == null ? "" : Request.QueryString["PaymentType"];
                    string DocumentName = Request.QueryString["DocumentName"] == null ? "" : Request.QueryString["DocumentName"];
                    var monthDtl = GetVatSummery.GetVatSummeryDtl(fromDate, toDate, PaymentType, DocumentName, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport incomeStatementRpt = new Reports.VATReport.VatSalePurchaseSummeryReport(companyInfo, monthDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "VatPayableReport";
                    }
                    else
                    {

                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("Month");
                        dt.Columns.Add("TotalSales");
                        dt.Columns.Add("VatIn");
                        dt.Columns.Add("TotalSalesReturn");
                        dt.Columns.Add("SaleReturnVat");
                        dt.Columns.Add("TotalPurchases");
                        dt.Columns.Add("VatOut");
                        dt.Columns.Add("TotalPurchaseReturn");
                        dt.Columns.Add("PurchaseReturnVat");
                        dt.Columns.Add("TotalExpense");
                        dt.Columns.Add("ExpenseVatOut");
                        dt.Columns.Add("OtherVocherVat");
                        dt.Columns.Add("VATPayable/Receivable");
                        DataRow row;
                        int i = 0;
                        foreach (var item in monthDtl)
                        {
                            row = dt.NewRow();
                            row["Month"] = ++i + ":  " + monthNames[item.Month] + " " + item.Year;
                            row["TotalSales"] = item.TotalSales.ToString("N2");
                            row["VatIn"] = item.VatAmountIn.ToString("N2");
                            row["TotalPurchases"] = item.TotalPurchase.ToString("N2");
                            row["VatOut"] = item.VatAmountOut.ToString("N2");
                            row["TotalExpense"] = item.TotalExpense.ToString("N2");
                            row["ExpenseVatOut"] = item.TotalVat.ToString("N2");
                            row["TotalSalesReturn"] = item.TotalSaleReturn.ToString("N2");
                            row["SaleReturnVat"] = item.SaleReturnVat.ToString("N2");
                            row["TotalPurchaseReturn"] = item.TotalPurchaseReturn.ToString("N2");
                            row["PurchaseReturnVat"] = item.PurchaseReturnVat.ToString("N2");
                            row["OtherVocherVat"] = item.OtherVocherVat.ToString("N2");
                            row["VATPayable/Receivable"] = ((item.VatAmountIn + item.PurchaseReturnVat) - (item.VatAmountOut + item.SaleReturnVat + item.TotalVat)).ToString("N2");
                            dt.Rows.Add(row);
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["Month"]);
                        //ASPxGridView1.Settings.ShowGroupPanel.s.sort.SortBy(DevExpress.Data.ColumnSortOrder.Ascending);
                        ASPxGridView1.ExpandAll();
                        ASPxGridView1.Columns["TotalSales"].Width = 120;
                        ASPxGridView1.Columns["VatIn"].Width = 120;
                        ASPxGridView1.Columns["TotalPurchases"].Width = 120;
                        ASPxGridView1.Columns["VatOut"].Width = 120;
                        ASPxGridView1.Columns["VATPayable/Receivable"].Width = 120;
                        ASPxGridView1.Columns["TotalExpense"].Width = 120;
                        ASPxGridView1.Columns["ExpenseVatOut"].Width = 120;
                        ASPxGridView1.Columns["TotalSalesReturn"].Width = 120;
                        ASPxGridView1.Columns["SaleReturnVat"].Width = 120;
                        ASPxGridView1.Columns["TotalPurchaseReturn"].Width = 120;
                        ASPxGridView1.Columns["PurchaseReturnVat"].Width = 120;
                        ASPxGridView1.Columns["OtherVocherVat"].Width = 120;
                        ASPxGridView1.Columns["OtherVocherVat"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["OtherVocherVat"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalSalesReturn"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalSalesReturn"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["SaleReturnVat"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["SaleReturnVat"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalPurchaseReturn"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalPurchaseReturn"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["PurchaseReturnVat"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["PurchaseReturnVat"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalSales"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalSales"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["VatIn"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["VatIn"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalPurchases"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalPurchases"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["VatOut"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["VatOut"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["VATPayable/Receivable"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["VATPayable/Receivable"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalExpense"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalExpense"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["ExpenseVatOut"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["ExpenseVatOut"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem OtherVocherVat = new ASPxSummaryItem();
                        OtherVocherVat.FieldName = "OtherVocherVat";
                        OtherVocherVat.DisplayFormat = "{0:#,0.00}";
                        OtherVocherVat.SummaryType = SummaryItemType.Sum;
                        OtherVocherVat.ShowInColumn = "OtherVocherVat";
                        ASPxGridView1.TotalSummary.Add(OtherVocherVat);
                        ASPxSummaryItem TotalSalesReturn = new ASPxSummaryItem();
                        TotalSalesReturn.FieldName = "TotalSalesReturn";
                        TotalSalesReturn.DisplayFormat = "{0:#,0.00}";
                        TotalSalesReturn.SummaryType = SummaryItemType.Sum;
                        TotalSalesReturn.ShowInColumn = "TotalSalesReturn";
                        ASPxGridView1.TotalSummary.Add(TotalSalesReturn);
                        ASPxSummaryItem SaleReturnVat = new ASPxSummaryItem();
                        SaleReturnVat.FieldName = "SaleReturnVat";
                        SaleReturnVat.DisplayFormat = "{0:#,0.00}";
                        SaleReturnVat.SummaryType = SummaryItemType.Sum;
                        SaleReturnVat.ShowInColumn = "SaleReturnVat";
                        ASPxGridView1.TotalSummary.Add(SaleReturnVat);
                        ASPxSummaryItem TotalPurchaseReturn = new ASPxSummaryItem();
                        TotalPurchaseReturn.FieldName = "TotalPurchaseReturn";
                        TotalPurchaseReturn.DisplayFormat = "{0:#,0.00}";
                        TotalPurchaseReturn.SummaryType = SummaryItemType.Sum;
                        TotalPurchaseReturn.ShowInColumn = "TotalPurchaseReturn";
                        ASPxGridView1.TotalSummary.Add(TotalPurchaseReturn);
                        ASPxSummaryItem PurchaseReturnVat = new ASPxSummaryItem();
                        PurchaseReturnVat.FieldName = "PurchaseReturnVat";
                        PurchaseReturnVat.DisplayFormat = "{0:#,0.00}";
                        PurchaseReturnVat.SummaryType = SummaryItemType.Sum;
                        PurchaseReturnVat.ShowInColumn = "PurchaseReturnVat";
                        ASPxGridView1.TotalSummary.Add(PurchaseReturnVat);
                        ASPxSummaryItem TotalSale = new ASPxSummaryItem();
                        TotalSale.FieldName = "TotalSales";
                        TotalSale.DisplayFormat = "{0:#,0.00}";
                        TotalSale.SummaryType = SummaryItemType.Sum;
                        TotalSale.ShowInColumn = "TotalSales";
                        ASPxGridView1.TotalSummary.Add(TotalSale);
                        ASPxSummaryItem VATIN = new ASPxSummaryItem();
                        VATIN.FieldName = "VatIn";
                        VATIN.DisplayFormat = "{0:#,0.00}";
                        VATIN.SummaryType = SummaryItemType.Sum;
                        VATIN.ShowInColumn = "VatIn";
                        ASPxGridView1.TotalSummary.Add(VATIN);
                        ASPxSummaryItem TotalPurchases = new ASPxSummaryItem();
                        TotalPurchases.FieldName = "TotalPurchases";
                        TotalPurchases.DisplayFormat = "{0:#,0.00}";
                        TotalPurchases.SummaryType = SummaryItemType.Sum;
                        TotalPurchases.ShowInColumn = "TotalPurchases";
                        ASPxGridView1.TotalSummary.Add(TotalPurchases);
                        ASPxSummaryItem VatOut = new ASPxSummaryItem();
                        VatOut.FieldName = "VatOut";
                        VatOut.DisplayFormat = "{0:#,0.00}";
                        VatOut.SummaryType = SummaryItemType.Sum;
                        VatOut.ShowInColumn = "VatOut";
                        ASPxGridView1.TotalSummary.Add(VatOut);
                        ASPxSummaryItem VATPaybleReceivble = new ASPxSummaryItem();
                        VATPaybleReceivble.FieldName = "VATPayable/Receivable";
                        VATPaybleReceivble.DisplayFormat = "{0:#,0.00}";
                        VATPaybleReceivble.SummaryType = SummaryItemType.Sum;
                        VATPaybleReceivble.ShowInColumn = "VATPayable/Receivable";
                        ASPxGridView1.TotalSummary.Add(VATPaybleReceivble);
                        ASPxSummaryItem TotalExpense = new ASPxSummaryItem();
                        TotalExpense.FieldName = "TotalExpense";
                        TotalExpense.DisplayFormat = "{0:#,0.00}";
                        TotalExpense.SummaryType = SummaryItemType.Sum;
                        TotalExpense.ShowInColumn = "TotalExpense";
                        ASPxGridView1.TotalSummary.Add(TotalExpense);
                        ASPxSummaryItem ExpenseVatOut = new ASPxSummaryItem();
                        ExpenseVatOut.FieldName = "ExpenseVatOut";
                        ExpenseVatOut.DisplayFormat = "{0:#,0.00}";
                        ExpenseVatOut.SummaryType = SummaryItemType.Sum;
                        ExpenseVatOut.ShowInColumn = "ExpenseVatOut";
                        ASPxGridView1.TotalSummary.Add(ExpenseVatOut);
                    }
                }
                else if (formName == "PurchaseMonth" || formName == "SaleMonth" || formName == "DailyExpense")
                {

                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string PaymentType = Request.QueryString["PaymentType"] == null ? "" : Request.QueryString["PaymentType"];
                    string DocumentName = Request.QueryString["DocumentName"] == null ? "" : Request.QueryString["DocumentName"];
                    var monthDtl = IncoiceReportMonth.IncoiceReportMonthDtl(fromDate, toDate, PaymentType, DocumentName, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.VATReport.PurchaseVatReport(companyInfo, monthDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), formName);
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "VatPayableReport";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("Month");
                        dt.Columns.Add("Date");
                        if (formName == "PurchaseMonth" || formName == "SaleMonth")
                        {
                            if (formName == "SaleMonth")
                            {
                                dt.Columns.Add("CustomerName");
                                dt.Columns.Add("CustomerVat");
                            }
                            else
                            {
                                dt.Columns.Add("SupplierName");
                                dt.Columns.Add("SupplierVat");
                            }

                        }
                        dt.Columns.Add("PurchaseInv");
                        dt.Columns.Add("TotalInvPrice");
                        dt.Columns.Add("VatAmount");
                        dt.Columns.Add("Total");
                        DataRow row;
                        int i = 0;
                        foreach (var item in monthDtl)
                        {
                            foreach (var list in item.Sales)
                            {
                                row = dt.NewRow();
                                row["Month"] = Convert.ToDateTime(item.Date).ToString("M MMMM yyyy");
                                row["Date"] = list.Date.ToString("dd MMMM yyyy");
                                if (formName == "PurchaseMonth" || formName == "SaleMonth")
                                {
                                    if (formName == "SaleMonth")
                                    {
                                        row["CustomerName"] = list.CustomerName;
                                        row["CustomerVat"] = list.CustomerVat;
                                    }
                                    else
                                    {
                                        row["SupplierName"] = list.CustomerName;
                                        row["SupplierVat"] = list.CustomerVat;
                                    }

                                }
                                row["PurchaseInv"] = list.InvoiceNo;
                                row["TotalInvPrice"] = list.Amount.ToString("N2");
                                row["VatAmount"] = list.VATamount.ToString("N2");
                                row["Total"] = list.TotalAmount.ToString("N2");
                                dt.Rows.Add(row);
                            }
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["Month"]);
                        ASPxGridView1.ExpandAll();

                        ASPxGridView1.Columns["Month"].Visible = false;
                        if (formName == "PurchaseMonth" || formName == "SaleMonth")
                        {
                            ASPxGridView1.Columns["Date"].Width = 120;
                            if (formName == "PurchaseMonth" || formName == "SaleMonth")
                            {
                                if (formName == "SaleMonth")
                                {
                                    ASPxGridView1.Columns["CustomerName"].Width = 555;
                                    ASPxGridView1.Columns["CustomerVat"].Width = 120;

                                }
                                else
                                {

                                    ASPxGridView1.Columns["SupplierName"].Width = 555;
                                    ASPxGridView1.Columns["SupplierVat"].Width = 120;
                                }

                            }
                            ASPxGridView1.Columns["PurchaseInv"].Width = 110;
                            ASPxGridView1.Columns["TotalInvPrice"].Width = 105;
                            ASPxGridView1.Columns["VatAmount"].Width = 100;
                            ASPxGridView1.Columns["Total"].Width = 100;

                        }
                        else
                        {
                            ASPxGridView1.Columns["Date"].Width = 200;
                            ASPxGridView1.Columns["PurchaseInv"].Width = 90;
                            ASPxGridView1.Columns["TotalInvPrice"].Width = 200;
                            ASPxGridView1.Columns["VatAmount"].Width = 200;
                            ASPxGridView1.Columns["Total"].Width = 200;
                        }
                        ASPxGridView1.Columns["VatAmount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["VatAmount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalInvPrice"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["TotalInvPrice"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Vat = new ASPxSummaryItem();
                        Vat.FieldName = "VatAmount";
                        Vat.DisplayFormat = "{0:#,0.00}";
                        Vat.SummaryType = SummaryItemType.Sum;
                        Vat.ShowInColumn = "VatAmount";
                        ASPxGridView1.TotalSummary.Add(Vat);
                        ASPxSummaryItem TotalInvPrice = new ASPxSummaryItem();
                        TotalInvPrice.FieldName = "TotalInvPrice";
                        TotalInvPrice.DisplayFormat = "{0:#,0.00}";
                        TotalInvPrice.SummaryType = SummaryItemType.Sum;
                        TotalInvPrice.ShowInColumn = "TotalInvPrice";
                        ASPxGridView1.TotalSummary.Add(TotalInvPrice);
                        ASPxSummaryItem Total = new ASPxSummaryItem();
                        Total.FieldName = "Total";
                        Total.DisplayFormat = "{0:#,0.00}";
                        Total.SummaryType = SummaryItemType.Sum;
                        Total.ShowInColumn = "Total";
                        ASPxGridView1.TotalSummary.Add(Total);
                        //Group footer Sum calculations 
                        ASPxGridView1.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleAlways;
                        ASPxGridView1.GroupSummary.Clear();
                        ASPxSummaryItem GVat = new ASPxSummaryItem();
                        GVat.ShowInGroupFooterColumn = "VatAmount";
                        GVat.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        GVat.DisplayFormat = "{0:#,0.00}";
                        GVat.FieldName = "VatAmount";
                        ASPxGridView1.GroupSummary.Add(GVat);
                        ASPxSummaryItem GTotalInvPricee = new ASPxSummaryItem();
                        GTotalInvPricee.ShowInGroupFooterColumn = "TotalInvPrice";
                        GTotalInvPricee.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        GTotalInvPricee.DisplayFormat = "{0:#,0.00}";
                        GTotalInvPricee.FieldName = "TotalInvPrice";
                        ASPxGridView1.GroupSummary.Add(GTotalInvPricee);
                        ASPxSummaryItem GTotal = new ASPxSummaryItem();
                        GTotal.ShowInGroupFooterColumn = "Total";
                        GTotal.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        GTotal.DisplayFormat = "{0:#,0.00}";
                        GTotal.FieldName = "Total";
                        ASPxGridView1.GroupSummary.Add(GTotal);

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