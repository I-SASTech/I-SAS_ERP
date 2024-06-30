using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.StockTransfer
{
    public partial class StockTransferTemplet1 : DevExpress.XtraReports.UI.XtraReport
    {
        public StockTransferTemplet1(CompanyDto companyInfo, StockTransferLookupModel stockInfo, PrintSetting printSetting)
        {
            InitializeComponent();
            stockTransfer.DataSource = stockInfo;
            A4_Default_PrintSetting.DataSource = printSetting;
            if (printSetting.HeaderImage != null && printSetting.HeaderImage != "" && printSetting.HeaderImage != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
