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
using Noble.Report.NobleReportServices;

namespace Noble.Report.Reports.ReportManagementModule.AccountFinanceReports
{
    public partial class AccountFinanceReports : System.Web.UI.Page
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


                if (formName == "TrialBalanceReport")
                {

                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];

                    var trailbalanceDetail = GetTrailBalanceDetail.TrailBalanceTreeDtl(fromDate, toDate, companyId, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport trailbalanceDTL = new Noble.Report.Reports.Reports.Account_FinanceReport.TrailBalanceReport(companyInfo, trailbalanceDetail, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(trailbalanceDTL);
                        trailbalanceDTL.DisplayName = "Trail Balance Detail";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("Code");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("DetailName");
                        dt.Columns.Add("Debit");
                        dt.Columns.Add("Credit");
                        dt.Columns.Add("Total");
                        DataRow row;
                        int i = 1;
                        foreach (var item in trailbalanceDetail)
                        {
                            foreach (var val in item.TrialBalanceModel)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["DetailName"] = item.Name;
                                row["Code"] = item.Code;
                                row["Name"] = val.AccountName;
                                row["Debit"] = val.Debit.ToString("N2");
                                row["Credit"] = val.Credit.ToString("N2");
                                row["Total"] = val.Total.ToString("N2");
                                dt.Rows.Add(row);
                            }

                        }

                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["DetailName"]);
                        ASPxGridView1.ExpandAll();
                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["Code"].Width = 120;
                        ASPxGridView1.Columns["Name"].Width = 510;
                        ASPxGridView1.Columns["Debit"].Width = 145;
                        ASPxGridView1.Columns["Credit"].Width = 120;
                        ASPxGridView1.Columns["Total"].Width = 120;
                        ASPxGridView1.Columns["Debit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Debit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Total = new ASPxSummaryItem();
                        Total.FieldName = "Total";
                        Total.DisplayFormat = "{0:#,0.00}";
                        Total.SummaryType = SummaryItemType.Sum;
                        Total.ShowInColumn = "Total";
                        ASPxGridView1.TotalSummary.Add(Total);
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
                        ASPxGridView1.Settings.ColumnMinWidth = 30;
                    }
                }
                else if (formName == "TrialBalanceAccountReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];

                    var TrailBalanceAccount = GetTrailAccount.TrialBalanceAccountDtl(fromDate, toDate, companyId, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.Account_FinanceReport.TrailBalanceAccountReport(companyInfo, TrailBalanceAccount, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Trail Balance Account";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("Code");
                        dt.Columns.Add("AccountName");
                        dt.Columns.Add("AccoutnType");
                        dt.Columns.Add("CostCenter");
                        dt.Columns.Add("Debit");
                        dt.Columns.Add("Credit");
                        dt.Columns.Add("Total");
                        DataRow row;
                        int i = 1;
                        foreach (var item in TrailBalanceAccount)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["Code"] = item.AccountCode;
                            row["AccountName"] = item.AccountName;
                            row["AccoutnType"] = item.AccountType;
                            row["CostCenter"] = item.CostCenter;
                            row["Debit"] = item.Debit.ToString("N2");
                            row["Credit"] = item.Credit.ToString("N2");
                            row["Total"] = item.Total.ToString("N2");
                            dt.Rows.Add(row);
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["AccoutnType"]);
                        ASPxGridView1.ExpandAll();

                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["Code"].Width = 100;
                        ASPxGridView1.Columns["AccountName"].Width = 305;
                        ASPxGridView1.Columns["CostCenter"].Width = 250;
                        ASPxGridView1.Columns["Debit"].Width = 120;
                        ASPxGridView1.Columns["Credit"].Width = 120;
                        ASPxGridView1.Columns["Total"].Width = 120;
                        ASPxGridView1.Columns["Debit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Debit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Total = new ASPxSummaryItem();
                        Total.FieldName = "Total";
                        Total.DisplayFormat = "{0:#,0.00}";
                        Total.SummaryType = SummaryItemType.Sum;
                        Total.ShowInColumn = "Total";
                        ASPxGridView1.TotalSummary.Add(Total);
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
                        ASPxGridView1.Settings.ColumnMinWidth = 30;

                    }
                }
                else if (formName == "TrialBalanceTreeReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];

                    var TrailBalance = GetTrailBalance.TrialLookUpDtl(fromDate, toDate, companyId, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.Account_FinanceReport.TrailBalanceDetailReport(companyInfo, TrailBalance, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Trail Balance";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("TrailBalanceName");
                        dt.Columns.Add("CostCenter");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("Debit");
                        dt.Columns.Add("Credit");
                        dt.Columns.Add("Total");
                        DataRow row;
                        int i = 1;


                        foreach (var Trail in TrailBalance)
                        {
                            foreach (var CostCenter in Trail.CostCenterLookUpModel)
                            {

                                foreach (var TBalance in CostCenter.TrialBalanceModel)
                                {
                                    row = dt.NewRow();
                                    row["TrailBalanceName"] = Trail.Name;
                                    row["CostCenter"] = CostCenter.AccountName;
                                    row["Name"] = TBalance.AccountName + " " + TBalance.AccountNameArabic;
                                    row["Debit"] = TBalance.Debit;
                                    row["Credit"] = TBalance.Credit;
                                    row["Total"] = TBalance.Total;
                                    dt.Rows.Add(row);
                                }
                            }
                        }

                        DataTable groupedDataTable = new DataTable();
                        groupedDataTable.Columns.Add("TrailBalanceName");
                        groupedDataTable.Columns.Add("CostCenter");
                        groupedDataTable.Columns.Add("Name");
                        groupedDataTable.Columns.Add("Debit");
                        groupedDataTable.Columns.Add("Credit");
                        groupedDataTable.Columns.Add("Total");

                        var groupedRows = dt.AsEnumerable().GroupBy(x => x["Name"]).Select(group => new
                        {
                            Name = group.Key.ToString(),
                            CostCenterName = group.FirstOrDefault()["CostCenter"],
                            TrailBalanceName = group.FirstOrDefault()["TrailBalanceName"],
                            DebitSum = group.Sum(x => Convert.ToDecimal(x["Debit"])),
                            CreditSum = group.Sum(x => Convert.ToDecimal(x["Credit"])),
                            TotalSum = group.Sum(x => Convert.ToDecimal(x["Total"])),
                        }).ToList();
                        dt.Clear();
                        dt.Dispose();
                        foreach (var group in groupedRows)
                        {
                            DataRow newRow = groupedDataTable.NewRow();
                            newRow["TrailBalanceName"] = group.TrailBalanceName;
                            newRow["CostCenter"] = group.CostCenterName;
                            newRow["Name"] = group.Name;
                            newRow["Debit"] = group.DebitSum;
                            newRow["Credit"] = group.CreditSum;
                            newRow["Total"] = group.TotalSum;
                            groupedDataTable.Rows.Add(newRow);
                        }


                        ASPxGridView1.DataSource = groupedDataTable;
                        ASPxGridView1.KeyFieldName = "Name";
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["TrailBalanceName"]);
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["CostCenter"]);
                        ASPxGridView1.ExpandAll();
                        ASPxGridView1.Columns["Debit"].Width = 120;
                        ASPxGridView1.Columns["Credit"].Width = 120;
                        ASPxGridView1.Columns["Total"].Width = 120;
                        ASPxGridView1.Columns["Name"].Width = 665;
                        ASPxGridView1.Columns["Debit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Debit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Total"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Total = new ASPxSummaryItem();
                        Total.FieldName = "Total";
                        Total.DisplayFormat = "{0:#,0.00}";
                        Total.SummaryType = SummaryItemType.Sum;
                        Total.ShowInColumn = "Total";
                        ASPxGridView1.TotalSummary.Add(Total);
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
                        ASPxGridView1.Settings.ColumnMinWidth = 30;
                    }
                }
                else if (formName == "BalanceSheetReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];

                    var BalanceSheetDtl = GetBalanceSheet.BalanceSheetDtlt(fromDate, toDate, companyId, Session["Reporting"].ToString(), serverAddress);

                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.Account_FinanceReport.BalanceSheetReport(companyInfo, BalanceSheetDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Balance Sheet Report";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("Code");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("Account");
                        dt.Columns.Add("Amount");
                        DataRow row;
                        int i = 1;
                        foreach (var item in BalanceSheetDtl.Asset)
                        {
                            row = dt.NewRow();
                            row["Code"] = item.Code;
                            row["Account"] = item.AccountType;
                            row["Name"] = item.CostCenter;
                            row["Amount"] = item.Amount < 0 ? "(" + (item.Amount * -1).ToString("N2") + ")" : item.Amount.ToString("N2");
                            dt.Rows.Add(row);
                        }
                        foreach (var item in BalanceSheetDtl.Liability)
                        {
                            row = dt.NewRow();
                            row["Code"] = item.Code;
                            row["Account"] = item.AccountType;
                            row["Name"] = item.CostCenter;
                            row["Amount"] = item.Amount < 0 ? "(" + (item.Amount * -1).ToString("N2") + ")" : item.Amount.ToString("N2");
                            dt.Rows.Add(row);
                        }
                        foreach (var item in BalanceSheetDtl.Equity)
                        {
                            row = dt.NewRow();
                            row["Code"] = item.Code;
                            row["Account"] = item.AccountType;
                            row["Name"] = item.CostCenter;
                            row["Amount"] = item.Amount < 0 ? "(" + (item.Amount * -1).ToString("N2") + ")" : item.Amount.ToString("N2");
                            dt.Rows.Add(row);
                        }

                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.KeyFieldName = "Name";
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["Account"]);
                        ASPxGridView1.ExpandAll();
                        ASPxGridView1.Columns["Code"].Width = 120;
                        ASPxGridView1.Columns["Name"].Width = 450;
                        ASPxGridView1.Columns["Amount"].Width = 450;
                        ASPxGridView1.Columns["Amount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Amount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Amount = new ASPxSummaryItem();
                        Amount.FieldName = "Amount";
                        Amount.DisplayFormat = "{0:#,0.00}";
                        Amount.SummaryType = SummaryItemType.Sum;
                        Amount.ShowInColumn = "Amount";
                        ASPxGridView1.TotalSummary.Add(Amount);
                    }
                }
                else if (formName == "AdvanceBalanceSheetReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    var BalanceSheet = GetAdvanceBalanceSheet.GetAdvanceBalanceSheetDtl(fromDate, toDate, numberOfPeriods, compareWith, Session["Reporting"].ToString(), serverAddress);
                    if (compareWith != null && compareWith != "" && compareWith != "null")
                    {
                        var Accounts = GetAccounts.GetAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.Advance_Account_Finance_Comparison(companyInfo, BalanceSheet, Accounts, numberOfPeriods, compareWith);
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Advance BalanceSheet Report" + DateTime.Now;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceBalanceSheet(companyInfo, BalanceSheet, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Advance BalanceSheet Report" + DateTime.Now;
                    }
                }
                else if (formName == "MonthlyAdvanceIcomeStatementReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    var BalanceSheet = GetAdvanceIncomeStatement.GetAdvanceIncomeStatementDtl(fromDate, toDate, numberOfPeriods, compareWith, Session["Reporting"].ToString(), serverAddress);
                    if (compareWith != null && compareWith != "" && compareWith != "null")
                    {
                        var Accounts = GetAccounts.GetAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceIncomeStatement.AdvanceIncomeStatementComparison(companyInfo, BalanceSheet, Accounts, numberOfPeriods, compareWith);
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Monthly Advance Icome Statement Report" + DateTime.Now;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceIncomeStatement.AdvanceIncomeStatement(companyInfo, BalanceSheet, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Monthly Advance Icome Statement Report" + DateTime.Now;
                    }
                }
                else if (formName == "AdvanceTrialBalance")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    var trailBalance = GetAdvanceTrailBalance.GetAdvanceTrailBalanceDtl(fromDate, toDate, numberOfPeriods, compareWith, Session["Reporting"].ToString(), serverAddress);
                    if (compareWith != null && compareWith != "" && compareWith != "null")
                    {
                        var Accounts = GetAllAccounts.GetAllAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalance.AdvanceTrialBalanceComparison(companyInfo, trailBalance, compareWith, numberOfPeriods);
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Advance Trail Balance Report" + DateTime.Now;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalance.AdvanceTrialBalance(companyInfo, trailBalance, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Advance Trail Balance Report" + DateTime.Now;
                    }
                }
                else if (formName == "AdvanceTrialBalanceAccount")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    var trailBalance = GetAdvanceTrailBalanceAccount.GetAdvanceTrailBalanceDtl(fromDate, toDate, numberOfPeriods, compareWith, Session["Reporting"].ToString(), serverAddress);
                    if (compareWith != null && compareWith != "" && compareWith != "null")
                    {
                        var Accounts = GetAllAccounts.GetAllAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalanceAccount.advanceTrailBalanceAccountComparison(companyInfo, trailBalance, Accounts, numberOfPeriods, compareWith);
                        ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                        ComparisonBalanceSheetReport.DisplayName = "Advance Trail Balance Report" + DateTime.Now;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalanceAccount.advanceTrailBalanceAccount(companyInfo, trailBalance, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                        BalanceSheetReport.DisplayName = "Advance Trail Balance Report" + DateTime.Now;
                    }
                }
                else if (formName == "IncomeStatementReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];

                    var incomeStatement = GetIncomeStatement.IncomeStatementListDtl(fromDate, toDate, companyId, Session["Reporting"].ToString(), serverAddress);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.Account_FinanceReport.IncomeStatement(companyInfo, incomeStatement, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "Income Statement";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("Code");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("Status");
                        DataRow row;
                        int i = 1;
                        foreach (var item in incomeStatement.Income)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["Code"] = item.Code;
                            row["Amount"] = item.Amount.ToString("N2");
                            row["Status"] = "Revenue";
                            dt.Rows.Add(row);
                        }

                        var dt1 = new DataTable();
                        dt1.Columns.Add("#");
                        dt1.Columns.Add("Code");
                        dt1.Columns.Add("Amount");
                        dt1.Columns.Add("Status");
                        DataRow row1;
                        foreach (var item in incomeStatement.Expenses)
                        {
                            row1 = dt1.NewRow();
                            row1["#"] = i++;
                            row1["Code"] = item.Code;
                            row1["Amount"] = item.Amount.ToString("N2");
                            row1["Status"] = "Expenses";
                            dt1.Rows.Add(row1);
                        }
                        dt.Merge(dt1);
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.GroupBy(ASPxGridView1.Columns["Status"]);
                        ASPxGridView1.ExpandAll();
                        ASPxGridView1.Columns["Status"].Visible = false;
                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["Code"].Width = 515;
                        ASPxGridView1.Columns["Amount"].Width = 500;
                        ASPxGridView1.Columns["Amount"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Amount"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Amount = new ASPxSummaryItem();
                        Amount.FieldName = "Amount";
                        Amount.DisplayFormat = "{0:#,0.00}";
                        Amount.SummaryType = SummaryItemType.Sum;
                        Amount.ShowInColumn = "Amount";
                        ASPxGridView1.TotalSummary.Add(Amount);
                        ASPxGridView1.Settings.ColumnMinWidth = 30;
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