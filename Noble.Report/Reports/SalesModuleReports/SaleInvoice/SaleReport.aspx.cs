using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using Noble.Report.NobleDefaultServices;
using Noble.Report.Reports.Invoice;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Noble.Report.Reports.SalesModuleReports.SaleInvoice
{
    public partial class SaleReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var companyId = Request.QueryString["CompanyId"];
            var branchId = Request.QueryString["BranchId"];

            var myData = GetToken.TokenValue(Guid.Parse(companyId));
            Session["CompanyId"] = myData.Where(x => x.TokenName == "ServerAddress").Select(x => x.CompanyId).FirstOrDefault();
            Session["ServerAddress"] = myData.Where(x => x.Token == "ServerAddress").Select(x => x.TokenName).FirstOrDefault();
            Session["Sales"] = myData.Where(x => x.TokenName == "Sales").Select(x => x.Token).FirstOrDefault();

            var PageType = Request.QueryString["PageType"];
            var isSale = Request.QueryString["isSale"];
            var formName = Request.QueryString["formName"];
            string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
            string report = Request.QueryString["reportName"] == null ? null : Request.QueryString["reportName"];
            string id = Request.QueryString["id"] == null ? null : Request.QueryString["id"];
            bool isFifo = bool.Parse(Request.QueryString["isFifo"] == null ? "false" : Request.QueryString["isFifo"]);
            int openBatch = int.Parse(Request.QueryString["openBatch"] == null || Request.QueryString["openBatch"] == "" ? "0" : Request.QueryString["openBatch"]);
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
            string customerId = Request.QueryString["customerId"] == null ? "" : Request.QueryString["customerId"];
            var companyInfo = GetCompanyInfo.GetCompanyInfodetials(companyId, Session["Sales"].ToString(), serverAddress);

            var saleDetail = GetInvoiceDetails.InvoiceDetails(Guid.Parse(id), isReturn, invoiceNo, isFifo, openBatch, colorVariants, deliveryChallan, simpleQuery, Session["Sales"].ToString(), serverAddress);
            if (printSetting.PrintTemplate == "Template2")
            {
                if (Convert.ToBoolean(isDownload))
                {
                    // Enable CORS
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                    XtraReport report1 = new SA_A4_Template2(companyInfo, printSetting, saleDetail);
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
                else
                {

                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new SA_A4_Template2(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;

                        tool.Print();
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new SA_A4_Template2(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                    }
                }

            }
            else if (printSetting.PrintTemplate == "Template6")
            {

                if (Convert.ToBoolean(isDownload))
                {
                    // Enable CORS
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                    XtraReport report1 = new SA_A4_Template6(companyInfo, printSetting, saleDetail);
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
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new SA_A4_Template6(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                        tool.Print();
                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(saleDetail.Note))
                        {
                            saleDetail.SaleItems.Add(new SaleItemLookupModel
                            {
                                Description = saleDetail.Note

                            });
                        }
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new SA_A4_Template6(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                    }
                }
            }
            else if (printSetting.PrintTemplate == "PK-General A4")
            {

                if (Convert.ToBoolean(isDownload))
                {
                    // Enable CORS
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    XtraReport report1 = new GeneralSalesReport(companyInfo, printSetting, saleDetail);

                    //XtraReport report1 = new SA_A4_Template6(companyInfo, printSetting, saleDetail);
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
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new GeneralSalesReport(companyInfo, printSetting, saleDetail);
                        //XtraReport report1 = new SA_A4_Template6(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                        tool.Print();
                    }

                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        //XtraReport report1 = new SA_A4_Template6(companyInfo, printSetting, saleDetail);
                        XtraReport report1 = new GeneralSalesReport(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                    }
                }
            }
            else if (printSetting.PrintTemplate == "Template3")
            {
                if (Convert.ToBoolean(isDownload))
                {
                    // Enable CORS
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                    XtraReport report1 = new SA_A4_Templet3(companyInfo, saleDetail);
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
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new SA_A4_Templet3(companyInfo, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;

                        tool.Print();
                    }

                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport temp6 = new SA_A4_Templet3(companyInfo, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(temp6);
                        temp6.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                    }
                }
            }
            else if (printSetting.PrintTemplate == "Template4")
            {
                if (Convert.ToBoolean(isDownload))
                {
                    // Enable CORS
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                    XtraReport report1 = new SA_A4_Template4(companyInfo, printSetting, saleDetail);
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
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new SA_A4_Template4(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                        tool.Print();
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new SA_A4_Template4(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report);
                        report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                    }
                }
            }
            else if (printSetting.PrintTemplate == "Default" && printSetting.PrintSize == "A4")
            {
                if (Convert.ToBoolean(isDownload))
                {
                    // Enable CORS
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                    XtraReport report1 = new A4_DefaultTemplet(companyInfo, printSetting, saleDetail);
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
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        XtraReport report1 = new A4_DefaultTemplet(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;

                        tool.Print();
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new A4_DefaultTemplet(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                    }
                }
            }
            else if (printSetting.PrintTemplate == "Template1")
            {
                if (Convert.ToBoolean(isDownload))
                {
                    // Enable CORS
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                    XtraReport report1 = new A4_SA_Template1(companyInfo, printSetting, saleDetail);
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
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new A4_SA_Template1(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new A4_SA_Template1(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                    }
                }
            }
            else if (printSetting.PrintTemplate == "Default" && printSetting.PrintSize == "Thermal")
            {
                if (CounterId != null && CounterId != "00000000-0000-0000-0000-000000000000")
                {
                    var TerminalDtl = GetTerminalDtl.GetTerminalInfo(Guid.Parse(CounterId), companyInfo.Id, Session["Sales"].ToString(), serverAddress);

                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_Default(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = TerminalDtl.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                        tool.Print();
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_Default(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + "_" + saleDetail.CustomerName;
                    }
                }
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_Default(companyInfo, printSetting, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                        tool.Print();
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_Default(companyInfo, printSetting, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                    }
                }
            }
            else if (printSetting.PrintTemplate == "RetailSaTemplate1" && printSetting.PrintSize == "Thermal")
            {
                if (CounterId != null && CounterId != "00000000-0000-0000-0000-000000000000")
                {
                    var TerminalDtl = GetTerminalDtl.GetTerminalInfo(Guid.Parse(CounterId), companyInfo.Id, Session["Sales"].ToString(), serverAddress);
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_RetailSaTemplate1(companyInfo, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = TerminalDtl.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                        tool.Print();
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_RetailSaTemplate1(companyInfo, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + "_" + saleDetail.CustomerName;
                    }
                }
                else
                {
                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_RetailSaTemplate1(companyInfo, saleDetail);
                        report1.CreateDocument();
                        PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                        tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                        tool.PrinterSettings.Copies = 1;
                        tool.Print();
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new TuchInvoice.SA_TP_RetailSaTemplate1(companyInfo, saleDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleDetail.RegistrationNo + "_" + saleDetail.CustomerName;
                    }
                }
            }
        }
    }
}