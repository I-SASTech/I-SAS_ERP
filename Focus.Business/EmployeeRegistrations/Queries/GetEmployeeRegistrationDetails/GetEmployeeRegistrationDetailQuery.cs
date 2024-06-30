using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.EmployeeRegistrations.Queries.GetEmployeeRegistrationDetails
{
    public class GetEmployeeRegistrationDetailQuery : IRequest<EmployeeRegistrationDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetEmployeeRegistrationDetailQuery, EmployeeRegistrationDetailsLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetEmployeeRegistrationDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<EmployeeRegistrationDetailsLookUpModel> Handle(GetEmployeeRegistrationDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var employee = await _context.EmployeeRegistrations.AsNoTracking()
                                .Include(x => x.EmployeeAttachments)
                                .Include(x=>x.Designation)
                                .Include(x=>x.Department)
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


                    var attachmentList = await _context.Attachments.AsNoTracking().Where(x => x.DocumentId == request.Id).Select(x => new AttachmentLookUpModel
                    {
                        Id = x.Id,
                        DocumentId = x.DocumentId,
                        Date = x.Date,
                        Description = x.Description,
                        FileName = x.FileName,
                        Path = x.Path
                    }).ToListAsync(cancellationToken: cancellationToken);


                    if (employee != null)
                    {
                        return new EmployeeRegistrationDetailsLookUpModel()
                        {
                            Id = employee.Id,
                            Code = employee.Code,
                            EnglishName = employee.EnglishName,
                            DesigantionName = employee.Designation?.Name,
                            DepartmentName = employee.Department?.Name,
                            FatherName = employee.FatherName,
                            ArabicName = employee.ArabicName,
                            RegistrationDate = employee.RegistrationDate,
                            Gender = employee.Gender,
                            SalaryType = employee.SalaryType,
                            MartialStatus = employee.MartialStatus,
                            Nationality = employee.Nationality,
                            DateOfBirth = employee.DateOfBirth,
                            MobileNo = employee.MobileNo,
                            OtherContact = employee.OtherContact,
                            Email = employee.Email,
                            BloodGroup = employee.BloodGroup,
                            Address = employee.Address,
                            City = employee.City,
                            Country = employee.Country,
                            PrimaryNameOfPerson = employee.PrimaryNameOfPerson,
                            PrimaryRelation = employee.PrimaryRelation,
                            PrimaryContactNumber = employee.PrimaryContactNumber,
                            PrimaryReferenceEmail = employee.PrimaryReferenceEmail,
                            SecondaryNameOfPerson = employee.SecondaryNameOfPerson,
                            SecondaryRelation = employee.SecondaryRelation,
                            SecondaryContactNumber = employee.SecondaryContactNumber,
                            SecondaryReferenceEmail = employee.SecondaryReferenceEmail,
                            HomePersonName = employee.HomePersonName,
                            HomeRelation = employee.HomeRelation,
                            HomeContactNumber = employee.HomeContactNumber,
                            HomeReferenceEmail = employee.HomeReferenceEmail,
                            HomeCity = employee.HomeCity,
                            HomeCountry = employee.HomeCountry,
                            IDNumber = employee.IDNumber,
                            IDType = employee.IDType,
                            Title = employee.Title,
                            ExpiryDate = employee.ExpiryDate,
                            PassportNumber = employee.PassportNumber,
                            PassportIssueDate = employee.PassportIssueDate,
                            PassportExpiryDate = employee.PassportExpiryDate,
                            PassportIssuingAuthority = employee.PassportIssuingAuthority,
                            DrivingLicenseNumber = employee.DrivingLicenseNumber,
                            DrivingLicenseType = employee.DrivingLicenseType,
                            DrivingExpiryDate = employee.DrivingExpiryDate,
                            DrivingIssuingAuthority = employee.DrivingIssuingAuthority,
                            MedicalPolicNumber = employee.MedicalPolicNumber,
                            MedicalPolicType = employee.MedicalPolicType,
                            MedicalPolicProvider = employee.MedicalPolicProvider,
                            MedicalPolicExpiryDate = employee.MedicalPolicExpiryDate,
                            DepartmentId = employee.DepartmentId,
                            DesignationId = employee.DesignationId,
                            NationalOrForeign = employee.NationalOrForeign,
                            SpouseName1 = employee.SpouseName1,
                            SpouseName2 = employee.SpouseName2,
                            SpouseName3 = employee.SpouseName3,
                            SpouseName4 = employee.SpouseName4,
                            IsActive = !employee.IsActive,
                            Reason = employee.Reason,
                            BankName = employee.BankName,
                            AccountName = employee.AccountName,
                            IbanNumber = employee.IbanNumber,
                            AccountNumber = employee.AccountNumber,
                            BranchName = employee.BranchName,
                            BankAddress = employee.BankAddress,
                            AccountType = employee.AccountType,
                            EmployeeAccess = employee.EmployeeAccess,

                            TemporaryCashRequester = employee.TemporaryCashRequester,
                            TemporaryCashIssuer = employee.TemporaryCashIssuer,
                            TemporaryCashReceiver = employee.TemporaryCashReceiver,
                            Days = employee.Days,
                            Limit = employee.Limit,
                            PerDayWorkHour = employee.PerDayWorkHour,
                            AttachmentList = attachmentList,

                            EmployeeType = employee.EmployeeType == Domain.Enum.EmployeeType.Default ? "" : employee.EmployeeType.ToString(),
                            EmployeeAttachments = employee.EmployeeAttachments.Select(x => new EmployeeRegistrationAttachment()
                            {
                                Id = x.Id,
                                EmployeeId = x.EmployeeId,
                                CNIC = x.CNIC,
                                Passport = x.Passport,
                                DrivingLicense = x.DrivingLicense,
                                Photo = x.Photo,
                                Date = x.Date
                            }).ToList()
                        };
                        
                    }
                    throw new NotFoundException(nameof(employee), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
