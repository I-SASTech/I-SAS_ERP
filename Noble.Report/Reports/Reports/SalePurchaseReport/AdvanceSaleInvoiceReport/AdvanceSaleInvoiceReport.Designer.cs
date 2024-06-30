namespace Noble.Report.Reports.Reports.SalePurchaseReport.AdvanceSaleInvoiceReport
{
    partial class AdvanceSaleInvoiceReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode1 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode1 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source1 = new DevExpress.DataAccess.DataFederation.Source();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode2 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode2 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source2 = new DevExpress.DataAccess.DataFederation.Source();
            DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo objectConstructorInfo1 = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.federationDataSource1 = new DevExpress.DataAccess.DataFederation.FederationDataSource();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.CompanyInfo = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.SaleInvoice = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 18F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 18F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1,
            this.xrTable2,
            this.xrTable1});
            this.Detail.HeightF = 73F;
            this.Detail.Name = "Detail";
            // 
            // xrLabel2
            // 
            this.xrLabel2.BackColor = System.Drawing.Color.Gainsboro;
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ApexChartValue]")});
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(304.1667F, 0F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(486.8333F, 23F);
            this.xrLabel2.StylePriority.UseBackColor = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "xrLabel2";
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.Gainsboro;
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompareWith]")});
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(304.1667F, 23F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "xrLabel1";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 48F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(791F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow2.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseFont = false;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalSales]")});
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "xrTableCell1";
            this.xrTableCell7.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell7.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalGrossAmount]")});
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "xrTableCell2";
            this.xrTableCell8.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell8.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalDiscountAmount]")});
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "xrTableCell3";
            this.xrTableCell9.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell9.Weight = 1.1185217856155185D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalNetTotal]")});
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "xrTableCell4";
            this.xrTableCell10.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell10.Weight = 1.134324065351908D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalVatAmount]")});
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "xrTableCell5";
            this.xrTableCell11.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell11.Weight = 0.87357701664477316D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalTotalAmount]")});
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "xrTableCell6";
            this.xrTableCell12.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell12.Weight = 0.87357713238780033D;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(791F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow1.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseBackColor = false;
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Total Sales";
            this.xrTableCell1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Total Gross Value";
            this.xrTableCell2.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Total Disount Amount";
            this.xrTableCell3.Weight = 1.1185217856155183D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "Total Net Sale Amount";
            this.xrTableCell4.Weight = 1.1343236023798002D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "Total VAT Amount";
            this.xrTableCell5.Weight = 0.87357747961688115D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Total Amount";
            this.xrTableCell6.Weight = 0.87357713238780033D;
            // 
            // federationDataSource1
            // 
            this.federationDataSource1.Name = "federationDataSource1";
            selectNode1.Alias = "CompanyInfo";
            sourceNode1.Alias = null;
            source1.DataMember = "";
            source1.DataSource = this.CompanyInfo;
            source1.Name = "CompanyInfo";
            sourceNode1.Source = source1;
            selectNode1.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Id", "Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "NameEnglish", "NameEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "NameArabic", "NameArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompanyNameEnglish", "CompanyNameEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompanyNameArabic", "CompanyNameArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VatRegistrationNo", "VatRegistrationNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompanyEmail", "CompanyEmail"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CityEnglish", "CityEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CityArabic", "CityArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Base64Logo", "Base64Logo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CountryEnglish", "CountryEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CountryArabic", "CountryArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CategoryEnglish", "CategoryEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CategoryArabic", "CategoryArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "PhoneNo", "PhoneNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Website", "Website"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "AddressEnglish", "AddressEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "AddressArabic", "AddressArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "DfeNumber", "DfeNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CreatedDate", "CreatedDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "LogoPath", "LogoPath"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompanyRegNo", "CompanyRegNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "HouseNumber", "HouseNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Postcode", "Postcode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Town", "Town"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "LandLine", "LandLine"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ParentId", "ParentId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ClientParentId", "ClientParentId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BusinessId", "BusinessId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ClientNo", "ClientNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "FromDate", "FromDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ToDate", "ToDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsBlock", "IsBlock"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsActive", "IsActive"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "NumberOfUsers", "NumberOfUsers"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompanyType", "CompanyType"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "NoOfTransactionsAllow", "NoOfTransactionsAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompanyLicenceId", "CompanyLicenceId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Currency", "Currency"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsMultiUnit", "IsMultiUnit"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsProduction", "IsProduction"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsMulti", "IsMulti"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "InvoiceWoInventory", "InvoiceWoInventory"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsArea", "IsArea"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsProceed", "IsProceed"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "English", "English"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Arabic", "Arabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Terminal", "Terminal"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "DayStart", "DayStart"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsDayStart", "IsDayStart"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CashVoucher", "CashVoucher"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsCashVoucher", "IsCashVoucher"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsOpenDay", "IsOpenDay"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "OpenDay", "OpenDay"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsDailyExpense", "IsDailyExpense"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "DailyExpense", "DailyExpense"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsTransferAllow", "IsTransferAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TransferAllow", "TransferAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "MasterProduct", "MasterProduct"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsMasterProduct", "IsMasterProduct"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Step1", "Step1"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Step2", "Step2"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Step3", "Step3"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Step4", "Step4"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Step5", "Step5"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SimpleInvoice", "SimpleInvoice"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SoInventoryReserve", "SoInventoryReserve"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsSaleOrder", "IsSaleOrder"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SaleOrder", "SaleOrder"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "PurchaseOrder", "PurchaseOrder"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "InternationalPurchase", "InternationalPurchase"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "PartiallyPurchase", "PartiallyPurchase"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VersionAllow", "VersionAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ExpenseToGst", "ExpenseToGst"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsInternationalPurchase", "IsInternationalPurchase"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "AutoPurchaseVoucher", "AutoPurchaseVoucher"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsForMedical", "IsForMedical"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SuperAdminDayStart", "SuperAdminDayStart"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BankDetail", "BankDetail"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TaxInvoiceLabel", "TaxInvoiceLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsTaxLabel", "IsTaxLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TaxInvoiceLabelAr", "TaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsTaxInvoiceLabelAr", "IsTaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SimplifyTaxInvoiceLabel", "SimplifyTaxInvoiceLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsSimplifyTaxInvoiceLabel", "IsSimplifyTaxInvoiceLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SimplifyTaxInvoiceLabelAr", "SimplifyTaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsSimplifyTaxInvoiceLabelAr", "IsSimplifyTaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompanyPermissionType", "CompanyPermissionType")});
            selectNode1.Root = sourceNode1;
            selectNode2.Alias = "SaleInvoice";
            sourceNode2.Alias = null;
            source2.DataMember = "";
            source2.DataSource = this.SaleInvoice;
            source2.Name = "SaleInvoice";
            sourceNode2.Source = source2;
            selectNode2.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TotalSales", "TotalSales"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TotalGrossAmount", "TotalGrossAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TotalDiscountAmount", "TotalDiscountAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TotalNetTotal", "TotalNetTotal"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TotalVatAmount", "TotalVatAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TotalTotalAmount", "TotalTotalAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompareWith", "CompareWith"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ApexChartValue", "ApexChartValue")});
            selectNode2.Root = sourceNode2;
            this.federationDataSource1.Queries.AddRange(new DevExpress.DataAccess.DataFederation.QueryNode[] {
            selectNode1,
            selectNode2});
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.ReportHeader.HeightF = 141.1436F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel10,
            this.xrLabel13,
            this.xrLabel7,
            this.xrLabel8,
            this.xrLabel9,
            this.xrLabel11,
            this.xrLabel12});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(791F, 126.8519F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.BackColor = System.Drawing.Color.Empty;
            this.xrPictureBox1.ImageUrl = "C:\\NOBLEPOS-ERP\\Noble.web\\dist\\favicon.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 15F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(93F, 93.00007F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.StylePriority.UseBackColor = false;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyInfo].[CompanyNameArabic]")});
            this.xrLabel10.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.xrLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(103F, 62.00005F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(251.3887F, 18F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseForeColor = false;
            this.xrLabel10.Text = "SaudKhan";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel13.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyInfo].[CompanyNameEnglish]")});
            this.xrLabel13.Font = new DevExpress.Drawing.DXFont("Arial", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(103F, 41.00003F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(251.3887F, 21.00002F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseForeColor = false;
            this.xrLabel13.Text = "AlTamam";
            // 
            // xrLabel7
            // 
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(639.2916F, 80.00007F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(151.7084F, 18F);
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(504.9167F, 80.00007F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(134.375F, 18F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseForeColor = false;
            this.xrLabel8.Text = "To Period";
            // 
            // xrLabel9
            // 
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(639.2916F, 62.00003F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(151.7084F, 18F);
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(504.9167F, 62.00003F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(134.375F, 18F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseForeColor = false;
            this.xrLabel11.Text = "From Period";
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new DevExpress.Drawing.DXFont("Calibri", 16F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(504.9167F, 26.02943F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(286.0833F, 35.97062F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseForeColor = false;
            this.xrLabel12.Text = "Sale Invoice Summary";
            // 
            // CompanyInfo
            // 
            this.CompanyInfo.Constructor = objectConstructorInfo1;
            this.CompanyInfo.DataSource = typeof(Noble.Report.Models.CompanyDto);
            this.CompanyInfo.Name = "CompanyInfo";
            // 
            // SaleInvoice
            // 
            this.SaleInvoice.Constructor = objectConstructorInfo1;
            this.SaleInvoice.DataSource = typeof(Noble.Report.Models.SaleInvoiceSummaryLookupModel);
            this.SaleInvoice.Name = "SaleInvoice";
            // 
            // AdvanceSaleInvoiceReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.CompanyInfo,
            this.SaleInvoice,
            this.federationDataSource1});
            this.DataMember = "SaleInvoice";
            this.DataSource = this.federationDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(18F, 18F, 18F, 18F);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource CompanyInfo;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource SaleInvoice;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
    }
}
