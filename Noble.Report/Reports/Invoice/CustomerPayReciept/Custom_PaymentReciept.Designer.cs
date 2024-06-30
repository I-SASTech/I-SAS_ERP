namespace Noble.Report.Reports.Invoice.CustomerPayReciept
{
    partial class Custom_PaymentReciept
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
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
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo objectConstructorInfo1 = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode1 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode1 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source1 = new DevExpress.DataAccess.DataFederation.Source();
            DevExpress.DataAccess.DataFederation.SelectNode selectNode2 = new DevExpress.DataAccess.DataFederation.SelectNode();
            DevExpress.DataAccess.DataFederation.SourceNode sourceNode2 = new DevExpress.DataAccess.DataFederation.SourceNode();
            DevExpress.DataAccess.DataFederation.Source source2 = new DevExpress.DataAccess.DataFederation.Source();
            this.CompanyInfo = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.Payment = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.CashAccountname = new DevExpress.XtraReports.UI.XRLabel();
            this.CashAccount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.NameCust = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.VocherNo = new DevExpress.XtraReports.UI.XRLabel();
            this.Date = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.QRCode = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.RunningBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Balance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ClosingBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.federationDataSource1 = new DevExpress.DataAccess.DataFederation.FederationDataSource();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // CompanyInfo
            // 
            this.CompanyInfo.Constructor = objectConstructorInfo1;
            this.CompanyInfo.DataSource = typeof(Noble.Report.Models.CompanyDto);
            this.CompanyInfo.Name = "CompanyInfo";
            // 
            // Payment
            // 
            this.Payment.Constructor = objectConstructorInfo1;
            this.Payment.DataSource = typeof(Noble.Report.Models.PaymentVoucherLookUpModel);
            this.Payment.Name = "Payment";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel2,
            this.xrLabel11,
            this.xrLabel12,
            this.CashAccountname,
            this.CashAccount,
            this.xrLabel5,
            this.NameCust,
            this.xrLabel3,
            this.VocherNo,
            this.Date,
            this.xrLabel1});
            this.Detail.HeightF = 187.469F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 143.969F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(82.52081F, 16.75F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Recieve By:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel12.Font = new DevExpress.Drawing.DXFont("calibri", 9.75F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(2.520879F, 160.719F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(259.4792F, 16.75002F);
            this.xrLabel12.StylePriority.UseBorderDashStyle = false;
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // CashAccountname
            // 
            this.CashAccountname.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.CashAccountname.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100.9689F);
            this.CashAccountname.Multiline = true;
            this.CashAccountname.Name = "CashAccountname";
            this.CashAccountname.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.CashAccountname.SizeF = new System.Drawing.SizeF(82.52081F, 16.75F);
            this.CashAccountname.StylePriority.UseFont = false;
            this.CashAccountname.StylePriority.UseTextAlignment = false;
            this.CashAccountname.Text = "Cash Account:";
            this.CashAccountname.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // CashAccount
            // 
            this.CashAccount.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.CashAccount.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.CashAccount.Font = new DevExpress.Drawing.DXFont("calibri", 9.75F);
            this.CashAccount.LocationFloat = new DevExpress.Utils.PointFloat(82.52074F, 100.9689F);
            this.CashAccount.Multiline = true;
            this.CashAccount.Name = "CashAccount";
            this.CashAccount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.CashAccount.SizeF = new System.Drawing.SizeF(177.0001F, 16.75F);
            this.CashAccount.StylePriority.UseBorderDashStyle = false;
            this.CashAccount.StylePriority.UseBorders = false;
            this.CashAccount.StylePriority.UseFont = false;
            this.CashAccount.StylePriority.UseTextAlignment = false;
            this.CashAccount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(2.520688F, 65.7917F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(50F, 16.75F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Mr.";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // NameCust
            // 
            this.NameCust.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.NameCust.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.NameCust.Font = new DevExpress.Drawing.DXFont("calibri", 9.75F);
            this.NameCust.LocationFloat = new DevExpress.Utils.PointFloat(2.520752F, 82.54172F);
            this.NameCust.Multiline = true;
            this.NameCust.Name = "NameCust";
            this.NameCust.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.NameCust.SizeF = new System.Drawing.SizeF(259.4792F, 16F);
            this.NameCust.StylePriority.UseBorderDashStyle = false;
            this.NameCust.StylePriority.UseBorders = false;
            this.NameCust.StylePriority.UseFont = false;
            this.NameCust.StylePriority.UseTextAlignment = false;
            this.NameCust.Text = "Date:";
            this.NameCust.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.04168F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(71.93307F, 16.75F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Vocher No:";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // VocherNo
            // 
            this.VocherNo.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.VocherNo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.VocherNo.Font = new DevExpress.Drawing.DXFont("calibri", 9.75F);
            this.VocherNo.LocationFloat = new DevExpress.Utils.PointFloat(71.93298F, 49.04175F);
            this.VocherNo.Multiline = true;
            this.VocherNo.Name = "VocherNo";
            this.VocherNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.VocherNo.SizeF = new System.Drawing.SizeF(187.5879F, 16.75F);
            this.VocherNo.StylePriority.UseBorderDashStyle = false;
            this.VocherNo.StylePriority.UseBorders = false;
            this.VocherNo.StylePriority.UseFont = false;
            this.VocherNo.StylePriority.UseTextAlignment = false;
            this.VocherNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // Date
            // 
            this.Date.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.Date.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Date.Font = new DevExpress.Drawing.DXFont("calibri", 9.75F);
            this.Date.LocationFloat = new DevExpress.Utils.PointFloat(49.99987F, 32.29167F);
            this.Date.Multiline = true;
            this.Date.Name = "Date";
            this.Date.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Date.SizeF = new System.Drawing.SizeF(209.5208F, 16.75F);
            this.Date.StylePriority.UseBorderDashStyle = false;
            this.Date.StylePriority.UseBorders = false;
            this.Date.StylePriority.UseFont = false;
            this.Date.StylePriority.UseTextAlignment = false;
            this.Date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 32.29167F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(50F, 16.75F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Date:";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 12.77F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 1.438586F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.QRCode,
            this.xrTable1});
            this.ReportFooter.HeightF = 107.8035F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // QRCode
            // 
            this.QRCode.LocationFloat = new DevExpress.Utils.PointFloat(97.97488F, 43.11599F);
            this.QRCode.Name = "QRCode";
            this.QRCode.SizeF = new System.Drawing.SizeF(64.06251F, 54.68752F);
            this.QRCode.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(71.93307F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(190.0668F, 41.14581F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.RunningBalance});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Running Balance: ";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell1.Weight = 1.0958106803412087D;
            // 
            // RunningBalance
            // 
            this.RunningBalance.Font = new DevExpress.Drawing.DXFont("Calibri", 8F);
            this.RunningBalance.Multiline = true;
            this.RunningBalance.Name = "RunningBalance";
            this.RunningBalance.StylePriority.UseFont = false;
            this.RunningBalance.StylePriority.UseTextAlignment = false;
            this.RunningBalance.Text = "RunningBalance";
            this.RunningBalance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.RunningBalance.TextFormatString = "{0:#0,#0.00}";
            this.RunningBalance.Weight = 0.90418931965879135D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.Balance});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Balance: ";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell4.Weight = 1.0958106803412087D;
            // 
            // Balance
            // 
            this.Balance.Font = new DevExpress.Drawing.DXFont("Calibri", 8F);
            this.Balance.Multiline = true;
            this.Balance.Name = "Balance";
            this.Balance.StylePriority.UseFont = false;
            this.Balance.StylePriority.UseTextAlignment = false;
            this.Balance.Text = "Balance";
            this.Balance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Balance.TextFormatString = "{0:#0,#0.00}";
            this.Balance.Weight = 0.90418931965879135D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.ClosingBalance});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Closing Balance: ";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell7.Weight = 1.0958106803412087D;
            // 
            // ClosingBalance
            // 
            this.ClosingBalance.Font = new DevExpress.Drawing.DXFont("Calibri", 8F);
            this.ClosingBalance.Multiline = true;
            this.ClosingBalance.Name = "ClosingBalance";
            this.ClosingBalance.StylePriority.UseFont = false;
            this.ClosingBalance.StylePriority.UseTextAlignment = false;
            this.ClosingBalance.Text = "ClosingBalance";
            this.ClosingBalance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.ClosingBalance.TextFormatString = "{0:#0,#0.00}";
            this.ClosingBalance.Weight = 0.90418931965879135D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrPanel1,
            this.xrLabel19,
            this.xrLabel21});
            this.PageHeader.HeightF = 143.723F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new DevExpress.Drawing.DXFont("Calibri", 10F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(262F, 23F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Customer Payment Reciept";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel28,
            this.xrLabel27,
            this.xrLabel40,
            this.xrLabel26,
            this.xrLabel35,
            this.xrLabel31});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 70F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(262.0001F, 70.0347F);
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel28.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyInfo].[CompanyRegNo]")});
            this.xrLabel28.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(82.52084F, 24.00002F);
            this.xrLabel28.Multiline = true;
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(177F, 13.99998F);
            this.xrLabel28.StylePriority.UseBorders = false;
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "xrLabel28";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel27
            // 
            this.xrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel27.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyInfo].[VatRegistrationNo]")});
            this.xrLabel27.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(82.52084F, 10.00005F);
            this.xrLabel27.Multiline = true;
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(177.0002F, 13.99997F);
            this.xrLabel27.StylePriority.UseBorders = false;
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.Text = "xrLabel27";
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel40.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyInfo].[AddressEnglish]")});
            this.xrLabel40.Font = new DevExpress.Drawing.DXFont("Calibri", 8F);
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(2.520859F, 52.00002F);
            this.xrLabel40.Multiline = true;
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(257F, 13.99991F);
            this.xrLabel40.StylePriority.UseBorders = false;
            this.xrLabel40.StylePriority.UseFont = false;
            this.xrLabel40.StylePriority.UseTextAlignment = false;
            this.xrLabel40.Text = "xrLabel40";
            this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel26.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyInfo].[CompanyNameEnglish]")});
            this.xrLabel26.Font = new DevExpress.Drawing.DXFont("Calibri", 8F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(2.520859F, 38.00002F);
            this.xrLabel26.Multiline = true;
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(257.0001F, 14F);
            this.xrLabel26.StylePriority.UseBorders = false;
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "xrLabel26";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel35.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(2.520818F, 24.00002F);
            this.xrLabel35.Multiline = true;
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(80F, 14F);
            this.xrLabel35.StylePriority.UseBorders = false;
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            this.xrLabel35.Text = "CR No: ";
            this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel31
            // 
            this.xrLabel31.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel31.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(2.520866F, 10.00002F);
            this.xrLabel31.Multiline = true;
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(80F, 14F);
            this.xrLabel31.StylePriority.UseBorders = false;
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            this.xrLabel31.Text = "Vat No: ";
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel19
            // 
            this.xrLabel19.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyInfo].[CompanyNameEnglish]")});
            this.xrLabel19.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33.41668F);
            this.xrLabel19.Multiline = true;
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(262.0001F, 18.00001F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "xrLabel2";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel21
            // 
            this.xrLabel21.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "CompanyInfo].[CategoryEnglish]")});
            this.xrLabel21.Font = new DevExpress.Drawing.DXFont("Calibri", 8F);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.41671F);
            this.xrLabel21.Multiline = true;
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(262F, 13.30559F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
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
            selectNode2.Alias = "Payment";
            sourceNode2.Alias = null;
            source2.DataMember = "";
            source2.DataSource = this.Payment;
            source2.Name = "Payment";
            sourceNode2.Source = source2;
            selectNode2.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Id", "Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Date", "Date"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Dates", "Dates"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "NatureOfPayment", "NatureOfPayment"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "VoucherNumber", "VoucherNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Narration", "Narration"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ChequeNumber", "ChequeNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BillsName", "BillsName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Amount", "Amount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "QRCode", "QRCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Vatamount", "Vatamount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ApprovalStatus", "ApprovalStatus"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentVoucherType", "PaymentVoucherType"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PettyCash", "PettyCash"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PurchaseInvoice", "PurchaseInvoice"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BillsId", "BillsId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PurchaseInvoiceCode", "PurchaseInvoiceCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SaleInvoice", "SaleInvoice"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "SaleInvoiceCode", "SaleInvoiceCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BankCashAccountId", "BankCashAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BankCashAccountName", "BankCashAccountName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ContactAccountId", "ContactAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ContactAccountName", "ContactAccountName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ContactAccountNameAr", "ContactAccountNameAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "DraftBy", "DraftBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ApprovedBy", "ApprovedBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "RejectBy", "RejectBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "VoidBy", "VoidBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Reason", "Reason"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "DraftDate", "DraftDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "ApprovedDate", "ApprovedDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "RejectDate", "RejectDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "VoidDate", "VoidDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentMode", "PaymentMode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentMethod", "PaymentMethod"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentMethods", "PaymentMethods"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentModes", "PaymentModes"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Image", "Image"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TaxMethod", "TaxMethod"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "TransactionId", "TransactionId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Prefix", "Prefix"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "VatName", "VatName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "Rate", "Rate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "AttachmentList", "AttachmentList"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentVoucherItems", "PaymentVoucherItems"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "BranchCode", "BranchCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "IsRefund", "IsRefund"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "DocumentType", "DocumentType"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "DocumentId", "DocumentId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode2, "PaymentRefunds", "PaymentRefunds")});
            selectNode2.Root = sourceNode2;
            this.federationDataSource1.Queries.AddRange(new DevExpress.DataAccess.DataFederation.QueryNode[] {
            selectNode1,
            selectNode2});
            // 
            // xrLabel6
            // 
            this.xrLabel6.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new DevExpress.Drawing.DXFont("Calibri", 8F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 15.08865F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(251.9999F, 15.08862F);
            this.xrLabel6.StylePriority.UseBorderDashStyle = false;
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel6.Visible = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Calibri", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(108.5625F, 15.08862F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Document Number: ";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel2.Visible = false;
            // 
            // Custom_PaymentReciept
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter,
            this.PageHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.CompanyInfo,
            this.Payment,
            this.federationDataSource1});
            this.DataMember = "Payment.PaymentVoucherItems";
            this.DataSource = this.federationDataSource1;
            this.Margins = new DevExpress.Drawing.DXMargins(5F, 3F, 12.77F, 1.438586F);
            this.PageWidth = 272;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.RollPaper = true;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.CompanyInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel VocherNo;
        private DevExpress.XtraReports.UI.XRLabel Date;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel NameCust;
        private DevExpress.XtraReports.UI.XRLabel CashAccountname;
        private DevExpress.XtraReports.UI.XRLabel CashAccount;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell RunningBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell Balance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell ClosingBalance;
        private DevExpress.XtraReports.UI.XRPictureBox QRCode;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel28;
        private DevExpress.XtraReports.UI.XRLabel xrLabel27;
        private DevExpress.XtraReports.UI.XRLabel xrLabel40;
        private DevExpress.XtraReports.UI.XRLabel xrLabel26;
        private DevExpress.XtraReports.UI.XRLabel xrLabel35;
        private DevExpress.XtraReports.UI.XRLabel xrLabel31;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource CompanyInfo;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource Payment;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
