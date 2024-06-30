namespace Noble.Report.Reports.Reports.LedgerReport.AdvanceCustomerLedgerReport
{
    partial class AdvanceCustomerSupplierReport
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo objectConstructorInfo1 = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.federationDataSource1 = new DevExpress.DataAccess.DataFederation.FederationDataSource();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cousomerInfo = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.CompanyInfo = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cousomerInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyInfo)).BeginInit();
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
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // federationDataSource1
            // 
            this.federationDataSource1.Name = "federationDataSource1";
            selectNode1.Alias = "cousomerInfo";
            sourceNode1.Alias = null;
            source1.DataMember = "";
            source1.DataSource = this.cousomerInfo;
            source1.Name = "cousomerInfo";
            sourceNode1.Source = source1;
            selectNode1.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompareWith", "CompareWith"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ContactCode", "ContactCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ContactName", "ContactName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Date", "Date"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "DocumentDate", "DocumentDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VatNo", "VatNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "AccountCode", "AccountCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "AccountName", "AccountName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "AccountNameArabic", "AccountNameArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsCustomer", "IsCustomer"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Amount", "Amount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CompareWithList", "CompareWithList")});
            selectNode1.Root = sourceNode1;
            selectNode2.Alias = "CompanyInfo";
            sourceNode2.Alias = null;
            source2.DataMember = "";
            source2.DataSource = this.CompanyInfo;
            source2.Name = "CompanyInfo";
            sourceNode2.Source = source2;
            selectNode2.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Id", "Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "NameEnglish", "NameEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "NameArabic", "NameArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompanyNameEnglish", "CompanyNameEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompanyNameArabic", "CompanyNameArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "VatRegistrationNo", "VatRegistrationNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompanyEmail", "CompanyEmail"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CityEnglish", "CityEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CityArabic", "CityArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Base64Logo", "Base64Logo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CountryEnglish", "CountryEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CountryArabic", "CountryArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CategoryEnglish", "CategoryEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CategoryArabic", "CategoryArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PhoneNo", "PhoneNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Website", "Website"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "AddressEnglish", "AddressEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "AddressArabic", "AddressArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "DfeNumber", "DfeNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CreatedDate", "CreatedDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "LogoPath", "LogoPath"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompanyRegNo", "CompanyRegNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "HouseNumber", "HouseNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Postcode", "Postcode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Town", "Town"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "LandLine", "LandLine"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ParentId", "ParentId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ClientParentId", "ClientParentId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BusinessId", "BusinessId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ClientNo", "ClientNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "FromDate", "FromDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ToDate", "ToDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsBlock", "IsBlock"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsActive", "IsActive"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "NumberOfUsers", "NumberOfUsers"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompanyType", "CompanyType"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "NoOfTransactionsAllow", "NoOfTransactionsAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompanyLicenceId", "CompanyLicenceId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Currency", "Currency"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsMultiUnit", "IsMultiUnit"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsProduction", "IsProduction"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsMulti", "IsMulti"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "InvoiceWoInventory", "InvoiceWoInventory"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsArea", "IsArea"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsProceed", "IsProceed"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "English", "English"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Arabic", "Arabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Terminal", "Terminal"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "DayStart", "DayStart"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsDayStart", "IsDayStart"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CashVoucher", "CashVoucher"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsCashVoucher", "IsCashVoucher"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsOpenDay", "IsOpenDay"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "OpenDay", "OpenDay"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsDailyExpense", "IsDailyExpense"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "DailyExpense", "DailyExpense"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsTransferAllow", "IsTransferAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TransferAllow", "TransferAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "MasterProduct", "MasterProduct"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsMasterProduct", "IsMasterProduct"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Step1", "Step1"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Step2", "Step2"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Step3", "Step3"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Step4", "Step4"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Step5", "Step5"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SimpleInvoice", "SimpleInvoice"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SoInventoryReserve", "SoInventoryReserve"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsSaleOrder", "IsSaleOrder"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SaleOrder", "SaleOrder"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PurchaseOrder", "PurchaseOrder"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "InternationalPurchase", "InternationalPurchase"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PartiallyPurchase", "PartiallyPurchase"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "VersionAllow", "VersionAllow"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ExpenseToGst", "ExpenseToGst"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsInternationalPurchase", "IsInternationalPurchase"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "AutoPurchaseVoucher", "AutoPurchaseVoucher"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsForMedical", "IsForMedical"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SuperAdminDayStart", "SuperAdminDayStart"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BankDetail", "BankDetail"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TaxInvoiceLabel", "TaxInvoiceLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsTaxLabel", "IsTaxLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TaxInvoiceLabelAr", "TaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsTaxInvoiceLabelAr", "IsTaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SimplifyTaxInvoiceLabel", "SimplifyTaxInvoiceLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsSimplifyTaxInvoiceLabel", "IsSimplifyTaxInvoiceLabel"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SimplifyTaxInvoiceLabelAr", "SimplifyTaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsSimplifyTaxInvoiceLabelAr", "IsSimplifyTaxInvoiceLabelAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "CompanyPermissionType", "CompanyPermissionType")});
            selectNode2.Root = sourceNode2;
            this.federationDataSource1.Queries.AddRange(new DevExpress.DataAccess.DataFederation.QueryNode[] {
            selectNode1,
            selectNode2});
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.ReportHeader.HeightF = 135.9352F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel10,
            this.xrLabel13,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
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
            // xrLabel4
            // 
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(639.2916F, 80.00007F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(151.7084F, 18F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseForeColor = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(504.9167F, 80.00007F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(134.375F, 18F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.Text = "To Period";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(639.2916F, 62.00003F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(151.7084F, 18F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(504.9167F, 62.00003F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(134.375F, 18F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.Text = "From Period";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Calibri", 16F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(504.9167F, 26.02943F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(286.0833F, 35.97062F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(791F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableRow1.BorderColor = System.Drawing.Color.Silver;
            this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
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
            this.xrTableRow1.StylePriority.UseBorderColor = false;
            this.xrTableRow1.StylePriority.UseBorders = false;
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "#";
            this.xrTableCell1.Weight = 0.39159295893499135D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Code";
            this.xrTableCell2.Weight = 0.58122691312421D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Customer Name";
            this.xrTableCell3.Weight = 2.6438048081813958D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "Account Code";
            this.xrTableCell4.Weight = 0.73103641167603306D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "Vat No.";
            this.xrTableCell5.Weight = 0.96049406917297131D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Balance";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell6.Weight = 0.69184483891039827D;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
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
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DataSource.CurrentRowIndex]+1")});
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "xrTableCell1";
            this.xrTableCell7.Weight = 0.39159295893499135D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ContactCode]")});
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "xrTableCell2";
            this.xrTableCell8.Weight = 0.58122714461026437D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AccountName]")});
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "xrTableCell3";
            this.xrTableCell9.Weight = 2.6438045766953415D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AccountCode]")});
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "xrTableCell4";
            this.xrTableCell10.Weight = 0.73103641167603306D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[VatNo]")});
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "xrTableCell5";
            this.xrTableCell11.Weight = 0.96049406917297131D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amount]")});
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "xrTableCell6";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell12.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell12.Weight = 0.69184483891039827D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.ReportFooter.HeightF = 25F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(791F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableRow3.BorderColor = System.Drawing.Color.Silver;
            this.xrTableRow3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell18});
            this.xrTableRow3.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseBackColor = false;
            this.xrTableRow3.StylePriority.UseBorderColor = false;
            this.xrTableRow3.StylePriority.UseBorders = false;
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Multiline = true;
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "Total";
            this.xrTableCell17.Weight = 5D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Amount])")});
            this.xrTableCell18.Multiline = true;
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell18.Summary = xrSummary1;
            this.xrTableCell18.Text = "xrTableCell18";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell18.TextFormatString = "{0:#,#0.00}";
            this.xrTableCell18.Weight = 1D;
            // 
            // cousomerInfo
            // 
            this.cousomerInfo.Constructor = objectConstructorInfo1;
            this.cousomerInfo.DataSource = typeof(Noble.Report.Models.AdvanceCustomerLedgerLookupModel);
            this.cousomerInfo.Name = "cousomerInfo";
            // 
            // CompanyInfo
            // 
            this.CompanyInfo.Constructor = objectConstructorInfo1;
            this.CompanyInfo.DataSource = typeof(Noble.Report.Models.CompanyDto);
            this.CompanyInfo.Name = "CompanyInfo";
            // 
            // AdvanceCustomerSupplierReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.GroupHeader1,
            this.ReportFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.cousomerInfo,
            this.CompanyInfo,
            this.federationDataSource1});
            this.DataMember = "cousomerInfo";
            this.DataSource = this.federationDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(18F, 18F, 18F, 18F);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cousomerInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource cousomerInfo;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource CompanyInfo;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
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
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
    }
}
