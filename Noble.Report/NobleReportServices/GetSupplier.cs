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
    public static class GetSupplier
    {

        public static List<PurchasePostLookupModel> GetSupplierDtl(string fromDate, string toDate, string supplierId, string token, string serverName, string branchId)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);

            // create a new RestRequest instance for the API endpoint
            RestRequest request1 = new RestRequest("Report/SupplierPurchaseReport?supplierId=" + supplierId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&branchId=" + branchId);

            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var AccountDtl = JsonConvert.DeserializeObject<List<PurchasePostLookupModel>>(content1);

            return AccountDtl;
        }
    }
}