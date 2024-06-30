using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Configuration;


namespace Noble.Report.NobleDefaultServices
{
    public static class GetInActiveDay
    {

        public static DayStartLookUpModel GetInActiveDayStart(string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client2 = new RestClient(serverName);
            RestRequest request2 = new RestRequest("Product/GetDayStartDetail");
            request2.AddHeader("Authorization", "Bearer " + token);
            var response2 = client2.Execute(request2);
            var content1 = response2.Content;
            var daystart = JsonConvert.DeserializeObject<DayStartLookUpModel>(content1);

            return daystart;
        }
    }
}