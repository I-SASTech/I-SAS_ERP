using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using Noble.Report.Reports.Invoice.PurchaseOrder;
using System;
using System.IO;
using System.Linq;
using System.Web;
namespace Noble.Report.Reports.PurchaseModule.PurchaseOrder
{
    public partial class PurchaseOrderReport : System.Web.UI.Page
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


                var formName = Request.QueryString["formName"];
                string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
                string id = Request.QueryString["id"] == null ? null : Request.QueryString["id"];
                bool isFifo = bool.Parse(Request.QueryString["isFifo"] == null ? "false" : Request.QueryString["isFifo"]);
                int openBatch = int.Parse(Request.QueryString["openBatch"] == null || Request.QueryString["openBatch"] == "" ? "0" : Request.QueryString["openBatch"]);
                bool colorVariants = bool.Parse(Request.QueryString["colorVariants"] == null ? "false" : Request.QueryString["colorVariants"]);
                bool isReturn = bool.Parse(Request.QueryString["isReturn"] == null ? "false" : Request.QueryString["isReturn"]);
                bool deliveryChallan = bool.Parse(Request.QueryString["deliveryChallan"] == null ? "false" : Request.QueryString["deliveryChallan"]);
                bool simpleQuery = bool.Parse(Request.QueryString["simpleQuery"] == null ? "false" : Request.QueryString["simpleQuery"]);
                bool isMultiUnit = bool.Parse(Request.QueryString["isMultiUnit"] == null ? "false" : Request.QueryString["isMultiUnit"]);
                bool isReturnView = bool.Parse(Request.QueryString["isReturnView"] == null ? "false" : Request.QueryString["isReturnView"]);
                var serverAddress = Session["ServerAddress"].ToString() == null ? null : Session["ServerAddress"].ToString();
                var printSetting = GetPrintSetting.PrintDetails(branchId,Session["Sales"].ToString(), serverAddress);
                string Print = Request.QueryString["Print"] == null ? "" : Request.QueryString["Print"];
                var companyInfo = GetCompanyInfo.GetCompanyInfodetials(companyId, Session["Sales"].ToString(), serverAddress);



                var purchaseOrderDetails = GetPurchaseOrder.GetPurchaseorderDetails(Guid.Parse(id), isMultiUnit, Session["Purchase"].ToString(), serverAddress);

                if (printSetting.PrintTemplate == "Default")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new PurchaseOrderDefaultTemplate(companyInfo, printSetting, purchaseOrderDetails);
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
                            XtraReport report1 = new PurchaseOrderDefaultTemplate(companyInfo, printSetting, purchaseOrderDetails);
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
                            XtraReport report1 = new PurchaseOrderDefaultTemplate(companyInfo, printSetting, purchaseOrderDetails);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = purchaseOrderDetails.RegistrationNo + "_" + purchaseOrderDetails.SupplierId;

                        }
                    }
                }
                else if (printSetting.PrintTemplate == "Template6")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new PurchaseOrderTemplate6(companyInfo, printSetting, purchaseOrderDetails);
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
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport temp6 = new PurchaseOrderTemplate6(companyInfo, printSetting, purchaseOrderDetails);
                        ASPxWebDocumentViewer1.OpenReport(temp6);
                        temp6.DisplayName = purchaseOrderDetails.RegistrationNo + " - " + purchaseOrderDetails.SupplierId;
                    }
                }
                else if (printSetting.PrintTemplate == "PK-General A4")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new PurchaseOrderGeneralTemplate(companyInfo, printSetting, purchaseOrderDetails);
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
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport temp6 = new PurchaseOrderGeneralTemplate(companyInfo, printSetting, purchaseOrderDetails);
                        ASPxWebDocumentViewer1.OpenReport(temp6);
                        temp6.DisplayName = purchaseOrderDetails.RegistrationNo + " - " + purchaseOrderDetails.SupplierId;
                    }
                }
                else
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new Invoice.PurchaseOrder.PurchaseOrder(companyInfo, printSetting, purchaseOrderDetails);
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
                            XtraReport report1 = new Invoice.PurchaseOrder.PurchaseOrder(companyInfo, printSetting, purchaseOrderDetails);
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
                            XtraReport report1 = new Invoice.PurchaseOrder.PurchaseOrder(companyInfo, printSetting, purchaseOrderDetails);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = purchaseOrderDetails.RegistrationNo + "_" + purchaseOrderDetails.SupplierName;
                        }
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