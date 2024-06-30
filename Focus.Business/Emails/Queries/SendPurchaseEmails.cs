using Focus.Business.Common;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Emails.Models;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Emails.Queries
{
    public class SendPurchaseEmails : IRequest<Message>
    {
        public Guid Id { get; set; }
        public bool IsPurchaseOrder { get; set; }
        public bool IsGoodsReceive { get; set; }
        public SendEmailFromListLookupModel EmailCompose { get; set; }
        public class Handler : IRequestHandler<SendPurchaseEmails, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly ISendEmail sendEmail;
            public readonly IConfiguration _configuration;
            private readonly IHostingEnvironment _hostingEnvironment;
            public Handler(IApplicationDbContext context, IUserHttpContextProvider contextProvider, ISendEmail _sendEmail, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
            {
                _context = context;
                _contextProvider = contextProvider;
                sendEmail = _sendEmail;
                _configuration = configuration;
                _hostingEnvironment = hostingEnvironment;
            }
            public async Task<Message> Handle(SendPurchaseEmails request, CancellationToken cancellationToken)
            {
                try
                {
                    dynamic sale;
                    string reportFullPath = _configuration.GetSection("ReportServer:Path").Value;
                    Uri uri = new Uri(reportFullPath);
                    string baseUrl = uri.GetLeftPart(UriPartial.Authority);
                    byte[] pdfBytes;
                    var pdfbytesList = new List<AttachmentBytesLookupModel>();
                    var cCEmails = new List<EmailTo>();

                    if (request.IsPurchaseOrder)
                    {
                        sale = await _context.PurchaseOrders.FindAsync(request.Id);

                        if (request.EmailCompose.IsInvoice)
                        {

                            string Url = baseUrl + "/Reports/PurchaseModule/PurchaseOrder/PurchaseOrderReport.aspx?Id=" + request.Id 
                                        + "&isFifo=" + false
                                        + "&openBatch=" + 0
                                        + "&isReturn=" + false
                                        + "&deliveryChallan=" + false
                                        + "&simpleQuery=" + false
                                        + "&colorVariants=" + false
                                        + "&isMultiUnit=" + false
                                        + "&isReturnView=" + true
                                        + "&CompanyId=" + _contextProvider.GetCompanyId()
                                        + "&formName=PurchaseOrder"
                                        + "&isDownload=" + false;

                            using var httpClient = new HttpClient();

                            using (HttpResponseMessage response = await httpClient.GetAsync(new Uri(Url)))
                            {
                                response.EnsureSuccessStatusCode();
                                pdfBytes = await response.Content.ReadAsByteArrayAsync();
                            }

                            pdfbytesList.Add(new AttachmentBytesLookupModel
                            {
                                FileName = "Attachment",
                                Bytes = pdfBytes
                            });
                        }
                    }
                    else if (request.IsGoodsReceive)
                    {
                        sale = await _context.GoodReceives.FindAsync(request.Id);

                        if (request.EmailCompose.IsInvoice)
                        {

                            string Url = baseUrl + "/Reports/PurchaseModule/GoodsReceive/GoodsReceiveReport.aspx?Id=" + request.Id 
                                        + "&isFifo=" + false
                                        + "&openBatch=" + 0
                                        + "&isReturn=" + false
                                        + "&deliveryChallan=" + false
                                        + "&simpleQuery=" + false
                                        + "&colorVariants=" + false
                                        + "&isMultiUnit=" + false
                                        + "&isReturnView=" + true
                                        + "&CompanyId=" + _contextProvider.GetCompanyId()
                                        + "&formName=GoodReceive"
                                        + "&isDownload=" + false;
                            using var httpClient = new HttpClient();

                            using (HttpResponseMessage response = await httpClient.GetAsync(new Uri(Url)))
                            {
                                response.EnsureSuccessStatusCode();
                                pdfBytes = await response.Content.ReadAsByteArrayAsync();
                            }

                            pdfbytesList.Add(new AttachmentBytesLookupModel
                            {
                                FileName = "Attachment",
                                Bytes = pdfBytes
                            });
                        }
                    }
                    else
                    {
                        sale = await _context.PurchasePosts.FindAsync(request.Id);

                        if (request.EmailCompose.IsInvoice)
                        {

                            string Url = baseUrl + "/Reports/PurchaseModule/PurchaseInvoice/PurchaseInvoiceReport.aspx?Id=" + request.Id 
                                          + "&isFifo=" + false
                                          + "&openBatch=" + 0
                                          + "&isReturn=" + false
                                          + "&deliveryChallan=" + false
                                          + "&simpleQuery=" + false
                                          + "&colorVariants=" + false
                                          + "&isMultiUnit=" + false
                                          + "&isReturnView=" + false
                                          + "&CompanyId=" + _contextProvider.GetCompanyId()
                                          + "&formName=PurchasePOS"
                                          + "&isDownload=" + false;

                            using var httpClient = new HttpClient();

                            using (HttpResponseMessage response = await httpClient.GetAsync(new Uri(Url)))
                            {
                                response.EnsureSuccessStatusCode();
                                pdfBytes = await response.Content.ReadAsByteArrayAsync();
                            }

                            pdfbytesList.Add(new AttachmentBytesLookupModel
                            {
                                FileName = "Attachment",
                                Bytes = pdfBytes
                            });
                        }
                    }

                    var companyId = _contextProvider.GetCompanyId();
                    var companyDto = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
                    var emailConfiguration = await _context.EmailConfiguration.FirstOrDefaultAsync(x => x.IsDefault);
                    var emailConfigurationDto = new EmailConfigurationLookupModel();
                    var sendEmailDto = new SendEmailDto();
                    var sb = new StringBuilder();



                    string grossAmount = Convert.ToDecimal(sale.TotalWithOutAmount).ToString("N2");
                    string discountAmount = Convert.ToDecimal(sale.DiscountAmount).ToString("N2");
                    string discount = Convert.ToDecimal(sale.Discount).ToString("N2");
                    string vatAmount = Convert.ToDecimal(sale.VatAmount).ToString("N2");
                    string totalAmount = Convert.ToDecimal(sale.TotalAmount).ToString("N2");

                    string customerName = sale.Supplier.EnglishName;
                    string customerAddress = sale.Supplier.Address;
                    string customerVat = sale.Supplier.VatNo;
                    string email = sale.Supplier.Email;
                    string registrationNo = sale.RegistrationNo;

                    string textbody = string.IsNullOrEmpty(request.EmailCompose.Body) ? "" : request.EmailCompose.Body;

                    sb.Append("<div>");
                    sb.Append("" + textbody + "");
                    sb.Append("</div>");
                    sb.Append("<div style='display: flex; justify-content: center; align-items: center; font-family:system-ui;'>");
                    sb.Append("<div style='padding-top: 20px; text-align: center; border: 1px solid rgb(220, 215, 215); border-radius: 5px; width: 600px; max-width: 600px; height: auto; margin: auto; '>");
                    sb.Append("<a href='#'><img src='https://mannatthemes.com/dastone/default/assets/images/logo-sm.png' alt='' style='height: 40px; margin-left: auto; margin-right: auto; display:block;'></a>");
                    sb.Append("<div style='padding: 0 0 20px;'>");
                    sb.Append("<h2 style='font-size: 24px; color:#50649c; line-height: 1.2em; font-weight: 600; text-align: center;' align='center'>Thanks for visit <span style='color: #4d79f6; font-weight: 700;'>" + companyDto.NameEnglish + "</span>.</h2>");
                    sb.Append("</div>");
                    sb.Append("<div style='text-align: left; padding-left: 70px;'>");
                    sb.Append("<p style='font-size: 14px; padding: 0; margin: 4px;'>" + customerName + "</p>");
                    sb.Append("<p style='font-size: 14px; padding: 0; margin: 4px;'>" + customerAddress + "</p>");
                    sb.Append("<p style='font-size: 14px; padding: 0; margin: 4px;'>" + customerVat + "</p>");
                    sb.Append("<p style='font-size: 14px; padding: 0; margin: 4px;'><b>" + registrationNo + "</b></p>");
                    sb.Append("<p style='font-size: 14px; padding: 0; margin: 4px;'><b>" + sale.Date.ToString("dd MMMM, yyyy") + "</b></p>");
                    sb.Append("<table style='width: 85%; margin-top: 5px; '>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0;'>");
                    sb.Append("<span style='font-size: 14px; '> Total Amount</span>");
                    sb.Append("</td>");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0; text-align: right;' >");
                    sb.Append("<span style='font-size: 14px; '>" + grossAmount + "</span>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0;'>");
                    sb.Append("<span style='font-size: 14px; '> Discount</span>");
                    sb.Append("</td>");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0; text-align: right;' >");
                    sb.Append("<span style='font-size: 14px; '>" + discountAmount + "</span>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr >");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0;'>");
                    sb.Append("<span style='font-size: 14px; '> Adjustment</span>");
                    sb.Append("</td>");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0; text-align: right;' >");
                    sb.Append("<span style='font-size: 14px; '>" + discount + "</span>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr >");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0;'>");
                    sb.Append("<span style='font-size: 14px; '> Vat</span>");
                    sb.Append("</td>");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0; text-align: right;' >");
                    sb.Append("<span style='font-size: 14px; '>" + vatAmount + "</span>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr >");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0;'>");
                    sb.Append("</td>");
                    sb.Append("<td style='border-top-width: 1px; border-top-color: #eee; border-top-style: solid; padding: 7px 0; text-align: right;' >");
                    sb.Append("<span style='font-size: 14px; '> Grand Total: </span>");
                    sb.Append("<span style='font-size: 14px; '><b> " + totalAmount + "</b></span>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("<p style='font-size: 18px; margin: 50px 0; text-align: center; font-weight: 700; color:#50649c;'>");
                    sb.Append("" + companyDto.AddressEnglish + "");
                    sb.Append("</p>");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    if (request.EmailCompose.AttachmentList.Count > 0)
                    {
                        foreach (var item in request.EmailCompose.AttachmentList)
                        {
                            var path = Path.Combine(_hostingEnvironment.WebRootPath) + item.Path;
                            var provider = new FileExtensionContentTypeProvider();
                            if (!provider.TryGetContentType(item.Path, out var contentType))
                            {
                                contentType = "application/octet-stream";
                            }

                            var bytes = await System.IO.File.ReadAllBytesAsync(path);

                            var fileName = string.IsNullOrEmpty(item.FileName) ? "File" : item.FileName;
                            pdfbytesList.Add(new AttachmentBytesLookupModel
                            {

                                FileName = fileName,
                                Bytes = bytes
                            });
                        }

                    }
                    if (request.EmailCompose.Cc.Count > 0)
                    {
                        foreach (var item in request.EmailCompose.Cc)
                        {
                            cCEmails.Add(new EmailTo
                            {
                                Id = item.Id,
                                Cc = item.Cc
                            });
                        }
                    }

                    sendEmailDto.EmailTo = email;
                    sendEmailDto.Subject = string.IsNullOrEmpty(request.EmailCompose.Subject) ? registrationNo : request.EmailCompose.Subject;
                    sendEmailDto.Body = sb.ToString();

                    emailConfigurationDto.Email = emailConfiguration.Email;
                    emailConfigurationDto.Port = emailConfiguration.Port;
                    emailConfigurationDto.Password = emailConfiguration.Password;
                    emailConfigurationDto.Server = emailConfiguration.Server;
                    emailConfigurationDto.SmtpServer = emailConfiguration.SmtpServer;
                    emailConfigurationDto.Attachments = pdfbytesList;
                    emailConfigurationDto.Cc = cCEmails;

                    await sendEmail.SendMailMessage(sendEmailDto, emailConfigurationDto);

                    return new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "Email send on this address " + " " + email,
                    };
                }
                catch (Exception ex)
                {
                    return new Message
                    {
                        IsSuccess = false,
                        IsAddUpdate = ex.Message,
                    };
                }
            }
        }
    }
}
