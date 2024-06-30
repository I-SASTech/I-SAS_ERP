namespace Noble.Report.Reports.Invoice.BranchVoucher
{
    partial class branchVoucher
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
            DevExpress.DataAccess.DataFederation.SelectNode selectNode1 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode1 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source1 = new DevExpress.DataAccess.DataFederation.Source();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode2 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode2 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source2 = new DevExpress.DataAccess.DataFederation.Source();
            this.Company = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.branch = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.federationDataSource1 = new DevExpress.DataAccess.DataFederation.FederationDataSource();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.Footer = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.branch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Company
            // 
            this.Company.Constructor = objectConstructorInfo1;
            this.Company.DataSource = typeof(Noble.Report.Models.CompanyDto);
            this.Company.Name = "Company";
            // 
            // branch
            // 
            this.branch.Constructor = objectConstructorInfo1;
            this.branch.DataSource = typeof(Noble.Report.Models.BranchVoucherModel);
            this.branch.Name = "branch";
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
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
            this.Detail.HeightF = 194.9583F;
            this.Detail.Name = "Detail";
            // 
            // xrLabel18
            // 
            this.xrLabel18.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(724.7499F, 152.875F);
            this.xrLabel18.Multiline = true;
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(56.25F, 17.79169F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "الكمية";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(697.6248F, 93.08328F);
            this.xrLabel17.Multiline = true;
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(83.79187F, 17.79169F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "وسيلة الدفع";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(258.9583F, 93.08332F);
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(38.54166F, 17.79169F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "حالة";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(685.1666F, 56.95832F);
            this.xrLabel15.Multiline = true;
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "اسم جهة الإتصال";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(715.375F, 25.70833F);
            this.xrLabel14.Multiline = true;
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(66.04169F, 17.79167F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "رقم القسيمة";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(270.4167F, 25.70833F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(43.75F, 17.79167F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "تاريخ";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(448.6665F, 93.08332F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(100F, 17.79169F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "Payment Mode";
            // 
            // xrLabel12
            // 
            this.xrLabel12.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PaymentMode]")});
            this.xrLabel12.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(548.6665F, 93.08332F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(148.9583F, 17.79166F);
            this.xrLabel12.StylePriority.UseBorderDashStyle = false;
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "xrLabel1";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel9
            // 
            this.xrLabel9.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ApprovalStatus]")});
            this.xrLabel9.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(110F, 93.08332F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(148.9583F, 17.79166F);
            this.xrLabel9.StylePriority.UseBorderDashStyle = false;
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "xrLabel1";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 93.08332F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(100F, 17.79169F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.Text = "Approval Status";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(524.75F, 152.875F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(100F, 17.79169F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "Amount";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 56.95832F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 17.79167F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "Contact Name";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(384.0832F, 25.70833F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(120.8333F, 17.79167F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Voucher Number";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Calibri", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 25.70833F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(51.04166F, 17.79167F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "Date";
            // 
            // xrLabel4
            // 
            this.xrLabel4.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[VoucherNumber]")});
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(504.9165F, 25.70833F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(210.4585F, 17.79167F);
            this.xrLabel4.StylePriority.UseBorderDashStyle = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "xrLabel4";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ContactName]")});
            this.xrLabel3.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(110F, 56.95832F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(575.1666F, 17.79167F);
            this.xrLabel3.StylePriority.UseBorderDashStyle = false;
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "xrLabel3";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Date]")});
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(61.04167F, 25.70833F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(209.375F, 17.79167F);
            this.xrLabel2.StylePriority.UseBorderDashStyle = false;
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "xrLabel2";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel2.TextFormatString = "{0:dd MMMM yyyy}";
            // 
            // xrLabel1
            // 
            this.xrLabel1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amount]")});
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(624.7499F, 152.875F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 17.79166F);
            this.xrLabel1.StylePriority.UseBorderDashStyle = false;
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "xrLabel1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel1.TextFormatString = "{0:#,#0.00}";
            // 
            // federationDataSource1
            // 
            this.federationDataSource1.Name = "federationDataSource1";
            selectNode1.Alias = "Company";
            sourceNode1.Alias = null;
            source1.DataMember = "";
            source1.DataSource = this.Company;
            source1.Name = "Company";
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
            selectNode2.Alias = "branch";
            sourceNode2.Alias = null;
            source2.DataMember = "";
            source2.DataSource = this.branch;
            source2.Name = "branch";
            sourceNode2.Source = source2;
            selectNode2.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Id", "Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Date", "Date"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "VoucherNumber", "VoucherNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Narration", "Narration"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ChequeNumber", "ChequeNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "NatureOfPayment", "NatureOfPayment"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Amount", "Amount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ApprovalStatus", "ApprovalStatus"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentMode", "PaymentMode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentMethod", "PaymentMethod"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BankCashAccountId", "BankCashAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ContactAccountId", "ContactAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TaxMethod", "TaxMethod"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TaxRateId", "TaxRateId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BranchId", "BranchId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsSupplier", "IsSupplier"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BankName", "BankName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ContactName", "ContactName")});
            selectNode2.Root = sourceNode2;
            this.federationDataSource1.Queries.AddRange(new DevExpress.DataAccess.DataFederation.QueryNode[] {
            selectNode1,
            selectNode2});
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            this.ReportHeader.HeightF = 99.81994F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(790.15F, 99.81994F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Footer});
            this.ReportFooter.HeightF = 59.47958F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.PrintAtBottom = true;
            // 
            // Footer
            // 
            this.Footer.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.Footer.Name = "Footer";
            this.Footer.SizeF = new System.Drawing.SizeF(791.0001F, 59.47958F);
            this.Footer.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // branchVoucher
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.ReportFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.Company,
            this.branch,
            this.federationDataSource1});
            this.DataMember = "branch";
            this.DataSource = this.federationDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Calibri", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(18F, 18F, 18F, 18F);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.Company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.branch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource Company;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource branch;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel17;
        private DevExpress.XtraReports.UI.XRLabel xrLabel18;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRPictureBox Footer;
    }
}
