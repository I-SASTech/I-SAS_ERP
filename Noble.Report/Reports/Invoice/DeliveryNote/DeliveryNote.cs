using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.DeliveryNote
{
    public partial class DeliveryNote : XtraReport
    {
        public DeliveryNote(CompanyDto companyDto, PrintSetting printSetting, DeliveryChallanLookUp deliveryChallanLookUp)
        {
            InitializeComponent();
            SA_A4_DeliveryNote_PrintSetting.DataSource = printSetting;
            SA_A4_DeliveryNote_CompanyDto.DataSource = companyDto;
            SA_A4_DeliveryNote_Delivery.DataSource = deliveryChallanLookUp;
            //deliveryChallanLookUp.DeliveryChallanItems.Where(x => x.Id == null).ToList().ForEach(b => b.Id = b.Id);
            xrLabel2.Text = deliveryChallanLookUp.InvoiceNote;
            IsSaleOrder.Text = deliveryChallanLookUp.IsQuotation? "Quotation No/ رقم الفاتورة" : "Sale Order / أمر بيع" ;

            int transitionIndex = -1;

            for (int i = 0; i < deliveryChallanLookUp.CustomerName.Length; i++)
            {
                // Check if the character is an Arabic character
                if (deliveryChallanLookUp.CustomerName[i] >= 0x0600 && deliveryChallanLookUp.CustomerName[i] <= 0x06FF)
                {
                    transitionIndex = i;
                    break;
                }
            }
            xrLabel6.Text = deliveryChallanLookUp.CustomerCode;

            string englishText = deliveryChallanLookUp.CustomerName.Substring(0, transitionIndex).Trim();
            string arabicText = deliveryChallanLookUp.CustomerName.Substring(transitionIndex).Trim();



            if (englishText==null)
            {
                englishText = "";
            }
            if (arabicText == null)
            {
                arabicText = "";
            }
           
            {
                englishName.Text = englishText+" "+ arabicText;
            }

         

            deliveryChallanLookUp.DeliveryChallanItems.Where(x => x.ProductName == null).ToList().ForEach(b => b.ProductName = b.Description);
            SA_A4_DeliveryNote_deliveryItem.DataSource = deliveryChallanLookUp.DeliveryChallanItems;
            if (!string.IsNullOrEmpty(printSetting.FooterImageForPrint) && printSetting.FooterImageForPrint != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
            if (!string.IsNullOrEmpty(printSetting.FooterImageForPrint) && printSetting.FooterImageForPrint != string.Empty)
            {
                byte[] footerData = Convert.FromBase64String(printSetting.FooterImageForPrint);
                MemoryStream Footerms = new MemoryStream(footerData);
                Bitmap FooterImg = new Bitmap(Footerms);
                Footer.Image = FooterImg;
            }
        }

    }
}
