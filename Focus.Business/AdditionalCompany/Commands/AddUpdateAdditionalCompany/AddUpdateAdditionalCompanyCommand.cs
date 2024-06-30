using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.AdditionalCompany.Commands.AddUpdateAdditionalCompany
{
    public class AddUpdateAdditionalCompanyCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string NameArabic { get; set; }
        public string CommercialRegNo { get; set; }
        public string VatRegistrationNo { get; set; }
        public string CityArabic { get; set; }
        public string CountryArabic { get; set; }
        public string Mobile { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string AddressArabic { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CompanyId { get; set; }
        
        public class Handler : IRequestHandler<AddUpdateAdditionalCompanyCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly IPrincipal _principal;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            
            public Handler(IApplicationDbContext context, ILogger<AddUpdateAdditionalCompanyCommand> logger, IPrincipal principal, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _principal = principal;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateAdditionalCompanyCommand request, CancellationToken cancellationToken)
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
                        var additionalCompanyInformations = await _context.companyInformations.FindAsync(request.Id);

                        var isadditionalCompanyNameExist = await _context.companyInformations
                            .Where(x => x.NameArabic == request.NameArabic)
                            .AnyAsync(cancellationToken);
                        

                        if (additionalCompanyInformations == null)
                            throw new NotFoundException(request.NameArabic, request.Id);

                        if (additionalCompanyInformations.NameArabic != request.NameArabic && isadditionalCompanyNameExist)
                            return Guid.Empty;

                        additionalCompanyInformations.NameArabic = request.NameArabic;
                        additionalCompanyInformations.CommercialRegNo = request.CommercialRegNo;
                        additionalCompanyInformations.VatRegistrationNo = request.VatRegistrationNo;
                        additionalCompanyInformations.CityArabic = request.CityArabic;
                        additionalCompanyInformations.CountryArabic = request.CountryArabic;
                        additionalCompanyInformations.Mobile = request.Mobile;
                        additionalCompanyInformations.PhoneNo = request.PhoneNo;
                        additionalCompanyInformations.Email = request.Email;
                        additionalCompanyInformations.Website = request.Website;
                        additionalCompanyInformations.AddressArabic = request.AddressArabic;
                        additionalCompanyInformations.CreatedDate = request.CreatedDate;


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=request.VatRegistrationNo
                        }, cancellationToken);


                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Additional Company Information id after successfully updating data
                        return additionalCompanyInformations.Id;
                        //Check Additional Company Information exist

                    }
                    else
                    {
                        //Check Additional Company Information name is already exists
                        var isadditionalCompanyNameExist = await _context.companyInformations
                            .Where(x => x.NameArabic == request.NameArabic)
                            .AnyAsync(cancellationToken);
                        
                        if (!isadditionalCompanyNameExist)
                        {
                                var sadditionalCompanyName = new CompanyInformation
                                {
                                    NameArabic = request.NameArabic,
                                    CommercialRegNo = request.CommercialRegNo,
                                    VatRegistrationNo = request.VatRegistrationNo,
                                    CityArabic = request.CityArabic,
                                    CountryArabic = request.CountryArabic,
                                    Mobile = request.Mobile,
                                    PhoneNo = request.PhoneNo,
                                    Email = request.Email,
                                    Website = request.Website,
                                    AddressArabic = request.AddressArabic,
                                    CreatedDate = DateTime.UtcNow,
                                    CompanyId = _principal.Identity.CompanyId(),
                                };

                                //Add Additional Company Information to database
                                await _context.companyInformations.AddAsync(sadditionalCompanyName, cancellationToken);

                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    Code = _code,
                                    DocumentNo=sadditionalCompanyName.VatRegistrationNo
                                }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                                return sadditionalCompanyName.Id;
                          
                        }

                        return Guid.Empty;
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("AdditionalCompany Name Already Exist");
                }
            }
        }
    }
}
