using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetPurchasePost
    {

        public static PurchasePostLookupModel  GetPurchasePostDetails(Guid Id, bool isReturnView, bool isMultiUnit, string token, string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("PurchasePost/PurchasePostDetail?Id=" + Id + "&isMultiUnit=" + isMultiUnit + "&isReturnView=" + isReturnView+ "&isReport=" + true);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var   purchaseDetails = JsonConvert.DeserializeObject<PurchasePostLookupModel>(content1);

            return purchaseDetails;
        }
    }
}