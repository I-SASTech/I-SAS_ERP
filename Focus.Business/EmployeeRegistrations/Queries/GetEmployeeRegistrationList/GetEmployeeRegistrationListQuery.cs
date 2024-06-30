using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;

namespace Focus.Business.EmployeeRegistrations.Queries.GetEmployeeRegistrationList
{
    public class GetEmployeeRegistrationListQuery : PagedRequest, IRequest<PagedResult<List<EmployeeRegistrationLookUpModel>>>
    {
        public string employeeType { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? DepartmentId { get; set; }

        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public bool isSignup { get; set; }

        public class Handler : IRequestHandler<GetEmployeeRegistrationListQuery, PagedResult<List<EmployeeRegistrationLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly IUserComponent _userComponent;

            public Handler(IApplicationDbContext context,
                ILogger<GetEmployeeRegistrationListQuery> logger,
                IMapper mapper, IUserComponent userComponent)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _userComponent = userComponent;
            }
            public async Task<PagedResult<List<EmployeeRegistrationLookUpModel>>> Handle(GetEmployeeRegistrationListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isSignup)
                    {
                        var employeeIdsHasCounter = _userComponent.ListingUsers().Select(x => x.EmployeeId).ToList();

                        var query = _context.EmployeeRegistrations
                            .AsNoTracking()
                            .Where(x => !employeeIdsHasCounter.Contains(x.Id) && !x.IsActive)
                            .AsQueryable();

                        var contactList = await query.OrderBy(x => x.EnglishName)
                            .ProjectTo<EmployeeRegistrationLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<List<EmployeeRegistrationLookUpModel>>
                        {
                            Results = contactList
                        };
                    }
                    else if(request.IsDropDown)
                    {

                        var query = _context.EmployeeRegistrations
                            .AsNoTracking()
                            .Include(x=>x.EmployeeSalaries)
                            .Where(x=>!x.IsActive)
                            .AsQueryable();
                        if (request.employeeType == "Contractor")
                        {
                            query = query.Where(x => x.EmployeeType == EmployeeType.Contractor);
                        }

                        var contactList = await query.OrderBy(x => x.Code)
                            .ProjectTo<EmployeeRegistrationLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<List<EmployeeRegistrationLookUpModel>>
                        {
                            Results = contactList
                        };
                    }
                    else
                    {
                        var query = _context.EmployeeRegistrations.AsNoTracking().Include(x=>x.EmployeeSalaries).AsQueryable();
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                                x.EnglishName.Contains(searchTerm) || x.ArabicName.Contains(searchTerm) ||x.Code.Contains(searchTerm));

                        }
                        if(request.DepartmentId!= null)
                        {
                            query=query.Where(x=>x.DepartmentId==request.DepartmentId);
                        }
                        if (request.DesignationId != null)
                        {
                            query = query.Where(x => x.DesignationId == request.DesignationId);
                        }


                        if (!string.IsNullOrEmpty(request.employeeType))
                        {
                            EmployeeType employeeType;
                            if (Enum.TryParse(request.employeeType, out employeeType))
                            {
                                query = query.Where(x => x.EmployeeType == employeeType);


                            }
                        }
                        
                        query = query.OrderByDescending(x => x.Code);

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var employeeList = await query
                            .ProjectTo<EmployeeRegistrationLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<List<EmployeeRegistrationLookUpModel>>
                        {
                            Results = employeeList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = employeeList.Count / request.PageSize
                        };
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
