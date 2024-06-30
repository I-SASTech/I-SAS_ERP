using DevExpress.CodeParser;
using DevExpress.DataAccess.Native.Web;
using DevExpress.Pdf.Native;
using DevExpress.Xpo;
using DevExpress.XtraEditors.Filtering.Templates;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetBranchVocher
    {

        public static BranchVoucherModel GetBranchVocherdetials(Guid id,string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("PaymentVoucher/BranchVoucherDetail?Id=" + id);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var BranchVoucher = JsonConvert.DeserializeObject<BranchVoucherModel>(content1);
            return BranchVoucher;
        }
    }
}