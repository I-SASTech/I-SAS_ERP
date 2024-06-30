using System;
using Focus.Business.Extensions;
using Focus.Domain.Enum;

namespace Focus.Business.FinancialYears.Queries.GetCurrentYear
{
   public class CompanySubmissionVmodel
    {
        public string Year { get; set; }
        public PeriodName PeriodName { get; set; }
        public string PeriodDescription => $" {PeriodName.GetDisplayName()} {Year}";
        public bool IsPeriodClosed { get; set; }
        public Guid Id { get; set; }
    }
    }
