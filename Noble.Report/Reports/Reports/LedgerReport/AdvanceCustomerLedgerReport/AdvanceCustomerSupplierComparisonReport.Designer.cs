namespace Noble.Report.Reports.Reports.LedgerReport.AdvanceCustomerLedgerReport
{
    partial class AdvanceCustomerSupplierComparisonReport
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
            DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo objectConstructorInfo1 = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition crossTabColumnDefinition1 = new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition(2F);
            DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition crossTabColumnDefinition2 = new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition(2F);
            DevExpress.XtraReports.UI.CrossTab.CrossTabColumnField crossTabColumnField1 = new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnField();
            DevExpress.XtraReports.UI.CrossTab.CrossTabDataField crossTabDataField1 = new DevExpress.XtraReports.UI.CrossTab.CrossTabDataField();
            DevExpress.XtraReports.UI.CrossTab.CrossTabDataField crossTabDataField2 = new DevExpress.XtraReports.UI.CrossTab.CrossTabDataField();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode1 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode1 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source1 = new DevExpress.DataAccess.DataFederation.Source();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode2 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode2 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source2 = new DevExpress.DataAccess.DataFederation.Source();
            this.LedgerInfo = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.Company = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrCrossTab1 = new DevExpress.XtraReports.UI.XRCrossTab();
            this.crossTabHeaderCell1 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabDataCell1 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabHeaderCell2 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabHeaderCell3 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabHeaderCell4 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabTotalCell1 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabHeaderCell5 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabHeaderCell6 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabHeaderCell7 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabHeaderCell8 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabDataCell2 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.crossTabTotalCell2 = new DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell();
            this.federationDataSource1 = new DevExpress.DataAccess.DataFederation.FederationDataSource();
            this.crossTabGeneralStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.crossTabHeaderStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.crossTabDataStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.crossTabTotalStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this.LedgerInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCrossTab1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // LedgerInfo
            // 
            this.LedgerInfo.Constructor = objectConstructorInfo1;
            this.LedgerInfo.DataSource = typeof(Noble.Report.Models.AdvanceCustomerLedgerLookupModel);
            this.LedgerInfo.Name = "LedgerInfo";
            // 
            // Company
            // 
            this.Company.Constructor = objectConstructorInfo1;
            this.Company.DataSource = typeof(Noble.Report.Models.CompanyDto);
            this.Company.Name = "Company";
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
            this.xrCrossTab1});
            this.Detail.Name = "Detail";
            // 
            // xrCrossTab1
            // 
            this.xrCrossTab1.Cells.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.crossTabHeaderCell1,
            this.crossTabDataCell1,
            this.crossTabHeaderCell2,
            this.crossTabHeaderCell3,
            this.crossTabHeaderCell4,
            this.crossTabTotalCell1,
            this.crossTabHeaderCell5,
            this.crossTabHeaderCell6,
            this.crossTabHeaderCell7,
            this.crossTabHeaderCell8,
            this.crossTabDataCell2,
            this.crossTabTotalCell2});
            crossTabColumnDefinition1.Visible = false;
            crossTabColumnDefinition2.Visible = false;
            this.xrCrossTab1.ColumnDefinitions.AddRange(new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition[] {
            new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition(100F),
            new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition(100F),
            new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnDefinition(100F),
            crossTabColumnDefinition1,
            crossTabColumnDefinition2});
            crossTabColumnField1.FieldName = "CompareWith";
            this.xrCrossTab1.ColumnFields.AddRange(new DevExpress.XtraReports.UI.CrossTab.CrossTabColumnField[] {
            crossTabColumnField1});
            this.xrCrossTab1.DataAreaStyleName = "crossTabDataStyle1";
            crossTabDataField1.FieldName = "AccountCode";
            crossTabDataField2.FieldName = "Amount";
            this.xrCrossTab1.DataFields.AddRange(new DevExpress.XtraReports.UI.CrossTab.CrossTabDataField[] {
            crossTabDataField1,
            crossTabDataField2});
            this.xrCrossTab1.DataMember = "LedgerInfo";
            this.xrCrossTab1.DataSource = this.federationDataSource1;
            this.xrCrossTab1.GeneralStyleName = "crossTabGeneralStyle1";
            this.xrCrossTab1.HeaderAreaStyleName = "crossTabHeaderStyle1";
            this.xrCrossTab1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrCrossTab1.Name = "xrCrossTab1";
            this.xrCrossTab1.RowDefinitions.AddRange(new DevExpress.XtraReports.UI.CrossTab.CrossTabRowDefinition[] {
            new DevExpress.XtraReports.UI.CrossTab.CrossTabRowDefinition(25F),
            new DevExpress.XtraReports.UI.CrossTab.CrossTabRowDefinition(25F),
            new DevExpress.XtraReports.UI.CrossTab.CrossTabRowDefinition(25F)});
            this.xrCrossTab1.SizeF = new System.Drawing.SizeF(304F, 75F);
            this.xrCrossTab1.TotalAreaStyleName = "crossTabTotalStyle1";
            // 
            // crossTabHeaderCell1
            // 
            this.crossTabHeaderCell1.ColumnIndex = 0;
            this.crossTabHeaderCell1.Name = "crossTabHeaderCell1";
            this.crossTabHeaderCell1.RowIndex = 0;
            this.crossTabHeaderCell1.RowSpan = 2;
            // 
            // crossTabDataCell1
            // 
            this.crossTabDataCell1.ColumnIndex = 1;
            this.crossTabDataCell1.Name = "crossTabDataCell1";
            this.crossTabDataCell1.RowIndex = 2;
            // 
            // crossTabHeaderCell2
            // 
            this.crossTabHeaderCell2.ColumnIndex = 1;
            this.crossTabHeaderCell2.ColumnSpan = 2;
            this.crossTabHeaderCell2.Name = "crossTabHeaderCell2";
            this.crossTabHeaderCell2.RowIndex = 0;
            // 
            // crossTabHeaderCell3
            // 
            this.crossTabHeaderCell3.ColumnIndex = 0;
            this.crossTabHeaderCell3.Name = "crossTabHeaderCell3";
            this.crossTabHeaderCell3.RowIndex = 2;
            // 
            // crossTabHeaderCell4
            // 
            this.crossTabHeaderCell4.ColumnIndex = 3;
            this.crossTabHeaderCell4.ColumnSpan = 2;
            this.crossTabHeaderCell4.Name = "crossTabHeaderCell4";
            this.crossTabHeaderCell4.RowIndex = 0;
            this.crossTabHeaderCell4.Text = "Grand Total";
            // 
            // crossTabTotalCell1
            // 
            this.crossTabTotalCell1.ColumnIndex = 3;
            this.crossTabTotalCell1.Name = "crossTabTotalCell1";
            this.crossTabTotalCell1.RowIndex = 2;
            // 
            // crossTabHeaderCell5
            // 
            this.crossTabHeaderCell5.ColumnIndex = 1;
            this.crossTabHeaderCell5.Name = "crossTabHeaderCell5";
            this.crossTabHeaderCell5.RowIndex = 1;
            this.crossTabHeaderCell5.Text = "Account Code";
            // 
            // crossTabHeaderCell6
            // 
            this.crossTabHeaderCell6.ColumnIndex = 2;
            this.crossTabHeaderCell6.Name = "crossTabHeaderCell6";
            this.crossTabHeaderCell6.RowIndex = 1;
            this.crossTabHeaderCell6.Text = "Amount";
            // 
            // crossTabHeaderCell7
            // 
            this.crossTabHeaderCell7.ColumnIndex = 3;
            this.crossTabHeaderCell7.Name = "crossTabHeaderCell7";
            this.crossTabHeaderCell7.RowIndex = 1;
            this.crossTabHeaderCell7.Text = "Account Code";
            // 
            // crossTabHeaderCell8
            // 
            this.crossTabHeaderCell8.ColumnIndex = 4;
            this.crossTabHeaderCell8.Name = "crossTabHeaderCell8";
            this.crossTabHeaderCell8.RowIndex = 1;
            this.crossTabHeaderCell8.Text = "Amount";
            // 
            // crossTabDataCell2
            // 
            this.crossTabDataCell2.ColumnIndex = 2;
            this.crossTabDataCell2.Name = "crossTabDataCell2";
            this.crossTabDataCell2.RowIndex = 2;
            // 
            // crossTabTotalCell2
            // 
            this.crossTabTotalCell2.ColumnIndex = 4;
            this.crossTabTotalCell2.Name = "crossTabTotalCell2";
            this.crossTabTotalCell2.RowIndex = 2;
            // 
            // federationDataSource1
            // 
            this.federationDataSource1.Name = "federationDataSource1";
            selectNode1.Alias = "LedgerInfo";
            sourceNode1.Alias = null;
            source1.DataMember = "";
            source1.DataSource = this.LedgerInfo;
            source1.Name = "LedgerInfo";
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
            selectNode2.Alias = "Company";
            sourceNode2.Alias = null;
            source2.DataMember = "";
            source2.DataSource = this.Company;
            source2.Name = "Company";
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
            // crossTabGeneralStyle1
            // 
            this.crossTabGeneralStyle1.BackColor = System.Drawing.Color.White;
            this.crossTabGeneralStyle1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.crossTabGeneralStyle1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.crossTabGeneralStyle1.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.crossTabGeneralStyle1.ForeColor = System.Drawing.Color.Black;
            this.crossTabGeneralStyle1.Name = "crossTabGeneralStyle1";
            this.crossTabGeneralStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // crossTabHeaderStyle1
            // 
            this.crossTabHeaderStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.crossTabHeaderStyle1.Name = "crossTabHeaderStyle1";
            this.crossTabHeaderStyle1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // crossTabDataStyle1
            // 
            this.crossTabDataStyle1.Name = "crossTabDataStyle1";
            this.crossTabDataStyle1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // crossTabTotalStyle1
            // 
            this.crossTabTotalStyle1.Name = "crossTabTotalStyle1";
            this.crossTabTotalStyle1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // AdvanceCustomerSupplierComparisonReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.LedgerInfo,
            this.Company,
            this.federationDataSource1});
            this.DataSource = this.federationDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(18F, 18F, 18F, 18F);
            this.PageWidth = 4400;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.crossTabGeneralStyle1,
            this.crossTabHeaderStyle1,
            this.crossTabDataStyle1,
            this.crossTabTotalStyle1});
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.LedgerInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCrossTab1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource LedgerInfo;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource Company;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.XRCrossTab xrCrossTab1;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell1;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabDataCell1;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell2;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell3;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell4;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabTotalCell1;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell5;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell6;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell7;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabHeaderCell8;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabDataCell2;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabTotalCell2;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabGeneralStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabHeaderStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabDataStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabTotalStyle1;
    }
}
