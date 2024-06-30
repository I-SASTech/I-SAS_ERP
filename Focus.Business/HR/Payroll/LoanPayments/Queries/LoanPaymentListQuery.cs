using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.LoanPayments.Commands;
using Focus.Business.HR.Payroll.LoanPays.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.LoanPayments.Queries
{
    public class LoanPaymentListQuery : PagedRequest, IRequest<PagedResult<List<LoanPaymentLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<LoanPaymentListQuery, PagedResult<List<LoanPaymentLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<LoanPaymentListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<LoanPaymentLookUpModel>>> Handle(LoanPaymentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.LoanPayments.AsNoTracking()
                        .Include(x=>x.EmployeeRegistration)
                        .Include(x=>x.LoanPays)
                        .AsQueryable();

                   

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                            x.EmployeeRegistration.EnglishName.Contains(searchTerm) 
                            || x.EmployeeRegistration.ArabicName.Contains(searchTerm));

                    }

                    query = query.OrderBy(x => x.Id);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<LoanPaymentLookUpModel>();
                    foreach (var loanPayment in query)
                    {
                        list.Add(new LoanPaymentLookUpModel()
                        {
                            Id = loanPayment.Id,
                            EmployeeRegistrationId = loanPayment.EmployeeRegistrationId,
                            Description = loanPayment.Description,
                            LoanType = loanPayment.LoanType,
                            RecoveryMethod = loanPayment.RecoveryMethod,
                            InstallmentMethod = loanPayment.InstallmentMethod,
                            LoanDate = loanPayment.LoanTakenDate.ToString("MM/dd/yyyy"),
                            PaymentStartDate = loanPayment.PaymentStartDate,
                            LoanAmount = loanPayment.LoanAmount,
                            RecoveryLoanAmount = loanPayment.RecoveryLoanAmount,
                            Payment = loanPayment.Payment,
                            InstallmentBaseSalary = loanPayment.InstallmentBaseSalary,
                            EmployeeSalary = loanPayment.EmployeeSalary,
                            EmployeeName = loanPayment.EmployeeRegistration.EnglishName,
                            EmployeeNameArabic = loanPayment.EmployeeRegistration.ArabicName,
                            DeductionValue = loanPayment.DeductionValue,
                            RemainingLoan = loanPayment.RemainingLoan,
                            IsActive = loanPayment.IsActive,
                            LoanPays = loanPayment.LoanPays.Select(x =>
                                new LoanPayLookUpModel()
                                {
                                    Id = x.Id,
                                    RecoveryDate = x.PaymentDate.ToString("MM/dd/yyyy"),
                                    LoanPaymentId = x.LoanPaymentId,
                                    Comments = x.Comments,
                                    Amount = x.Amount,
                                    RemainingLoan = x.RemainingLoan,



                                }).ToList(),
                        });
                    }

                    return new PagedResult<List<LoanPaymentLookUpModel>>
                    {
                        Results = list,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = list.Count / request.PageSize
                    };





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
