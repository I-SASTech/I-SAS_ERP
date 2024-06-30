using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using Noble.Report.NobleDefaultServices;
using Noble.Report.Reports.Invoice.ServiceSaleOrder;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Noble.Report.Reports.SalesModuleReports.SaleOrder
{
    public partial class SaleOrderReport : System.Web.UI.Page
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
                bool colorVariants = bool.Parse(Request.QueryString["colorVariants"] == null ? "false" : Request.QueryString["colorVariants"]);
                var formName = Request.QueryString["formName"];
                string report = Request.QueryString["reportName"] == null ? null : Request.QueryString["reportName"];
                string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
                var serverAddress = Session["ServerAddress"].ToString() == null ? null : Session["ServerAddress"].ToString();
                var printSetting = GetPrintSetting.PrintDetails(branchId,Session["Sales"].ToString(), serverAddress);
                string Print = Request.QueryString["Print"] == null ? "" : Request.QueryString["Print"];
                var companyInfo = GetCompanyInfo.GetCompanyInfodetials(companyId, Session["Sales"].ToString(), serverAddress);


                var saleOrderDetail = GetSaleOrder.GetSaleOrderDetail(Guid.Parse(id), isFifo, openBatch, deliveryChallan, Session["Sales"].ToString(), serverAddress);
                if (printSetting.PrintTemplate == "Template2")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new Invoice.ServiceSaleOrder.SaleOrder(companyInfo, printSetting, saleOrderDetail, Convert.ToBoolean(report));
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
                            XtraReport report1 = new Invoice.ServiceSaleOrder.SaleOrder(companyInfo, printSetting, saleOrderDetail, Convert.ToBoolean(report));
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
                            XtraReport report1 = new Invoice.ServiceSaleOrder.SaleOrder(companyInfo, printSetting, saleOrderDetail, Convert.ToBoolean(report));
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = saleOrderDetail.RegistrationNo + " - " + saleOrderDetail.CustomerName;
                        }
                    }
                }
                if (printSetting.PrintTemplate == "Template3")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new SaleOrder_SA_Template1(companyInfo, printSetting, saleOrderDetail);
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

                            XtraReport report1 = new SaleOrder_SA_Template1(companyInfo, printSetting, saleOrderDetail);
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
                            XtraReport report1 = new SaleOrder_SA_Template1(companyInfo, printSetting, saleOrderDetail);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = saleOrderDetail.RegistrationNo + " - " + saleOrderDetail.CustomerName;
                        }
                    }
                }
                //PurchaseMonth
                else if (printSetting.PrintTemplate == "Template4")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new Invoice.ServiceSaleOrder.SaleOrder(companyInfo, printSetting, saleOrderDetail, Convert.ToBoolean(report));
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
                        XtraReport report1 = new Invoice.ServiceSaleOrder.SaleOrder(companyInfo, printSetting, saleOrderDetail, Convert.ToBoolean(report));
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleOrderDetail.RegistrationNo + " - " + saleOrderDetail.CustomerName;
                    }
                }
                else if (printSetting.PrintTemplate == "PK-General A4")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new SaleOrderGeneralTemplate(companyInfo, printSetting, saleOrderDetail);
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
                        XtraReport temp6 = new SaleOrderGeneralTemplate(companyInfo, printSetting, saleOrderDetail);
                        ASPxWebDocumentViewer1.OpenReport(temp6);
                        temp6.DisplayName = saleOrderDetail.RegistrationNo + " - " + saleOrderDetail.CustomerName;
                    }

                }
                else if (printSetting.PrintTemplate == "Template6")
                {
                    if (!string.IsNullOrEmpty(saleOrderDetail.NoteDescription))
                    {
                        saleOrderDetail.SaleOrderItems.Add(new SaleOrderItemLookupModel()
                        {
                            Description = saleOrderDetail.NoteDescription

                        });
                    }


                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new SaleOrder_SATemplate6(companyInfo, printSetting, saleOrderDetail);
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

                        if (!string.IsNullOrEmpty(saleOrderDetail.Note))
                        {
                            saleOrderDetail.SaleOrderItems.Add(new SaleOrderItemLookupModel()
                            {
                                Description = saleOrderDetail.Note
                            });
                        }

                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport temp6 = new SaleOrder_SATemplate6(companyInfo, printSetting, saleOrderDetail);
                        ASPxWebDocumentViewer1.OpenReport(temp6);
                        temp6.DisplayName = saleOrderDetail.RegistrationNo + " - " + saleOrderDetail.CustomerName;
                    }

                }
                else if (printSetting.PrintTemplate == "Default")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new SaleOrderDefaultTemplate(companyInfo, printSetting, saleOrderDetail);
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
                        XtraReport report1 = new SaleOrderDefaultTemplate(companyInfo, printSetting, saleOrderDetail);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleOrderDetail.RegistrationNo + " - " + saleOrderDetail.CustomerName;
                    }
                }
                else if (printSetting.PrintTemplate == "Template1")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new Invoice.ServiceSaleOrder.SaleOrder(companyInfo, printSetting, saleOrderDetail, Convert.ToBoolean(report));
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
                        XtraReport report1 = new Invoice.ServiceSaleOrder.SaleOrder(companyInfo, printSetting, saleOrderDetail, Convert.ToBoolean(report));
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = saleOrderDetail.RegistrationNo + " - " + saleOrderDetail.CustomerName;
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