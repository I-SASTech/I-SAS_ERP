using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using Noble.Report.Reports.Invoice.CustomerPayReciept;
using Noble.Report.Reports.Invoice.CustomerPayReciept.AdvanceCustomerPaymentVocher;
using System;
using System.Linq;

namespace Noble.Report.Reports.SalesModuleReports.CustomerPaymentReceipt
{
    public partial class CustomerPaymentReceiptReport : System.Web.UI.Page
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


                string id = Request.QueryString["id"] == null ? null : Request.QueryString["id"];
                bool isFifo = bool.Parse(Request.QueryString["isFifo"] == null ? "false" : Request.QueryString["isFifo"]);
                int openBatch = int.Parse(Request.QueryString["openBatch"] == null || Request.QueryString["openBatch"] == "" ? "0" : Request.QueryString["openBatch"]);
                bool isReturn = bool.Parse(Request.QueryString["isReturn"] == null ? "false" : Request.QueryString["isReturn"]);
                var formName = Request.QueryString["formName"];
                string isDownload = Request.QueryString["isDownload"] == null || Request.QueryString["isDownload"] == "undefined" ? null : Request.QueryString["isDownload"];
                bool colorVariants = bool.Parse(Request.QueryString["colorVariants"] == null ? "false" : Request.QueryString["colorVariants"]);
                bool deliveryChallan = bool.Parse(Request.QueryString["deliveryChallan"] == null ? "false" : Request.QueryString["deliveryChallan"]);
                bool simpleQuery = bool.Parse(Request.QueryString["simpleQuery"] == null ? "false" : Request.QueryString["simpleQuery"]);
                var PageType = Request.QueryString["PageType"];
                bool aging = bool.Parse(Request.QueryString["aging"] == null ? "aging" : Request.QueryString["aging"]);
                var serverAddress = Session["ServerAddress"].ToString() == null ? null : Session["ServerAddress"].ToString();
                var printSetting = GetPrintSetting.PrintDetails(branchId,Session["Sales"].ToString(), serverAddress);
                string Print = Request.QueryString["Print"] == null ? "" : Request.QueryString["Print"];
                var companyInfo = GetCompanyInfo.GetCompanyInfodetials(companyId, Session["Sales"].ToString(), serverAddress);



                var paymentVoucher = GetPaymentVocher.GetPaymentVocherdetials(Guid.Parse(id), Session["Sales"].ToString(), serverAddress);
                if (Convert.ToBoolean(aging))
                {
                    ASPxWebDocumentViewer1.Visible = true;
                    ASPxGridView1.Visible = false;
                    if (PageType == "print")
                    {
                        if (printSetting.PrintTemplate == "Template3")
                        {
                            if (printSetting.IsBlindPrint)
                            {
                                XtraReport report1 = new Templet3(companyInfo, paymentVoucher, printSetting);
                                report1.CreateDocument();
                                PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                                tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                                tool.PrinterSettings.Copies = 1;
                                tool.Print();
                            }
                            else
                            {
                                XtraReport report2 = new Templet3(companyInfo, paymentVoucher, printSetting);

                                ASPxWebDocumentViewer1.OpenReport(report2);
                                report2.DisplayName = paymentVoucher.SaleInvoiceCode + " " + DateTime.Now;
                            }
                        }
                        else if (printSetting.PrintTemplate == "Template2")
                        {
                            if (printSetting.IsBlindPrint)
                            {
                                XtraReport report1 = new Templet2(companyInfo, paymentVoucher, printSetting);
                                report1.CreateDocument();
                                PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                                tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                                tool.PrinterSettings.Copies = 1;
                                tool.Print();
                            }
                            else
                            {
                                XtraReport report2 = new Templet2(companyInfo, paymentVoucher, printSetting);

                                ASPxWebDocumentViewer1.OpenReport(report2);
                                report2.DisplayName = paymentVoucher.SaleInvoiceCode + " " + DateTime.Now;
                            }
                        }
                        else if (printSetting.PrintTemplate == "Template6")
                        {
                            if (printSetting.IsBlindPrint)
                            {
                                XtraReport report1 = new Templet6(companyInfo, paymentVoucher, printSetting);
                                report1.CreateDocument();
                                PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                                tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                                tool.PrinterSettings.Copies = 1;
                                tool.Print();
                            }
                            else
                            {
                                XtraReport report2 = new Templet6(companyInfo, paymentVoucher, printSetting);

                                ASPxWebDocumentViewer1.OpenReport(report2);
                                report2.DisplayName = paymentVoucher.SaleInvoiceCode + " " + DateTime.Now;
                            }
                        }
                        else if (printSetting.PrintTemplate == "Default")
                        {
                            if (printSetting.IsBlindPrint)
                            {
                                XtraReport report1 = new Custom_PaymentReciept(companyInfo, paymentVoucher);
                                report1.CreateDocument();
                                PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                                tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                                tool.PrinterSettings.Copies = 1;
                                tool.Print();
                            }
                            else
                            {
                                XtraReport report2 = new DefaultTemplet(companyInfo, paymentVoucher);

                                ASPxWebDocumentViewer1.OpenReport(report2);
                                report2.DisplayName = paymentVoucher.SaleInvoiceCode + " " + DateTime.Now;
                            }
                        }
                    }
                }
                else
                {
                    if (PageType == "A4")
                    {

                        if (printSetting.IsBlindPrint)
                        {
                            XtraReport report1 = new A5_PaymentVicherList2(companyInfo, printSetting, paymentVoucher);
                            report1.CreateDocument();
                            PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                            tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                            tool.PrinterSettings.Copies = 1;
                            tool.Print();
                        }
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        ASPxWebDocumentViewer1.OpenReport(new A5_PaymentVicherList2(companyInfo, printSetting, paymentVoucher));
                    }
                    else if (PageType == "A5")
                    {
                        if (printSetting.IsBlindPrint)
                        {
                            XtraReport report1 = new A5_paymentVoucherList(companyInfo, printSetting, paymentVoucher);
                            report1.CreateDocument();
                            PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                            tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                            tool.PrinterSettings.Copies = 1;

                            tool.Print();
                        }
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        ASPxWebDocumentViewer1.OpenReport(new A5_paymentVoucherList(companyInfo, printSetting, paymentVoucher));
                    }
                    else if (PageType == "A3")
                    {

                        if (printSetting.IsBlindPrint)
                        {
                            XtraReport report1 = new Custom_PaymentReciept(companyInfo, paymentVoucher);
                            report1.CreateDocument();
                            PrintToolBase tool = new PrintToolBase(report1.PrintingSystem);
                            tool.PrinterSettings.PrinterName = printSetting.PrinterName;
                            tool.PrinterSettings.Copies = 1;
                            tool.Print();
                        }
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        ASPxWebDocumentViewer1.OpenReport(new Custom_PaymentReciept(companyInfo, paymentVoucher));
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