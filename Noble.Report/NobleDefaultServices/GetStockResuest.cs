﻿using Noble.Report.Models;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Noble.Report.NobleDefaultServices
{
    public static class GetStockResuest
    {

        public static StockRequestLookupModel GetStockResuestDtl(string id, string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Purchase/GetStockRequestDetails?id=" + id);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var   Stock = JsonConvert.DeserializeObject<StockRequestLookupModel>(content1);

            return Stock;
        }
    }
}