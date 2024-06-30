using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class WhiteLabeling: BaseEntity
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string ApplicationName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Email { get; set; }
        public string FavName { get; set; }
        public string SideMenuColor { get; set; }
        public string SideMenuBtnColor { get; set; }
        public string SideMenuBtnClickColor { get; set; }
        public string SaveBtnBgColor { get; set; }
        public string SaveBtnColor { get; set; }
        public string CancelBgBtnColor { get; set; }
        public string CancelBtnColor { get; set; }
        public string HeadingColor { get; set; }
        public string TableHeaderBgColor { get; set; }
        public string TableHeaderColor { get; set; }
        public string InvoiceTitleBgColor { get; set; }
        public string InvoiceTitleColor { get; set; }
        public Guid? BranchId { get; set; }

    }
}
