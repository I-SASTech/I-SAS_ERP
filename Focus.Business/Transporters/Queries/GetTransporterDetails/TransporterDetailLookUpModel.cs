using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Focus.Domain.Entities;

namespace Focus.Business.Transporters.Queries.GetTransporterDetails
{
  public  class TransporterDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public static Expression<Func<Transporter, TransporterDetailLookUpModel>> Projection
        {
            get
            {
                return transporter => new TransporterDetailLookUpModel
                {
                    Id = transporter.Id,
                    City = transporter.City,
                    Name = transporter.Name,
                };
            }
        }
        public static TransporterDetailLookUpModel Create(Transporter transporter)
        {
            return Projection.Compile().Invoke(transporter);
        }
  }
}