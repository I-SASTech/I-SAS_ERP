using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noble.Report.NobleDefaultServices
{
    public static class GetGoodReceive
    {
        public static GoodReceiveLookUpModel GetGoodReceiveDetails(Guid Id, bool isMultiUnit, string token, string serverName)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Purchase/GoodReceiveDetail?Id=" + Id + "&isMultiUnit=" + isMultiUnit+"&isReport="+true);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var purchaseOrderDetails = JsonConvert.DeserializeObject<GoodReceiveLookUpModel>(content1);

            return purchaseOrderDetails;
        }
    }
}
