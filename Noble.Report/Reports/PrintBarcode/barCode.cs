using System;
using System.Drawing;
using Noble.Report.Models;
using System.IO;
using System.Drawing.Imaging;
using ZXing.QrCode;
using ZXing;

namespace Noble.Report.Reports.PrintBarcode
{
    public partial class barCode : DevExpress.XtraReports.UI.XtraReport
    {
        public barCode (string barcodeInfo,barCodeData bar,CompanyDto company)
        {
            InitializeComponent();
            string invoiceBarcode;

                xrLabel3.Text = bar.itemEngName;
            string barCode_Code = "";



            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128, // Specify the barcode format you want
                Options = new QrCodeEncodingOptions
                {
                    Width = 150,
                    Height = 45,
                }
            };
            var barcodeBitmap = barcodeWriter.Write(bar.code);
            using (var stream = new MemoryStream())
            {
                using (var ms = new MemoryStream())
                {

                    barcodeBitmap.Save(stream, ImageFormat.Png);
                    using (var bitmap = new Bitmap(barcodeBitmap))
                    {
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        invoiceBarcode = Convert.ToBase64String(ms.GetBuffer());
                    }
                    xrPictureBox1.Image = barcodeBitmap;
                }
            }

            xrLabel4.Text = bar.currency+" "+ bar.price.ToString("N2");

        }
    }
}
