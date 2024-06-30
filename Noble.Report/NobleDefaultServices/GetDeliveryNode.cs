using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Noble.Report.NobleDefaultServices
{
    public static class GetDeliveryNode
    {

        public static DeliveryChallanLookUp GetDeliveryNodeDetail(Guid id, bool isFifo, int openBatch, bool isSale, bool deliveryChallan, string token, string serverName)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Purchase/DeliveryChallanDetail?Id=" + id + "&isReport=" + true);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var DeliveryNote = JsonConvert.DeserializeObject<DeliveryChallanLookUp>(content1);

            return DeliveryNote;
        }
    }
    }