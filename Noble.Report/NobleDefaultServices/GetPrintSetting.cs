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
    public static class GetPrintSetting
    {
        public static PrintSetting PrintDetails(string branchId,string token,string serverName)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client = new RestClient(serverName);
            RestRequest request = new RestRequest("Sale/PrintSettingDetail?branchId=" + branchId);
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Execute(request);
            var content = response.Content;
            var PrintDetail = JsonConvert.DeserializeObject<PrintSetting>(content);
            return PrintDetail;
        }
        }
    }