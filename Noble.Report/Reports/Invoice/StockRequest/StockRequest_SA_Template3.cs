using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Noble.Report.Reports.Invoice.StockRequest
{
    public partial class StockRequest_SA_Template3 : DevExpress.XtraReports.UI.XtraReport
    {
        public StockRequest_SA_Template3(CompanyDto companyDto, StockRequestLookupModel stockInfo, PrintSetting printSetting)
        {
            InitializeComponent();
            Company.DataSource= companyDto;
            Stock.DataSource= stockInfo;
            Printer.DataSource = printSetting;
            try
            {
                if (printSetting.HeaderImage != null && printSetting.HeaderImage != "" && printSetting.HeaderImage != string.Empty)
                {
                    byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
                    MemoryStream ms = new MemoryStream(imageData);
                    Bitmap image = new Bitmap(ms);
                    XRPictureBox pictureBox = new XRPictureBox();
                    xrPictureBox1.Image = image;
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                  xrPictureBox1.ImageUrl = printSetting.HeaderImage;
            }
           
        }

    }
}
