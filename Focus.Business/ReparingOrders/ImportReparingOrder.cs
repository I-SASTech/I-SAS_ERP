using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Dapper;
using Focus.Business.Contacts.Commands.AddUpdateContact;
using Focus.Business.Interface;
using Focus.Business.StockAdjustments.ImportStock;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;

namespace Focus.Business.ReparingOrders
{
  public  class ImportReparingOrder : IRequest<string>
    {
        // delegate int NumberChanger(int n);
        public IFormFile File { get; set; }
        public bool IsCustomer { get; set; }

        public static IList<ReparingOrderFile> ErrorStockFiles = new List<ReparingOrderFile>();

        public class Handler : IRequestHandler<ImportReparingOrder, string>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IHostingEnvironment _environment;

            private string _adjustmentCode = null;
           
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<ImportReparingOrder> logger,
                IMediator mediator, IConfiguration configuration, IUserHttpContextProvider contextProvider,
                IHostingEnvironment environment)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                _configuration = configuration;
                _contextProvider = contextProvider;
                _environment = environment;
            }

            public async Task<string> Handle(ImportReparingOrder request, CancellationToken cancellationToken)
            {


                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var companyId = _contextProvider.GetCompanyId().ToString();
                    var sqlQuery =
                       
                         "select  * from ReparingOrderTypes where CompanyId ='" + companyId +
                                   "';" +
                         "select  * from EmployeeRegistrations where CompanyId ='" +
                                   companyId + "';";
                       

                    var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    using (var multi = connection.QueryMultiple(sqlQuery, null))
                    {
                        DropDownCodeModel.ReparingOrderTypes = multi.Read<ReparingOrderType>().ToList();
                        DropDownCodeModel.EmployeeRegistrations = multi.Read<EmployeeRegistration>().ToList();
                    }



                    var excelEngine = new ExcelEngine();
                    //Instantiate the Excel application object
                    var application = excelEngine.Excel;
                    //Assigns default application version
                    application.DefaultVersion = ExcelVersion.Excel2007;
                    //A existing workbook is opened.
                    var workbook = application.Workbooks.Open(request.File.OpenReadStream());
                    var worksheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "Sheet1");
                    if (worksheet == null) throw new ArgumentException("File not in the desired format.");

                    var rowData = worksheet.Rows.Where(x => x.Row > 1 && !x.IsBlank).ToList();

                    //Create Error file workbook
                    var excelEngineError = new ExcelEngine();
                    //Instantiate the Excel application object
                    var applicationError = excelEngineError.Excel;
                    //Assigns default application version
                    applicationError.DefaultVersion = ExcelVersion.Excel2007;

                    IWorkbook workbookError = applicationError.Workbooks.Create(1);
                    IWorksheet worksheetError = workbookError.Worksheets[0];
                    worksheetError.Name = "Sheet1";


                    if (rowData.Count > 0)
                    {
                        var errorContactFileDataList = new List<ReparingOrderFile>();
                        foreach (var contact in rowData)
                        {
                            var customerId = Guid.Empty;
                            var reparingId = Guid.Empty;
                            var upsId = Guid.Empty;
                            var customer = Context.Contacts.FirstOrDefault(x => x.ContactNo1 == contact.Columns[4].Value);
                           
                          
                            if(contact.Columns[4].Value==null && contact.Columns[3].Value == null)
                            {
                                customerId = Guid.Empty;
                            }
                            else  if (contact.Columns[4].Value == "" && contact.Columns[3].Value == "")
                            {
                                customerId = Guid.Empty;
                            }
                            else
                            {

                                if (customer == null)
                                {
                                   
                                    {
                                        var message = await _mediator.Send(new AddUpdateContactCommand
                                        {
                                            Id = Guid.Empty,
                                            CustomerType = "Factory",
                                            Category = "B2C – Business to Client",
                                            EnglishName = contact.Columns[3].Value == null ? "" : contact.Columns[3].Value,
                                            PaymentTerms =null,
                                            ContactNo1 = contact.Columns[4].Value == null ? "" : contact.Columns[4].Value,
                                            IsActive=true,
                                            IsCustomer=true



                                        });;
                                        customerId = message.Id;
                                    }
                                  
                                  

                                }
                            }

                           
                           
                            var employee = DropDownCodeModel.EmployeeRegistrations.FirstOrDefault(x => x.EnglishName == contact.Columns[2].Value);
                            //var doneBY = DropDownCodeModel.EmployeeRegistrations.FirstOrDefault(x => x.EnglishName == contact.Columns[10].Value);
                            var doneBY = Guid.Parse("2e7e1ef6-6ba6-4e5b-b624-51d6dd045e37");
                            var accessory = Guid.Parse("6ac9dffb-dc5a-4b73-e61c-08da76105701");
                            var ups = DropDownCodeModel.ReparingOrderTypes.FirstOrDefault(x => x.Name == contact.Columns[5].Value);

                            if (ups == null)
                            {
                                var reparingOrdertType = new ReparingOrderType
                                {

                                    Name = contact.Columns[5].Value,
                                    ReparingOrderTypeEnums = ReparingOrderTypeEnum.UpsDescription,
                                    isActive = true,
                                };
                                //Add Department to database
                                await Context.ReparingOrderTypes.AddAsync(reparingOrdertType, cancellationToken);
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = Context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await Context.SaveChangesAsync(cancellationToken);
                                upsId = reparingOrdertType.Id;
                            }
                            else
                            {
                                upsId = Guid.Empty;
                            }

                            var issues = DropDownCodeModel.ReparingOrderTypes.FirstOrDefault(x => x.Name == contact.Columns[6].Value);
                            if(issues==null)
                            {
                                var reparingOrdertType = new ReparingOrderType
                                {

                                    Name = contact.Columns[6].Value,
                                    ReparingOrderTypeEnums = ReparingOrderTypeEnum.Problem,
                                    isActive = true,
                                };
                                //Add Department to database
                                await Context.ReparingOrderTypes.AddAsync(reparingOrdertType, cancellationToken);
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = Context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await Context.SaveChangesAsync(cancellationToken);
                                reparingId = reparingOrdertType.Id;
                            }
                            else
                            {
                                reparingId = Guid.Empty;
                            }
                            //var accessory = DropDownCodeModel.ReparingOrderTypes.FirstOrDefault(x => x.Name == contact.Columns[8].Value);
                            var reparingOrder = new ReparingOrder();
                            bool isRepared = false;
                            bool isCollected = false;
                            bool isReturned = false;
                            if(contact.Columns[7].Value== "Repaired")
                            {
                                isRepared = true;
                            }
                            else if(contact.Columns[7].Value== "Cash Received")
                            {
                                isCollected = true;
                            }
                            else if(contact.Columns[7].Value== "Returned")
                            {
                                isReturned = true;
                            }
                            if(contact.Columns[10].Value=="" || contact.Columns[10].Value ==null )
                            {
                                contact.Columns[10].Value = "0";
                            }
                            if(contact.Columns[11].Value=="" || contact.Columns[11].Value ==null )
                            {
                                contact.Columns[11].Value = "0";
                            }
                            if(contact.Columns[12].Value=="" || contact.Columns[12].Value ==null )
                            {
                                contact.Columns[12].Value = "0";
                            }
                            //if(contact.Columns[0].Value== "4058")
                            //{
                            //}
                            if (customerId==Guid.Empty)
                            {
                                reparingOrder = new ReparingOrder
                                {
                                    Date = Convert.ToDateTime(contact.Columns[1].Value),
                                    RegistrationNo = contact.Columns[0].Value,
                                    CustomerId = customer == null ? customerId : customer.Id,
                                    EmployeeId = employee == null ? Guid.Empty : employee.Id,
                                    UpsDescriptionId = ups == null ? upsId : ups.Id,
                                    ProblemId = issues == null ? reparingId : issues.Id,
                                    Status = contact.Columns[7].Value,
                                    AcessoryIncludedId = accessory == null ? Guid.Empty : accessory,
                                    RegisteredById = doneBY == null ? Guid.Empty : doneBY,
                                    JobAssignId = doneBY == null ? Guid.Empty : doneBY,
                                    AdvanceAmount = Convert.ToDecimal(contact.Columns[10].Value),
                                    CashAmount = Convert.ToDecimal(contact.Columns[11].Value),
                                    RemaningPrice = Convert.ToDecimal(contact.Columns[12].Value),
                                    IsCollected = isCollected,
                                    IsRepared = isRepared,
                                    IsReturn = isReturned,
                                    IsCashed = isCollected,
                                    PaymentType = "Cash",

                                };
                            }
                            else
                            {
                                reparingOrder = new ReparingOrder
                                {
                                    Date = Convert.ToDateTime(contact.Columns[1].Value),
                                    RegistrationNo = contact.Columns[0].Value,
                                    CustomerId = customer == null ? customerId : customer.Id,
                                    EmployeeId = employee == null ? Guid.Empty : employee.Id,
                                    UpsDescriptionId = ups == null ? upsId : ups.Id,
                                    ProblemId = issues == null ? reparingId : issues.Id,
                                    Status = contact.Columns[7].Value,
                                    AcessoryIncludedId = accessory == null ? Guid.Empty : accessory,
                                    RegisteredById = doneBY == null ? Guid.Empty : doneBY,
                                    JobAssignId = doneBY == null ? Guid.Empty : doneBY,
                                    AdvanceAmount = Convert.ToDecimal(contact.Columns[10].Value),
                                    CashAmount = Convert.ToDecimal(contact.Columns[11].Value),
                                    RemaningPrice = Convert.ToDecimal(contact.Columns[12].Value),
                                    IsCollected = isCollected,
                                    IsRepared = isRepared,
                                    IsReturn = isReturned,
                                    IsCashed = isCollected,
                                    PaymentType = "Cash",

                                };
                            }
                            if(reparingOrder.CustomerId==Guid.Empty)
                            {
                                reparingOrder.CustomerId = null;
                            }
                            
                            Context.ReparingOrders.Add(reparingOrder);
                            Context.SaveChanges();



                        }

                        return "Success";

                    }
                    else
                    {
                        return "Failure";
                    }


                }
                catch (Exception e)
                {

                    if (e.Message.Contains("No members are mapped for type "))
                    {
                        return "ExceptionEmpty";
                    }
                    else
                    {
                        return "Exception " + e.Message;
                    }


                }



            }


            public string GenerateCodeFirstTime(bool isCustomer)
            {
                if (isCustomer)
                {
                    return "CU-00001";
                }
                return "SU-00001";
            }

            public string GenerateNewCode(string soNumber, bool isCustomer)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = isCustomer ? "CU-" + autoNo.ToString(format) : "SU-" + autoNo.ToString(format);
                return newCode;

            }

        }
    }

    public class AccountsDataForCategory
    {
        public string AccountCode { get; set; }
        public string CostCenterCode { get; set; }
        public Guid CostCenterId { get; set; }
    }
    public static class DropDownCodeModel
    {

        public static ICollection<ReparingOrderType> ReparingOrderTypes { get; set; }
        public static ICollection<EmployeeRegistration> EmployeeRegistrations { get; set; }
    }

    public class ReparingOrderFile 
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }

        public Guid? WarrantyCategoryId { get; set; }
        public Guid? UpsDescriptionId { get; set; }
        public Guid? ProblemId { get; set; }
        public Guid? AcessoryIncludedId { get; set; }
        public string SerialNo { get; set; }
        public decimal RemaningPrice { get; set; }
        public decimal Discount { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public decimal EstimateAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal CashAmount { get; set; }
        public bool IsCashed { get; set; }
        public string PaymentType { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid? JobAssignId { get; set; }
        public Guid? RegisteredById { get; set; }
        public bool IsComplete { get; set; }
        public bool IsCollected { get; set; }
        public bool IsRepared { get; set; }
        public bool IsReturn { get; set; }
        public bool IsPrint { get; set; }
        public string CradNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string DealerRef { get; set; }
        public string FaultInfo { get; set; }








    }
}
