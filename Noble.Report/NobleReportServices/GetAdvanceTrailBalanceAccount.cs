using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;

namespace Noble.Report.NobleReportServices
{
    public static class GetAdvanceTrailBalanceAccount
    {

        public static List<AdvanceTrialBalanceAccountLookupModel> GetAdvanceTrailBalanceDtl (string fromDate, string toDate, string numberOfPeriods, string compareWith, string token, string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Report/GetAdvanceTrialBalanceAccount?fromDate=" + fromDate + "&toDate=" + toDate + "&numberOfPeriods=" + numberOfPeriods + "&compareWith=" + compareWith);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var trailBalance = JsonConvert.DeserializeObject<List<AdvanceTrialBalanceAccountLookupModel>>(content1);

            return trailBalance;
        }
    }
}