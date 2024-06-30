using DevExpress.Data;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Noble.Report.Reports.ReportManagementModule.BalanceReports
{
    public partial class BalanceReports : System.Web.UI.Page
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
                string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];

                if (formName == "Customer" || formName == "Supplier")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string AccoutnId = Request.QueryString["accountId"] == null ? "" : Request.QueryString["accountId"];

                    var VatDtl = GetCustomerBalanceSheet.CustBalanceSheetDtl(fromDate, toDate, AccoutnId, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new Noble.Report.Reports.Reports.BalanceReport.CustomerBalanceReport(companyInfo, VatDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), formName);
                        report1.CreateDocument();
                        using (var stream = new MemoryStream())
                        {
                            report1.ExportToPdf(stream);
                            byte[] bytes = stream.ToArray();
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=Invoice.pdf");
                            Response.BinaryWrite(bytes);
                        }
                    }
                    else if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.BalanceReport.CustomerBalanceReport(companyInfo, VatDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), formName);
                        ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                        incomeStatementRpt.DisplayName = "CustomerBalanceReport";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("DocumentCode");
                        dt.Columns.Add("TransactionType");
                        dt.Columns.Add("Description");
                        dt.Columns.Add("Debit");
                        dt.Columns.Add("Credit");
                        dt.Columns.Add("Balance");

                        DataRow row;
                        int i = 1;
                        foreach (var item in VatDtl.Transactions)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["Date"] = Convert.ToDateTime(item.Date).ToString("dd MMMM yyyy");
                            row["DocumentCode"] = item.DocumentNumber;
                            row["TransactionType"] = item.TransactionType;
                            row["Description"] = item.Description;
                            row["Debit"] = item.DebitAmount.ToString("N2");
                            row["Credit"] = item.CreditAmount.ToString("N2");
                            row["Balance"] = (item.DebitAmount - item.CreditAmount).ToString("N2");
                            dt.Rows.Add(row);
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();

                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["Date"].Width = 120;
                        ASPxGridView1.Columns["DocumentCode"].Width = 120;
                        ASPxGridView1.Columns["TransactionType"].Width = 120;
                        ASPxGridView1.Columns["Description"].Width = 235;
                        ASPxGridView1.Columns["Debit"].Width = 150;
                        ASPxGridView1.Columns["Credit"].Width = 150;
                        ASPxGridView1.Columns["Balance"].Width = 150;

                        ASPxGridView1.Columns["Debit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Debit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Credit"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Balance"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Balance"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;

                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem Debit = new ASPxSummaryItem();
                        Debit.FieldName = "Debit";
                        Debit.DisplayFormat = "{0:#,0.00}";
                        Debit.SummaryType = SummaryItemType.Sum;
                        Debit.ShowInColumn = "Debit";
                        ASPxGridView1.TotalSummary.Add(Debit);
                        ASPxSummaryItem Credit = new ASPxSummaryItem();
                        Credit.FieldName = "Credit";
                        Credit.DisplayFormat = "{0:#,0.00}";
                        Credit.SummaryType = SummaryItemType.Sum;
                        Credit.ShowInColumn = "Credit";
                        ASPxGridView1.TotalSummary.Add(Credit);
                        ASPxSummaryItem Balance = new ASPxSummaryItem();
                        Balance.FieldName = "Balance";
                        Balance.DisplayFormat = "{0:#,0.00}";
                        Balance.SummaryType = SummaryItemType.Sum;
                        Balance.ShowInColumn = "Balance";
                        ASPxGridView1.TotalSummary.Add(Balance);
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