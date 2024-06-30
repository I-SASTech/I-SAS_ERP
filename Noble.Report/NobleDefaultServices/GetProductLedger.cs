using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetProductLedger
    {

        public static List<ProductLedgerModel> GetProductLedgerDtl(string ProductId, string fromdate, string todate, string token, string serverName, string branchId)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Report/ProductLedgerReport?productId=" + ProductId + "&fromDate=" + fromdate + "&toDate=" + todate + "&branchId=" + branchId);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var ProductLedgerModel = JsonConvert.DeserializeObject<List<ProductLedgerModel>>(content1);

            return ProductLedgerModel;
        }
    }
}