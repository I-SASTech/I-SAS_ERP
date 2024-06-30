using Noble.Report.Models;

namespace Noble.Report.Reports.PurchaseModule.GoodsReceive
{
    public partial class GoodsReceiveReport : DevExpress.XtraReports.UI.XtraReport
    {
        public GoodsReceiveReport(CompanyDto companyDto, PrintSetting printSetting, GoodReceiveLookUpModel purchaseOrderDetails)
        {
            InitializeComponent();
            PrintSetting.DataSource = printSetting;
            CompanyDto.DataSource = companyDto;
            GoodReceive.DataSource = purchaseOrderDetails;
            Address.Text = purchaseOrderDetails.SupplierAddress;
            xrLabel63.Text = companyDto.CompanyNameEnglish + " - " + companyDto.PhoneNo + " - " + companyDto.CompanyEmail;
        }
    }
}
