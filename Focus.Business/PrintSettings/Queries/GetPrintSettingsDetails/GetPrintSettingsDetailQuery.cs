using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.PrintOptions.Commands.AddPrintOption;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PrintSettings.Queries.GetPrintSettingsDetails
{
    public class GetPrintSettingDetailQuery : IRequest<PrintSettingDetailLookUpModel>
    {
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetPrintSettingDetailQuery, PrintSettingDetailLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IHostingEnvironment _hostingEnvironment;
            public readonly UserManager<ApplicationUser> UserManager;
            private readonly IUserHttpContextProvider _contextProvider;

            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<GetPrintSettingDetailQuery> logger, IHostingEnvironment hostingEnvironment,
                IMediator mediator, UserManager<ApplicationUser> userManager
                    ,IUserHttpContextProvider contextProvider)
            {
                UserManager = userManager;
                _context = context;
                _logger = logger;
                _hostingEnvironment = hostingEnvironment;
                _mediator = mediator;
                _contextProvider = contextProvider;


            }
            public async Task<PrintSettingDetailLookUpModel> Handle(GetPrintSettingDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var userId = _contextProvider.GetUserId();

                
                    var count = await _context.PrintOptions.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
                    if (count == 0)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        Guid? printSettingId = (request.BranchId == null || request.BranchId == Guid.Empty) ? await _context.PrintSettings.Select(x => x.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken) :
                            await _context.PrintSettings.Where(x => x.BranchId == request.BranchId).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        {

                            if (printSettingId == Guid.Empty)
                            {
                                printSettingId = null;
                            }
                            var pritntOptions = new List<PrintOption>()
                            {
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Invoice Type (Ar)",
                                    LabelNameArabic = "نوع الفاتورة (عربي)",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Invoice Type (Eng)",
                                    LabelNameArabic = "نوع الفاتورة (انجليزي)",
                                    Value=true,
                                    Type = "Header"
                                },

                                new PrintOption
                                {
                                    Label="Print Logo",
                                    LabelNameArabic = "طـبـاعـة الشعـار",
                                    PrintSettingId = printSettingId,
                                    Value=true,
                                    Type = "Header"


                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Business Name (Ar)",
                                    LabelNameArabic = "نـوع الـتـجـارة (عربي)",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Business Name (Eng)",
                                    LabelNameArabic = "الاسم التجاري (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Business Type (Ar)",
                                    LabelNameArabic = "نوع العمل (عربي)",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Business Type (Eng)",
                                    LabelNameArabic = "نـوع الـتـجـارة (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Management No (Ar)",
                                    LabelNameArabic = "رقم الإدارة (عربي)",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Management No (Eng)",
                                    LabelNameArabic = "رقم الإدارة (الإنجليزية)",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Store Contact No (Ar)",
                                    LabelNameArabic = "الـرقم الإدارة / المحل (عربي) ",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Store Contact No (Eng)",
                                    LabelNameArabic = "الـرقم الإدارة  (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Header"
                                },

                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Welcome/Tagline (Ar)",
                                    LabelNameArabic = "الـرقم الإدارة (عربي) ",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Welcome/Tagline (Eng)",
                                    LabelNameArabic = "رسالة أهلا بك / سطر الوصف(إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Header"
                                },

                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="VAT / Tax ID (Ar)",
                                    LabelNameArabic = "الرقـم الـضـريبـى (عربي)",
                                    Value=true,
                                    Type = "LineItem"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="VAT / Tax ID (Eng)",
                                    LabelNameArabic = "الرقـم الـضـريبـى (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "LineItem"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Company Name (Ar)",
                                    LabelNameArabic = "الاسم الـشـركـة (عربي)",
                                    Value=true,
                                    Type = "LineItem"

                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Company Name (Eng)",
                                    LabelNameArabic = "الاسم الـشـركـة (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "LineItem"

                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Company Address (Ar)",
                                    LabelNameArabic = "الـعـنـوان الـشـركـة / المحل (عربي) ",
                                    Value=true,
                                    Type = "Header"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Company Address (Eng)",
                                    LabelNameArabic = "الـعـنـوان الـشـركـة / المحل (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Header"
                                },

                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Invoice Number(Ar)",
                                    LabelNameArabic="رقم الفاتورة (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Invoice Number(Eng)",
                                    LabelNameArabic = "رقم الفاتورة (عـربـي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Print Barcode",
                                    LabelNameArabic = "طـبـاعـة الـبـاركـود",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="User Name (Eng)",
                                    LabelNameArabic = "الاسم االمستخدم (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="User Name (Ar)",
                                    LabelNameArabic = "الاسم االمستخدم (عـربـي)",
                                    Value=true,
                                    Type = "Footer"
                                },



                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Counter Name (Eng)",
                                    LabelNameArabic = "رقـم الـكـاؤنـتـر(إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Counter Number (Ar)",
                                    LabelNameArabic = "رقـم الـكـاؤنـتـر (عـربـي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Print QR",
                                    LabelNameArabic = "طباعة رمز الكيو ار",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="CR Number (Ar)",
                                    LabelNameArabic = "الرقـم السـجـل الـتـجـارى (عربي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="CR Number (Eng)",
                                    LabelNameArabic = "الرقـم السـجـل الـتـجـارى (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Footer"
                                },

                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Gratitude Message (Eng)",
                                    LabelNameArabic = "الـرســالــة الـشـكـر (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Gratitude Message (Ar)",
                                    LabelNameArabic = "الـرســالــة الـشـكـر (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Footer"
                                },

                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Terms & Conditions (En)",
                                    LabelNameArabic = "أدخل الشروط والأحكام (الإنجليزي)",
                                    Value=true,
                                    Type = "Footer"
                                },
                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Terms & Conditions (Ar)",
                                    LabelNameArabic = "أدخل الشروط والأحكام (عربي)",
                                    Value=true,
                                    Type = "Footer"
                                },

                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Business Address (Arabic)",
                                    LabelNameArabic = "الـعـنـوان التجارة (عربي) ",
                                    Value=true,
                                    Type = "Header"
                                },

                                new PrintOption
                                {
                                    PrintSettingId = printSettingId,
                                    Label="Business Address (English)",
                                    LabelNameArabic = "الـعـنـوان التجارة (إنجـلـيـزي)",
                                    Value=true,
                                    Type = "Header"
                                },
                            };



                            await _context.PrintOptions.AddRangeAsync(pritntOptions, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    PrintSetting printSetting;
                    if (request.BranchId != null)
                    {
                        printSetting = await _context.PrintSettings
                            .AsNoTracking()
                            .Include(x => x.PrintOptions)
                            .Include(x => x.Bank1)
                            .Include(x => x.Bank2)
                            .FirstOrDefaultAsync(x => x.BranchId == request.BranchId, cancellationToken: cancellationToken);

                    }
                    else
                    {
                        printSetting = await _context.PrintSettings
                            .AsNoTracking()
                            .Include(x => x.PrintOptions)
                            .Include(x => x.Bank1)
                            .Include(x => x.Bank2)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    }

                    if (printSetting != null)
                    {

                        string bankName = "";
                        string bankAccount = "";

                        var users = await UserManager.Users.AsNoTracking().Include(x=>x.Bank).FirstOrDefaultAsync(x => x.Id == userId.Value.ToString(), cancellationToken: cancellationToken);
                        if (users != null)
                        {
                            if (!string.IsNullOrEmpty(users.DocumentType) )
                            {
                                if (users.DocumentType == "User Wise" && users.BankId != null)
                                {
                                    bankName = users.Bank.BankName;
                                    bankAccount = users.Bank.AccountNumber;

                                }
                                else if (users.DocumentType == "Department Wise")
                                {
                                    if (users.EmployeeId != null)
                                    {
                                        var employee = _context.EmployeeRegistrations.Include(x => x.Department)
                                            .ThenInclude(x=>x.Bank)
                                            .FirstOrDefault(x => x.Id == users.EmployeeId);
                                        if (employee != null)
                                        {
                                            if (employee.Department?.BankId != null)
                                            {
                                                bankName = employee.Department.Bank.BankName;
                                                bankAccount = employee.Department.Bank.AccountNumber;
                                            }
                                            else
                                            {
                                                bankName = printSetting.Bank1 == null ? "" : printSetting.Bank1.BankName;
                                                bankAccount = printSetting.Bank2 == null ? "" : printSetting.Bank2.BankName;
                                            }
                                        }
                                        else
                                        {
                                            bankName = printSetting.Bank1 == null ? "" : printSetting.Bank1.BankName;
                                            bankAccount = printSetting.Bank2 == null ? "" : printSetting.Bank2.BankName;
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    bankName = printSetting.Bank1 == null ? "" : printSetting.Bank1.BankName;
                                    bankAccount = printSetting.Bank2 == null ? "" : printSetting.Bank2.BankName;
                                }

                            }
                            else
                            {
                                bankName = printSetting.Bank1 == null ? "" : printSetting.Bank1.BankName;
                                bankAccount = printSetting.Bank2 == null ? "" : printSetting.Bank2.BankName;
                            }
                        }
                        else
                        {
                             bankName = printSetting.Bank1 == null ? "" : printSetting.Bank1.BankName;
                             bankAccount = printSetting.Bank2 == null ? "" : printSetting.Bank2.BankName;

                        }

                        return new PrintSettingDetailLookUpModel
                        {
                            Id = printSetting.Id,

                            WarrantyImage = printSetting.WarrantyImage,
                            WarrantyImageForPrint = string.IsNullOrEmpty(printSetting.WarrantyImage) ? "" : GetBaseImage(printSetting.WarrantyImage).Result,
                            HeaderImageForPrint = string.IsNullOrEmpty(printSetting.HeaderImage) ? "" : GetBaseImage(printSetting.HeaderImage).Result,
                            HeaderImage1ForPrint = string.IsNullOrEmpty(printSetting.HeaderImage1) ? "" : GetBaseImage(printSetting.HeaderImage1).Result,
                            TagImageForPrint = string.IsNullOrEmpty(printSetting.TagsImages) ? "" : GetBaseImage(printSetting.TagsImages).Result,
                            ProposalImageForPrint = string.IsNullOrEmpty(printSetting.ProposalImage) ? "" : GetBaseImage(printSetting.ProposalImage).Result,
                            HeaderImage1 = printSetting.HeaderImage1,
                            HeaderImage = printSetting.HeaderImage,
                            TagsImages = printSetting.TagsImages,
                            ProposalImage = printSetting.ProposalImage,
                            FooterImage = printSetting.FooterImage,
                            FooterImageForPrint = string.IsNullOrEmpty(printSetting.FooterImage) ? "" : GetBaseImage(printSetting.FooterImage).Result,
                            ContactNo = printSetting.ContactNo,
                            ManagementNo = printSetting.ManagementNo,
                            PrintTemplate = printSetting.PrintTemplate,
                            ReturnDays = printSetting.ReturnDays,
                            isActive = printSetting.isActive,
                            PrintSize = printSetting.PrintSize.ToString(),
                            TermsInAr = printSetting.TermsInAr,
                            TermsInEng = printSetting.TermsInEng,
                            CashAccountId = printSetting.CashAccountId,
                            BankAccountId = printSetting.BankAccountId,
                            PrinterName = printSetting.PrinterName,
                            IsHeaderFooter = printSetting.IsHeaderFooter,
                            EnglishName = printSetting.EnglishName,
                            ArabicName = printSetting.ArabicName,
                            ShowBankAccount = printSetting.ShowBankAccount,
                            CustomerAddress = printSetting.CustomerAddress,
                            CustomerVat = printSetting.CustomerVat,
                            CustomerNumber = printSetting.CustomerNumber,
                            CargoName = printSetting.CargoName,
                            CustomerTelephone = printSetting.CustomerTelephone,
                            ItemPieceSize = printSetting.ItemPieceSize,
                            StyleNo = printSetting.StyleNo,
                            BlindPrint = printSetting.BlindPrint,
                            BankAccount1 = bankAccount,
                            BankAccount2 = printSetting.BankAccount2,
                            BankIcon1 = bankName,
                            BankIcon2 = printSetting.Bank2 == null ? "" : printSetting.Bank2.BankName,
                            InvoicePrint = printSetting.InvoicePrint,
                            IsBlindPrint = printSetting.IsBlindPrint,
                            IsDeliveryNote = printSetting.IsDeliveryNote,
                            TermAndConditionLength = printSetting.TermAndConditionLength,
                            AutoPaymentVoucher = printSetting.AutoPaymentVoucher,
                            CustomerNameEn = printSetting.CustomerNameEn,
                            CustomerNameAr = printSetting.CustomerNameAr,
                            ExchangeDays = printSetting.ExchangeDays,
                            Bank1Id = printSetting.Bank1Id,
                            Bank2Id = printSetting.Bank2Id,
                            WelcomeLineEn = printSetting.WelcomeLineEn,
                            WelcomeLineAr = printSetting.WelcomeLineAr,
                            IsQuotationPrint = printSetting.IsQuotationPrint,
                            WalkInInvoiceType = printSetting.WalkInInvoiceType,
                            BusinessAddressArabic = printSetting.BusinessAddressArabic,
                            BusinessAddressEnglish = printSetting.BusinessAddressEnglish,
                            ClosingLineEn = string.IsNullOrEmpty(printSetting.ClosingLineEn) ? "Thankyou for your visit" : printSetting.ClosingLineEn,
                            ClosingLineAr = string.IsNullOrEmpty(printSetting.ClosingLineAr) ? "شـكـرا لــزيــارتـكـــ" : printSetting.ClosingLineAr,

                            WithSubTotal = printSetting.WithSubTotal,
                            ContinueWithPage = printSetting.ContinueWithPage,
                            SubTotalWithDashes = printSetting.SubTotalWithDashes,
                            DiscountTypeOption = printSetting.DiscountTypeOption,
                            TaxMethod = printSetting.TaxMethod,
                            TaxRateId = printSetting.TaxRateId,

                            PrintOptions = printSetting.PrintOptions.Select(x => new PrintOptionLookUp
                            {
                                Id = x.Id,
                                Label = x.Label,
                                PrintSettingId = x.PrintSettingId,
                                LabelNameArabic = x.LabelNameArabic,
                                Value = x.Value,
                                Type = x.Type


                            }).ToList()

                        };

                    }



                    return new PrintSettingDetailLookUpModel();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
            public async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return filePath;

                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return "";

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
