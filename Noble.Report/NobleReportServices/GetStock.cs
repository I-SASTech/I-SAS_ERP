using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetStock
    {

        public static StockListLookUpModel GetStockDtl (string fromdate, string todate, string warehouseId,string search, string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Company/GetStockList?fromDate=" + fromdate + "&toDate=" + todate + "&warehouseId=" + warehouseId+ "&search="+search);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var Stock = JsonConvert.DeserializeObject<StockListLookUpModel>(content1);

            return Stock;
        }
    }
}