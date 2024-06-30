using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Permission.Commands.AddUpdateNoblePermission;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Permission.Commands
{
    public class ApplyWhiteLabelling : IRequest<Message>
    {
        //Get all variable in entity
        public WhiteLabelLookUp WhiteLabelLookUp { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<ApplyWhiteLabelling, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            public readonly ILogger<ApplyWhiteLabelling> Logger;
            private readonly IHostingEnvironment _hostingEnvironment;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<ApplyWhiteLabelling> logger, IHostingEnvironment hostingEnvironment)
            {
                Context = context;
                Logger = logger;
                _hostingEnvironment = hostingEnvironment;
            }


            public async Task<Message> Handle(ApplyWhiteLabelling request, CancellationToken cancellationToken)
            {

                try
                {
                    string host = "ftp://" + request.WhiteLabelLookUp.Host;
                    if (string.IsNullOrEmpty(request.WhiteLabelLookUp.Port))
                        host += ":" + request.WhiteLabelLookUp.Port;
                    if (request.WhiteLabelLookUp.IsWhiteLabbeling)
                    {
                        if (!string.IsNullOrEmpty(request.WhiteLabelLookUp.TagImage1Name)) PushWhiteLabelingImagesAsync(request.WhiteLabelLookUp.TagImage1Path, request.WhiteLabelLookUp.TagImage1Name, host, request.WhiteLabelLookUp.UserName, request.WhiteLabelLookUp.Password);
                        if (!string.IsNullOrEmpty(request.WhiteLabelLookUp.LoginScreenImageName)) PushWhiteLabelingImagesAsync(request.WhiteLabelLookUp.LoginScreenImagePath, request.WhiteLabelLookUp.LoginScreenImageName, host, request.WhiteLabelLookUp.UserName, request.WhiteLabelLookUp.Password);
                        if (!string.IsNullOrEmpty(request.WhiteLabelLookUp.LoginLogoName)) PushWhiteLabelingImagesAsync(request.WhiteLabelLookUp.LoginLogoPath, request.WhiteLabelLookUp.LoginLogoName, host, request.WhiteLabelLookUp.UserName, request.WhiteLabelLookUp.Password);
                        if (!string.IsNullOrEmpty(request.WhiteLabelLookUp.BackgroundImageName)) PushWhiteLabelingImagesAsync(request.WhiteLabelLookUp.BackgroundImagePath, request.WhiteLabelLookUp.BackgroundImageName, host, request.WhiteLabelLookUp.UserName, request.WhiteLabelLookUp.Password);
                        if (!string.IsNullOrEmpty(request.WhiteLabelLookUp.SidebarImageName)) PushWhiteLabelingImagesAsync(request.WhiteLabelLookUp.SidebarImagePath, request.WhiteLabelLookUp.SidebarImageName, host, request.WhiteLabelLookUp.UserName, request.WhiteLabelLookUp.Password);
                        if (!!string.IsNullOrEmpty(request.WhiteLabelLookUp.FavIconName)) PushWhiteLabelingImagesAsync(request.WhiteLabelLookUp.FavIconPath, request.WhiteLabelLookUp.FavIconName, host, request.WhiteLabelLookUp.UserName, request.WhiteLabelLookUp.Password);

                    }
                    else
                    {
                        ReadCssFile(request.WhiteLabelLookUp);
                    }
                    return new Message()
                    {
                        IsAddUpdate = "WhiteLabelling applying successfully"
                    };
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    Logger.LogError(exception.InnerException.ToString());
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    Logger.LogError(exception.InnerException.ToString());
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    Logger.LogError(exception.InnerException.ToString());
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public async void PushWhiteLabelingImagesAsync(string base64Image, string imageName, string host, string userName, string password)
            {
                //string sourcePath = _hostingEnvironment.ContentRootPath + _configuration.GetSection("Ftp:LocalFolder").Value; ;


                try
                {
                    if (!string.IsNullOrEmpty(base64Image) && !string.IsNullOrEmpty(imageName))
                    {

                        string targetPath = imageName == "logo-mini.svg" ? "/images/" : "/";
                        FtpWebRequest uploadRequest =
                        (FtpWebRequest)WebRequest.Create(host + targetPath + imageName);
                        uploadRequest.Credentials = new NetworkCredential(userName, password);
                        uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                        var byteImage = Convert.FromBase64String(base64Image);
                        //using (Stream fileStream = System.IO.File.OpenRead(path))
                        uploadRequest.ContentLength = byteImage.Length;
                        using (Stream ftpStream = uploadRequest.GetRequestStream())
                        {
                            await ftpStream.WriteAsync(byteImage, 0, byteImage.Length);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }


            public async void ReadCssFile(WhiteLabelLookUp whiteLabelLookUp)
            {
                if (string.IsNullOrEmpty(whiteLabelLookUp.SideMenuColor) && string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnColor)
                    && string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnClickColor) && string.IsNullOrEmpty(whiteLabelLookUp.InvoiceTitleBgColor)
                    && string.IsNullOrEmpty(whiteLabelLookUp.InvoiceTitleColor) && string.IsNullOrEmpty(whiteLabelLookUp.TableHeaderBgColor)
                    && string.IsNullOrEmpty(whiteLabelLookUp.TableHeaderColor) && string.IsNullOrEmpty(whiteLabelLookUp.HeadingColor)
                    && string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnBgColor) && string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnColor)
                    && string.IsNullOrEmpty(whiteLabelLookUp.CancelBgBtnColor) && string.IsNullOrEmpty(whiteLabelLookUp.CancelBtnColor))
                    return;

                string host = "ftp://" + whiteLabelLookUp.Host;
                if (string.IsNullOrEmpty(whiteLabelLookUp.Port))
                    host += ":" + whiteLabelLookUp.Port;
                var rootPath = _hostingEnvironment.WebRootPath;
                var originalFile = rootPath + "\\paper-dashboard.txt";
                var duplicateFile = rootPath + "\\Attachment\\paper-dashboard.css";
                if (File.Exists(originalFile))
                {

                    //duplicateFile = Path.ChangeExtension(duplicateFile, ".css");
                    //File.Copy(originalFile, duplicateFile, true);

                    string[] lines = File.ReadAllLines(originalFile);
                    var menuColor = false;
                    var appColor = false;
                    var menuClickBg = false;
                    var headingColor = false;
                    var buttonColor = false;
                    var cancelColor = false;
                    var theadColor = false;
                    var ivoicePicColor = false;
                    var cardColor = false;
                    var setupMenu = false;
                    var count = 0;
                    var lineEdit = new Dictionary<int, string>();
                    foreach (string line in lines)
                    {

                        //Cancel color
                        if (line.Contains(".setup_menu {") || line.Contains(".setup_icon_wrapper {") || line.Contains(".setup_menu:hover .setup_menu_link .setup_menu_titile {") || line.Contains(".setup_menu:hover .setup_menu_link .setup_menu_desc {") || line.Contains(".setup_menu:hover .setup_icon_wrapper img {"))
                            setupMenu = true;
                        //SideBar color
                        else if (line.Contains(".sidebar .sidebar-wrapper,") || line.Contains(".sidebar:after") || line.Contains(".sidebar .nav li > a,") || line.Contains(".sidebar .sidebar-wrapper > .nav [data-toggle=") || line.Contains(".ImageWidth {") || line.Contains(".router-link-exact-active img, active {") || line.Contains(".setup_menu_titile {") || line.Contains(".setup_menu:hover {") || line.Contains(".setup_menu_desc {") || line.Contains(".setup_icon_wrapper img {"))
                            menuColor = true;
                        //SideBar color
                        else if (line.Contains(".router-link-exact-active, active {") || line.Contains(".router-link-exact-active, active span {") || line.Contains(".router-link-exact-active .sidebar-mini-icon {") || line.Contains(".router-link-exact-active i {"))
                            menuClickBg = true;
                        //SideBar color
                        else if (line.Contains(".invoice_top_bg {") || line.Contains(".bt_bg_color {") || line.Contains(".title_heading {") || line.Contains(".txt_description {"))
                            ivoicePicColor = true;
                        //Heading Color
                        else if (line.Contains(".page_title") || line.Contains(".headingOfVersion {") || line.Contains(".lightParagraph {") || line.Contains(".nobleHeading {") || line.Contains(".view_page_title") || line.Contains(".MainLightHeading {") || line.Contains(".DayHeading {") || line.Contains(".page_title {") || line.Contains(".DayHeading1 {"))
                            headingColor = true;
                        //Primary Button color
                        else if (line.Contains(".btn-primary {") || line.Contains(".btn-outline-primary {"))
                            buttonColor = true;
                        //Cancel color
                        else if (line.Contains(".btn-danger {") || line.Contains(".btn-outline-danger {"))
                            cancelColor = true;
                        //Cancel color
                        else if (line.Contains(".tableHeaderColor {") || line.Contains(".add_table_list_bg thead tr {"))
                            theadColor = true;
                        //Cancel color
                        else if (line.Contains(".main-panel {") || line.Contains(".perfect-scrollbar-on .main-panel {") || line.Contains(".backgroundColorlightBlue {") || line.Contains(".navbar.navbar-transparent {") || line.Contains(".main {") || line.Contains(".el-input__inner {") || (line.Contains(".form-control {") && line.Length < 20) || (line.Contains(".multiselect__tags {")) || (line.Contains(".multiselect__input, .multiselect__single {")) || (line.Contains(".multiselect--disabled .multiselect__current, .multiselect--disabled .multiselect__select {")))
                            appColor = true;
                        //Cancel color
                        else if (line.Contains(".card {") || line.Contains(".loginScreen {") || line.Contains(".pd_payment_methd {") || line.Contains(".setup_sidebar {") || (line.Contains(".form-control:focus {") && line.Length < 30) || (line.Contains(".card label {")))
                            cardColor = true;



                        if (menuColor && line.Contains("background") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuColor))
                        {
                            lineEdit.Add(count, "background: " + whiteLabelLookUp.SideMenuColor + ";");
                            menuColor = false;
                        }
                        else if (menuColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.SideMenuBtnColor + ";");
                            menuColor = false;
                        }
                        else if (menuColor && line.Contains("filter") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnColor) && whiteLabelLookUp.SideMenuBtnColor != "#FFFFFF" && whiteLabelLookUp.SideMenuBtnColor.ToLower() != "white")
                        {
                            if (line.Contains("filter: invert(0%) sepia(0%) saturate(0%) hue-rotate(116deg) brightness(106%) contrast(101%);") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnColorFilter)) lineEdit.Add(count, "filter: " + whiteLabelLookUp.SideMenuBtnColorFilter + ";");
                            if (line.Contains("invert(38%) sepia(93%) saturate(7000%) hue-rotate(206deg) brightness(100%) contrast(96%)") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnClickColorFilter)) lineEdit.Add(count, "filter: " + whiteLabelLookUp.SideMenuBtnClickColorFilter + ";");
                            menuColor = false;
                        }
                        else if (menuClickBg && line.Contains("background") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnClickBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.SideMenuBtnClickBgColor + " !important;");
                            menuClickBg = false;
                        }
                        else if (menuClickBg && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.SideMenuBtnClickColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.SideMenuBtnClickColor + " !important;");
                            menuClickBg = false;
                        }
                        else if (ivoicePicColor && line.Contains("background") && !string.IsNullOrEmpty(whiteLabelLookUp.InvoiceTitleBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.InvoiceTitleBgColor + " !important;");
                            ivoicePicColor = false;
                        }
                        else if (ivoicePicColor && line.Contains("color") && !line.Contains(".bt_bg_color {") && !string.IsNullOrEmpty(whiteLabelLookUp.InvoiceTitleColor))
                        {
                            lineEdit.Add(count, " color: " + whiteLabelLookUp.InvoiceTitleColor + " !important;");
                            ivoicePicColor = false;
                        }
                        else if (headingColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.HeadingColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.HeadingColor + " !important;");
                            headingColor = false;
                        }
                        else if (appColor && line.Contains("background-color") && !string.IsNullOrEmpty(whiteLabelLookUp.ApplicationBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.ApplicationBgColor + " !important;");
                            //appColor = false;
                        }
                        else if (appColor && line.Contains("border:") && !string.IsNullOrEmpty(whiteLabelLookUp.ApplicationBgColor))
                        {
                            lineEdit.Add(count, "border: 1px solid " + whiteLabelLookUp.ApplicationBgColor + " !important;");
                            //appColor = false;
                        }
                        else if (appColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.ApplicationTextColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.ApplicationTextColor + " !important;");
                            appColor = false;
                        }
                        else if (cardColor && line.Contains("background-color") && !string.IsNullOrEmpty(whiteLabelLookUp.CardBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.CardBgColor + " !important;");
                            if (!lines[count + 1].Contains("color"))
                                cardColor = false;
                        }
                        else if (cardColor && line.Contains("color") && !string.IsNullOrEmpty(whiteLabelLookUp.CardTextColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.CardTextColor + " !important;");
                            cardColor = false;
                        }
                        else if (setupMenu && line.Contains("color: #ffffff;") && !string.IsNullOrEmpty(whiteLabelLookUp.SetupTextColor))
                        {
                            lineEdit.Add(count, "color: " + whiteLabelLookUp.SetupTextColor + ";");
                            setupMenu = false;
                        }
                        else if (setupMenu && line.Contains("background-color") && !string.IsNullOrEmpty(whiteLabelLookUp.SetupBgColor))
                        {
                            lineEdit.Add(count, "background-color: " + whiteLabelLookUp.SetupBgColor + ";");
                            setupMenu = false;
                        }
                        else if (setupMenu && line.Contains("filter") && !string.IsNullOrEmpty(whiteLabelLookUp.SetupCssFilter))
                        {
                            var index = whiteLabelLookUp.SetupCssFilter.IndexOf("brightness");
                            var middleIndex = index + 16;
                            var data = whiteLabelLookUp.SetupCssFilter.Substring(0, index) + "brightness(0%) " + whiteLabelLookUp.SetupCssFilter.Substring(middleIndex, whiteLabelLookUp.SetupCssFilter.Length - middleIndex);
                            lineEdit.Add(count, "filter: " + whiteLabelLookUp.SetupCssFilter + ";");
                            setupMenu = false;
                        }
                        else if (theadColor && (line.Contains("background-color: #3178F6 !important;") || line.Contains("background-color: #3178f6;")))
                        {
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.TableHeaderBgColor)) lineEdit.Add(count, "background-color: " + whiteLabelLookUp.TableHeaderBgColor + " !important;");
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.TableHeaderColor)) lineEdit.Add(++count, "color: " + whiteLabelLookUp.TableHeaderColor + " !important;");
                            theadColor = false;
                            continue;
                        }
                        else if (buttonColor)
                        {
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnBgColor) && (line.Contains("background-color: #3178F6;") || line.Contains("background-color: #5491ff !important;") || line.Contains("background-color: #3178F6 !important;"))) lineEdit.Add(count, "background-color: " + whiteLabelLookUp.SaveBtnBgColor + " !important;");
                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnBgColor) && (line.Contains("border: 1px solid #3178F6;") || line.Contains("border: 1px solid #5491ff !important;") || line.Contains("border: 1px solid #3178F6 !important;"))) lineEdit.Add(count, "border: 1px solid " + whiteLabelLookUp.SaveBtnBgColor + " !important;");
                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnColor) && (line.Contains("color: #3178F6;") || line.Contains("color: #FFFFFF;") || line.Contains("color: #6bd098 !important;"))) lineEdit.Add(count, "color: " + whiteLabelLookUp.SaveBtnColor + " !important;");

                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.SaveBtnBgColor) && (line.Contains("border-color: #3178f6;") || line.Contains("border-color: #3178F6 !important;")))
                            {
                                lineEdit.Add(count, "border-color: " + whiteLabelLookUp.TableHeaderColor + ";");
                                buttonColor = false;
                            }

                        }
                        else if (cancelColor)
                        {
                            if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBgBtnColor) && (line.Contains("background-color: #EB5757;") || line.Contains("background-color: #e77676 !important;") || line.Contains("background-color: #e77676;") || line.Contains("background-color: #EB5757 !important;"))) lineEdit.Add(count, "background-color: " + whiteLabelLookUp.CancelBgBtnColor + " !important;");

                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBgBtnColor) && (line.Contains("border: 1px solid #EB5757;") || line.Contains(" border: 1px solid #e77676 !important;") || line.Contains("border: 1px solid #EB5757 !important;"))) lineEdit.Add(count, " border: 1px solid  " + whiteLabelLookUp.CancelBgBtnColor + " !important;");
                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBtnColor) && (line.Contains("color: #EB5757;") || line.Contains("color: #FFFFFF;"))) lineEdit.Add(count, "color: " + whiteLabelLookUp.CancelBtnColor + ";");

                            else if (!string.IsNullOrEmpty(whiteLabelLookUp.CancelBgBtnColor) && (line.Contains(" border-color: #ef8157;") || line.Contains("border-color: #ef8157 !important;")))
                            {
                                lineEdit.Add(count, "border-color: " + whiteLabelLookUp.CancelBgBtnColor + ";");
                                cancelColor = false;
                            }

                        }

                        count++;
                    }
                    foreach (var line in lineEdit)
                    {
                        lines[line.Key] = line.Value;
                    }
                    File.WriteAllLines(duplicateFile, lines);
                    var bytes = File.ReadAllBytes(duplicateFile);
                    FtpWebRequest uploadRequest =
                           (FtpWebRequest)WebRequest.Create(host + "/assets/css/paper-dashboard.css");
                    uploadRequest.Credentials = new NetworkCredential(whiteLabelLookUp.UserName, whiteLabelLookUp.Password);
                    uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    uploadRequest.ContentLength = bytes.Length;
                    using (Stream ftpStream = uploadRequest.GetRequestStream())
                    {
                        await ftpStream.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}
