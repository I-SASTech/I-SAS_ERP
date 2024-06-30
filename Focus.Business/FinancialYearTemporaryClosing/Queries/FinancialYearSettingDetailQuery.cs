using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.FinancialYearTemporaryClosing.Models;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.FinancialYearTemporaryClosing.Queries
{
    public class FinancialYearSettingDetailQuery : IRequest<FinancialYearSettingDetailModel>
    {
        public class Handler : IRequestHandler<FinancialYearSettingDetailQuery, FinancialYearSettingDetailModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<FinancialYearSettingDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<FinancialYearSettingDetailModel> Handle(FinancialYearSettingDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    //Financial Year Settings
                    var financialYearSetting = await _context.FinancialYearSettings
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    if (financialYearSetting == null)
                    {
                        return new FinancialYearSettingDetailModel();
                    }

                    var companySubmissionPeriod = await _context.CompanySubmissionPeriods
                        .AsNoTracking()
                        .Select(x => new
                        {
                            x.YearlyPeriod.IsYearlyPeriodClosed,
                            x.YearlyPeriodId,
                            x.PeriodStart,
                            x.PeriodEnd,
                        }).FirstOrDefaultAsync(x => x.PeriodStart.Date <= DateTime.Now.Date && DateTime.Now.Date <= x.PeriodEnd.Date, cancellationToken: cancellationToken);

                    if (companySubmissionPeriod == null)
                    {
                        var lastFinancialYear = DateTime.Now.AddYears(-1);
                        var lastCompanySubmissionPeriod = await _context.CompanySubmissionPeriods
                            .AsNoTracking()
                            .Select(x => new
                            {
                                x.YearlyPeriod.IsYearlyPeriodClosed,
                                x.YearlyPeriodId,
                                x.PeriodStart,
                                x.PeriodEnd,
                            }).FirstOrDefaultAsync(x => x.PeriodStart.Date <= lastFinancialYear.Date && lastFinancialYear.Date <= x.PeriodEnd.Date, cancellationToken: cancellationToken);


                        if (lastCompanySubmissionPeriod != null)
                        {
                            var yearlyPeriod = await _context.CompanySubmissionPeriods
                                .Where(x => x.YearlyPeriodId == lastCompanySubmissionPeriod.YearlyPeriodId)
                                .Select(z => new
                                {
                                    z.IsPeriodClosed,
                                    z.PeriodStart,
                                    z.PeriodEnd,
                                    z.PeriodName,
                                }).ToListAsync(cancellationToken: cancellationToken);

                            var closingTypePeriodList = new List<ClosingTypePeriodListModel>();
                            if (financialYearSetting.ClosingType == "Month")
                            {
                                foreach (var item in yearlyPeriod)
                                {
                                    closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                    {
                                        Name = item.PeriodName.ToString(),
                                        IsClose = item.IsPeriodClosed,
                                        FromDate = item.PeriodStart,
                                        ToDate = item.PeriodEnd,
                                    });
                                }
                            }
                            else if (financialYearSetting.ClosingType == "Quarterly")
                            {
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "First Quarter",
                                    IsClose = yearlyPeriod[0].IsPeriodClosed,
                                    FromDate = yearlyPeriod[0].PeriodStart,
                                    ToDate = yearlyPeriod[2].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Second Quarter",
                                    IsClose = yearlyPeriod[3].IsPeriodClosed,
                                    FromDate = yearlyPeriod[3].PeriodStart,
                                    ToDate = yearlyPeriod[5].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Third Quarter",
                                    IsClose = yearlyPeriod[6].IsPeriodClosed,
                                    FromDate = yearlyPeriod[6].PeriodStart,
                                    ToDate = yearlyPeriod[8].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Fourth Quarter",
                                    IsClose = yearlyPeriod[9].IsPeriodClosed,
                                    FromDate = yearlyPeriod[9].PeriodStart,
                                    ToDate = yearlyPeriod[11].PeriodEnd
                                });
                            }
                            else if (financialYearSetting.ClosingType == "6 Months")
                            {
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "First Half-Year",
                                    IsClose = yearlyPeriod[0].IsPeriodClosed,
                                    FromDate = yearlyPeriod[0].PeriodStart,
                                    ToDate = yearlyPeriod[5].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Second Half-Year",
                                    IsClose = yearlyPeriod[6].IsPeriodClosed,
                                    FromDate = yearlyPeriod[6].PeriodStart,
                                    ToDate = yearlyPeriod[11].PeriodEnd
                                });
                            }
                            else if (financialYearSetting.ClosingType == "Year")
                            {
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Annual Financial Year",
                                    IsClose = yearlyPeriod[0].IsPeriodClosed,
                                    FromDate = yearlyPeriod[0].PeriodStart,
                                    ToDate = yearlyPeriod[11].PeriodEnd,
                                });
                            }

                            return new FinancialYearSettingDetailModel
                            {
                                Id = financialYearSetting.Id,
                                IsAutoClosing = financialYearSetting.IsAutoClosing,
                                ClosingType = financialYearSetting.ClosingType,
                                ClosingTypePeriodList = closingTypePeriodList

                            };
                        }

                        return new FinancialYearSettingDetailModel();
                    }
                    else
                    {
                        var closingTypePeriodList = new List<ClosingTypePeriodListModel>();

                        //Last Financial Year
                        var lastFinancialYear = DateTime.Now.AddYears(-1);
                        var lastCompanySubmissionPeriod = await _context.CompanySubmissionPeriods
                            .AsNoTracking()
                            .Select(x => new
                            {
                                x.YearlyPeriod.IsYearlyPeriodClosed,
                                x.YearlyPeriodId,
                                x.PeriodStart,
                                x.PeriodEnd,
                            }).FirstOrDefaultAsync(x => x.PeriodStart.Date <= lastFinancialYear.Date && lastFinancialYear.Date <= x.PeriodEnd.Date, cancellationToken: cancellationToken);

                        if (lastCompanySubmissionPeriod != null)
                        {
                            var lastYearlyPeriod = await _context.CompanySubmissionPeriods
                            .Where(x => x.YearlyPeriodId == lastCompanySubmissionPeriod.YearlyPeriodId)
                            .Select(z => new
                            {
                                z.IsPeriodClosed,
                                z.PeriodStart,
                                z.PeriodEnd,
                                z.PeriodName,
                            }).ToListAsync(cancellationToken: cancellationToken);


                            if (financialYearSetting.ClosingType == "Month")
                            {
                                foreach (var item in lastYearlyPeriod)
                                {
                                    closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                    {
                                        Name = item.PeriodName.ToString(),
                                        IsClose = item.IsPeriodClosed,
                                        FromDate = item.PeriodStart,
                                        ToDate = item.PeriodEnd,
                                    });
                                }
                            }
                            else if (financialYearSetting.ClosingType == "Quarterly")
                            {
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "First Quarter",
                                    IsClose = lastYearlyPeriod[0].IsPeriodClosed,
                                    FromDate = lastYearlyPeriod[0].PeriodStart,
                                    ToDate = lastYearlyPeriod[2].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Second Quarter",
                                    IsClose = lastYearlyPeriod[3].IsPeriodClosed,
                                    FromDate = lastYearlyPeriod[3].PeriodStart,
                                    ToDate = lastYearlyPeriod[5].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Third Quarter",
                                    IsClose = lastYearlyPeriod[6].IsPeriodClosed,
                                    FromDate = lastYearlyPeriod[6].PeriodStart,
                                    ToDate = lastYearlyPeriod[8].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Fourth Quarter",
                                    IsClose = lastYearlyPeriod[9].IsPeriodClosed,
                                    FromDate = lastYearlyPeriod[9].PeriodStart,
                                    ToDate = lastYearlyPeriod[11].PeriodEnd
                                });
                            }
                            else if (financialYearSetting.ClosingType == "6 Months")
                            {
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "First Half-Year",
                                    IsClose = lastYearlyPeriod[0].IsPeriodClosed,
                                    FromDate = lastYearlyPeriod[0].PeriodStart,
                                    ToDate = lastYearlyPeriod[5].PeriodEnd
                                });
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Second Half-Year",
                                    IsClose = lastYearlyPeriod[6].IsPeriodClosed,
                                    FromDate = lastYearlyPeriod[6].PeriodStart,
                                    ToDate = lastYearlyPeriod[11].PeriodEnd
                                });
                            }
                            else if (financialYearSetting.ClosingType == "Year")
                            {
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = "Annual Financial Year",
                                    IsClose = lastYearlyPeriod[0].IsPeriodClosed,
                                    FromDate = lastYearlyPeriod[0].PeriodStart,
                                    ToDate = lastYearlyPeriod[11].PeriodEnd,
                                });
                            }

                        }

                        var yearlyPeriod = await _context.CompanySubmissionPeriods
                        .Where(x => x.YearlyPeriodId == companySubmissionPeriod.YearlyPeriodId)
                        .Select(z => new
                        {
                            z.IsPeriodClosed,
                            z.PeriodStart,
                            z.PeriodEnd,
                            z.PeriodName,
                        }).ToListAsync(cancellationToken: cancellationToken);


                        if (financialYearSetting.ClosingType == "Month")
                        {
                            foreach (var item in yearlyPeriod)
                            {
                                closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                                {
                                    Name = item.PeriodName.ToString(),
                                    IsClose = item.IsPeriodClosed,
                                    FromDate = item.PeriodStart,
                                    ToDate = item.PeriodEnd,
                                });
                            }
                        }
                        else if (financialYearSetting.ClosingType == "Quarterly")
                        {
                            closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                            {
                                Name = "First Quarter",
                                IsClose = yearlyPeriod[0].IsPeriodClosed,
                                FromDate = yearlyPeriod[0].PeriodStart,
                                ToDate = yearlyPeriod[2].PeriodEnd
                            });
                            closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                            {
                                Name = "Second Quarter",
                                IsClose = yearlyPeriod[3].IsPeriodClosed,
                                FromDate = yearlyPeriod[3].PeriodStart,
                                ToDate = yearlyPeriod[5].PeriodEnd
                            });
                            closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                            {
                                Name = "Third Quarter",
                                IsClose = yearlyPeriod[6].IsPeriodClosed,
                                FromDate = yearlyPeriod[6].PeriodStart,
                                ToDate = yearlyPeriod[8].PeriodEnd
                            });
                            closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                            {
                                Name = "Fourth Quarter",
                                IsClose = yearlyPeriod[9].IsPeriodClosed,
                                FromDate = yearlyPeriod[9].PeriodStart,
                                ToDate = yearlyPeriod[11].PeriodEnd
                            });
                        }
                        else if (financialYearSetting.ClosingType == "6 Months")
                        {
                            closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                            {
                                Name = "First Half-Year",
                                IsClose = yearlyPeriod[0].IsPeriodClosed,
                                FromDate = yearlyPeriod[0].PeriodStart,
                                ToDate = yearlyPeriod[5].PeriodEnd
                            });
                            closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                            {
                                Name = "Second Half-Year",
                                IsClose = yearlyPeriod[6].IsPeriodClosed,
                                FromDate = yearlyPeriod[6].PeriodStart,
                                ToDate = yearlyPeriod[11].PeriodEnd
                            });
                        }
                        else if (financialYearSetting.ClosingType == "Year")
                        {
                            closingTypePeriodList.Add(new ClosingTypePeriodListModel()
                            {
                                Name = "Annual Financial Year",
                                IsClose = yearlyPeriod[0].IsPeriodClosed,
                                FromDate = yearlyPeriod[0].PeriodStart,
                                ToDate = yearlyPeriod[11].PeriodEnd,
                            });
                        }

                        return new FinancialYearSettingDetailModel
                        {
                            Id = financialYearSetting.Id,
                            IsAutoClosing = financialYearSetting.IsAutoClosing,
                            ClosingType = financialYearSetting.ClosingType,
                            ClosingTypePeriodList = closingTypePeriodList
                        };
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
