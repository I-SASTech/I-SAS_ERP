using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.CreditNote
{
    public partial class CreditNote : DevExpress.XtraReports.UI.XtraReport
    {
        public CreditNote(CompanyDto companyDto, PrintSetting printSetting, CreditNotesModel creditNotesModel)
        {
            InitializeComponent();
            PrintSetting.DataSource = printSetting;
            CompanyDtl.DataSource = companyDto;
            CreditNoteDtl.DataSource = creditNotesModel;
            xrLabel27.Text=creditNotesModel.InvoiceNote;
            int transitionIndex = -1;

            for (int i = 0; i < creditNotesModel.CustomerNameAr.Length; i++)
            {
                // Check if the character is an Arabic character
                if (creditNotesModel.CustomerNameAr[i] >= 0x0600 && creditNotesModel.CustomerNameAr[i] <= 0x06FF)
                {
                    transitionIndex = i;
                    break;
                }
            }

            string englishText = creditNotesModel.CustomerNameAr.Substring(0, transitionIndex).Trim();
            string arabicText = creditNotesModel.CustomerNameAr.Substring(transitionIndex).Trim();



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
            BankDetails.Text = printSetting.BankIcon1 + "   " + printSetting.BankAccount1;
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
