using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Drawing.Printing;
using System.IO;

namespace Noble.Report.Reports.Invoice.BranchVoucher
{
    public partial class branchVoucher : DevExpress.XtraReports.UI.XtraReport
    {
        public branchVoucher(CompanyDto companyInfo, PrintSetting printSetting, BranchVoucherModel branchInfo)
        {
            InitializeComponent();
            Company.DataSource = companyInfo;
            branch.DataSource = branchInfo;
            if (printSetting.HeaderImageForPrint != null && printSetting.HeaderImageForPrint != "" && printSetting.HeaderImageForPrint != string.Empty)
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
