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
    public static class GetInvoiceDetails
    {
        public static SaleDetailLookupModel InvoiceDetails(Guid id, bool isReturn, string invoiceNo, bool isFifo, int openBatch, bool colorVariants, bool deliveryChallan, bool simpleQuery, string token,string serverName)
        {

            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client = new RestClient(serverName);
            RestRequest request = new RestRequest("Sale/SaleDetail?Id=" + id + "&isFifo=" + isFifo + "&openBatch=" + openBatch + "&colorVariants=" + colorVariants + "&simpleQuery=" + simpleQuery+"&isReport="+true);
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Execute(request);
            var content = response.Content;
            var saleDetails = JsonConvert.DeserializeObject<SaleDetailLookupModel>(content);
            return saleDetails;
        }
    }
}