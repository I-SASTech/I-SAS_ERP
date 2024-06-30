using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using Noble.Report.NobleDefaultServices;
using Noble.Report.Reports.Invoice.CustomerPO;
using Noble.Report.Reports.Invoice.PerformaInvoice;
using Noble.Report.Reports.SalesModuleReports.SaleInvoice;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Noble.Report.Reports.SalesModuleReports.ProformaInvoice
{
    public partial class ProformaInvoiceReport : System.Web.UI.Page
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

                string id = Request.QueryString["id"] == null ? null : Request.QueryString["id"];
                bool isFifo = bool.Parse(Request.QueryString["isFifo"] == null ? "false" : Request.QueryString["isFifo"]);
                int openBatch = int.Parse(Request.QueryString["openBatch"] == null || Request.QueryString["openBatch"] == "" ? "0" : Request.QueryString["openBatch"]);
                bool isReturn = bool.Parse(Request.QueryString["isReturn"] == null ? "false" : Request.QueryString["isReturn"]);
                bool deliveryChallan = bool.Parse(Request.QueryString["deliveryChallan"] == null ? "false" : Request.QueryString["deliveryChallan"]);
                bool simpleQuery = bool.Parse(Request.QueryString["simpleQuery"] == null ? "false" : Request.QueryString["simpleQuery"]);
                string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
                var formName = Request.QueryString["formName"];
                string invoiceNo = Request.QueryString["invoiceNo"] == null ? "" : Request.QueryString["invoiceNo"];
                bool colorVariants = bool.Parse(Request.QueryString["colorVariants"] == null ? "false" : Request.QueryString["colorVariants"]);

                var serverAddress = Session["ServerAddress"].ToString() == null ? null : Session["ServerAddress"].ToString();
                var printSetting = GetPrintSetting.PrintDetails(branchId,Session["Sales"].ToString(), serverAddress);
                string Print = Request.QueryString["Print"] == null ? "" : Request.QueryString["Print"];
                var companyInfo = GetCompanyInfo.GetCompanyInfodetials(companyId, Session["Sales"].ToString(), serverAddress);

                if (formName == "CustomerPurchaseOrder")
                {
                    var saleDetail = GetInvoiceDetails.InvoiceDetails(Guid.Parse(id), isReturn, invoiceNo, isFifo, openBatch, colorVariants, deliveryChallan, simpleQuery, Session["Sales"].ToString(), serverAddress);

                    if (printSetting.PrintTemplate == "Default")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new CustomerPO_SA_Default_Templet(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new CustomerPO_SA_Default_Templet(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new CustomerPO_SA_Default_Templet(companyInfo, printSetting, saleDetail);
                                ASPxWebDocumentViewer1.OpenReport(report1);
                                report1.DisplayName = saleDetail.RefrenceNo + "_" + saleDetail.CustomerName;
                            }
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template6")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new ProformaInvoicesTemplate6(companyInfo, printSetting, saleDetail);
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
                            XtraReport temp6 = new ProformaInvoicesTemplate6(companyInfo, printSetting, saleDetail);
                            ASPxWebDocumentViewer1.OpenReport(temp6);
                            temp6.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                        }
                    }
                    else if (printSetting.PrintTemplate == "PK-General A4")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new GeneralSalesReport(companyInfo, printSetting, saleDetail);
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
                            XtraReport temp6 = new GeneralSalesReport(companyInfo, printSetting, saleDetail);
                            ASPxWebDocumentViewer1.OpenReport(temp6);
                            temp6.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new CustomerPO_SA_Template1(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new CustomerPO_SA_Template1(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new CustomerPO_SA_Template1(companyInfo, printSetting, saleDetail);
                                ASPxWebDocumentViewer1.OpenReport(report1);
                                report1.DisplayName = saleDetail.RefrenceNo + "_" + saleDetail.CustomerName;
                            }
                        }
                    }
                }
                else
                {
                    var saleDetail = GetInvoiceDetails.InvoiceDetails(Guid.Parse(id), isReturn, invoiceNo, isFifo, openBatch, colorVariants, deliveryChallan, simpleQuery, Session["Sales"].ToString(), serverAddress);


                    if (printSetting.PrintTemplate == "Default")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new ProformaInvoiceDefaultTemplate(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new ProformaInvoiceDefaultTemplate(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new ProformaInvoiceDefaultTemplate(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new GeneralSalesReport(companyInfo, printSetting, saleDetail);
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
                                XtraReport temp6 = new GeneralSalesReport(companyInfo, printSetting, saleDetail);
                                ASPxWebDocumentViewer1.OpenReport(temp6);
                                temp6.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                            }
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template6")
                    {
                        if (!string.IsNullOrEmpty(saleDetail.Note))
                        {
                            saleDetail.SaleItems.Add(new SaleItemLookupModel
                            {
                                Description = saleDetail.Note

                            });
                        }
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new ProformaInvoicesTemplate6(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new ProformaInvoicesTemplate6(companyInfo, printSetting, saleDetail);
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
                                if (!string.IsNullOrEmpty(saleDetail.Note))
                                {
                                    saleDetail.SaleItems.Add(new SaleItemLookupModel
                                    {
                                        Description = saleDetail.Note

                                    });
                                }
                                XtraReport temp6 = new ProformaInvoicesTemplate6(companyInfo, printSetting, saleDetail);
                                ASPxWebDocumentViewer1.OpenReport(temp6);
                                temp6.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                            }
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template1")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new ProformaInvoice_SA_Template1(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new ProformaInvoice_SA_Template1(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new ProformaInvoice_SA_Template1(companyInfo, printSetting, saleDetail);
                                ASPxWebDocumentViewer1.OpenReport(report1);
                                report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new Invoice.PerformaInvoice.PerformaInvoice(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new Invoice.PerformaInvoice.PerformaInvoice(companyInfo, printSetting, saleDetail);
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
                                XtraReport report1 = new Invoice.PerformaInvoice.PerformaInvoice(companyInfo, printSetting, saleDetail);
                                ASPxWebDocumentViewer1.OpenReport(report1);
                                report1.DisplayName = saleDetail.RegistrationNo + " - " + saleDetail.CustomerName;
                            }
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