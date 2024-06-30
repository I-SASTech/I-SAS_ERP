using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.PerformaInvoice
{
    public partial class PerformaInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public PerformaInvoice(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();

            SA_A4_Temp2_PrintSetting.DataSource = printSetting;
            SA_A4_Temp2_CompanyDto.DataSource = companyDto;
            SA_A4_Temp2SaleDetails.DataSource = saleDetail;
            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            SA_A4_Temp2_SaleItem.DataSource = saleDetail.SaleItems;

            InvoiceType.Text = saleDetail.IsCredit ? "Credit" : "Cash";
            BankDetails.Text = printSetting.BankIcon1 + "   " + printSetting.BankAccount1;

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
            xrLabel36.Text = removeinvoicetagesEn.ToString();
            xrLabel37.Text = removeinvoicetagesAr.ToString();

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
