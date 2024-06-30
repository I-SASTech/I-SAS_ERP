namespace Noble.Report.Reports.Vochers
{
    partial class PaymentVocher
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
            DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo objectConstructorInfo1 = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.federationDataSource1 = new DevExpress.DataAccess.DataFederation.FederationDataSource();
            this.Payment = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.QRCode = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.RunningBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Balance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ClosingBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.RecievedBy = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.CashAccount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.SumofSR = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.NameCust = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.VocherNo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.Date = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.Footer = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,
            this.Footer});
            this.Detail.HeightF = 384.5029F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 18F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 18F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // federationDataSource1
            // 
            this.federationDataSource1.Name = "federationDataSource1";
            selectNode1.Alias = "Payment";
            sourceNode1.Alias = null;
            source1.DataMember = "";
            source1.DataSource = this.Payment;
            source1.Name = "Payment";
            sourceNode1.Source = source1;
            selectNode1.Expressions.AddRange(new DevExpress.DataAccess.DataFederation.ISelectExpression[] {
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Id", "Id"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Date", "Date"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Dates", "Dates"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VoucherNumber", "VoucherNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Narration", "Narration"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ChequeNumber", "ChequeNumber"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BillsName", "BillsName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Amount", "Amount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Vatamount", "Vatamount"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "PurchaseInvoice", "PurchaseInvoice"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BillsId", "BillsId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "PurchaseInvoiceCode", "PurchaseInvoiceCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SaleInvoice", "SaleInvoice"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "SaleInvoiceCode", "SaleInvoiceCode"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BankCashAccountId", "BankCashAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "BankCashAccountName", "BankCashAccountName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ContactAccountId", "ContactAccountId"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ContactAccountName", "ContactAccountName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ContactAccountNameAr", "ContactAccountNameAr"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "DraftBy", "DraftBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ApprovedBy", "ApprovedBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "RejectBy", "RejectBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VoidBy", "VoidBy"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Reason", "Reason"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "DraftDate", "DraftDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "ApprovedDate", "ApprovedDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "RejectDate", "RejectDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VoidDate", "VoidDate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Image", "Image"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "TaxMethod", "TaxMethod"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "VatName", "VatName"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "Rate", "Rate"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "CurrentBalance", "CurrentBalance"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "runningBalance", "runningBalance"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "closingBalance", "closingBalance"),
            new DevExpress.DataAccess.DataFederation.SelectColumnExpression(sourceNode1, "QRCode", "QRCode")});
            selectNode1.Root = sourceNode1;
            this.federationDataSource1.Queries.AddRange(new DevExpress.DataAccess.DataFederation.QueryNode[] {
            selectNode1});
            // 
            // Payment
            // 
            this.Payment.Constructor = objectConstructorInfo1;
            this.Payment.DataSource = typeof(Noble.Report.Models.PaymentVoucherLookUpModel);
            this.Payment.Name = "Payment";
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.QRCode,
            this.xrTable1,
            this.xrLabel29,
            this.RecievedBy,
            this.xrLabel28,
            this.CashAccount,
            this.xrLabel24,
            this.xrLabel25,
            this.SumofSR,
            this.xrLabel21,
            this.xrLabel22,
            this.NameCust,
            this.xrLabel15,
            this.xrLabel16,
            this.VocherNo,
            this.xrLabel10,
            this.xrLabel11,
            this.Date,
            this.xrLabel4,
            this.xrLabel2});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(791.0004F, 340.0584F);
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // QRCode
            // 
            this.QRCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.QRCode.LocationFloat = new DevExpress.Utils.PointFloat(138.5294F, 230.0585F);
            this.QRCode.Name = "QRCode";
            this.QRCode.SizeF = new System.Drawing.SizeF(100F, 100F);
            this.QRCode.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.QRCode.StylePriority.UseBorders = false;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(413.8923F, 240.0697F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(313.1175F, 74.99998F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.RunningBalance,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Running Balance: ";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell1.Weight = 1.0958106803412087D;
            // 
            // RunningBalance
            // 
            this.RunningBalance.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.RunningBalance.Multiline = true;
            this.RunningBalance.Name = "RunningBalance";
            this.RunningBalance.StylePriority.UseFont = false;
            this.RunningBalance.Text = "RunningBalance";
            this.RunningBalance.TextFormatString = "{0:#0,#0.00}";
            this.RunningBalance.Weight = 0.90418931965879135D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "الميزان الجاري";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell3.Weight = 1D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.Balance,
            this.xrTableCell6});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Balance: ";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell4.Weight = 1.0958106803412087D;
            // 
            // Balance
            // 
            this.Balance.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.Balance.Multiline = true;
            this.Balance.Name = "Balance";
            this.Balance.StylePriority.UseFont = false;
            this.Balance.Text = "Balance";
            this.Balance.TextFormatString = "{0:#0,#0.00}";
            this.Balance.Weight = 0.90418931965879135D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "الرصيد ";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell6.Weight = 1D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.ClosingBalance,
            this.xrTableCell9});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Closing Balance: ";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell7.Weight = 1.0958106803412087D;
            // 
            // ClosingBalance
            // 
            this.ClosingBalance.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.ClosingBalance.Multiline = true;
            this.ClosingBalance.Name = "ClosingBalance";
            this.ClosingBalance.StylePriority.UseFont = false;
            this.ClosingBalance.Text = "ClosingBalance";
            this.ClosingBalance.TextFormatString = "{0:#0,#0.00}";
            this.ClosingBalance.Weight = 0.90418931965879135D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "رصيد الإغلاق";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell9.Weight = 1D;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel29.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(90.28139F, 202.9902F);
            this.xrLabel29.Multiline = true;
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(31.37253F, 15.08862F);
            this.xrLabel29.StylePriority.UseBorders = false;
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.Text = "For:";
            // 
            // RecievedBy
            // 
            this.RecievedBy.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.RecievedBy.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.RecievedBy.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.RecievedBy.LocationFloat = new DevExpress.Utils.PointFloat(584.3696F, 172.1568F);
            this.RecievedBy.Multiline = true;
            this.RecievedBy.Name = "RecievedBy";
            this.RecievedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.RecievedBy.SizeF = new System.Drawing.SizeF(173.2284F, 15.08862F);
            this.RecievedBy.StylePriority.UseBorderDashStyle = false;
            this.RecievedBy.StylePriority.UseBorders = false;
            this.RecievedBy.StylePriority.UseFont = false;
            this.RecievedBy.StylePriority.UseTextAlignment = false;
            this.RecievedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel28.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(452.598F, 172.1568F);
            this.xrLabel28.Multiline = true;
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(131.7717F, 15.08862F);
            this.xrLabel28.StylePriority.UseBorders = false;
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "Received by";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // CashAccount
            // 
            this.CashAccount.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.CashAccount.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.CashAccount.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.CashAccount.LocationFloat = new DevExpress.Utils.PointFloat(147.8991F, 149.9576F);
            this.CashAccount.Multiline = true;
            this.CashAccount.Name = "CashAccount";
            this.CashAccount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.CashAccount.SizeF = new System.Drawing.SizeF(187.9341F, 15.08862F);
            this.CashAccount.StylePriority.UseBorderDashStyle = false;
            this.CashAccount.StylePriority.UseBorders = false;
            this.CashAccount.StylePriority.UseFont = false;
            this.CashAccount.StylePriority.UseTextAlignment = false;
            this.CashAccount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel24.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(335.8333F, 149.9576F);
            this.xrLabel24.Multiline = true;
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(60.90216F, 15.08862F);
            this.xrLabel24.StylePriority.UseBorders = false;
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = ":حساب نقدي ";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel25.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(30.83327F, 149.9576F);
            this.xrLabel25.Multiline = true;
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(102.8613F, 15.08862F);
            this.xrLabel25.StylePriority.UseBorders = false;
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Cash Account:";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // SumofSR
            // 
            this.SumofSR.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.SumofSR.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.SumofSR.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amount]")});
            this.SumofSR.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.SumofSR.LocationFloat = new DevExpress.Utils.PointFloat(121.6539F, 115.4378F);
            this.SumofSR.Multiline = true;
            this.SumofSR.Name = "SumofSR";
            this.SumofSR.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.SumofSR.SizeF = new System.Drawing.SizeF(612.6599F, 15.08862F);
            this.SumofSR.StylePriority.UseBorderDashStyle = false;
            this.SumofSR.StylePriority.UseBorders = false;
            this.SumofSR.StylePriority.UseFont = false;
            this.SumofSR.StylePriority.UseTextAlignment = false;
            this.SumofSR.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.SumofSR.TextFormatString = "{0:#,#0.00}";
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel21.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(734.3137F, 115.4378F);
            this.xrLabel21.Multiline = true;
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(55.3623F, 15.08862F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = ":المبلغ";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel22.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(30.83327F, 115.4378F);
            this.xrLabel22.Multiline = true;
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(90.82056F, 15.08862F);
            this.xrLabel22.StylePriority.UseBorders = false;
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "Sum of S.R:";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // NameCust
            // 
            this.NameCust.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.NameCust.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.NameCust.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ContactAccountName]")});
            this.NameCust.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.NameCust.LocationFloat = new DevExpress.Utils.PointFloat(121.6539F, 75.70411F);
            this.NameCust.Multiline = true;
            this.NameCust.Name = "NameCust";
            this.NameCust.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.NameCust.SizeF = new System.Drawing.SizeF(612.6599F, 15.08862F);
            this.NameCust.StylePriority.UseBorderDashStyle = false;
            this.NameCust.StylePriority.UseBorders = false;
            this.NameCust.StylePriority.UseFont = false;
            this.NameCust.StylePriority.UseTextAlignment = false;
            this.NameCust.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel15.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(734.3137F, 75.7041F);
            this.xrLabel15.Multiline = true;
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(55.36237F, 15.08862F);
            this.xrLabel15.StylePriority.UseBorders = false;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = ":استمنامن";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel16.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(30.83321F, 75.70411F);
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(90.82059F, 15.08862F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "MR:";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // VocherNo
            // 
            this.VocherNo.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.VocherNo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.VocherNo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[VoucherNumber]")});
            this.VocherNo.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.VocherNo.LocationFloat = new DevExpress.Utils.PointFloat(502.2619F, 36.96079F);
            this.VocherNo.Multiline = true;
            this.VocherNo.Name = "VocherNo";
            this.VocherNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.VocherNo.SizeF = new System.Drawing.SizeF(224.7479F, 15.08862F);
            this.VocherNo.StylePriority.UseBorderDashStyle = false;
            this.VocherNo.StylePriority.UseBorders = false;
            this.VocherNo.StylePriority.UseFont = false;
            this.VocherNo.StylePriority.UseTextAlignment = false;
            this.VocherNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel10.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(730.9902F, 36.96079F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(58.68591F, 15.08862F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = ":رقم القسيمة";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(413.8923F, 36.96079F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(88.3696F, 15.08862F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Voucher No:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // Date
            // 
            this.Date.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.Date.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Date.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Date]")});
            this.Date.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.Date.LocationFloat = new DevExpress.Utils.PointFloat(84.88924F, 36.96079F);
            this.Date.Multiline = true;
            this.Date.Name = "Date";
            this.Date.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Date.SizeF = new System.Drawing.SizeF(223.9833F, 15.08862F);
            this.Date.StylePriority.UseBorderDashStyle = false;
            this.Date.StylePriority.UseBorders = false;
            this.Date.StylePriority.UseFont = false;
            this.Date.StylePriority.UseTextAlignment = false;
            this.Date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.Date.TextFormatString = "{0:dd MMMM yyyy}";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Arial", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(308.8726F, 36.96079F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(37.49997F, 15.08862F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = ":تاريخ";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(30.83334F, 36.96079F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(54.05591F, 15.08862F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Date :";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // Footer
            // 
            this.Footer.LocationFloat = new DevExpress.Utils.PointFloat(0F, 340.0584F);
            this.Footer.Name = "Footer";
            this.Footer.SizeF = new System.Drawing.SizeF(789.6761F, 44.44446F);
            this.Footer.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            this.ReportHeader.HeightF = 110.1863F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(791F, 99.81994F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // PaymentVocher
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.Payment,
            this.federationDataSource1});
            this.DataMember = "Payment";
            this.DataSource = this.federationDataSource1;
            this.Landscape = true;
            this.Margins = new DevExpress.Drawing.DXMargins(18F, 18F, 18F, 18F);
            this.PageHeight = 583;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A5;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.federationDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Payment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource Payment;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPictureBox QRCode;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell RunningBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell Balance;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell ClosingBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel29;
        private DevExpress.XtraReports.UI.XRLabel RecievedBy;
        private DevExpress.XtraReports.UI.XRLabel xrLabel28;
        private DevExpress.XtraReports.UI.XRLabel CashAccount;
        private DevExpress.XtraReports.UI.XRLabel xrLabel24;
        private DevExpress.XtraReports.UI.XRLabel xrLabel25;
        private DevExpress.XtraReports.UI.XRLabel SumofSR;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRLabel xrLabel22;
        private DevExpress.XtraReports.UI.XRLabel NameCust;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.XRLabel VocherNo;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel Date;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRPictureBox Footer;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
