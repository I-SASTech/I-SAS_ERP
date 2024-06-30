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
using DevExpress.Utils.Extensions;

namespace Noble.Report.NobleDefaultServices
{
    public static class IncoiceReportMonth
    {

        public static List<SaleMonthWiseLookUpModel> IncoiceReportMonthDtl (string fromDate, string toDate, string PaymentType, string DocumentName,string token, string serverName,string branchId) {
        
            string ipPath = ConfigurationManager.ConnectionStrings["ipAdress"].ConnectionString;
            RestClient client1 = new RestClient(serverName);
            string url="";

                url = "Report/SaleInvoiceReportMonthWise?fromDate=";
 
            RestRequest request1 = new RestRequest(url + fromDate + "&toDate=" + toDate+"&paymentType="+PaymentType+"&DocumentName="+DocumentName + "&branchId=" + branchId);
            // add the token to the Authorization header of the request
            // add the token to the Authorization header of the request
            request1.AddHeader("Authorization", "Bearer " + token);
            var response1 = client1.Execute(request1);
            var content1 = response1.Content;
            var SaleReportDtl = JsonConvert.DeserializeObject<List<SaleMonthWiseLookUpModel>>(content1);
            //if (formName == "Sale")
            //{
            //    SaleReportDtl = SaleReportDtl.Where(x => x.DocumentType != DocumentType.ProformaInvoice).ToList();
            //    if (Invoice == "Sale Return")
            //    {
            //        SaleReportDtl = SaleReportDtl.Where(x => x.DocumentType != DocumentType.ProformaInvoice && x.DocumentType !=DocumentType.SaleInvoice).ToList();
            //        SaleReportDtl = SaleReportDtl.Select(x =>
            //        {
            //            if (x.DocumentType == DocumentType.SaleReturn)
            //            {
            //                x.InvoiceNo = x.InvoiceNo + " Return Invoice";
            //            }
            //            return x;
            //        }).ToList();
            //    }
            //    else if (Invoice == "Sale Invoice")
            //    {
            //        SaleReportDtl = SaleReportDtl.Where(x => x.DocumentType != DocumentType.ProformaInvoice && x.DocumentType != DocumentType.SaleReturn).ToList();
            //    }
            //    else 
            //    {
            //        SaleReportDtl = SaleReportDtl.Select(x =>
            //        {
            //            if (x.DocumentType == DocumentType.SaleReturn)
            //            {
            //                x.InvoiceNo = x.InvoiceNo + " Return Invoice";
            //            }
            //            return x;
            //        }).ToList();
            //    }
            //}
            //else
            //{
            //    SaleReportDtl = SaleReportDtl.Where(x => x.DocumentType != DocumentType.ProformaInvoice).ToList();
            //    if (Invoice == "Purchase Return")
            //    {
            //        SaleReportDtl = SaleReportDtl.Where(x => x.DocumentType != DocumentType.ProformaInvoice && x.IsSaleReturnPost ==true).ToList();
            //        SaleReportDtl = SaleReportDtl.Select(x =>
            //        {
            //            if (x.IsSaleReturnPost)
            //            {
            //                x.InvoiceNo = x.InvoiceNo + " Return Invoice";
            //            }
            //            return x;
            //        }).ToList();
            //    }
            //    else if (Invoice == "Purchase Invoice")
            //    {
            //        SaleReportDtl = SaleReportDtl.Where(x => x.DocumentType != DocumentType.ProformaInvoice && x.IsSaleReturnPost == false).ToList();
            //    }
            //    else
            //    {
            //        SaleReportDtl = SaleReportDtl.Select(x =>
            //        {
            //            if (x.IsSaleReturnPost)
            //            {
            //                x.InvoiceNo = x.InvoiceNo + " Return Invoice";
            //            }
            //            return x;
            //        }).ToList();
            //    }
            //}
            return SaleReportDtl;
        }
    }
}