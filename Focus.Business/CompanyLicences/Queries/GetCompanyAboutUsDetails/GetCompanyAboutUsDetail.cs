using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Focus.Business.ModulesInformation.ModuleRights.Queries.GetModuleRightList;
using Focus.Business.RolePermission.Queries.GetCompanyPermission;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.CompanyLicences.Queries.GetCompanyAboutUsDetails
{
    public class GetCompanyAboutUsDetail : IRequest<AboutUsLookUpModel>
    {

        public string CategoryName { get; set; }

        public string UserId { get; set; }
        public bool IsAboutUs { get; set; }


        public class Handler : IRequestHandler<GetCompanyAboutUsDetail, AboutUsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCompanyAboutUsDetail> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<AboutUsLookUpModel> Handle(GetCompanyAboutUsDetail request, CancellationToken cancellationToken)
            {
                try
                {

                    var companyInfo = _context.CompanyLicences.OrderBy(x=>x.Id).LastOrDefault();
                    if (companyInfo == null)
                        throw new ApplicationException("You do not have any License");
                    var moduleList = new List<string>();

                    var groupName = "";
                    if (request.IsAboutUs)
                    {
                        var groupInfo = _context.NobleGroups.FirstOrDefault();
                        if (groupInfo == null)
                            throw new ApplicationException("You do not have any Group");
                        var companyPermission = _context.CompanyPermissions.AsEnumerable().GroupBy(x => x.NobleModules).Select(x => x.Key).ToList();
                        if (companyPermission == null)
                            throw new ApplicationException("You do not have any Permission");

                        foreach (var nobleModule in companyPermission)
                        {
                            moduleList.Add(nobleModule.ModuleName);
                        }

                        groupName = groupInfo.GroupName + "-" + Enum.GetName(typeof(GroupType), groupInfo.GroupType);
                    }
                    
                    var aboutUs = new AboutUsLookUpModel()
                    {
                        LicenseType = companyInfo.LicenseType,
                        FromDate = companyInfo.FromDate.ToString("d"),
                        ToDate = companyInfo.ToDate.ToString("d"),
                        IsTechnicalSupport = (companyInfo.IsTechnicalSupport || companyInfo.IsUpdateTechnicalSupport),
                        SupportType = companyInfo.TechnicalSupportPeriod,
                        ModuleList = moduleList,
                        GroupName = groupName,
                        PaymentFrequency = companyInfo.PaymentFrequency,
                        IsActive = companyInfo.IsActive,
                        IsGracePeriod = companyInfo.GracePeriod
                    };
                    return aboutUs;
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
