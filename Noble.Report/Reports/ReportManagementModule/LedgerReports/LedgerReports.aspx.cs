using DevExpress.Data;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noble.Report.Reports.ReportManagementModule.LedgerReports
{
    public partial class LedgerReports : System.Web.UI.Page
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

                if (formName == "AccountLedger")
                {
                    // Company / GetContactOpeningBalanceList
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string AccoutnId = Request.QueryString["accountId"] == null ? "" : Request.QueryString["accountId"];
                    string dateType = Request.QueryString["dateType"] == null ? "" : Request.QueryString["dateType"];
                    var AccountDtl = GetAccountLedger.AccountLedgerDtl(fromDate, toDate, AccoutnId, dateType, Session["Reporting"].ToString(), serverAddress);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport AccountLedger = new Noble.Report.Reports.Reports.LedgerReport.AccountLedgerReport(companyInfo, AccountDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(AccountLedger);
                        AccountLedger.DisplayName = "AccountLedger";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("TransactionDate");
                        dt.Columns.Add("DocumentDate");
                        dt.Columns.Add("DocumentCode");
                        dt.Columns.Add("Naration");
                        dt.Columns.Add("Debit");
                        dt.Columns.Add("Credit");
                        dt.Columns.Add("Balance");
                        DataRow row;
                        int i = 1;

                        foreach (var item in AccountDtl.TransactionList)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["TransactionDate"] = Convert.ToDateTime(item.Date).ToString("dd MMMM yyyy");
                            row["DocumentDate"] = Convert.ToDateTime(item.DocumentDate).ToString("dd MMMM yyyy");
                            row["DocumentCode"] = item.DocumentNumber;
                            row["Naration"] = item.Description;
                            row["Debit"] = item.DebitAmount.ToString("N2");
                            row["Credit"] = item.CreditAmount.ToString("N2");
                            row["Balance"] = item.OpeningBalance.ToString("N2");
                            dt.Rows.Add(row);

                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["TransactionDate"].Width = 200;
                        ASPxGridView1.Columns["DocumentDate"].Width = 200;
                        ASPxGridView1.Columns["DocumentCode"].Width = 120;
                        ASPxGridView1.Columns["Naration"].Width = 385;
                        ASPxGridView1.Columns["Debit"].Width = 270;
                        ASPxGridView1.Columns["Credit"].Width = 100;
                        ASPxGridView1.Columns["Balance"].Width = 100;
                        ASPxGridView1.Columns["Debit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Debit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Balance"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Balance"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Balance = new ASPxSummaryItem();
                        Balance.FieldName = "Balance";
                        Balance.DisplayFormat = "{0:#,0.00}";
                        Balance.SummaryType = SummaryItemType.Sum;
                        Balance.ShowInColumn = "Balance";
                        ASPxGridView1.TotalSummary.Add(Balance);
                        ASPxSummaryItem Credit = new ASPxSummaryItem();
                        Credit.FieldName = "Credit";
                        Credit.DisplayFormat = "{0:#,0.00}";
                        Credit.SummaryType = SummaryItemType.Sum;
                        Credit.ShowInColumn = "Credit";
                        ASPxGridView1.TotalSummary.Add(Credit);
                        ASPxSummaryItem Debit = new ASPxSummaryItem();
                        Debit.FieldName = "Debit";
                        Debit.DisplayFormat = "{0:#,0.00}";
                        Debit.SummaryType = SummaryItemType.Sum;
                        Debit.ShowInColumn = "Debit";
                        ASPxGridView1.TotalSummary.Add(Debit);
                    }
                }
                else if (formName == "AccountLedgerCostCenterWise")
                {
                    //// Company / GetContactOpeningBalanceList
                    //string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    //string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    //string AccoutnId = Request.QueryString["accountId"] == null ? "" : Request.QueryString["accountId"];
                    //string dateType = Request.QueryString["dateType"] == null ? "" : Request.QueryString["dateType"];
                    //var accountList = Request.QueryString["accounts"] == null ? "" : Request.QueryString["accounts"];
                    //string[] accounts = Request.Form.GetValues("accounts[]");
                    //var AccountDtl = AccountLedgerCostCenterWise.AccountLedgerCostCenterWiseDtl(fromDate, toDate, accountList, dateType, Session["CompanyId"].ToString(), serverAddress);
                    if (Print == "true")
                    {
                        //   XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.LedgerReport.AccountLedgerCostCenterWiseReport(companyInfo, AccountDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        // ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        //incomeStatementRpt.DisplayName = "AccountLedgerCostCenterWise";
                    }
                    else
                    {

                    }

                }
                else if (formName == "CustomerLedgerReport_true" || formName == "CustomerLedgerReport_false")
                {
                    // Company / GetContactOpeningBalanceList
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string AccoutnId = Request.QueryString["accountId"] == null ? "" : Request.QueryString["accountId"];
                    //string Account = Request.QueryString["Account"] == null ? "" : Request.QueryString["Account"];
                    string dateType = Request.QueryString["dateType"] == null ? "" : Request.QueryString["dateType"];
                    formName = formName == "CustomerLedgerReport_true" ? "true" : "false";
                    var CustomerDtl = GetCustomerLedger.CustomerLedger(fromDate, toDate, formName, dateType, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport CustomerLedgerReport = new Noble.Report.Reports.Reports.LedgerReport.CustomerLedgerReport(companyInfo, CustomerDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), formName == "true" ? formName = "Customer Ledger Report" : formName = "Supplier Ledger Report");
                        ASPxWebDocumentViewer1.OpenReport(CustomerLedgerReport);
                        CustomerLedgerReport.DisplayName = "CustomerLedgerReport";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("Code");
                        if (formName == "true")
                        {

                            dt.Columns.Add("CustomerName");
                        }
                        else
                        {

                            dt.Columns.Add("SupplierName");
                        }
                        dt.Columns.Add("AccountCode");
                        dt.Columns.Add("VatNo");
                        dt.Columns.Add("Amount");
                        DataRow row;
                        int i = 1;

                        foreach (var item in CustomerDtl)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["Code"] = item.AccountCode;
                            if (formName == "true")
                            {

                                row["CustomerName"] = item.ContactName;

                            }
                            else
                            {

                                row["SupplierName"] = item.ContactName;
                            }
                            row["AccountCode"] = item.AccountCode;
                            row["VatNo"] = item.VatNo;
                            row["Amount"] = item.Amount.ToString("N2");
                            dt.Rows.Add(row);

                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["Code"].Width = 120;
                        if (formName == "true")
                        {

                            ASPxGridView1.Columns["CustomerName"].Width = 565;
                        }
                        else
                        {
                            ASPxGridView1.Columns["SupplierName"].Width = 565;
                        }
                        ASPxGridView1.Columns["AccountCode"].Width = 120;
                        ASPxGridView1.Columns["VatNo"].Width = 120;
                        ASPxGridView1.Columns["Amount"].Width = 120;

                        ASPxGridView1.TotalSummary.Clear();

                        ASPxSummaryItem TotalSale = new ASPxSummaryItem();
                        ASPxSummaryItem total = new ASPxSummaryItem();
                        total.FieldName = "Amount";
                        total.DisplayFormat = "{0:#,0.00}";
                        total.SummaryType = SummaryItemType.Sum;
                        total.ShowInColumn = "Amount";
                        ASPxGridView1.TotalSummary.Add(total);

                    }


                }
                else if (formName == "AdvanceAccountLedger")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    string isLedger = Request.QueryString["isLedger"] == null ? "" : Request.QueryString["isLedger"];
                    string accountId = Request.QueryString["accountId"] == null ? "" : Request.QueryString["accountId"];
                    string dateType = Request.QueryString["dateType"] == null ? "" : Request.QueryString["dateType"];
                    var ledger = GetAdvanceLedger.GetAdvanceLedgerDtl(fromDate, toDate, accountId, dateType, numberOfPeriods, compareWith, Session["Reporting"].ToString(), serverAddress);
                    if (compareWith != null && compareWith != "" && compareWith != "null")
                    {
                        var Accounts = GetAllAccounts.GetAllAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.LedgerReport.Advance_Ledger_Report.advanceAccountLedger(companyInfo, ledger, compareWith, numberOfPeriods);
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Advance Account Ledger Report" + DateTime.Now;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.LedgerReport.Advance_Ledger_Report.advanceLedgerReport(companyInfo, ledger, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Advance Account Ledger Report" + DateTime.Now;
                    }
                }
                else if (formName == "AdvanceCustomerLedger")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    string IsCustomer = Request.QueryString["IsCustomer"] == null ? "" : Request.QueryString["IsCustomer"];
                    var Customer = GetAdvanceCustomerLedger.GetAdvanceCustomerLedgerDtl(fromDate, toDate, numberOfPeriods, compareWith, IsCustomer, Session["Reporting"].ToString(), serverAddress);
                    if (compareWith != null && compareWith != "" && compareWith != "null")
                    {
                        var Accounts = GetAllAccounts.GetAllAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.LedgerReport.AdvanceCustomerLedgerReport.AdvanceCustomerSupplierComparisonReport(companyInfo, Customer, numberOfPeriods, compareWith, Convert.ToBoolean(IsCustomer));
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Advance Customer Supplier Comparison Report" + DateTime.Now;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.LedgerReport.AdvanceCustomerLedgerReport.AdvanceCustomerSupplierReport(companyInfo, Customer, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), Convert.ToBoolean(IsCustomer));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Advance Customer Supplier Comparison Report" + DateTime.Now;
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