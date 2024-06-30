using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.ReturnInvoice
{
    public partial class ReturnInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public ReturnInvoice(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            SA_A4_Temp2_PrintSetting.DataSource = printSetting;
            SA_A4_Temp2_CompanyDto.DataSource = companyDto;
            SA_A4_Temp2SaleDetails.DataSource = saleDetail;
            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            SA_A4_Temp2_SaleItem.DataSource = saleDetail.SaleItems;
            var invoicenameenglish = "";
            var invoicenameengarabic = "";

            if (saleDetail.CustomerCategory == "B2B – Business to Business")
            {
                invoicenameenglish = "Tax Invoice";
                invoicenameengarabic = "فاتورة ضريبية";
            }
            else if (saleDetail.CustomerCategory == "B2C – Business to Client")
            {

                invoicenameenglish = "Simplified Tax Invoice";
                invoicenameengarabic = "فاتورة ضريبية مبسطة";
            }

            int transitionIndex = -1;

            for (int i = 0; i < saleDetail.CustomerName.Length; i++)
            {
                // Check if the character is an Arabic character
                if (saleDetail.CustomerName[i] >= 0x0600 && saleDetail.CustomerName[i] <= 0x06FF)
                {
                    transitionIndex = i;
                    break;
                }
            }

            string englishText = saleDetail.CustomerName.Substring(0, transitionIndex).Trim();
            string arabicText = saleDetail.CustomerName.Substring(transitionIndex).Trim();



            if (string.IsNullOrEmpty(englishText))
            {
                englishName.Text = "";
            }
            else
            {
                englishName.Text = englishText;
            }

            if (string.IsNullOrEmpty(arabicText))
            {
                arabicName.Text = "";
            }
            else
            {
                arabicName.Text = arabicText;
            }

            InvoiceType.Text = saleDetail.IsCredit ? "Credit" : "Cash";
            InvoiceEn.Text = invoicenameenglish.ToString();
            InvoiceAr.Text = invoicenameengarabic.ToString();
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
