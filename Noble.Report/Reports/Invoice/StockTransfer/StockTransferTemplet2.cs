using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.StockTransfer
{
    public partial class StockTransferTemplet2 : DevExpress.XtraReports.UI.XtraReport
    {
        public StockTransferTemplet2(CompanyDto companyInfo, StockTransferLookupModel stockInfo, PrintSetting printSetting)
        {
            InitializeComponent();

                stockTransfer.DataSource = stockInfo;
                if ((printSetting.TermsInEng != "" && printSetting.TermsInEng != null) || (printSetting.TermsInAr != "" && printSetting.TermsInAr != null))
                {
                    xrLabel36.Text = printSetting.TermsInEng.ToString();
                    xrLabel37.Text = printSetting.TermsInAr.ToString();
                }
                if (printSetting.FooterImageForPrint != null && printSetting.FooterImageForPrint != "" && printSetting.FooterImageForPrint != string.Empty)
                {
                    byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
                    MemoryStream ms = new MemoryStream(imageData);
                    Bitmap image = new Bitmap(ms);
                    XRPictureBox pictureBox = new XRPictureBox();
                    xrPictureBox1.Image = image;
                }
                if (printSetting.FooterImageForPrint != null && printSetting.FooterImageForPrint != "" && printSetting.FooterImageForPrint != string.Empty)
                {
                    byte[] footerData = Convert.FromBase64String(printSetting.FooterImageForPrint);
                    MemoryStream Footerms = new MemoryStream(footerData);
                    Bitmap FooterImg = new Bitmap(Footerms);
                    Footer.Image = FooterImg;
                }
        }

    }
}
