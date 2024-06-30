using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Noble.Report.Reports.Stocks
{
    public partial class StocksReport : System.Web.UI.Page
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
                string id = Request.QueryString["id"] == null ? null : Request.QueryString["id"];


                if (formName == "stocktransfer")
                {
                    string isStockReceived = Request.QueryString["isStockReceived"] == null ? "" : Request.QueryString["isStockReceived"];
                    var Stock = GetStockTransfer.GetStockTransferDtl(id, isStockReceived, branchId, Session["Sales"].ToString(), serverAddress);
                    if (printSetting.PrintTemplate == "Template6")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferTemplet6(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferTemplet6(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }

                    }
                    else if (printSetting.PrintTemplate == "Default")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferDefault(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferDefault(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template3")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferTemplet1(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferTemplet1(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }

                    }
                    else if (printSetting.PrintTemplate == "Template2")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferTemplet2(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockTransfer.StockTransferTemplet2(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }

                    }

                }
                else if (formName == "StockRequestReport")
                {
                    var Stock = GetStockResuest.GetStockResuestDtl(id, Session["Sales"].ToString(), serverAddress);
                    if (printSetting.PrintTemplate == "Template6")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockRequest.StockRequestTemplet6(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockRequest.StockRequestTemplet6(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }

                    }
                    else if (printSetting.PrintTemplate == "Default")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockRequest.StockRequestDefault(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockRequest.StockRequestDefault(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template3")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockRequest.StockRequest_SA_Template3(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockRequest.StockRequest_SA_Template3(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }

                    }
                }
                else if (formName == "stockreceived")
                {
                    var Stock = GetStockReceived.GetStockReceivedDtl(id, branchId, Session["Sales"].ToString(), serverAddress);
                    if (printSetting.PrintTemplate == "Template6")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.StockRecievedTemplet6(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.StockRecievedTemplet6(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }

                    }
                    else if (printSetting.PrintTemplate == "Default")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.stockReceivedDefualt(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.stockReceivedDefualt(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template3")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.StockReceivedTemplet3(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.StockReceivedTemplet3(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
                        }

                    }
                    else if (printSetting.PrintTemplate == "Template2")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.StockReceivedTemplet2(companyInfo, Stock, printSetting);
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
                            XtraReport report1 = new Noble.Report.Reports.Invoice.StockReceived.StockReceivedTemplet2(companyInfo, Stock, printSetting);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = Stock.Code + " " + DateTime.Now;
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