using DevExpress.Data;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using Noble.Report.NobleDefaultServices;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Noble.Report.Reports.ReportManagementModule.InventoryReports
{
    public partial class InventoryReports : System.Web.UI.Page
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

                if (formName == "StockReport")
                {

                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string warehouseId = Request.QueryString["warehouseId"] == null ? "" : Request.QueryString["warehouseId"];
                    string search = Request.QueryString["search"] == null ? "" : Request.QueryString["search"];

                    var comparison = GetStock.GetStockDtl(fromDate, toDate, warehouseId, search, Session["Reporting"].ToString(), serverAddress);

                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        var report1 = new Noble.Report.Reports.Reports.InventoryReport.StockReport(companyInfo, comparison, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(report1);
                        report1.DisplayName = "Stock Report" + DateTime.Now.ToString("dd MMMM yyyy");

                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("ProducName");
                        dt.Columns.Add("Units");
                        dt.Columns.Add("QuantityIn");
                        dt.Columns.Add("QuantityOut");
                        dt.Columns.Add("AveragePrice");
                        DataRow row;
                        int i = 1;

                        foreach (var item in comparison.InventoryLook)
                        {
                            row = dt.NewRow();
                            row["#"] = i++;
                            row["ProducName"] = item.ProductName + " " + item.ProductNameArabic;
                            row["Units"] = Convert.ToInt32(item.UnitPerPack);
                            row["QuantityIn"] = Convert.ToInt32(item.QuantityIn);
                            row["QuantityOut"] = Convert.ToInt32(item.QuantityOut);
                            row["AveragePrice"] = item.AvgPrice.ToString("N2");
                            dt.Rows.Add(row);
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.Columns["#"].Width = 40;
                        ASPxGridView1.Columns["ProducName"].Width = 900;
                        ASPxGridView1.Columns["Units"].Width = 110;
                        ASPxGridView1.Columns["QuantityIn"].Width = 110;
                        ASPxGridView1.Columns["QuantityOut"].Width = 110;
                        ASPxGridView1.Columns["AveragePrice"].Width = 150;
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.Columns["Units"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["Units"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["QuantityIn"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["QuantityIn"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["QuantityOut"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["QuantityOut"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["AveragePrice"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.Columns["AveragePrice"].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                        ASPxGridView1.TotalSummary.Clear();
                        ASPxSummaryItem AveragePrice = new ASPxSummaryItem();
                        AveragePrice.FieldName = "AveragePrice";
                        AveragePrice.DisplayFormat = "{0:#,0.00}";
                        AveragePrice.SummaryType = SummaryItemType.Sum;
                        AveragePrice.ShowInColumn = "AveragePrice";
                        ASPxGridView1.TotalSummary.Add(AveragePrice);
                        ASPxSummaryItem QuantityOut = new ASPxSummaryItem();
                        QuantityOut.FieldName = "QuantityOut";
                        QuantityOut.DisplayFormat = "{0:#,0.00}";
                        QuantityOut.SummaryType = SummaryItemType.Sum;
                        QuantityOut.ShowInColumn = "QuantityOut";
                        ASPxGridView1.TotalSummary.Add(QuantityOut);
                        ASPxSummaryItem QuantityIn = new ASPxSummaryItem();
                        QuantityIn.FieldName = "QuantityIn";
                        QuantityIn.DisplayFormat = "{0:#,0.00}";
                        QuantityIn.SummaryType = SummaryItemType.Sum;
                        QuantityIn.ShowInColumn = "QuantityIn";
                        ASPxGridView1.TotalSummary.Add(QuantityIn);
                        ASPxSummaryItem Units = new ASPxSummaryItem();
                        Units.FieldName = "Units";
                        Units.DisplayFormat = "{0:#,0.00}";
                        Units.SummaryType = SummaryItemType.Sum;
                        Units.ShowInColumn = "Units";
                        ASPxGridView1.TotalSummary.Add(Units);

                    }

                }
                else if (formName == "ProductComparisionReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string wareHouseId = Request.QueryString["wareHouseId"] == null ? "" : Request.QueryString["wareHouseId"];

                    var ProductInventoryDtl = GetProductInventory.GetProductInventoryDtl(fromDate, toDate, wareHouseId, companyId, Session["Reporting"].ToString(), serverAddress, branchId);
                    if (Print == "true")
                    {
                        ASPxWebDocumentViewer1.Visible = true;
                        ASPxGridView1.Visible = false;
                        XtraReport ProductComparision = new Noble.Report.Reports.Reports.InventoryReport.ProductComparisionReport(companyInfo, ProductInventoryDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                        ASPxWebDocumentViewer1.OpenReport(ProductComparision);
                        ProductComparision.DisplayName = "ProductComparisionReport";
                    }
                    else
                    {
                        ASPxWebDocumentViewer1.Visible = false;
                        ASPxGridView1.Visible = true;
                        var dt = new DataTable();
                        dt.Columns.Add("#");
                        dt.Columns.Add("DocumentNumber");
                        dt.Columns.Add("ProductName");
                        dt.Columns.Add("Date");
                        dt.Columns.Add("TransectionType");
                        dt.Columns.Add("AveragePrice");
                        dt.Columns.Add("Price");
                        dt.Columns.Add("Quantity");
                        dt.Columns.Add("CurrentQuantity");
                        dt.Columns.Add("SalePrice");
                        DataRow row;
                        int i = 1;

                        foreach (var item in ProductInventoryDtl)
                        {
                            foreach (var Pitem in item.ProductInventoryReport)
                            {
                                row = dt.NewRow();
                                row["#"] = i++;
                                row["DocumentNumber"] = Pitem.DocumentNumber;
                                row["ProductName"] = item.ProductName;
                                row["Date"] = Convert.ToDateTime(Pitem.Date).ToString("dd MMM yy");
                                row["TransectionType"] = Pitem.TransactionType;
                                row["Price"] = Pitem.Price.ToString("N2");
                                row["AveragePrice"] = Pitem.AveragePrice.ToString("N2");
                                row["Quantity"] = Pitem.Quantity.ToString("-#");
                                row["CurrentQuantity"] = Pitem.CurrentQuantity.ToString("N2");
                                row["SalePrice"] = Pitem.SalePrice.ToString("N2");
                                dt.Rows.Add(row);

                            }
                        }
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        ASPxGridView1.Columns["ProductName"].Width = 505;
                    }
                }
                else if (formName == "SupplierPurchaseReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string supplierId = Request.QueryString["supplierId"] == null ? "" : Request.QueryString["supplierId"];
                    // Company / GetContactOpeningBalanceList
                    if (supplierId != "")
                    {
                        var supplierDtl = GetSupplier.GetSupplierDtl(fromDate, toDate, supplierId, Session["Reporting"].ToString(), serverAddress, branchId);

                        if (Print == "true")
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            XtraReport SupplierPurchaseReport = new Noble.Report.Reports.Reports.InventoryReport.SupplierPurchaseReport(companyInfo, supplierDtl, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                            ASPxWebDocumentViewer1.OpenReport(SupplierPurchaseReport);
                            SupplierPurchaseReport.DisplayName = "SupplierPurchaseReport";
                            ASPxGridView1.Visible = false;
                        }
                        else
                        {
                            ASPxWebDocumentViewer1.Visible = false;
                            ASPxGridView1.Visible = true;
                            var dt = new DataTable();
                            dt.Columns.Add("#");
                            dt.Columns.Add("InvoiceNumber");
                            dt.Columns.Add("EnglishName");
                            dt.Columns.Add("ArabicName");
                            dt.Columns.Add("TexMathod");
                            dt.Columns.Add("Quantity");
                            dt.Columns.Add("UnitPrice");
                            dt.Columns.Add("Total");
                            DataRow row;
                            int i = 1;

                            foreach (var item in supplierDtl)
                            {
                                foreach (var Pitem in item.PurchasePostItems)
                                {
                                    row = dt.NewRow();
                                    row["#"] = i++;
                                    row["InvoiceNumber"] = item.InvoiceNo;
                                    row["EnglishName"] = Pitem.ProductName;
                                    row["ArabicName"] = Pitem.ProductNameArabic;
                                    row["TexMathod"] = Pitem.TaxMethod;
                                    row["Quantity"] = Pitem.Quantity;
                                    row["UnitPrice"] = Pitem.UnitPrice;
                                    row["Total"] = Pitem.Total;
                                    dt.Rows.Add(row);

                                }
                                ASPxGridView1.DataSource = dt;
                                ASPxGridView1.DataBind();
                            }
                        }
                    }

                }
                else if (formName == "ProductLedgerReport")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string productId = Request.QueryString["productId"] == null ? "" : Request.QueryString["productId"];
                    if (productId == "" || productId == null)
                    {

                    }
                    else
                    {
                        var ProductLedger = GetProductLedger.GetProductLedgerDtl(productId, fromDate, toDate, Session["Reporting"].ToString(), serverAddress, branchId);
                        if (Print == "true")
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport ProductLedgerReport = new Reports.InventoryReport.ProductLedgerReport(companyInfo, ProductLedger, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                            ASPxWebDocumentViewer1.OpenReport(ProductLedgerReport);
                            ProductLedgerReport.DisplayName = "ProductLedgerReport";

                        }
                        else
                        {
                            ASPxWebDocumentViewer1.Visible = false;
                            ASPxGridView1.Visible = true;
                            var dt = new DataTable();
                            dt.Columns.Add("#");
                            dt.Columns.Add("DocumentNumber");
                            dt.Columns.Add("Date");
                            dt.Columns.Add("TransectionType");
                            dt.Columns.Add("Description");
                            dt.Columns.Add("Debit");
                            dt.Columns.Add("Credit");
                            dt.Columns.Add("Account");
                            DataRow row;
                            int i = 1;

                            foreach (var item in ProductLedger)
                            {
                                foreach (var Pitem in item.LedgerTransactionLookUpModels)
                                {
                                    row = dt.NewRow();
                                    row["#"] = i++;
                                    row["DocumentNumber"] = Pitem.DocumentNumber;
                                    row["Date"] = Pitem.Date;
                                    row["TransectionType"] = Pitem.TransactionType;
                                    row["Description"] = Pitem.Description;
                                    row["Debit"] = Pitem.Debit;
                                    row["Credit"] = Pitem.Credit;
                                    dt.Rows.Add(row);

                                }
                                ASPxGridView1.DataSource = dt;
                                ASPxGridView1.DataBind();
                            }
                        }
                    }
                }
                else if (formName == "AdvanceIInventoryItem" || formName == "AdvanceQuantityWiseInventoryItem")
                {
                    string fromDate = Request.QueryString["fromDate"] == null ? "" : Request.QueryString["fromDate"];
                    string toDate = Request.QueryString["toDate"] == null ? "" : Request.QueryString["toDate"];
                    string numberOfPeriods = Request.QueryString["numberOfPeriods"] == null ? "" : Request.QueryString["numberOfPeriods"];
                    string compareWith = Request.QueryString["compareWith"] == null ? "" : Request.QueryString["compareWith"];
                    string productId = Request.QueryString["productId"] == null ? "" : Request.QueryString["productId"];
                    if (formName == "AdvanceIInventoryItem")
                    {
                        var Inventory = GetAdvanceIInventoryItem.GetAdvanceIInventoryItemDtl(fromDate, toDate, numberOfPeriods, compareWith, productId, Session["Reporting"].ToString(), serverAddress);
                        if (compareWith != null && compareWith != "" && compareWith != "null")
                        {
                            var Accounts = GetAllAccounts.GetAllAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.InventoryReport.AdvanceInventory.AdvanceIInventoryItemComparison(companyInfo, Inventory, compareWith, numberOfPeriods);
                            ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                            ComparisonBalanceSheetReport.DisplayName = "Advance Trail Balance Report" + DateTime.Now;
                        }
                        else
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport BalanceSheetReport = new Noble.Report.Reports.Reports.InventoryReport.AdvanceInventory.AdvanceIInventoryItem(companyInfo, Inventory, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                            ASPxWebDocumentViewer1.OpenReport(BalanceSheetReport);
                            BalanceSheetReport.DisplayName = "Advance Trail Balance Report" + DateTime.Now;
                        }
                    }
                    else if (formName == "AdvanceQuantityWiseInventoryItem")
                    {
                        var Inventory = GetAdvanceIInventoryItemQuantity.GetAdvanceIInventoryItemQuantityDtl(fromDate, toDate, numberOfPeriods, compareWith, productId, Session["Reporting"].ToString(), serverAddress);
                        if (compareWith != null && compareWith != "" && compareWith != "null")
                        {
                            var Accounts = GetAllAccounts.GetAllAccountsDtl(Session["Reporting"].ToString(), serverAddress);
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.InventoryReport.AdvanceInventory.AdvanceQuantityWiseInventoryItem.AdvanceQuantityWiseInventoryComparison(companyInfo, Inventory, compareWith, numberOfPeriods);
                            ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                            ComparisonBalanceSheetReport.DisplayName = "Advance Quantity Wise Inventory Item Report" + DateTime.Now;

                        }
                        else
                        {
                            ASPxWebDocumentViewer1.Visible = true;
                            ASPxGridView1.Visible = false;
                            XtraReport ComparisonBalanceSheetReport = new Noble.Report.Reports.Reports.InventoryReport.AdvanceInventory.AdvanceQuantityWiseInventoryItem.AdvanceQuantityWiseInventoryItem(companyInfo, Inventory, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                            ASPxWebDocumentViewer1.OpenReport(ComparisonBalanceSheetReport);
                            ComparisonBalanceSheetReport.DisplayName = "Advance Quantity Wise Inventory Item Report Report" + DateTime.Now;

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