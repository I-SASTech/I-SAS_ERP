using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Enums;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Noble.Report.NobleDefaultServices
{
    public static class GetTerminalDtl
    {

        public static TerminalLookUpModel  GetTerminalInfo(Guid id,Guid companyId,string token,string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Company/TerminalDetail?Id=" + id+ "&companyId="+ companyId);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
          var Terminal = JsonConvert.DeserializeObject<TerminalLookUpModel>(content1);

            return Terminal;
        }
    }
}