using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using Noble.Report.Models;
using Noble.Report.NobleDefaultServices;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Noble.Report.NobleReportServices;
using Noble.Report.Reports.ProductModule.BOM;

namespace Noble.Report.Reports.Invoice
{

    public partial class A4_DefaultTempletForm : System.Web.UI.Page
    {
        string[] monthNames = new string[]{"","January", "February", "March", "April", "May", "June",
                        "July", "August", "September", "October", "November", "December"};
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var companyId = Request.QueryString["CompanyId"];
                var branchId = Request.QueryString["BranchId"];
                if (Session["CompanyId"] == null && Session["CompanyId"] != companyId)
                {

                    var myData = GetToken.TokenValue(Guid.Parse(companyId));
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
                    Session["Production"] = myData.Where(x => x.TokenName == "Manufacturing And Production_token").Select(x => x.Token).FirstOrDefault();
                }

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

                if(formName == "Bom")
                {
                    var bom = BomDetailsService.GetBomDetails(Guid.Parse(id), Session["Sales"].ToString(), serverAddress);
                    if (printSetting.PrintTemplate == "PK-General A4")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                            XtraReport report1 = new BOMGeneralReport(companyInfo, printSetting, bom);
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
                            XtraReport report1 = new BOMGeneralReport(companyInfo, printSetting, bom);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = bom.Code + " " + DateTime.Now;
                        }

                    }
                }
                else if (formName == "DayEnd")
                {
                    var OpeningBalance = GetOpeningBalance.GetOpeningBalanceDtl(CounterId, Session["Sales"].ToString(), serverAddress);

                    var IsDayStartLookupModel = GetInActiveDay.GetInActiveDayStart(Session["Sales"].ToString(), serverAddress);

                    if (printSetting.IsBlindPrint)
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport report1 = new Noble.Report.Reports.TuchInvoice.SA_Default_DayEnd(companyInfo, printSetting, OpeningBalance, IsDayStartLookupModel);
                        ASPxWebDocumentViewer1.OpenReport(report1);
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
                        XtraReport report1 = new Noble.Report.Reports.TuchInvoice.SA_Default_DayEnd(companyInfo, printSetting, OpeningBalance, IsDayStartLookupModel);
                        ASPxWebDocumentViewer1.OpenReport(report1);
                    }
                }
                else if (formName == "DateWiseTransaction")
                {
                    // Company / GetContactOpeningBalanceList
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string AccoutnId = Request.QueryString["accountId"] == null ? "" : Request.QueryString["accountId"];

                    var VatDtl = GetCustomerBalanceSheet.CustBalanceSheetDtl(fromDate, toDate, AccoutnId, Session["Reporting"].ToString(), serverAddress, branchId);
                    XtraReport incomeStatementRpt = new Noble.Report.Reports.Reports.BalanceReport.CustomerBalanceReport(companyInfo, VatDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), formName);
                    ASPxWebDocumentViewer1.OpenReport(incomeStatementRpt);
                    incomeStatementRpt.DisplayName = "CustomerBalanceReport";

                }
                else if ( formName == "BranchVouchers")
                {
                    var branchVoucher = GetBranchVocher.GetBranchVocherdetials(Guid.Parse(id), Session["Sales"].ToString(), serverAddress);
                    ASPxWebDocumentViewer1.Visible = true;
                    ASPxGridView1.Visible = false;
                    XtraReport report1 = new BranchVoucher.branchVoucher(companyInfo, printSetting, branchVoucher);
                    ASPxWebDocumentViewer1.OpenReport(report1);
                    report1.DisplayName = branchVoucher.VoucherNumber + "_" + branchVoucher.ContactName;
                }
                else if (formName == "JournalVoucher" || formName == "OpeningCash")
                {
                    var JurnelVocher = GetJurnelVocher.GetJurnelVocherDtl(id, Session["Accounting"].ToString(), serverAddress);

                    if (printSetting.PrintTemplate == "Default")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new Invoice.JournalVoucher.A4_DefaultTemplet(companyInfo, printSetting, JurnelVocher, formName);
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
                                XtraReport report1 = new Invoice.JournalVoucher.A4_DefaultTemplet(companyInfo, printSetting, JurnelVocher, formName);
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
                                XtraReport report1 = new Invoice.JournalVoucher.A4_DefaultTemplet(companyInfo, printSetting, JurnelVocher, formName);
                                ASPxWebDocumentViewer1.OpenReport(report1);
                                report1.DisplayName = JurnelVocher.VoucherNumber + "_" + JurnelVocher.Date;

                            }
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template6")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new Invoice.JournalVoucher.SA_A4_Template6(companyInfo, printSetting, JurnelVocher, formName);
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
                            XtraReport report1 = new Invoice.JournalVoucher.SA_A4_Template6(companyInfo, printSetting, JurnelVocher, formName);
                            ASPxWebDocumentViewer1.OpenReport(report1);
                            report1.DisplayName = JurnelVocher.VoucherNumber + "_" + JurnelVocher.Date;
                        }

                    }
                    else if (printSetting.PrintTemplate == "Template2")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new Invoice.JournalVoucher.SA_A4_Template2(companyInfo, printSetting, JurnelVocher, formName);
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
                                XtraReport report1 = new Invoice.JournalVoucher.SA_A4_Template2(companyInfo, printSetting, JurnelVocher, formName);
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
                                XtraReport report1 = new Invoice.JournalVoucher.SA_A4_Template2(companyInfo, printSetting, JurnelVocher, formName);
                                ASPxWebDocumentViewer1.OpenReport(report1);
                                report1.DisplayName = JurnelVocher.VoucherNumber + "_" + JurnelVocher.Date;
                            }
                        }
                    }
                    else if (printSetting.PrintTemplate == "Template1")
                    {
                        if (Convert.ToBoolean(isDownload))
                        {
                            // Enable CORS
                            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                            XtraReport report1 = new Invoice.JournalVoucher.A4_SA_Template1(companyInfo, printSetting, JurnelVocher, formName);
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
                                XtraReport report1 = new Invoice.JournalVoucher.A4_SA_Template1(companyInfo, printSetting, JurnelVocher, formName);
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
                                XtraReport report1 = new Invoice.JournalVoucher.A4_SA_Template1(companyInfo, printSetting, JurnelVocher, formName);
                                ASPxWebDocumentViewer1.OpenReport(report1);
                            }
                        }
                    }
                }
                else if (formName == "MultiBarcodePrinting")
                {
                    ASPxGridView1.Visible = false;
                    ASPxWebDocumentViewer1.Visible = true;
                    string printerName = Request.QueryString["printerName"] == null || Request.QueryString["printerName"] == "undefined" ? null : Request.QueryString["printerName"];
                    var barcode = Request.QueryString["barcode"] == null || Request.QueryString["barcode"] == "undefined" ? null : Request.QueryString["barcode"];
                    string barcodeVal = Request.QueryString["barcodeVal"] == null || Request.QueryString["barcodeVal"] == "undefined" ? null : Request.QueryString["barcodeVal"];
                    var data = JsonConvert.DeserializeObject<barCodeData>(barcode);
                    XtraReport report1 = new Report.Reports.PrintBarcode.barCode(barcode, data, companyInfo);
                    report1.CreateDocument();
                    PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                    tool.PrinterSettings.PrinterName = data.printerName;
                    tool.PrinterSettings.Copies = Convert.ToInt16(data.value);
                    tool.Print();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
