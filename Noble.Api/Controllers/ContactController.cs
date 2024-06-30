using Focus.Business.Common;
using Focus.Business.Contacts.Commands.AddUpdateContact;
using Focus.Business.Contacts.Commands.DeleteContact;
using Focus.Business.Contacts.Queries;
using Focus.Business.Contacts.Queries.GetContactAttachments;
using Focus.Business.Contacts.Queries.GetContactDetails;
using Focus.Business.Contacts.Queries.GetContactList;
using Focus.Business.Contacts.Queries.GetContactRunningBalance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Noble.Api.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Focus.Business.Contacts.ImportContact;
using Focus.Business.StockAdjustments.ImportStock;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Focus.Business.Contacts.Queries.GetContactLedgerDetails;
using Focus.Business.ContactCustomerGroup.Model;
using Focus.Business.ContactCustomerGroup.Commands;
using Focus.Business.ContactCustomerGroup.Queries;
using OfficeOpenXml;
using Focus.Business.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IApplicationDbContext Context;

        public ContactController(IHostingEnvironment hostingEnvironment, IApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            Context = context;
        }

        [DisplayName("Auto Code Generate")]
        [Route("api/Contact/AutoGenerateCode")]
        [HttpGet("AutoGenerateCode")]
        [Roles("CanAddCustomer", "CanEditCustomer", "CanAddSupplier", "CanEditSupplier", "CanAddReparingOrder", "CanEditReparingOrder")]
        public async Task<IActionResult> AutoGenerateCode(bool isCustomer,bool isCashCustomer)
        {
            
            var autoNo = await Mediator.Send(new GetContactNumberQuery
            {
                isCustomer = isCustomer ,
                isCashCustomer= isCashCustomer
            });

            return Ok(autoNo);
        }

        [Route("api/Contact/SaveContact")]
        [HttpPost("SaveContact")]
        [Roles("CanAddCustomer", "CanEditCustomer", "CanAddSupplier", "CanEditSupplier", "CanAddReparingOrder", "CanEditReparingOrder")]
        public async Task<IActionResult> SaveContact([FromBody] ContactVm contactVm)
        {
            Message message;
            if (contactVm.Id == Guid.Empty)
            {

                message = await Mediator.Send(new AddUpdateContactCommand
                {
                    Id = new Guid(),
                    Code = contactVm.Code,
                    CustomerCode = contactVm.CustomerCode,
                    Prefix = contactVm.Prefix,
                    IsAddressOnAll = contactVm.IsAddressOnAll,
                    MultipleAddress = contactVm.MultipleAddress,
                    EnglishName = contactVm.EnglishName,
                    ArabicName = contactVm.ArabicName,
                    CompanyNameEnglish = contactVm.CompanyNameEnglish,
                    CompanyNameArabic = contactVm.CompanyNameArabic,
                    CustomerDisplayName = contactVm.CustomerDisplayName,
                    Telephone = contactVm.Telephone,
                    Email = contactVm.Email,
                    RegistrationDate = contactVm.RegistrationDate,
                    Category = contactVm.Category,
                    RegionId = contactVm.RegionId,
                    CustomerType = contactVm.CustomerType,
                    CustomerGroup = contactVm.CustomerGroup,
                    ContactNo1 = contactVm.ContactNo1,
                    IsCashCustomer = contactVm.IsCashCustomer,
                    Date = contactVm.Date,
                    CommercialRegistrationNo = contactVm.CommercialRegistrationNo,
                    VatNo = contactVm.VatNo,
                    IsUpdate = contactVm.IsUpdate,
                    WhatsAppNumber= contactVm.WhatsAppNumber,
                    CustomerGroupId= contactVm.CustomerGroupId,

                    CurrencyId = contactVm.CurrencyId,
                    TaxRateId = contactVm.TaxRateId,
                    Website = contactVm.Website,

                    BillingAttention = contactVm.BillingAttention,
                    BillingCountry = contactVm.BillingCountry,
                    BillingZipCode = contactVm.BillingZipCode,
                    BillingPhone = contactVm.BillingPhone,
                    BillingArea = contactVm.BillingArea,
                    BillingAddress = contactVm.BillingAddress,
                    BillingCity = contactVm.BillingCity,
                    BillingFax = contactVm.BillingFax,

                    ShippingAttention = contactVm.ShippingAttention,
                    ShippingCountry = contactVm.ShippingCountry,
                    ShippingZipCode = contactVm.ShippingZipCode,
                    ShippingPhone = contactVm.ShippingPhone,
                    ShippingArea = contactVm.ShippingArea,
                    ShippingAddress = contactVm.ShippingAddress,
                    ShippingCity = contactVm.ShippingCity,
                    ShippingFax = contactVm.ShippingFax,
                    DeliveryAddress = contactVm.DeliveryAddress,
                    DeliveryOther = contactVm.DeliveryOther,
                    ShippingOther = contactVm.ShippingOther,
                    PriceLabelId = contactVm.PriceLabelId,
                    
                    Remarks = contactVm.Remarks,
                    IsCustomer = contactVm.IsCustomer,
                    IsActive = contactVm.IsActive,
                    ProductList = contactVm.ProductList,
                    PaymentTerms = contactVm.PaymentTerms,
                    DeliveryTerm = contactVm.DeliveryTerm,
                    CreditDays = contactVm.CreditDays,
                    CreditLimit = contactVm.CreditLimit,
                    CreditPeriod = contactVm.CreditPeriod,

                    AttachmentList = contactVm.AttachmentList,
                    ContactPersonList = contactVm.ContactPersonList,
                    ContactBankAccountList = contactVm.ContactBankAccountList,
                    DeliveryAddressList = contactVm.DeliveryAddressList,
                    //End
                    SupplierType = contactVm.SupplierType,
                    ExpiryDate = contactVm.ExpiryDate,
                    Status = contactVm.Status,
                    AccountId = contactVm.AccountId,
                    IsRaw = contactVm.IsRaw,
                    
                    IsExpire = contactVm.IsExpire,
                    ActiveDate = contactVm.ActiveDate,
                    CaptureDate = contactVm.CaptureDate,
                    Reason = contactVm.Reason,
                    IsAllowEmail = contactVm.IsAllowEmail,
                    IsAutoEmail = contactVm.IsAutoEmail,

            });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                message = await Mediator.Send(new AddUpdateContactCommand
                {
                    Id = contactVm.Id,
                    RegionId = contactVm.RegionId,
                    Code = contactVm.Code,
                    CustomerCode = contactVm.CustomerCode,
                    IsAddressOnAll = contactVm.IsAddressOnAll,
                    MultipleAddress = contactVm.MultipleAddress,
                    Prefix = contactVm.Prefix,
                    IsUpdate = contactVm.IsUpdate,
                    EnglishName = contactVm.EnglishName,
                    ArabicName = contactVm.ArabicName,
                    CompanyNameEnglish = contactVm.CompanyNameEnglish,
                    CompanyNameArabic = contactVm.CompanyNameArabic,
                    CustomerDisplayName = contactVm.CustomerDisplayName,
                    Telephone = contactVm.Telephone,
                    DeliveryAddress = contactVm.DeliveryAddress,
                    DeliveryOther = contactVm.DeliveryOther,
                    ShippingOther = contactVm.ShippingOther,
                    Email = contactVm.Email,
                    IsCashCustomer = contactVm.IsCashCustomer,
                    RegistrationDate = contactVm.RegistrationDate,
                    Category = contactVm.Category,
                    CustomerType = contactVm.CustomerType,
                    CustomerGroup = contactVm.CustomerGroup,
                    ContactNo1 = contactVm.ContactNo1,
                    ProductList = contactVm.ProductList,

                    CommercialRegistrationNo = contactVm.CommercialRegistrationNo,
                    DeliveryAddressList = contactVm.DeliveryAddressList,
                    VatNo = contactVm.VatNo,

                    CurrencyId = contactVm.CurrencyId,
                    TaxRateId = contactVm.TaxRateId,
                    Website = contactVm.Website,

                    CustomerGroupId = contactVm.CustomerGroupId,

                    BillingAttention = contactVm.BillingAttention,
                    BillingCountry = contactVm.BillingCountry,
                    BillingZipCode = contactVm.BillingZipCode,
                    BillingPhone = contactVm.BillingPhone,
                    BillingArea = contactVm.BillingArea,
                    BillingAddress = contactVm.BillingAddress,
                    BillingCity = contactVm.BillingCity,
                    BillingFax = contactVm.BillingFax,
                    PriceLabelId = contactVm.PriceLabelId,
                    ShippingAttention = contactVm.ShippingAttention,
                    ShippingCountry = contactVm.ShippingCountry,
                    ShippingZipCode = contactVm.ShippingZipCode,
                    ShippingPhone = contactVm.ShippingPhone,
                    ShippingArea = contactVm.ShippingArea,
                    ShippingAddress = contactVm.ShippingAddress,
                    ShippingCity = contactVm.ShippingCity,
                    ShippingFax = contactVm.ShippingFax,

                    Remarks = contactVm.Remarks,
                    IsCustomer = contactVm.IsCustomer,
                    IsActive = contactVm.IsActive,

                    PaymentTerms = contactVm.PaymentTerms,
                    DeliveryTerm = contactVm.DeliveryTerm,
                    CreditDays = contactVm.CreditDays,
                    CreditLimit = contactVm.CreditLimit,
                    CreditPeriod = contactVm.CreditPeriod,

                    AttachmentList = contactVm.AttachmentList,
                    ContactPersonList = contactVm.ContactPersonList,
                    ContactBankAccountList = contactVm.ContactBankAccountList,
                    //End
                    SupplierType = contactVm.SupplierType,
                    ExpiryDate = contactVm.ExpiryDate,
                    Status = contactVm.Status,
                    AccountId = contactVm.AccountId,
                    IsRaw = contactVm.IsRaw,
                    WhatsAppNumber= contactVm.WhatsAppNumber,
                    Date = contactVm.Date,
                    IsExpire = contactVm.IsExpire,
                    ActiveDate = contactVm.ActiveDate,
                    CaptureDate = contactVm.CaptureDate,
                    Reason = contactVm.Reason,
                    IsAllowEmail = contactVm.IsAllowEmail,
                    IsAutoEmail = contactVm.IsAutoEmail,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }

            
        }
        [Route("api/Contact/ContactList")]
        [HttpGet("ContactList")]
        //[Roles("CanViewCustomer", "CashInvoices", "CreditInvoices", "TouchInvoiceTemplate1", "TouchInvoiceTemplate2", "TouchInvoiceTemplate3", "CanAddSaleReturn", "CanAddSaleOrder", "CanViewSaleOrder", "CashPurchase", "CreditPurchase", "CanPurchaseInvoiceCosting", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanAddPurchaseReturn", "CanViewDetailPurchaseReturn", "CanViewSupplier", "CanViewCustomerWiseProductsSale", "CanPrintCustomerWiseProductsSale", "CanViewSupplierWiseProductsPurchase", "CanPrintSupplierWiseProductsPurchase", "CanViewCustomerDiscountProducts", "CanViewSupplierDiscountProducts", "CanPrintCustomerDiscountProducts", "CanPrintSupplierDiscountProducts", "CanViewFreeOfCostPurchase", "CanViewFreeOfCostSale", "CanPrintFreeOfCostPurchase", "CanPrintFreeOfCostSale", "CanAddDispatchNote", "CreditServiceInvoices", "CashServiceInvoices", "CanHoldServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddServiceQuotation", "CanEditServiceQuotation", "CanDraftServiceQuotation", "CanAddInquiry", "CanEditInquiry", "CanAddAutoTemplate", "CanEditAutoTemplate", "CanAddReparingOrder", "CanEditReparingOrder")]
        public async Task<IActionResult> ContactList(bool isCustomer, string searchTerm, int? pageNumber, bool IsDropDown, bool IsActive, bool status , string paymentTerms, string paymentTerms2,bool isCashCustomer,bool multipleAddress,string customerCategory, Guid? customerId, Guid? customerGroupId,string category)
        {
            var contact = await Mediator.Send(new GetContactListQuery
            {
                isCustomer = isCustomer,
                IsMultipleAddress = multipleAddress,
                IsActive = IsActive,
                IsRaw = status,
                IsCashCustomer = isCashCustomer,
                SearchTerm = searchTerm,
                PaymentTerms = paymentTerms,
                PaymentTerms2 = paymentTerms2,
                IsDropDown = IsDropDown,
                PageNumber = pageNumber ?? 1,
                CustomerCategory= customerCategory,
                CustomerId =customerId,
                CustomerGroupId= customerGroupId,
                Category=category

            });
            return Ok(contact);
        }

        [Route("api/Contact/GetContactListExcel")]
        [HttpGet("GetContactListExcel")]
        public async Task<IActionResult> GetContactListExcel(bool isCustomer, string searchTerm, int? pageNumber, bool IsDropDown, bool IsActive, bool status, string paymentTerms, string paymentTerms2, bool isCashCustomer, bool multipleAddress, string customerCategory, Guid? customerId, Guid? customerGroupId, string category)
        {
            var contacts = await Context.Contacts.ToListAsync();

            var list = isCustomer ? contacts.Where(x => x.IsCustomer && !x.IsCashCustomer).ToList() : contacts.Where(x => !x.IsCustomer).ToList();

            List<string> columnHeaders = new List<string>();

            columnHeaders.Add("#");
            columnHeaders.Add("Code");
            columnHeaders.Add("Customer Status");
            columnHeaders.Add("Category");
            columnHeaders.Add("Name English");
            columnHeaders.Add("Name Other Language");
            columnHeaders.Add("DisplayName");
            columnHeaders.Add("Company Name English");
            columnHeaders.Add("Company Name Other Language");
            columnHeaders.Add("Registration Date");
            columnHeaders.Add("Customer Type");
            columnHeaders.Add("Phone");
            columnHeaders.Add("Email");
            columnHeaders.Add("Commercial Registration No ");
            columnHeaders.Add("VAT/NTN/Tax No");
            columnHeaders.Add("Customer Code ");
            columnHeaders.Add("Payment Terms ");
            columnHeaders.Add("Address");

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            // Create a new Excel package
            using (var excelPackage = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                var worksheet = excelPackage.Workbook.Worksheets.Add("Client Report");
                // Set column headers
                for (int i = 1; i <= columnHeaders.Count; i++)
                {
                    worksheet.Cells[1, i].Value = columnHeaders[i - 1];
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Cells[1, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(i).Style.Numberformat.Format = "General";
                }
                // Populate the data
                for (var row = 0; row < list.Count; row++)
                {
                    var item = list[row];

                    string customer = "";

                    if(item.IsCashCustomer)
                    {
                        customer = "Walk-In";
                    }
                    else
                    {
                        customer = "Cash/Credit";
                    }

                    // Adjust the row index accordingly (add 2 to account for header row)
                    var rowIndex = row + 2;
                    // Set the cell values
                    worksheet.Cells[rowIndex, 1].Value = row + 1;
                    worksheet.Cells[rowIndex, 2].Value = item.Code;
                    worksheet.Cells[rowIndex, 3].Value = customer;
                    worksheet.Cells[rowIndex, 4].Value = (item.Category == null || item.Category == "") ? "" : item.Category;
                    worksheet.Cells[rowIndex, 5].Value = (item.EnglishName == null || item.EnglishName == "") ? "" : item.EnglishName;
                    worksheet.Cells[rowIndex, 6].Value = (item.ArabicName == null || item.ArabicName == "") ? "" : item.ArabicName;
                    worksheet.Cells[rowIndex, 7].Value = (item.CustomerDisplayName == null || item.CustomerDisplayName == "") ? "" : item.CustomerDisplayName;
                    worksheet.Cells[rowIndex, 8].Value = (item.CompanyNameEnglish == null || item.CompanyNameEnglish == "") ? "" : item.CompanyNameEnglish;
                    worksheet.Cells[rowIndex, 9].Value = (item.CompanyNameArabic == null || item.CompanyNameArabic == "") ? "" : item.CompanyNameArabic;
                    worksheet.Cells[rowIndex, 10].Value = (item.RegistrationDate == null || item.RegistrationDate == "") ? "" :Convert.ToDateTime(item.RegistrationDate).ToString("dd/MM/yyyy");
                    worksheet.Cells[rowIndex, 11].Value = (item.CustomerType == null || item.CustomerType == "") ? "" : item.CustomerType; 
                    worksheet.Cells[rowIndex, 12].Value = (item.ContactNo1 == null || item.ContactNo1 == "") ? "" : item.ContactNo1;
                    worksheet.Cells[rowIndex, 13].Value = (item.Email == null || item.Email == "") ? "" : item.Email;
                    worksheet.Cells[rowIndex, 14].Value = (item.CommercialRegistrationNo == null || item.CommercialRegistrationNo == "") ? "" : item.CommercialRegistrationNo;
                    worksheet.Cells[rowIndex, 15].Value = (item.VatNo == null || item.VatNo == "") ? "" : item.VatNo;
                    worksheet.Cells[rowIndex, 16].Value = (item.CustomerCode == null || item.CustomerCode == "") ? "" : item.CustomerCode;
                    worksheet.Cells[rowIndex, 17].Value = (item.PaymentTerms == null || item.PaymentTerms == "") ? "" : item.PaymentTerms;
                    worksheet.Cells[rowIndex, 18].Value = (item.Address == null || item.Address == "") ? "" : item.Address;
                }
                // Auto-fit columns for better visibility
                //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                // Save the Excel package to a MemoryStream

                using var memoryStream = new MemoryStream();
                await excelPackage.SaveAsAsync(memoryStream);
                // Return the Excel file as a stream in the response
                var excelData = memoryStream.ToArray();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ClientReport.xlsx");
            }
        }


        [Route("api/Contact/ContactDelete")]
        [HttpGet("ContactDelete")]
        [Roles("CanAddCustomer", "CanEditCustomer", "CanAddSupplier", "CanEditSupplier")]
        public async Task<IActionResult> ContactDelete(Guid id)
        {
            var contactId = await Mediator.Send(new DeleteContactCommand
            {
                Id = id
            });
            return Ok(contactId);

        }
        [Route("api/Contact/ContactDetail")]
        [HttpGet("ContactDetail")]
        [Roles("CanAddCustomer", "CanEditCustomer", "CanAddSupplier", "CanEditSupplier")]
        public async Task<IActionResult> ContactDetail(Guid id,bool multipleAddress)
        {
            var contact = await Mediator.Send(new GetContactDetailQuery()
            {
                Id = id,
                IsMultipleAddress= multipleAddress
            });
            return Ok(contact);

        }

        [Route("api/Contact/ContactLedgerDetail")]
        [HttpGet("ContactLedgerDetail")]
        public async Task<IActionResult> ContactLedgerDetail(Guid id, string documentType, bool cashCustomer, bool isService, bool lastThreeMonth, bool isSupplierQuotation)
        {
            var contact = await Mediator.Send(new GetContactLedgerDetailQuery()
            {
                Id = id,
                DocumentType = documentType,
                CashCustomer = cashCustomer,
                IsService = isService,
                LastThreeMonth = lastThreeMonth,
                IsSupplierQuotation = isSupplierQuotation
            });
            return Ok(contact);

        }

        [Route("api/Contact/DownloadFile")]
        [HttpGet("DownloadFile")]
        //[Authorize(Roles = "User, Super Admin")]
        public async Task<IActionResult> DownloadFileAsync(string filePath)
        {
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/pdf" /*"image/png"*/, Path.GetFileName(path));


            //var path = Path.Combine(
            //               Directory.GetCurrentDirectory(),
            //               "wwwroot", filePath);

            //var memory = new MemoryStream();
            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    await stream.CopyToAsync(memory);
            //}
            //memory.Position = 0;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
        }


        [Route("api/Contact/GetBaseImage")]
        [HttpGet("GetBaseImage")]
      
        public async Task<IActionResult> GetBaseImage(string filePath)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            if ( System.IO.File.Exists(path))
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return Ok(base64);


            }
            return Ok(null);
           
        }
       

        [Route("api/Contact/DeleteAttachment")]
        [HttpGet("DeleteAttachment")]
       
        public async Task<IActionResult> DeleteAttachment(string path)
        {

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath) + path;
            System.IO.File.Delete(Path.Combine(filePath));
            return Ok(true);
        }

        [Route("api/Company/GetContactAttachments")]
        [HttpGet("GetContactAttachments")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetContactAttachments(string searchTerm, int? pageNumber, Guid id)
        {
            var companyListModel = await Mediator.Send(
                new GetContactAttachmentsQuery
                {
                    Id = id,
                    SearchTerm = searchTerm,
                    PageNumber = pageNumber ?? 1
                });
            return Ok(companyListModel);
        }

        [Route("api/Contact/GetCustomerRunningBalance")]
        [HttpGet("GetCustomerRunningBalance")]
       // [Roles("CanAddPettyCash", "CanDraftPettyCash", "CanEditPettyCash", "CanAddCPR", "CanDraftCPR", "CanEditCPR", "CanAddSPR", "CanEditCPR", "CanDraftSPR", "CanEditSPR", "CanRejectSPR", "CanRejectCPR", "CanRejectPettyCash", "CanAddTCAllocation", "CanDraftTCAllocation", "CanEditTCAllocation")]
        public async Task<IActionResult> GetCustomerRunningBalance(Guid id,DateTime? date)
        {
            var runningBalance = await Mediator.Send(
                new GetContactRunningBalanceDetail
                {
                    AccountId = id,
                    Date = date,
                });
            return Ok(runningBalance);
        } 
        
        
        [Route("api/Contact/ChangeCashCustomerToContact")]
        [HttpGet("ChangeCashCustomerToContact")]
        public async Task<IActionResult> ChangeCashCustomerToContact(bool changeDisplayName, bool cashCustomerToContact, bool invoiceChange,string documentType)
        {
            var runningBalance = await Mediator.Send(new CashCustomerToContactQuery
                {
                DocumentType = documentType,
                ChangeDisplayName = changeDisplayName,
                CashCustomerToContact = cashCustomerToContact,
                InvoiceChange = invoiceChange

            });
            return Ok(runningBalance);
        }

        #region Import Contact

        [Route("api/Contact/DownloadFileAsync")]
        [HttpGet("DownloadFileAsync")]
        [Roles("CanImportSupplier", "CanImportCustomer")]
        public async Task<IActionResult> DownloadTemplateFileAsync(string filePath)
        {
            var d = await Mediator.Send(new ExportStockCommand());
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/csv" /*"image/png"*/, Path.GetFileName(path));


        }


        [Route("api/Contact/UploadFilesForImport")]
        [HttpPost("UploadFilesForImport")]
        [Roles("CanImportSupplier", "CanImportCustomer")]
        public async Task<IActionResult> UploadFilesForImport([FromBody] List<ImportContactLookUp> contactVm, bool isContact)
        {
            var result = await Mediator.Send(new ImportContactCommand() { ContactLookUps = contactVm, IsCustomer = isContact });
            
            return Ok(result);
        }

        [Route("api/Contact/DownloadErrorFileAsync")]
        [HttpGet("DownloadErrorFileAsync")]
        [Roles("CanImportSupplier", "CanImportCustomer")]
        public async Task<IActionResult> DownloadErrorFileAsync(string filePath)
        {
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            if (System.IO.File.Exists(path))
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(path);

                return File(bytes, "application/xlsx" /*"image/png"*/, Path.GetFileName(path));
            }
            else
            {
                return Ok("File not exist");
            }

        }




        [Route("api/Contact/DeleteErrorFileAsync")]
        [HttpGet("DeleteErrorFileAsync")]
        [Roles("CanImportSupplier", "CanImportCustomer")]
        public IActionResult DeleteErrorFileAsync(string filePath)
        {
            // ... code for validation and get the file
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Ok("File Deleted Successfully");
            }
            else
            {
                return Ok("File Not Deleted Successfully");
            }

        }

        public class UploadFilesForImportContactVm
        {
            public IFormFile File { get; set; }
            public bool IsContact { get; set; }
        }

        #endregion

        #region Customer Group
        [Route("api/Contact/SaveCustomerGroup")]
        [HttpPost("SaveCustomerGroup")]
        public async Task<IActionResult> SaveCustomerGroup([FromBody] CustomerGroupLookupModel customerGroup)
        {
            var message = await Mediator.Send(new CustomerGroupAddUpdateCommand
            {
                customerGroup = customerGroup
            });
            return Ok(message);
        }
        [Route("api/Contact/CustomerGroupList")]
        [HttpGet("CustomerGroupList")]
        public async Task<IActionResult> CustomerGroupList(string searchTerm, bool isDropdown)
        {
            var city = await Mediator.Send(new CustomerGroupListQuery { 
                IsDropdown = isDropdown,
                SearchTerm = searchTerm
            });
            return Ok(city);
        }

        [Route("api/Contact/CustomerGroupDetail")]
        [HttpGet("CustomerGroupDetail")]
        public async Task<IActionResult> CustomerGroupDetailsQuery(Guid id)
        {
            var customer = await Mediator.Send(new CustomerGroupDetailsQuery()
            {
                Id = id
            });
            return Ok(customer);

        }

        [Route("api/Contact/CustomerGroupAutoCode")]
        [HttpGet("CustomerGroupAutoCode")]
        public async Task<IActionResult> CustomerGroupAutoCode()
        {
            var customer = await Mediator.Send(new CustomerGroupAutoCode(){});
            return Ok(customer);
        }
        #endregion
    }
}