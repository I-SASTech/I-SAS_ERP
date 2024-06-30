using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.NoblePermission.Commands.AddUpdateNoblePermission;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Permission.Commands.AddUpdateNoblePermission
{
    public class AddUpdateNoblePermissionCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public GetAllPermissionModuleAndGroup PermissionModuleAndGroup { get; set; }

        public string UserId { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateNoblePermissionCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IHostingEnvironment _hostingEnvironment;
            public IConfiguration Configuration { get; }
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateNoblePermissionCommand> logger,
                UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment, IConfiguration configuration, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _userManager = userManager;
                _hostingEnvironment = hostingEnvironment;
                Configuration = configuration;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateNoblePermissionCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var whiteLabeling = await Context.WhiteLabelings.FirstOrDefaultAsync(cancellationToken);
                    if (whiteLabeling == null)
                    {
                        var whiteLabel = new WhiteLabeling()
                        {
                            Heading = request.PermissionModuleAndGroup.WhiteLabelLookUp.Heading,
                            Description = request.PermissionModuleAndGroup.WhiteLabelLookUp.Description,
                            CompanyName = request.PermissionModuleAndGroup.WhiteLabelLookUp.CompanyName,
                            ApplicationName = request.PermissionModuleAndGroup.WhiteLabelLookUp.ApplicationName,
                            AddressLine1 = request.PermissionModuleAndGroup.WhiteLabelLookUp.AddressLine1,
                            AddressLine2 = request.PermissionModuleAndGroup.WhiteLabelLookUp.AddressLine2,
                            AddressLine3 = request.PermissionModuleAndGroup.WhiteLabelLookUp.AddressLine3,
                            Email = request.PermissionModuleAndGroup.WhiteLabelLookUp.Email,
                            FavName = request.PermissionModuleAndGroup.WhiteLabelLookUp.FavName,

                            SideMenuColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SideMenuColor,
                            SideMenuBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SideMenuBtnColor,
                            SideMenuBtnClickColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SideMenuBtnClickColor,
                            SaveBtnBgColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SaveBtnBgColor,
                            SaveBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SaveBtnColor,
                            CancelBgBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.CancelBgBtnColor,
                            CancelBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.CancelBtnColor,
                            HeadingColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.HeadingColor,
                            TableHeaderBgColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.TableHeaderBgColor,
                            TableHeaderColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.TableHeaderColor,
                            InvoiceTitleBgColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.InvoiceTitleBgColor,
                            InvoiceTitleColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.InvoiceTitleColor,
                        };
                        await Context.WhiteLabelings.AddAsync(whiteLabel, cancellationToken);
                    }
                    else
                    {
                        whiteLabeling.Heading = request.PermissionModuleAndGroup.WhiteLabelLookUp.Heading;
                        whiteLabeling.Description = request.PermissionModuleAndGroup.WhiteLabelLookUp.Description;
                        whiteLabeling.CompanyName = request.PermissionModuleAndGroup.WhiteLabelLookUp.CompanyName;
                        whiteLabeling.ApplicationName = request.PermissionModuleAndGroup.WhiteLabelLookUp.ApplicationName;
                        whiteLabeling.AddressLine1 = request.PermissionModuleAndGroup.WhiteLabelLookUp.AddressLine1;
                        whiteLabeling.AddressLine2 = request.PermissionModuleAndGroup.WhiteLabelLookUp.AddressLine2;
                        whiteLabeling.AddressLine3 = request.PermissionModuleAndGroup.WhiteLabelLookUp.AddressLine3;
                        whiteLabeling.Email = request.PermissionModuleAndGroup.WhiteLabelLookUp.Email;
                        whiteLabeling.FavName = request.PermissionModuleAndGroup.WhiteLabelLookUp.FavName;

                        whiteLabeling.SideMenuColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SideMenuColor;
                        whiteLabeling.SideMenuBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SideMenuBtnColor;
                        whiteLabeling.SideMenuBtnClickColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SideMenuBtnClickColor;
                        whiteLabeling.SaveBtnBgColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SaveBtnBgColor;
                        whiteLabeling.SaveBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.SaveBtnColor;
                        whiteLabeling.CancelBgBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.CancelBgBtnColor;
                        whiteLabeling.CancelBtnColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.CancelBtnColor;
                        whiteLabeling.HeadingColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.HeadingColor;
                        whiteLabeling.TableHeaderBgColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.TableHeaderBgColor;
                        whiteLabeling.TableHeaderColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.TableHeaderColor;
                        whiteLabeling.InvoiceTitleColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.InvoiceTitleColor;
                        whiteLabeling.InvoiceTitleBgColor = request.PermissionModuleAndGroup.WhiteLabelLookUp.InvoiceTitleBgColor;


                    }

                    var isAccountExist = Context.Accounts.Count();
                    if (isAccountExist == 0)
                    {
                        foreach (var item in request.PermissionModuleAndGroup.TemplateLists.AccountsType)
                        {
                            var accountType = new AccountType
                            {
                                Name = item.Name,
                                NameArabic = item.NameArabic,
                                IsActive = true
                            };

                            Context.AccountTypes.Add(accountType);
                            //var checkAccountType = accountTypes.FirstOrDefault(x => x.Name == item.Name);
                            //if (checkAccountType == null)
                            //    throw new ApplicationException("Invalid template. Please check your Account Types");

                            foreach (var costCenter in item.CostCenters)
                            {
                                var cost = new CostCenter
                                {
                                    Name = costCenter.Name,
                                    NameArabic = costCenter.NameArabic,
                                    Description = costCenter.Description,
                                    Code = costCenter.Code,
                                    IsActive = true,
                                    AccountTypeId = accountType.Id
                                };

                                await Context.CostCenters.AddAsync(cost, cancellationToken);

                                foreach (var account in costCenter.Accounts)
                                {
                                    var ac = new Account
                                    {
                                        Name = account.Name,
                                        NameArabic = account.NameArabic,
                                        Description = account.Description,
                                        Code = account.Code,
                                        IsActive = true,
                                        CostCenterId = cost.Id
                                    };
                                    await Context.Accounts.AddAsync(ac, cancellationToken);
                                }
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                    }

                    var groupData = Context.NobleGroups.FirstOrDefault(x =>
                    x.GroupName == request.PermissionModuleAndGroup.GroupLookUp.GroupName && x.GroupType == (GroupType)Enum.Parse(typeof(GroupType), request.PermissionModuleAndGroup.GroupLookUp.GroupType));
                    //var isExistGroupData = Context.CompanyPermissions
                    //    .Where(x => x.NobleGroupId == request.PermissionModuleAndGroup.GroupLookUp.Id).ToList();
                    if (groupData != null)
                    {
                        //Add Permission
                        var listOfRolePermission = new List<NobleRolePermission>();

                        // Query to Remove All the permission from Db which no longer
                        var noblePermissionList = Context.CompanyPermissions.Where(x => x.NobleGroupId == groupData.Id).ToList();

                        foreach (var permission in noblePermissionList)
                        {
                            if (request.PermissionModuleAndGroup.PermissionsLookUps.Count(x =>
                                x.Key == permission.Key) <= 0)
                            {
                                var nobleRolePermission = Context.NobleRolePermissions.Where(x => x.PermissionId == permission.Id);
                                Context.NobleRolePermissions.RemoveRange(nobleRolePermission);
                                Context.CompanyPermissions.Remove(permission);

                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        //End Query
                        //Query to add new Permission
                        //Get List of Noble Module
                        var listOfNobleModule = Context.NobleModules.ToList();
                        noblePermissionList.Clear();
                        //x.Name == "Admin" &&
                        noblePermissionList = Context.CompanyPermissions.Where(x => x.NobleGroupId == groupData.Id).ToList();
                        var nobleRoleList = Context.NobleRoles.ToList();
                        foreach (var permission in request.PermissionModuleAndGroup.PermissionsLookUps)
                        {

                            if (noblePermissionList.Count(x => x.Key == permission.Key) <= 0)
                            {
                                if (permission.ModuleId == null)
                                {
                                    Console.WriteLine("Issue");
                                }
                                var moduleId =
                                    listOfNobleModule.FirstOrDefault(x => x.Description == permission.ModuleId.ToString()).Id;
                                var companyPermission = new CompanyPermission()
                                {
                                    //Id = permission.Id,
                                    PermissionName = permission.PermissionName,
                                    Key = permission.Key,
                                    Value = permission.Value,
                                    NobleGroupId = groupData.Id,
                                    NobleModuleId = moduleId
                                };
                                Context.CompanyPermissions.Add(companyPermission);
                                noblePermissionList.Add(companyPermission);
                                if (nobleRoleList.Count > 0)
                                {
                                    foreach (var nobleRole in nobleRoleList)
                                    {
                                        if (nobleRole.Name == "Admin")
                                        {
                                            listOfRolePermission.Add(new NobleRolePermission()
                                            {
                                                RoleId = nobleRole.Id,
                                                PermissionId = companyPermission.Id,
                                                IsActive = true
                                            });

                                        }
                                        else
                                        {
                                            if (permission.ModuleId.ToString() == "3a0a2ddf-0773-4746-913d-0ef1397cc14f" || permission.Key == "3d1f65f1-3f72-4898-a175-1b6ab42b2b9d" || permission.Key == "7dc50e60-d5a2-419a-b12a-200ac71d7cb6")
                                            {
                                                listOfRolePermission.Add(new NobleRolePermission()
                                                {
                                                    RoleId = nobleRole.Id,
                                                    PermissionId = companyPermission.Id,
                                                    IsActive = true
                                                });
                                            }
                                            else
                                            {
                                                listOfRolePermission.Add(new NobleRolePermission()
                                                {
                                                    RoleId = nobleRole.Id,
                                                    PermissionId = companyPermission.Id,
                                                    IsActive = false
                                                });
                                            }

                                        }
                                    }
                                }
                            }
                            else if (permission.Key == "3d1f65f1-3f72-4898-a175-1b6ab42b2b9d" || permission.Key == "7dc50e60-d5a2-419a-b12a-200ac71d7cb6")
                            {
                                var d = noblePermissionList.FirstOrDefault(x =>
                                    x.Key == permission.Key);
                                if (d != null) d.Value = permission.Value;

                            }
                        }
                        Context.NobleRolePermissions.AddRange(listOfRolePermission);

                        //Delete Previous License
                        if (request.PermissionModuleAndGroup.CompanyLicenseLookUps.Count > 0)
                        {
                            Context.CompanyLicences.RemoveRange(Context.CompanyLicences.Where(x => x.CompanyId == request.PermissionModuleAndGroup.CompanyLicenseLookUps.ElementAt(0).CompanyId));
                            var companyLicenseList = new List<CompanyLicence>();
                            foreach (var companyLicenseLookUp in request.PermissionModuleAndGroup.CompanyLicenseLookUps)
                            {
                                companyLicenseList.Add(new CompanyLicence()
                                {
                                    FromDate = companyLicenseLookUp.FromDate,
                                    ToDate = companyLicenseLookUp.ToDate,
                                    IsBlock = companyLicenseLookUp.IsBlock,
                                    IsActive = companyLicenseLookUp.IsActive,
                                    CompanyId = companyLicenseLookUp.CompanyId,
                                    LicenseType = companyLicenseLookUp.LicenseType,
                                    IsTechnicalSupport = companyLicenseLookUp.IsTechnicalSupport,
                                    IsUpdateTechnicalSupport = companyLicenseLookUp.IsUpdateTechnicalSupport,
                                    TechnicalSupportPeriod = companyLicenseLookUp.TechnicalSupportPeriod,
                                    PaymentFrequency = companyLicenseLookUp.PaymentFrequency,
                                    GracePeriod = companyLicenseLookUp.GracePeriod,
                                    ActivationPlatform = String.IsNullOrEmpty(companyLicenseLookUp.ActivationPlatform) ? 0 : (ActivationPlatform)Enum.Parse(typeof(ActivationPlatform), companyLicenseLookUp.ActivationPlatform)
                                });
                            }
                            Context.CompanyLicences.AddRange(companyLicenseList);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                    }
                    else
                    {
                        // Add Noble group 
                        Context.NobleRolePermissions.RemoveRange(Context.NobleRolePermissions);
                        Context.CompanyPermissions.RemoveRange(Context.CompanyPermissions);
                        Context.NobleGroups.RemoveRange(Context.NobleGroups);
                        Context.NobleModules.RemoveRange(Context.NobleModules);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        var group = new NobleGroup()
                        {
                            GroupName = request.PermissionModuleAndGroup.GroupLookUp.GroupName,
                            GroupType = (GroupType)Enum.Parse(typeof(GroupType), request.PermissionModuleAndGroup.GroupLookUp.GroupType)
                        };
                        Context.NobleGroups.Add(group);


                        //Add Modules if not exist in database
                        var listOfNobleModule = new List<NobleModule>();
                        foreach (var module in request.PermissionModuleAndGroup.ModuleLookUps)
                        {
                            listOfNobleModule.Add(new NobleModule()
                            {
                                //Id = module.Id,
                                ModuleName = module.Value,
                                Description = module.Id.ToString()
                            });
                        }
                        Context.NobleModules.AddRange(listOfNobleModule);

                        //Add Permission
                        var listOfPermission = new List<CompanyPermission>();
                        var listOfRolePermission = new List<NobleRolePermission>();
                        foreach (var permission in request.PermissionModuleAndGroup.PermissionsLookUps)
                        {
                            var moduleId =
                                listOfNobleModule.FirstOrDefault(x => x.Description == permission.ModuleId.ToString()).Id;
                            listOfPermission.Add(new CompanyPermission()
                            {
                                //Id = permission.Id,
                                PermissionName = permission.PermissionName,
                                Key = permission.Key,
                                Value = permission.Value,
                                NobleGroupId = group.Id,
                                NobleModuleId = moduleId
                            });


                        }
                        //x.Name == "Admin" 
                        Context.CompanyPermissions.AddRange(listOfPermission);
                        var nobleRoleList = Context.NobleRoles.ToList();
                        var nobleUserRole = new NobleUserRoles();
                        if (nobleRoleList.Count > 0)
                        {
                            foreach (var roleList in nobleRoleList)
                            {
                                if (roleList.Name == "Admin")
                                {
                                    foreach (var permission in listOfPermission)
                                    {
                                        listOfRolePermission.Add(new NobleRolePermission()
                                        {
                                            RoleId = roleList.Id,
                                            PermissionId = permission.Id,
                                            IsActive = true
                                        });


                                    }
                                    var isUserRoleExist = Context.NobleUserRoles.FirstOrDefault(x => x.UserId == request.UserId);

                                    if (isUserRoleExist == null)
                                    {
                                        nobleUserRole.RoleId = roleList.Id;
                                        nobleUserRole.UserId = request.UserId;
                                        nobleUserRole.isActive = true;
                                        Context.NobleUserRoles.Add(nobleUserRole);
                                    }

                                }
                                else
                                {
                                    foreach (var permission in listOfPermission)
                                    {
                                        if (permission.NobleModules.Description == "3a0a2ddf-0773-4746-913d-0ef1397cc14f" || permission.Key == "3d1f65f1-3f72-4898-a175-1b6ab42b2b9d" || permission.Key == "7dc50e60-d5a2-419a-b12a-200ac71d7cb6")
                                        {
                                            listOfRolePermission.Add(new NobleRolePermission()
                                            {
                                                RoleId = roleList.Id,
                                                PermissionId = permission.Id,
                                                IsActive = true
                                            });
                                        }
                                        else
                                        {
                                            listOfRolePermission.Add(new NobleRolePermission()
                                            {
                                                RoleId = roleList.Id,
                                                PermissionId = permission.Id,
                                                IsActive = false
                                            });
                                        }



                                    }
                                }
                            }

                        }
                        Context.NobleRolePermissions.AddRange(listOfRolePermission);
                        //Noble Role

                        //Delete Previous License
                        if (request.PermissionModuleAndGroup.CompanyLicenseLookUps.Count > 0)
                        {
                            Context.CompanyLicences.RemoveRange(Context.CompanyLicences.Where(x => x.CompanyId == request.PermissionModuleAndGroup.CompanyLicenseLookUps.ElementAt(0).CompanyId));
                            var companyLicenseList = new List<CompanyLicence>();
                            foreach (var companyLicenseLookUp in request.PermissionModuleAndGroup.CompanyLicenseLookUps)
                            {
                                companyLicenseList.Add(new CompanyLicence()
                                {
                                    FromDate = companyLicenseLookUp.FromDate,
                                    ToDate = companyLicenseLookUp.ToDate,
                                    IsBlock = companyLicenseLookUp.IsBlock,
                                    IsActive = companyLicenseLookUp.IsActive,
                                    CompanyId = companyLicenseLookUp.CompanyId,
                                    LicenseType = companyLicenseLookUp.LicenseType,
                                    IsTechnicalSupport = companyLicenseLookUp.IsTechnicalSupport,
                                    IsUpdateTechnicalSupport = companyLicenseLookUp.IsUpdateTechnicalSupport,
                                    TechnicalSupportPeriod = companyLicenseLookUp.TechnicalSupportPeriod,
                                    PaymentFrequency = companyLicenseLookUp.PaymentFrequency,
                                    GracePeriod = companyLicenseLookUp.GracePeriod,
                                    ActivationPlatform = String.IsNullOrEmpty(companyLicenseLookUp.ActivationPlatform) ? 0 : (ActivationPlatform)Enum.Parse(typeof(ActivationPlatform), companyLicenseLookUp.ActivationPlatform)
                                });
                            }
                            Context.CompanyLicences.AddRange(companyLicenseList);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                    }
                    return request.PermissionModuleAndGroup.GroupLookUp.Id;
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

            public async void PushWhiteLabelingImagesAsync(string base64Image, string imageName)
            {
                //string sourcePath = _hostingEnvironment.ContentRootPath + _configuration.GetSection("Ftp:LocalFolder").Value; ;


                try
                {
                    if (!string.IsNullOrEmpty(base64Image) && !string.IsNullOrEmpty(imageName))
                    {
                        string host = "ftp://localhost:21";

                        string targetPath = imageName == "logo-mini.svg" ? "/images/" : "/";
                        FtpWebRequest uploadRequest =
                        (FtpWebRequest)WebRequest.Create(host + targetPath + imageName);
                        uploadRequest.Credentials = new NetworkCredential("", "");
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

                string host = "ftp://localhost:21";
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
                    uploadRequest.Credentials = new NetworkCredential("", "");
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
