using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Permission.Commands.AddUpdateNoblePermission
{
    public class WhiteLabelLookUp
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string TagImage1Name { get; set; }
        public string TagImage1Path { get; set; }
        public string TagImage2Name { get; set; }
        public string TagImage2Path { get; set; }
        public string CompanyName { get; set; }
        public string ApplicationName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string LoginScreenImageName { get; set; }
        public string LoginScreenImagePath { get; set; }
        public string LoginLogoName { get; set; }
        public string LoginLogoPath { get; set; }
        public string BackgroundImageName { get; set; }
        public string BackgroundImagePath { get; set; }
        public string SidebarImageName { get; set; }
        public string SidebarImagePath { get; set; }
        public string Email { get; set; }
        public string FavName { get; set; }
        public string FavIconName { get; set; }
        public string FavIconPath { get; set; }
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

        public string ApplicationBgColor { get; set; }
        public string ApplicationTextColor { get; set; }
        public string CardBgColor { get; set; }
        public string CardTextColor { get; set; }
        public string SetupBgColor { get; set; }
        public string SetupTextColor { get; set; }
        public string SetupCssFilter { get; set; }

        public string SideMenuBtnColorFilter { get; set; }
        public string SideMenuBtnClickBgColor { get; set; }
        public string SideMenuBtnClickColorFilter { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsWhiteLabbeling { get; set; }

        public bool Portuguese { get; set; }
    }
}
