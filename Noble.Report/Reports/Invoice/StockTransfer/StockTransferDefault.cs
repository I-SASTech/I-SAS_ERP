using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.StockTransfer
{
    public partial class StockTransferDefault : DevExpress.XtraReports.UI.XtraReport
    {
        public StockTransferDefault(CompanyDto companyDto, StockTransferLookupModel stockInfo, PrintSetting printSetting)
        {
            InitializeComponent();
            A4_Default_CompanyDto.DataSource = companyDto;
            stockTransfer.DataSource = stockInfo;
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
