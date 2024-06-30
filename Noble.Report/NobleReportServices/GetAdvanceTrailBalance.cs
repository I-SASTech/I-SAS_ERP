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
    public static class GetAdvanceTrailBalance
    {

        public static List<AdvanceTrialBalanceLookupModel> GetAdvanceTrailBalanceDtl(string fromDate, string toDate, string numberOfPeriods, string compareWith, string token, string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Report/GetAdvanceTrialBalanceReport?fromDate=" + fromDate + "&toDate=" + toDate + "&numberOfPeriods=" + numberOfPeriods + "&compareWith=" + compareWith);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var trailBalance = JsonConvert.DeserializeObject<List<AdvanceTrialBalanceLookupModel>>(content1);

            return trailBalance;
        }
    }
}