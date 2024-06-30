using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.CustomerPO
{
    public partial class CustomerPO_SA_Default_Templet : DevExpress.XtraReports.UI.XtraReport
    {
        public CustomerPO_SA_Default_Templet (CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            A4_Default_PrintSetting.DataSource = printSetting;
            A4_Default_CompanyDto.DataSource = companyDto;
            A4_Default_SaleDetails.DataSource = saleDetail;
            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            A4_Default_SaleItem.DataSource = saleDetail.SaleItems;
            var total = saleDetail.SaleItems.Sum(b => b.TotalAmount);

            var qty = 0;
            foreach (var item in saleDetail.SaleItems)
            {
                qty = qty + Convert.ToInt32(item.Quantity);
            }
            xrLabel47.Text = qty + ":الكمية الإجمالية";

            if (saleDetail.BarCode != null && saleDetail.BarCode != "" && saleDetail.BarCode != string.Empty)
            {
                byte[] barCode = Convert.FromBase64String(saleDetail.BarCode);
                MemoryStream barCodes = new MemoryStream(barCode);
                Bitmap barCodesImage = new Bitmap(barCodes);
                xrPictureBox2.Image = barCodesImage;
            }
            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            if ((saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) || (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null))
            {
                removeinvoicetagesEn = (saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) ? saleDetail.TermAndCondition.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null) ? saleDetail.TermAndConditionAr.Replace("<p>", " ").Replace("</p>", " ") : "";

            }
            else
            {
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            }
            xrLabel3.Text = removeinvoicetagesEn.ToString();
            xrLabel56.Text = removeinvoicetagesAr.ToString();
            if (companyDto.Base64Logo != null && companyDto.Base64Logo != "" && companyDto.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                Image.Image = image;
            }

        }

    }
}
