using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.RolePermission.Queries.GetRolesPermissionsList;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Domain.Enum;
using Focus.Domain.Enums;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.SyncRecords.Commands;
using DocumentFormat.OpenXml.Bibliography;

namespace Focus.Business.RolePermission.Commands.AddUpdateRolesPermissions
{
    public class CreateDefaultRolesOnLocation : IRequest<Message>
    {
        public Guid CompanyId { get; set; }



        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<CreateDefaultRolesOnLocation, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<CreateDefaultRolesOnLocation> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;

            }

            public async Task<Message> Handle(CreateDefaultRolesOnLocation request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    {
                        var account = Context.Accounts.AsNoTracking().Include(x => x.CostCenter)
                            .AsQueryable();
                        var bankAccount =await account.FirstOrDefaultAsync(x => x.Code == "10500001", cancellationToken: cancellationToken);
                        var cashAccount =await account.OrderBy(x => x.CostCenter.Code == "101000").LastOrDefaultAsync();
                        if (bankAccount == null)
                        {
                            throw new NotFoundException("Terminal Bank","Bank Account Not found");
                        }

                        var accountList = new List<Account>()
                        {
                            new Account()
                            {
                                IsActive = true,
                                Name = "Counter Cash 01",
                                Code = "CC-00001",
                                CostCenterId = cashAccount.CostCenterId,
                            },

                            new Account()
                            {
                                IsActive = true,
                                Name = "Counter Cash 02",
                                Code = "CC-00002",
                                CostCenterId = cashAccount.CostCenterId,
                            }
                        };

                        await Context.Accounts.AddRangeAsync(accountList, cancellationToken);

                        var terminal = new Terminal
                        {
                            Code = "TR-00001",
                            IsActive = true,
                            TerminalType = TerminalType.Terminal,
                            TerminalUserType= TerminalUserType.Offline

                        };
                        
                        var cashCounter = new Terminal
                        {
                            AccountId = bankAccount.Id,
                            CashAccountId = accountList[0].Id,
                            Code = "CC-00001",
                            IsActive = true,
                            TerminalType = TerminalType.CashCounter,
                            TerminalUserType = TerminalUserType.Offline


                        };
                        //var cashCounter1 = new Terminal
                        //{
                        //    AccountId = bankAccount.Id,
                        //    CashAccountId = accountList[1].Id,

                        //    Code = "CC-00002",
                        //    IsActive = false,
                        //    TerminalType = TerminalType.CashCounter

                        //};
                        await Context.Terminals.AddRangeAsync(terminal, cashCounter);
                        
                        var printSetting = new PrintSetting
                        {
                            
                            PrintSize = Print.A4,
                            BankAccountId = bankAccount.Id,
                           
                        };

                        await Context.PrintSettings.AddAsync(printSetting, cancellationToken);
                        

                        var warehouses = new List<Warehouse>();
                        warehouses.Add(new Warehouse()
                        {
                            StoreID = "S001",
                            Name = "Default (Show Room)",
                            NameArabic = "الافتراضي (صالة العرض)",
                            isActive = true,

                        });
                        warehouses.Add(new Warehouse()
                        {
                            StoreID = "S002",
                            Name = "Store 1",
                            NameArabic = "المتجر 1",
                            isActive = true,

                        });
                        await Context.Warehouses.AddRangeAsync(warehouses, cancellationToken);
                    }
                    var company = Context.Companies.FirstOrDefault(x=>x.Id==request.CompanyId);
                    company.IsProceed = true;


                    var companyPermissions = Context.CompanyPermissions.ToList();
                    var companyModule = companyPermissions.GroupBy(x=>x.NobleModuleId).ToList();
                    //Sales Man
                    var slaesMan = new NobleRoles()
                    {
                        Name = "Sales Man",
                        NormalizedName = "SALES MAN",
                        IsActive = true
                    };
                    await Context.NobleRoles.AddAsync(slaesMan, cancellationToken);



                    foreach (var item in companyPermissions)
                    {
                        var permission = new NobleRolePermission();
                        if (item.NobleModules.ModuleName == "Sales" || item.NobleModules.ModuleName == "DayStart" || item.NobleModules.ModuleName == "Expenses" || item.NobleModules.ModuleName == "Other")
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = true,
                                RoleId = slaesMan.Id,
                                PermissionId = item.Id
                            };
                        }
                        else
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = false,
                                RoleId = slaesMan.Id,
                                PermissionId = item.Id
                            };
                        }
                        await Context.NobleRolePermissions.AddAsync(permission, cancellationToken);


                    }

                    //Purchases
                    var purchases = new NobleRoles()
                    {
                        Name = "Purchases",
                        NormalizedName = "PURCHASES",
                        IsActive = true
                    };
                    await Context.NobleRoles.AddAsync(purchases, cancellationToken);


                    foreach (var item in companyPermissions)
                    {
                        var permission = new NobleRolePermission();
                        if (item.NobleModules.ModuleName == "Purchase" || item.NobleModules.ModuleName == "WareHouse Management" || item.NobleModules.ModuleName == "Other")
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = true,
                                RoleId = purchases.Id,
                                PermissionId = item.Id
                            };
                        }
                        else
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = false,
                                RoleId = purchases.Id,
                                PermissionId = item.Id
                            };
                        }
                        await Context.NobleRolePermissions.AddAsync(permission, cancellationToken);


                    }



                    //Accountant
                    var accountant = new NobleRoles()
                    {
                        Name = "Accountant",
                        NormalizedName = "ACCOUNTANT",
                        IsActive = true
                    };
                    await Context.NobleRoles.AddAsync(accountant, cancellationToken);


                    foreach (var item in companyPermissions)
                    {
                        var permission = new NobleRolePermission();
                        if (item.NobleModules.ModuleName == "Accounting" || item.NobleModules.ModuleName == "Expenses" || item.NobleModules.ModuleName == "Reporting" || item.NobleModules.ModuleName == "Other")
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = true,
                                RoleId = accountant.Id,
                                PermissionId = item.Id
                            };
                        }
                        else
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = false,
                                RoleId = accountant.Id,
                                PermissionId = item.Id
                            };
                        }
                        await Context.NobleRolePermissions.AddAsync(permission, cancellationToken);


                    }


                    //System User
                    var systemUser = new NobleRoles()
                    {
                        Name = "System User",
                        NormalizedName = "SYSTEM USER",
                        IsActive = true
                    };
                    await Context.NobleRoles.AddAsync(systemUser, cancellationToken);


                    foreach (var item in companyPermissions)
                    {
                        var permission = new NobleRolePermission();
                        if (item.NobleModules.ModuleName == "Product And Inventory Management" || item.NobleModules.ModuleName == "Setups And Configuration"|| item.NobleModules.ModuleName == "Settings And Permission" || item.NobleModules.ModuleName == "Other")
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = true,
                                RoleId = systemUser.Id,
                                PermissionId = item.Id
                            };
                        }
                        else
                        {


                            permission = new NobleRolePermission()
                            {

                                IsActive = false,
                                RoleId = systemUser.Id,
                                PermissionId = item.Id
                            };
                        }
                        await Context.NobleRolePermissions.AddAsync(permission, cancellationToken);


                    }


                    // Default Bank In company Setup
                    //Master Card
                    var paymentOptions = new List<PaymentOption>();
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "MasterCard",
                        NameArabic = "بطاقة ماستر بطاقة ائتمان",
                        IsActive = true,
                        Image = "/PaymentOptions/MasterCard.png"
                    });
                    
                    //Visa
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "Visa",
                        NameArabic = "تأشيرة دخول",
                        IsActive = true,
                        Image = "/PaymentOptions/Visa.png"
                    });
                    
                    //Mada
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "Mada",
                        NameArabic = "مدى",
                        IsActive = true,
                        Image = "/PaymentOptions/Mada.png"
                    });
                    
                    //Master Card
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "AmericanExpress",
                        NameArabic = "أمريكان اكسبريس",
                        IsActive = false,
                        Image = "/PaymentOptions/AmericanExpress.png"
                    });
                    //Master Card
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "ApplePay",
                        NameArabic = "أجر أبل",
                        IsActive = false,
                        Image = "/PaymentOptions/ApplePay.png"
                    });
                    
                    //Discover
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "Discover",
                        NameArabic = "يكتشف",
                        IsActive = false,
                        Image = "/PaymentOptions/Discover.png"
                    });
                    
                    //Paypal
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "PayPal",
                        NameArabic = "باي بال",
                        IsActive = false,
                        Image = "/PaymentOptions/PayPal.png"
                    });
                    
                    //Stc Pay
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "StcPay",
                        NameArabic = "دفعStc",
                        IsActive = false,
                        Image = "/PaymentOptions/StcPay.png"
                    });
                    
                    //Western Union
                    paymentOptions.Add(new PaymentOption
                    {
                        Name = "WesternUnion",
                        NameArabic = "الاتحاد الغربي",
                        IsActive = false,
                        Image = "/PaymentOptions/WesternUnion.png"
                    });

                    //Add Payment Options to database
                    await Context.PaymentOptions.AddRangeAsync(paymentOptions, cancellationToken);

                    // Denomination Setup
                    var denominationSetup = new List<DenominationSetup>();
                    denominationSetup.Add(new DenominationSetup
                    {
                        Number = 1,
                        isActive = true
                    });
                    denominationSetup.Add(new DenominationSetup
                    {
                        Number = 5,
                        isActive = true
                    });
                    denominationSetup.Add(new DenominationSetup
                    {
                        Number = 10,
                        isActive = true
                    });
                    denominationSetup.Add(new DenominationSetup
                    {
                        Number = 50,
                        isActive = true
                    });
                    denominationSetup.Add(new DenominationSetup
                    {
                        Number = 100,
                        isActive = true
                    });
                    denominationSetup.Add(new DenominationSetup
                    {
                        Number = 500,
                        isActive = true
                    });

                    await Context.DenominationSetups.AddRangeAsync(denominationSetup, cancellationToken);
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);
                    await Context.SaveChangesAsync(cancellationToken);
                    return new Message()
                    {
                        IsAddUpdate = "Data Updated Successfully"
                    };


                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Module Name Already Exist");
                }

            }
        }
    }
}


