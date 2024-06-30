using System.Configuration;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;

namespace Noble.Report.NobleReportServices
{
    public static class GetVatDtl
    {

        public static VatPayableListModel VatDtl(string fromDate, string toDate, string token, string serverName, string branchId)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Report/VatPayable?fromDate=" + fromDate + "&toDate=" + toDate + "&branchId=" + branchId);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var vatDtl = JsonConvert.DeserializeObject<VatPayableListModel>(content1);

            return vatDtl;
        }
    }
}