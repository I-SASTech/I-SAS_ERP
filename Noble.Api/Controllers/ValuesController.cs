using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Focus.Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Noble.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
     
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //var url = "https://intg.taqat.sa:7008/CS/SMS/v1/SmsService";
            //var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpRequest.Method = "POST";
            //httpRequest.ContentType = "text/xml;charset=\"utf-8\"";
            //httpRequest.Accept = "text/xml";
            //httpRequest.Headers["Authorization"] = "Basic c3Vib2xfdW5pOlB4SGtLcVpzU0RCQWl4NUszNUxD";
            //var data = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
            //                xmlns:v1='http://hrdf.gov.sa/ConnectivityServices/SMS/SmsService/v1'>
            //                 <soapenv:Header/>
            //                 <soapenv:Body>
            //                 <v1:SendSmsLoginRequest>
            //                 <v1:message>مرحبا عزيزي بندر رسالة اختبار &#13;&#10;&#13;&#10;&#13;&#10; سوبول يونيف باللغة العربية</v1:message>
            //                 <v1:number>+923464114840</v1:number>
            //                 <v1:sender>HRDF</v1:sender>
            //                 <v1:username>subol@hrdf.org.sa</v1:username>
            //                 <v1:password>Nw%1S9ZRA4</v1:password>
            //                 </v1:SendSmsLoginRequest>
            //                 </soapenv:Body>
            //                </soapenv:Envelope>";
            //using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            //{
            //    streamWriter.Write(data);
            //}
            //var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();
            //}
            //Console.WriteLine(httpResponse.StatusCode);

            return new string[] { "value1", "value2" , "value3" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
