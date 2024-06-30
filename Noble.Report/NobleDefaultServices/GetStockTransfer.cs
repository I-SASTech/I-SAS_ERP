using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetStockTransfer
    {

        public static StockTransferLookupModel GetStockTransferDtl(string id,string isStockReceived,string branchId, string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Purchase/GetStockTransferDetails?id=" + id+ "&isStockReceived="+ isStockReceived+ "&branchId="+ branchId);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var StockTransfer = JsonConvert.DeserializeObject<StockTransferLookupModel>(content1);

            return StockTransfer;
        }
    }
}