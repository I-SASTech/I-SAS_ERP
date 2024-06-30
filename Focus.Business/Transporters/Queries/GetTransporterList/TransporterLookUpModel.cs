using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Focus.Business.Common.Mappings;

using Focus.Domain.Entities;

namespace Focus.Business.Transporters.Queries.GetTransporterList
{
   public class TransporterLookUpModel : IMapFrom<Transporter>
   {
       public Guid Id { get; set; }
       public string City { get; set; }
       public string Name { get; set; }


        public void Mapping(Profile profile)
       {
           profile.CreateMap<Transporter, TransporterLookUpModel>();
       }
   }
}