using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.StockReceived
{
    public partial class stockReceivedDefualt : DevExpress.XtraReports.UI.XtraReport
    {
        public stockReceivedDefualt(CompanyDto companyDto, StockReceivedLookupModel stockInfo, PrintSetting printSetting)
        {
            InitializeComponent();
            A4_Default_CompanyDto.DataSource = companyDto;
            Stock.DataSource = stockInfo;
            Printer.DataSource = printSetting;
            if (companyDto.Base64Logo != null && companyDto.Base64Logo != "" && companyDto.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
