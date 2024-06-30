using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using iTextSharp.text;
using System.Collections.Generic;
using System.IO;

namespace Noble.Report.Reports.Reports.InventoryReport.AdvanceInventory
{
    public partial class AdvanceIInventoryItem : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceIInventoryItem(CompanyDto companyInfo, List<AdvanceInventoryItemLookupMdoel> inventoryInfo,DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();
            CompanyInfo.DataSource = companyInfo;
            Inventory.DataSource= inventoryInfo;
            xrLabel8.Text = fromTime.ToString("dd MMMM yyyy");
            xrLabel6.Text = toTime.ToString("dd MMMM yyyy");
            if (companyInfo.Base64Logo != null && companyInfo.Base64Logo != "" && companyInfo.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyInfo.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
