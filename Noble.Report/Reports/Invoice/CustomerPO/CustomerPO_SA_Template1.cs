using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.CustomerPO
{
    public partial class CustomerPO_SA_Template1 : DevExpress.XtraReports.UI.XtraReport
    {
        public CustomerPO_SA_Template1 (CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            A4_Default_PrintSetting.DataSource = printSetting;
            A4_Default_CompanyDto.DataSource = companyDto;
            A4_Default_SaleDetails.DataSource = saleDetail;
            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            A4_Default_SaleItem.DataSource = saleDetail.SaleItems;
            var total = saleDetail.SaleItems.Sum(b => b.TotalAmount);
            InvoiceType.Text = saleDetail.IsCredit ? "Credit ائتمان" : "Cash نقدي";
            var qty = saleDetail.SaleItems.Count();
            xrLabel19.Text = qty + ":الكمية الإجمالية";
            if (saleDetail.BarCode != null && saleDetail.BarCode != "" && saleDetail.BarCode != string.Empty)
            {
                byte[] barCode = Convert.FromBase64String(saleDetail.BarCode);
                MemoryStream barCodes = new MemoryStream(barCode);
                Bitmap barCodesImage = new Bitmap(barCodes);
                xrPictureBox2.Image = barCodesImage;
            }
            
        }

    }
}
