namespace Noble.Report.Reports.Invoice
{
    partial class A4_Templet3
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode1 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode1 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source1 = new DevExpress.DataAccess.DataFederation.Source();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode2 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode2 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source2 = new DevExpress.DataAccess.DataFederation.Source();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode3 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode3 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source3 = new DevExpress.DataAccess.DataFederation.Source();
            this.SaleItem = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.CompanyDto = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.PrintSetting = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.federationDataSource1 = new DevExpress.DataAccess.DataFederation.FederationDataSource();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.QrCode = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.SaleDetail = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SaleItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyDto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // SaleItem
            // 
            this.SaleItem.Constructor = objectConstructorInfo1;
            this.SaleItem.DataSource = typeof(Noble.Report.Models.SaleItemLookupModel);
            this.SaleItem.Name = "SaleItem";
            // 
            // CompanyDto
            // 
            this.CompanyDto.Constructor = objectConstructorInfo1;
            this.CompanyDto.DataSource = typeof(Noble.Report.Models.CompanyDto);
            this.CompanyDto.Name = "CompanyDto";
            // 
            // PrintSetting
            // 
            this.PrintSetting.Constructor = objectConstructorInfo1;
            this.PrintSetting.DataSource = typeof(Noble.Report.Models.PrintSetting);
            this.PrintSetting.Name = "PrintSetting";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 11.45833F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 10.03322F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 24F;
            this.Detail.Name = "Detail";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable2.BorderWidth = 1F;
            this.xrTable2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "AccessibleDescription", "[DataSource.CurrentRowIndex]+1")});
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(783.0002F, 21.66663F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell8});
            this.xrTableRow2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "AccessibleDescription", "[ReportItems.xrTableCell14].[Text]=[DataSource.CurrentRowIndex]+1")});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumRecordNumber([Code])")});
            this.xrTableCell14.Multiline = true;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Page;
            this.xrTableCell14.Summary = xrSummary1;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell14.Weight = 0.62145083132076784D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Code]")});
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell9.Weight = 0.77720324264485208D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Description]")});
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.Weight = 2.8807430149970816D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Quantity]")});
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "xrTableCell11";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell11.Weight = 0.52836756416329178D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPerPack]")});
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell12.TextFormatString = "{0:#0,#0.00}";
            this.xrTableCell12.Weight = 0.75680801652078622D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]")});
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "xrTableCell13";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell13.TextFormatString = "{0:#0,#0.00}";
            this.xrTableCell13.Weight = 1.1216377082150437D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalWithOutAmount]")});
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell1.TextFormatString = "{0:#0,#0.00}";
            this.xrTableCell1.Weight = 1.0322638999936538D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[VatAmount]")});
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell2.TextFormatString = "{0:#0,#0.00}";
            this.xrTableCell2.Weight = 1.1347711668467126D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Total]")});
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell8.TextFormatString = "{0:#0,#0.00}";
            this.xrTableCell8.Weight = 1.2126658610254464D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            this.ReportHeader.HeightF = 218.6479F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(790.9999F, 218.6479F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel3,
            this.xrLabel8,
            this.xrLabel2,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel1});
            this.PageHeader.HeightF = 108.4764F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel6
            // 
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\r\n")});
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(138.8889F, 75.94448F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(145.1389F, 23F);
            this.xrLabel6.Text = "xrLabel6";
            // 
            // xrLabel3
            // 
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyDto].[AddressEnglish]")});
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(542.6866F, 75.94447F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(144.7037F, 22.99999F);
            this.xrLabel3.Text = "xrLabel3";
            // 
            // xrLabel8
            // 
            this.xrLabel8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyDto].[VatRegistrationNo]")});
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(542.6866F, 29.94443F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(144.7037F, 23F);
            this.xrLabel8.Text = "xrLabel8";
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InvoiceNo]")});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(138.8889F, 29.94446F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(145.1389F, 23F);
            this.xrLabel2.Text = "xrLabel2";
            // 
            // xrLabel4
            // 
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(542.6866F, 52.94452F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(144.7037F, 23F);
            this.xrLabel4.Text = "xrLabel2";
            // 
            // xrLabel5
            // 
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(138.8889F, 52.94447F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(145.1389F, 23F);
            this.xrLabel5.Text = "xrLabel2";
            // 
            // xrLabel1
            // 
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PrintSetting].[CustomerNameAr]")});
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(791F, 29.94444F);
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "xrLabel1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // federationDataSource1
            // 
            this.federationDataSource1.Name = "federationDataSource1";
            selectNode1.Alias = "SaleItem";
            sourceNode1.Alias = null;
            source1.DataMember = "";
            source1.DataSource = this.SaleItem;
            source1.Name = "SaleItem";
            sourceNode1.Source = source1;
            selectNode1.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Id", "Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ProductId", "ProductId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ItemId", "ItemId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ServiceProductId", "ServiceProductId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ProductName", "ProductName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CategoryName", "CategoryName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Total", "Total"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "UnitPrice", "UnitPrice"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "UnitPerPack", "UnitPerPack"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Quantity", "Quantity"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Discount", "Discount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "FixDiscount", "FixDiscount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TaxRateId", "TaxRateId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TaxRate", "TaxRate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ExclusiveVat", "ExclusiveVat"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SaleId", "SaleId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "WareHouseId", "WareHouseId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "OfferQuantity", "OfferQuantity"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VatAmount", "VatAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Get", "Get"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Buy", "Buy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Code", "Code"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SaleReturnDays", "SaleReturnDays"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "PromotionId", "PromotionId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BundleId", "BundleId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "InclusiveVat", "InclusiveVat"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TaxMethod", "TaxMethod"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "DiscountAmount", "DiscountAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TotalWithOutAmount", "TotalWithOutAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TotalAmount", "TotalAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BundleAmount", "BundleAmount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ArabicName", "ArabicName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Description", "Description"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "RemainingQuantity", "RemainingQuantity"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ReturnQuantity", "ReturnQuantity"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SoQty", "SoQty"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BatchNo", "BatchNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BatchExpiry", "BatchExpiry"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "AutoNumber", "AutoNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ServiceItem", "ServiceItem"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsFree", "IsFree"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsActive", "IsActive"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Serial", "Serial"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "GuaranteeDate", "GuaranteeDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsSerial", "IsSerial"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "IsGuarantee", "IsGuarantee"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SoItemId", "SoItemId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ColorId", "ColorId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ColorName", "ColorName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "StyleNumber", "StyleNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SerialNo", "SerialNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SchemeQuantity", "SchemeQuantity"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Scheme", "Scheme"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SchemePhysicalQuantity", "SchemePhysicalQuantity"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "PromotionOfferQuantity", "PromotionOfferQuantity"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "LineTotal", "LineTotal")});
            selectNode1.Root = sourceNode1;
            selectNode2.Alias = "CompanyDto";
            sourceNode2.Alias = null;
            source2.DataMember = "";
            source2.DataSource = this.CompanyDto;
            source2.Name = "CompanyDto";
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
            selectNode3.Alias = "PrintSetting";
            sourceNode3.Alias = null;
            source3.DataMember = "";
            source3.DataSource = this.PrintSetting;
            source3.Name = "PrintSetting";
            sourceNode3.Source = source3;
            selectNode3.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "Id", "Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "isActive", "isActive"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ReturnDays", "ReturnDays"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "PrintSize", "PrintSize"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "WarrantyImage", "WarrantyImage"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "WarrantyImageForPrint", "WarrantyImageForPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "PrintTemplate", "PrintTemplate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "TermsInAr", "TermsInAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "TermsInEng", "TermsInEng"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CashAccountId", "CashAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BankAccountId", "BankAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "PrinterName", "PrinterName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "IsHeaderFooter", "IsHeaderFooter"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "EnglishName", "EnglishName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ArabicName", "ArabicName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CustomerAddress", "CustomerAddress"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CustomerVat", "CustomerVat"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CustomerNumber", "CustomerNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CargoName", "CargoName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CustomerTelephone", "CustomerTelephone"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ItemPieceSize", "ItemPieceSize"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "StyleNo", "StyleNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BlindPrint", "BlindPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ShowBankAccount", "ShowBankAccount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BankAccount1", "BankAccount1"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BankIcon1", "BankIcon1"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BankAccount2", "BankAccount2"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BankIcon2", "BankIcon2"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "InvoicePrint", "InvoicePrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "IsBlindPrint", "IsBlindPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "IsDeliveryNote", "IsDeliveryNote"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "AutoPaymentVoucher", "AutoPaymentVoucher"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CustomerNameEn", "CustomerNameEn"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "CustomerNameAr", "CustomerNameAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ExchangeDays", "ExchangeDays"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "Bank1Id", "Bank1Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "Bank2Id", "Bank2Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "WelcomeLineEn", "WelcomeLineEn"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "WelcomeLineAr", "WelcomeLineAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ClosingLineEn", "ClosingLineEn"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ClosingLineAr", "ClosingLineAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ContactNo", "ContactNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ManagementNo", "ManagementNo"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BusinessAddressArabic", "BusinessAddressArabic"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "BusinessAddressEnglish", "BusinessAddressEnglish"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "WalkInInvoiceType", "WalkInInvoiceType"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "HeaderImage", "HeaderImage"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "HeaderImageForPrint", "HeaderImageForPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "HeaderImage1ForPrint", "HeaderImage1ForPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "HeaderImage1", "HeaderImage1"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "TagsImages", "TagsImages"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ProposalImage", "ProposalImage"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "FooterImage", "FooterImage"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "FooterImageForPrint", "FooterImageForPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "TagImageForPrint", "TagImageForPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ProposalImageForPrint", "ProposalImageForPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "IsQuotationPrint", "IsQuotationPrint"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "TermAndConditionLength", "TermAndConditionLength"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "WithSubTotal", "WithSubTotal"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "ContinueWithPage", "ContinueWithPage"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode3, "SubTotalWithDashes", "SubTotalWithDashes")});
            selectNode3.Root = sourceNode3;
            this.federationDataSource1.Queries.AddRange(new DevExpress.DataAccess.DataFederation.QueryNode[] {
            selectNode1,
            selectNode2,
            selectNode3});
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.QrCode,
            this.xrLabel42,
            this.xrLabel30,
            this.xrLabel31,
            this.xrLabel32,
            this.xrLabel43});
            this.PageFooter.HeightF = 274.8579F;
            this.PageFooter.Name = "PageFooter";
            // 
            // QrCode
            // 
            this.QrCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.QrCode.LocationFloat = new DevExpress.Utils.PointFloat(376.7067F, 14.65404F);
            this.QrCode.Name = "QrCode";
            this.QrCode.SizeF = new System.Drawing.SizeF(100F, 100F);
            this.QrCode.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.QrCode.StylePriority.UseBorders = false;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel42.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Sum([TotalAmount])")});
            this.xrLabel42.Font = new DevExpress.Drawing.DXFont("calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(703.1117F, 91.6541F);
            this.xrLabel42.Multiline = true;
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel42.SizeF = new System.Drawing.SizeF(79.88843F, 23F);
            this.xrLabel42.StylePriority.UseBorders = false;
            this.xrLabel42.StylePriority.UseFont = false;
            this.xrLabel42.StylePriority.UseTextAlignment = false;
            this.xrLabel42.Text = "xrLabel30";
            this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel42.TextFormatString = "{0:#0,#0.00}";
            // 
            // xrLabel30
            // 
            this.xrLabel30.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel30.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sum([DiscountAmount])")});
            this.xrLabel30.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(703.1117F, 25.00001F);
            this.xrLabel30.Multiline = true;
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(79.88788F, 20.6541F);
            this.xrLabel30.StylePriority.UseBorders = false;
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "xrLabel30";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel30.TextFormatString = "{0:#0,#0.00}";
            // 
            // xrLabel31
            // 
            this.xrLabel31.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel31.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sum([TotalAmount])")});
            this.xrLabel31.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(703.1117F, 45.65399F);
            this.xrLabel31.Multiline = true;
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(79.88843F, 22.99999F);
            this.xrLabel31.StylePriority.UseBorders = false;
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            this.xrLabel31.Text = "xrLabel31";
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel31.TextFormatString = "{0:#0,#0.00}";
            // 
            // xrLabel32
            // 
            this.xrLabel32.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel32.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sum([VatAmount])")});
            this.xrLabel32.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(703.1117F, 68.65401F);
            this.xrLabel32.Multiline = true;
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(78.49127F, 23F);
            this.xrLabel32.StylePriority.UseBorders = false;
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = "xrLabel32";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel32.TextFormatString = "{0:#0,#0.00}";
            this.xrLabel32.WordWrap = false;
            // 
            // xrLabel43
            // 
            this.xrLabel43.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel43.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sum([SA_A4_Temp2SaleDetails].[SaleItems].[TotalWithOutAmount])")});
            this.xrLabel43.Font = new DevExpress.Drawing.DXFont("calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(703.1115F, 0F);
            this.xrLabel43.Multiline = true;
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(79.88849F, 23F);
            this.xrLabel43.StylePriority.UseBorders = false;
            this.xrLabel43.StylePriority.UseFont = false;
            this.xrLabel43.StylePriority.UseTextAlignment = false;
            this.xrLabel43.Text = "xrLabel43";
            this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel43.TextFormatString = "{0:#0,#0.00}";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.HeightF = 28.05699F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.HeightF = 42.07272F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // SaleDetail
            // 
            this.SaleDetail.Constructor = objectConstructorInfo1;
            this.SaleDetail.DataSource = typeof(Noble.Report.Models.SaleDetailLookupModel);
            this.SaleDetail.Name = "SaleDetail";
            // 
            // A4_Templet3
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.PageHeader,
            this.PageFooter,
            this.GroupHeader1,
            this.GroupFooter1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.SaleItem,
            this.CompanyDto,
            this.PrintSetting,
            this.SaleDetail,
            this.federationDataSource1});
            this.DataMember = "SaleItem";
            this.DataSource = this.federationDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(18F, 18F, 11.45833F, 10.03322F);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.SaleItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyDto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource SaleItem;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource CompanyDto;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource PrintSetting;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource SaleDetail;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPictureBox QrCode;
        private DevExpress.XtraReports.UI.XRLabel xrLabel42;
        private DevExpress.XtraReports.UI.XRLabel xrLabel30;
        private DevExpress.XtraReports.UI.XRLabel xrLabel31;
        private DevExpress.XtraReports.UI.XRLabel xrLabel32;
        private DevExpress.XtraReports.UI.XRLabel xrLabel43;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
