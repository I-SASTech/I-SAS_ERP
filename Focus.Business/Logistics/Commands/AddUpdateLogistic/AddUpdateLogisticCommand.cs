using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Logistics.Commands.AddUpdateLogistic
{
    public class AddUpdateLogisticCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string LicenseNo { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TermsAndCondition { get; set; }
        public string Xcoordinates { get; set; }
        public string Ycoordinates { get; set; }
        public SeaPort Ports { get; set; }
        public string ServiceFor { get; set; }
        public Guid? ClearanceAgent { get; set; }
        public Domain.Enum.Logistics LogisticsForm { get; set; }
        public bool IsActive { get; set; }
        public Guid? BranchId { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateLogisticCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateLogisticCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateLogisticCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Id != Guid.Empty)
                    {
                        var logistics = await _context.Logistics.FindAsync(request.Id);
                        if (logistics == null)
                            throw new NotFoundException("Logistic Name", request.Code);

                        if (logistics.LogisticsForm == Domain.Enum.Logistics.ClearanceAgent)
                        {
                            var ledgerAccount = await _context.LedgerAccounts.FirstOrDefaultAsync(x => x.AccountLedgerId == logistics.Id, cancellationToken: cancellationToken);
                            if (ledgerAccount != null)
                            {
                                ledgerAccount.Name = request.EnglishName;
                                ledgerAccount.NameArabic = request.ArabicName;
                            }
                        }

                        logistics.EnglishName = request.EnglishName;
                        logistics.ArabicName = request.ArabicName;
                        logistics.LicenseNo = request.LicenseNo;
                        logistics.Address = request.Address;
                        logistics.ContactNo = request.ContactNo;
                        logistics.ContactName = request.ContactName;
                        logistics.Email = request.Email;
                        logistics.Website = request.Website;
                        logistics.TermsAndCondition = request.TermsAndCondition;
                        logistics.Xcoordinates = request.Xcoordinates;
                        logistics.Xcoordinates = request.Xcoordinates;
                        logistics.Ycoordinates = request.Ycoordinates;
                        logistics.Ports = request.Ports;
                        logistics.ServiceFor = request.ServiceFor;
                        logistics.LogisticsForm = request.LogisticsForm;
                        logistics.IsActive = request.IsActive;
                        logistics.BranchId = request.BranchId;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = logistics.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        return logistics.Id;
                    }
                    else
                    {
                        //New logistic Added
                        Logistic logistic;
                        if (request.LogisticsForm == (Domain.Enum.Logistics.Transporter))
                        {
                            logistic = new Logistic
                            {
                                EnglishName = request.EnglishName,
                                ArabicName = request.ArabicName,
                                LicenseNo = request.LicenseNo,
                                Address = request.Address,
                                ContactNo = request.ContactNo,
                                ContactName = request.ContactName,
                                Email = request.Email,
                                Website = request.Website,
                                TermsAndCondition = request.TermsAndCondition,
                                Xcoordinates = request.Xcoordinates,
                                Ycoordinates = request.Ycoordinates,
                                LogisticsForm = request.LogisticsForm,
                                Code = request.Code,
                                IsActive = request.IsActive,
                                BranchId = request.BranchId,
                            };
                        }
                        else if (request.LogisticsForm == (Domain.Enum.Logistics.ClearanceAgent))
                        {
                            var accPay = await _context.Accounts.AsNoTracking()
                                .Include(x => x.CostCenter)
                                .FirstOrDefaultAsync(x => x.Code == "29000001", cancellationToken: cancellationToken);

                            if (accPay == null)
                            {
                                var accountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Liabilities" || x.NameArabic == "التزامات", cancellationToken: cancellationToken);
                                var newCostCenter = new CostCenter()
                                {
                                    Code = "290000",
                                    Name = "Clearance Agent",
                                    NameArabic = "وكيل التخليص",
                                    Description = "Clearance Agent",
                                    IsActive = true,
                                    AccountTypeId = accountType.Id
                                };
                                await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);

                                var account = new Account
                                {
                                    IsActive = true,
                                    Name = request.EnglishName + " Clearance Agent",
                                    NameArabic = request.ArabicName + " وكيل التخليص",
                                    Code = "29000001",
                                    CostCenterId = newCostCenter.Id,
                                };
                                await _context.Accounts.AddAsync(account, cancellationToken);

                                logistic = new Logistic
                                {
                                    EnglishName = request.EnglishName,
                                    ArabicName = request.ArabicName,
                                    LicenseNo = request.LicenseNo,
                                    Address = request.Address,
                                    ContactNo = request.ContactNo,
                                    ContactName = request.ContactName,
                                    Email = request.Email,
                                    Website = request.Website,
                                    TermsAndCondition = request.TermsAndCondition,
                                    Xcoordinates = request.Xcoordinates,
                                    Ycoordinates = request.Ycoordinates,
                                    Ports = request.Ports,
                                    ServiceFor = request.ServiceFor,
                                    ClearanceAgent = account.Id,
                                    LogisticsForm = request.LogisticsForm,
                                    Code = request.Code,
                                    IsActive = request.IsActive,
                                    BranchId = request.BranchId,
                                };

                                var ledgerAccount = new LedgerAccount()
                                {
                                    Name = request.EnglishName,
                                    NameArabic = request.ArabicName,
                                    Description = request.EnglishName + " " + request.Code,
                                    Code = request.Code,
                                    AccountId = account.Id,
                                    AccountLedgerId = logistic.Id,
                                    IsActive = request.IsActive,
                                    AccountLeaderType = AccountLeaderType.ClearanceAgent
                                };
                                await _context.LedgerAccounts.AddAsync(ledgerAccount, cancellationToken);
                            }
                            else
                            {
                                //var account = new Account
                                //{
                                //    IsActive = true,
                                //    Name = request.EnglishName + " Clearance Agent",
                                //    NameArabic = request.ArabicName + " وكيل التخليص",
                                //    Code = (Convert.ToInt64(accPay.Code) + 1).ToString(),
                                //    CostCenterId = accPay.CostCenterId,
                                //};
                                //await Context.Accounts.AddAsync(account, cancellationToken);

                                logistic = new Logistic
                                {
                                    EnglishName = request.EnglishName,
                                    ArabicName = request.ArabicName,
                                    LicenseNo = request.LicenseNo,
                                    Address = request.Address,
                                    ContactNo = request.ContactNo,
                                    ContactName = request.ContactName,
                                    Email = request.Email,
                                    Website = request.Website,
                                    TermsAndCondition = request.TermsAndCondition,
                                    Xcoordinates = request.Xcoordinates,
                                    Ycoordinates = request.Ycoordinates,
                                    Ports = request.Ports,
                                    ServiceFor = request.ServiceFor,
                                    ClearanceAgent = accPay.Id,
                                    LogisticsForm = request.LogisticsForm,
                                    Code = request.Code,
                                    IsActive = request.IsActive,
                                    BranchId = request.BranchId,
                                };

                                var ledgerAccount = new LedgerAccount()
                                {
                                    Name = request.EnglishName,
                                    NameArabic = request.ArabicName,
                                    Description = request.EnglishName + " " + request.Code,
                                    Code = request.Code,
                                    AccountId = accPay.Id,
                                    AccountLedgerId = logistic.Id,
                                    IsActive = request.IsActive,
                                    AccountLeaderType = AccountLeaderType.ClearanceAgent
                                };
                                await _context.LedgerAccounts.AddAsync(ledgerAccount, cancellationToken);
                            }
                        }
                        else
                        {
                            logistic = new Logistic
                            {
                                EnglishName = request.EnglishName,
                                ArabicName = request.ArabicName,
                                LicenseNo = request.LicenseNo,
                                Address = request.Address,
                                ContactNo = request.ContactNo,
                                ContactName = request.ContactName,
                                Email = request.Email,
                                Website = request.Website,
                                TermsAndCondition = request.TermsAndCondition,
                                Xcoordinates = request.Xcoordinates,
                                Ycoordinates = request.Ycoordinates,
                                LogisticsForm = request.LogisticsForm,
                                Code = request.Code,
                                IsActive = request.IsActive,
                                BranchId = request.BranchId,
                            };
                        }

                        //Add Department to database
                        await _context.Logistics.AddAsync(logistic, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = request.BranchId,
                            Code = _code
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return logistic.Id;
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
