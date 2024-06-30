using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetProductInventory
    {

        public static List<ProductList> GetProductInventoryDtl(string fromDate, string toDate, string wareHouseId, string Company, string token, string serverName, string branchId)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Report/ProductInventoryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&wareHouseId=" + wareHouseId + "&branchId=" + branchId);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var productList = JsonConvert.DeserializeObject<List<ProductList>>(content1);

            return productList;
        }
    }
}