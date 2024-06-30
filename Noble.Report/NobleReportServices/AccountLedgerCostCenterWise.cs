using DevExpress.CodeParser;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Noble.Report.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using RestSharp;
using System.Linq;

namespace Noble.Report.NobleDefaultServices
{
    public static class AccountLedgerCostCenterWise
    {

        public static List<CostCenterLookUpModel> AccountLedgerCostCenterWiseDtl(string fromdate,string todate,string accounts,string dateType, string token, string serverName) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);
            RestRequest request1 = new RestRequest("Company/GetCostCenterWiseLedgerLedgerList?fromDate =" + fromdate + "&toDate=" + todate + "&isLedger=true" + "&accountId=" +  "&dateType=" + dateType);

            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var AccountDtl = JsonConvert.DeserializeObject<List<CostCenterLookUpModel>>(content1);

            return AccountDtl;
        }
    }
}