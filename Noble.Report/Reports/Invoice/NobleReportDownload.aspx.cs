using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.CodeParser;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using Noble.Report.NobleDefaultServices;
using Noble.Report.Reports.Invoice;

namespace Noble.Report.Reports.Invoice
{
    public partial class NobleReportDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var CompanyId = Request.QueryString["CompanyId"];
            if (Session["CompanyId"] == null && Session["CompanyId"] != CompanyId)
            {
                var myData = GetToken.TokenValue(Guid.Parse(CompanyId));

                Session["CompanyId"] = myData.Where(x => x.TokenName == "ServerAddress").Select(x => x.CompanyId).FirstOrDefault();
                Session["ServerAddress"] = myData.Where(x => x.Token == "ServerAddress").Select(x => x.TokenName).FirstOrDefault();
                Session["Sales"] = myData.Where(x => x.TokenName == "Sales").Select(x => x.Token).FirstOrDefault();
                Session["Expenses"] = myData.Where(x => x.TokenName == "Expenses").Select(x => x.Token).FirstOrDefault();
                Session["Other"] = myData.Where(x => x.TokenName == "Other").Select(x => x.Token).FirstOrDefault();
                Session["Setups_And_Configuration"] = myData.Where(x => x.TokenName == "Setups_And_Configuration").Select(x => x.Token).FirstOrDefault();
                Session["Purchase"] = myData.Where(x => x.TokenName == "Purchase").Select(x => x.Token).FirstOrDefault();
                Session["Product_And_Inventory_Management"] = myData.Where(x => x.TokenName == "Product_And_Inventory_Management").Select(x => x.Token).FirstOrDefault();
                Session["WareHouse_Management"] = myData.Where(x => x.TokenName == "WareHouse_Management").Select(x => x.Token).FirstOrDefault();
                Session["HR_And_PayRoll"] = myData.Where(x => x.TokenName == "HR_And_PayRoll").Select(x => x.Token).FirstOrDefault();
                Session["Settings And Permission"] = myData.Where(x => x.TokenName == "Settings And Permission").Select(x => x.Token).FirstOrDefault();
                Session["Accounting"] = myData.Where(x => x.TokenName == "Accounting").Select(x => x.Token).FirstOrDefault();
                Session["DayStart"] = myData.Where(x => x.TokenName == "DayStart").Select(x => x.Token).FirstOrDefault();
                Session["Reporting"] = myData.Where(x => x.TokenName == "Reporting").Select(x => x.Token).FirstOrDefault();
                // use the salesToken value as needed
            }
            var branchId = Request.QueryString["BranchId"];
            var PageType = Request.QueryString["PageType"];
            var isSale = Request.QueryString["isSale"];
            var formName = Request.QueryString["formName"];
            string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
            string report = Request.QueryString["reportName"] == null ? null : Request.QueryString["reportName"];
            string id = Request.QueryString["id"] == null ? null : Request.QueryString["id"];
            bool isFifo = bool.Parse(Request.QueryString["isFifo"] == null ? "false" : Request.QueryString["isFifo"]);
            int openBatch = int.Parse(Request.QueryString["openBatch"] == null ? "0" : Request.QueryString["openBatch"]);
            bool IsDayStart = bool.Parse(Request.QueryString["IsDayStart"] == null || Request.QueryString["IsDayStart"] == "undefined" ? "false" : Request.QueryString["IsDayStart"]);
            string CounterId = Request.QueryString["CounterId"] == null ? null : Request.QueryString["CounterId"];
            bool colorVariants = bool.Parse(Request.QueryString["colorVariants"] == null ? "false" : Request.QueryString["colorVariants"]);
            bool isReturn = bool.Parse(Request.QueryString["isReturn"] == null ? "false" : Request.QueryString["isReturn"]);
            bool isMultiUnit = bool.Parse(Request.QueryString["isMultiUnit"] == null ? "false" : Request.QueryString["isMultiUnit"]);
            bool isReturnView = bool.Parse(Request.QueryString["isReturnView"] == null ? "false" : Request.QueryString["isReturnView"]);
            string invoiceNo = Request.QueryString["invoiceNo"] == null ? "" : Request.QueryString["invoiceNo"];
            bool deliveryChallan = bool.Parse(Request.QueryString["deliveryChallan"] == null ? "false" : Request.QueryString["deliveryChallan"]);
            bool simpleQuery = bool.Parse(Request.QueryString["simpleQuery"] == null ? "false" : Request.QueryString["simpleQuery"]);
            var serverAddress = Session["ServerAddress"].ToString() == null ? null : Session["ServerAddress"].ToString();
            var printSetting = GetPrintSetting.PrintDetails(branchId,Session["Sales"].ToString(), serverAddress);
            string Print = Request.QueryString["Print"] == null ? "" : Request.QueryString["Print"];
            var companyInfo = GetCompanyInfo.GetCompanyInfodetials(CompanyId, Session["Sales"].ToString(), serverAddress);
            if (formName == "ProformaInvoices" || formName == "ServiceProformaInvoice")
            {
                var saleDetail = GetInvoiceDetails.InvoiceDetails(Guid.Parse(id), isReturn, invoiceNo, isFifo, openBatch, colorVariants, deliveryChallan, simpleQuery, Session["Sales"].ToString(), serverAddress);
                if (printSetting.PrintTemplate == "Default")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        XtraReport report1 = new PerformaInvoice.ProformaInvoiceDefaultTemplate(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        using (var stream = new MemoryStream())
                        {
                            report1.SaveLayout(stream);
                            byte[] bytes = stream.ToArray();
                            //var download = bytes;
                        }

                    }
                }

            }
        }
    }
}