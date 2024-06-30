using Focus.Domain.Entities;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Logistics.Queries.GetLogisticDetails
{
    public class LogisticDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string LicenseNo { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactName { get; set; }
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

        public static Expression<Func<Logistic, LogisticDetailsLookUpModel>> Projection
        {
            get
            {
                return logistic => new LogisticDetailsLookUpModel
                {
                    Id = logistic.Id,
                    EnglishName = logistic.EnglishName,
                    ArabicName = logistic.ArabicName,
                    LicenseNo = logistic.LicenseNo,
                    Code = logistic.Code,
                    Address = logistic.Address,
                    ContactNo = logistic.ContactNo,
                    ContactName = logistic.ContactName,
                    Email = logistic.Email,
                    Website = logistic.Website,
                    TermsAndCondition = logistic.TermsAndCondition,
                    Xcoordinates = logistic.Xcoordinates,
                    Ycoordinates = logistic.Ycoordinates,
                    Ports = logistic.Ports,
                    ServiceFor = logistic.ServiceFor,
                    ClearanceAgent = logistic.ClearanceAgent,
                    LogisticsForm = logistic.LogisticsForm,
                    IsActive = logistic.IsActive
                };
            }
        }

        public static LogisticDetailsLookUpModel Create(Logistic logistic)
        {
            return Projection.Compile().Invoke(logistic);
        }
    }
}
