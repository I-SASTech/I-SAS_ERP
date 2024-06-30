using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Terminals.Queries.GetTerminalList
{
    public class TerminalLookUpModel 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public bool isActive { get; set; }
        public string PrinterName { get; set; }
        public string TerminalUserType { get; set; }
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<Terminal, TerminalLookUpModel>();
        //}
    }
}
