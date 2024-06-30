using DevExpress.CodeParser;
using DevExpress.DataAccess.Native.Web;
using DevExpress.Pdf.Native;
using DevExpress.Xpo;
using DevExpress.XtraEditors.Filtering.Templates;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetPaymentVocher
    {

        public static PaymentVoucherLookUpModel  GetPaymentVocherdetials(Guid id,string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("PaymentVoucher/PaymentVoucherDetails?Id=" + id + "&isEmail=" + true+"&isReport="+true);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var   paymentVoucher = JsonConvert.DeserializeObject<PaymentVoucherLookUpModel>(content1);
            //RestClient client2 = new RestClient(serverName);
            //RestRequest request2 = new RestRequest("Contact/GetCustomerRunningBalance?id="+paymentVoucher.ContactAccountId +"&date="+ paymentVoucher.Date);
            //request2.AddHeader("Authorization", "Bearer " + token);
            //var response2 = client2.Execute(request2);
            //var content2 = response2.Content;
            //var CurrentBalance = JsonConvert.DeserializeObject<decimal>(content2);
            //string runningBalance = "";
            //string closingBalance = "";

            //if (CurrentBalance != null)
            //{
            //    closingBalance = (CurrentBalance) >= 0 ? "Dr" + ((CurrentBalance * +1).ToString("N2")): "Cr" + ((CurrentBalance * (-1)).ToString("N2"));
            //    if ((CurrentBalance) >= 0)
            //    {
            //       runningBalance =( (CurrentBalance) +paymentVoucher.Amount).ToString();
            //        runningBalance = (decimal.Parse( runningBalance)) >= 0 ?("Dr" + (decimal.Parse(runningBalance) * 1).ToString("N2")) : "Cr" +( (decimal.Parse(runningBalance)) * (-1)).ToString("N2");
            //    }
            //    else
            //    {
            //       runningBalance = ((CurrentBalance) - paymentVoucher.Amount).ToString();
            //        if (decimal.Parse(runningBalance) >= 0)
            //        {
            //            runningBalance = (decimal.Parse(runningBalance)) >= 0 ? ("Dr" + (decimal.Parse(runningBalance) * 1).ToString("N2")) : "Cr" + ((decimal.Parse(runningBalance)) * (-1)).ToString("N2");
            //        }
            //        else
            //        {
            //            runningBalance = (decimal.Parse(runningBalance)) >= 0 ? ("Dr" + (decimal.Parse(runningBalance) * 1).ToString("N2")) : "Cr" + ((decimal.Parse(runningBalance)) * (-1)).ToString("N2");
            //        }
            //    }
            //}
            return paymentVoucher;
        }
    }
}