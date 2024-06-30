using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance;
using Focus.Business.HR.Payroll.Contributions.Commands;
using Focus.Business.HR.Payroll.Deductions.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.SalaryTemplates.Commands.AddSalaryTemplate
{
    public class GetSalaryTemplateDetailQuery : IRequest<SalaryTemplateLookUp>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetSalaryTemplateDetailQuery, SalaryTemplateLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetSalaryTemplateDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<SalaryTemplateLookUp> Handle(GetSalaryTemplateDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var salaryTemplate = await _context.SalaryTemplates.AsNoTracking()
                        .Include(x => x.SalaryContributions)
                        .ThenInclude(x => x.Contribution)
                        .Include(x => x.SalaryDeductions)
                        .ThenInclude(x => x.Deduction)
                        .Include(x => x.SalaryAllowances)
                        .ThenInclude(x => x.Allowance)
                        .ThenInclude(x=>x.AllowanceType)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    if (salaryTemplate != null)
                    {

                        return new SalaryTemplateLookUp()
                        {
                            Id = salaryTemplate.Id,
                            Code = salaryTemplate.Code,
                            StructureName = salaryTemplate.StructureName,
                            SalaryDeductions = salaryTemplate.SalaryDeductions.Select(x =>
                                new DeductionLookUpModel()
                                {
                                    Id = x.DeductionId,
                                    NameInPayslip = x.Deduction.NameInPayslip,
                                    NameInPayslipArabic = x.Deduction.NameInPayslipArabic,
                                    AmountType = x.AmountType,
                                    TaxPlan = x.TaxPlan,
                                    Amount = x.Amount,



                                }).ToList(),
                            SalaryContributions = salaryTemplate.SalaryContributions.Select(x =>
                                new ContributionLookUpModel()
                                {
                                    Id = x.ContributionId,
                                    NameInPayslip = x.Contribution.NameInPayslip,
                                    NameInPayslipArabic = x.Contribution.NameInPayslipArabic,
                                    AmountType = x.AmountType,
                                    Amount = x.Amount,



                                }).ToList(),
                            SalaryAllowances = salaryTemplate.SalaryAllowances.Select(x =>
                                new AllowanceLookUp()
                                {
                                    Id = x.AllowanceId,
                                    AllowanceNameEn = x.Allowance.AllowanceType.Name,
                                    AllowanceNameAr = x.Allowance.AllowanceType.NameArabic,
                                    AmountType = x.AmountType,
                                    TaxPlan = x.TaxPlan,
                                    Amount = x.Amount,



                                }).ToList(),

                        };
                    }
                    throw new NotFoundException(nameof(salaryTemplate), request.Id);
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
