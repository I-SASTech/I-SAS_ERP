using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Noble.Report.Reports.SalesModuleReports.DeliveryNote
{
    public partial class DeliveryNoteReport : System.Web.UI.Page
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
                string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
                var isSale = Request.QueryString["isSale"];
                var serverAddress = Session["ServerAddress"].ToString() == null ? null : Session["ServerAddress"].ToString();
                var printSetting = GetPrintSetting.PrintDetails(branchId,Session["Sales"].ToString(), serverAddress);
                string Print = Request.QueryString["Print"] == null ? "" : Request.QueryString["Print"];
                string customerId = Request.QueryString["customerId"] == null ? "" : Request.QueryString["customerId"];
                var companyInfo = GetCompanyInfo.GetCompanyInfodetials(companyId, Session["Sales"].ToString(), serverAddress);



                var DeliveryNoteDetail = GetDeliveryNode.GetDeliveryNodeDetail(Guid.Parse(id), isFifo, openBatch, Convert.ToBoolean(isSale), deliveryChallan, Session["Sales"].ToString(), serverAddress);

                if (printSetting.PrintTemplate == "PK-General A4")
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new DeliveryNoteGeneralA4(companyInfo, printSetting, DeliveryNoteDetail);
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
                            XtraReport report1 = new DeliveryNoteGeneralA4(companyInfo, printSetting, DeliveryNoteDetail);
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
                            XtraReport report1 = new DeliveryNoteGeneralA4(companyInfo, printSetting, DeliveryNoteDetail);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = DeliveryNoteDetail.RegistrationNo + "_" + DeliveryNoteDetail.CustomerNameEn;
                        }
                    }
                }
                else
                {
                    if (Convert.ToBoolean(isDownload))
                    {
                        // Enable CORS
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                        XtraReport report1 = new Noble.Report.Reports.Invoice.DeliveryNote.DeliveryNote(companyInfo, printSetting, DeliveryNoteDetail);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.DeliveryNote.DeliveryNote(companyInfo, printSetting, DeliveryNoteDetail);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.DeliveryNote.DeliveryNote(companyInfo, printSetting, DeliveryNoteDetail);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = DeliveryNoteDetail.RegistrationNo + "_" + DeliveryNoteDetail.CustomerNameEn;
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