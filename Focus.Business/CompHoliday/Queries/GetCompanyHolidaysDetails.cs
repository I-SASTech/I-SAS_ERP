using Focus.Business.CompHoliday.Models;
using Focus.Business.Holidays.Queries.GetHolidayDetails;
using Focus.Business.Holidays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Microsoft.Extensions.Logging;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.CompHoliday.Queries
{
    public class GetCompanyHolidaysDetails : IRequest<CompanyHolidaysLookupModel>
    {
        public Guid? Id { get; set; }
        public class Handler : IRequestHandler<GetCompanyHolidaysDetails, CompanyHolidaysLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
               ILogger<GetCompanyHolidaysDetails> logger,
               IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<CompanyHolidaysLookupModel> Handle(GetCompanyHolidaysDetails request, CancellationToken cancellationToken)
            {
                try
                {
                    var company =await _context.CompanyHolidays.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                    if(company != null)
                    {
                        return new CompanyHolidaysLookupModel
                        {
                            Id = company.Id,
                            HolidayType = company.HolidayType,
                            Date = company.Date,
                            Year = company.Year,
                            Description = company.Description,
                            PaidStatus = company.PaidStatus,
                            IsActive = company.IsActive,
                            Color = company.Color,
                        };
                    }
                    else
                    {
                        return null;
                    }
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
