using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Reports.InventoryReport.AdvanceInventory.AdvanceQuantityWiseInventoryItem
{
    public partial class AdvanceQuantityWiseInventoryItem : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceQuantityWiseInventoryItem(CompanyDto companyInfo, List<AdvanceQuantityWiseInventoryItemLookupModel> inventoryInfo, DateTime fromTime, DateTime toTime)
        {
            InitializeComponent();
            CompanyInfo.DataSource= companyInfo;
            Inventory.DataSource= inventoryInfo;
            xrLabel9.Text = fromTime.ToString("dd MMMM yyyy");
            xrLabel7.Text = toTime.ToString("dd MMMM yyyy");
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
