using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PrintSettings.Commands.AddUpdatePrintSettings
{
    public class AddUpdatePrintSettingCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public Print PrintSize { get; set; }
        public bool IsActive { get; set; }
        public string TermsInAr { get; set; }
        public string TermsInEng { get; set; }
        public decimal? ReturnDays { get; set; }
        public Guid? CashAccountId { get; set; }
        public Guid? BankAccountId { get; set; }
        public string PrintTemplate { get; set; }
        public string PrinterName { get; set; }
        public bool IsHeaderFooter { get; set; }
        public bool EnglishName { get; set; }
        public bool ArabicName { get; set; }
        public bool CustomerAddress { get; set; }
        public bool CustomerVat { get; set; }
        public bool CustomerNumber { get; set; }
        public bool CargoName { get; set; }
        public bool CustomerTelephone { get; set; }
        public bool ItemPieceSize { get; set; }
        public bool IsQuotationPrint { get; set; }
        public bool StyleNo { get; set; }
        public bool BlindPrint { get; set; }
        public bool ShowBankAccount { get; set; }
        public string BankAccount1 { get; set; }
        public string BankIcon1 { get; set; }
        public string BankAccount2 { get; set; }
        public string BankIcon2 { get; set; }
        public string InvoicePrint { get; set; }
        public bool IsBlindPrint { get; set; }
        public bool AutoPaymentVoucher { get; set; }
        public bool IsDeliveryNote { get; set; }
        public bool CustomerNameEn { get; set; }
        public bool CustomerNameAr { get; set; }
        public decimal? ExchangeDays { get; set; }
        public Guid? Bank1Id { get; set; }
        public Guid? Bank2Id { get; set; }
        public string WelcomeLineEn { get; set; }
        public string WelcomeLineAr { get; set; }
        public string ClosingLineEn { get; set; }
        public string ClosingLineAr { get; set; }
        public string ContactNo { get; set; }
        public string ManagementNo { get; set; }
        public string BusinessAddressArabic { get; set; }
        public string BusinessAddressEnglish { get; set; }
        public string WalkInInvoiceType { get; set; }
        public string HeaderImage { get; set; }
        public string HeaderImage1 { get; set; }
        public string TagsImages { get; set; }
        public string ProposalImage { get; set; }
        public string FooterImage { get; set; }
        public string WarrantyImage { get; set; }
        public bool TermAndConditionLength { get; set; }

        public bool WithSubTotal { get; set; }
        public bool ContinueWithPage { get; set; }
        public bool SubTotalWithDashes { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public string DiscountTypeOption { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdatePrintSettingCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdatePrintSettingCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdatePrintSettingCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.Id != Guid.Empty)
                    {
                        var printSettings = await _context.PrintSettings.FindAsync(request.Id);
                        if (printSettings == null)
                            throw new NotFoundException("Printer Setting", request.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        printSettings.IsQuotationPrint = request.IsQuotationPrint;
                        printSettings.HeaderImage = request.HeaderImage;
                        printSettings.HeaderImage1 = request.HeaderImage1;
                        printSettings.TagsImages = request.TagsImages;
                        printSettings.ProposalImage = request.ProposalImage;
                        printSettings.FooterImage = request.FooterImage;
                        printSettings.PrintTemplate = request.PrintTemplate;
                        printSettings.isActive = request.IsActive;
                        printSettings.ReturnDays = request.ReturnDays;
                        printSettings.PrintSize = request.PrintSize;
                        printSettings.TermsInAr = request.TermsInAr;
                        printSettings.TermsInEng = request.TermsInEng;
                        printSettings.BankAccountId = request.BankAccountId;
                        printSettings.CashAccountId = request.CashAccountId;
                        printSettings.PrinterName = request.PrinterName;
                        printSettings.IsHeaderFooter = request.IsHeaderFooter;
                        printSettings.EnglishName = request.EnglishName;
                        printSettings.ArabicName = request.ArabicName;
                        printSettings.ShowBankAccount = request.ShowBankAccount;
                        printSettings.CustomerAddress = request.CustomerAddress;
                        printSettings.CustomerVat = request.CustomerVat;
                        printSettings.CustomerNumber = request.CustomerNumber;
                        printSettings.CargoName = request.CargoName;
                        printSettings.CustomerTelephone = request.CustomerTelephone;
                        printSettings.ItemPieceSize = request.ItemPieceSize;
                        printSettings.StyleNo = request.StyleNo;
                        printSettings.BankAccount1 = request.BankAccount1;
                        printSettings.BankAccount2 = request.BankAccount2;
                        printSettings.BankIcon1 = request.BankIcon1;
                        printSettings.BankIcon2 = request.BankIcon2;
                        printSettings.BlindPrint = request.BlindPrint;
                        printSettings.InvoicePrint = request.InvoicePrint;
                        printSettings.IsBlindPrint = request.IsBlindPrint;
                        printSettings.AutoPaymentVoucher = request.AutoPaymentVoucher;
                        printSettings.IsDeliveryNote = request.IsDeliveryNote;
                        printSettings.CustomerNameEn = request.CustomerNameEn;
                        printSettings.CustomerNameAr = request.CustomerNameAr;
                        printSettings.ExchangeDays = request.ExchangeDays;
                        printSettings.Bank1Id = request.Bank1Id;
                        printSettings.Bank2Id = request.Bank2Id;
                        printSettings.WelcomeLineEn = request.WelcomeLineEn;
                        printSettings.WelcomeLineAr = request.WelcomeLineAr;
                        printSettings.ClosingLineEn = request.ClosingLineEn;
                        printSettings.ClosingLineAr = request.ClosingLineAr;
                        printSettings.ContactNo = request.ContactNo;
                        printSettings.ManagementNo = request.ManagementNo;
                        printSettings.WalkInInvoiceType = request.WalkInInvoiceType;
                        printSettings.BusinessAddressEnglish = request.BusinessAddressEnglish;
                        printSettings.BusinessAddressArabic = request.BusinessAddressArabic;
                        printSettings.WarrantyImage = request.WarrantyImage;
                        printSettings.TermAndConditionLength = request.TermAndConditionLength;

                        printSettings.WithSubTotal = request.WithSubTotal;
                        printSettings.ContinueWithPage = request.ContinueWithPage;
                        printSettings.SubTotalWithDashes = request.SubTotalWithDashes;
                        printSettings.BranchId = request.BranchId;
                        printSettings.DiscountTypeOption = request.DiscountTypeOption;
                        printSettings.TaxMethod = request.TaxMethod;
                        printSettings.TaxRateId = request.TaxRateId;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return printSettings.Id;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var printSetting = new PrintSetting
                        {
                            IsQuotationPrint = request.IsQuotationPrint,
                            HeaderImage = request.HeaderImage,
                            HeaderImage1 = request.HeaderImage1,
                            TagsImages = request.TagsImages,
                            ProposalImage = request.ProposalImage,
                            FooterImage = request.FooterImage,
                            PrintTemplate = request.PrintTemplate,
                            isActive = request.IsActive,
                            ReturnDays = request.ReturnDays,
                            PrintSize = request.PrintSize,
                            TermsInAr = request.TermsInAr,
                            TermsInEng = request.TermsInEng,
                            CashAccountId = request.CashAccountId,
                            BankAccountId = request.BankAccountId,
                            PrinterName = request.PrinterName,
                            IsHeaderFooter = request.IsHeaderFooter,
                            EnglishName = request.EnglishName,
                            ArabicName = request.ArabicName,
                            CustomerNameEn = request.CustomerNameEn,
                            CustomerNameAr = request.CustomerNameAr,
                            ShowBankAccount = request.ShowBankAccount,
                            CustomerAddress = request.CustomerAddress,
                            CustomerVat = request.CustomerVat,
                            CustomerNumber = request.CustomerNumber,
                            CargoName = request.CargoName,
                            CustomerTelephone = request.CustomerTelephone,
                            ItemPieceSize = request.ItemPieceSize,
                            StyleNo = request.StyleNo,
                            BankAccount1 = request.BankAccount1,
                            BankAccount2 = request.BankAccount2,
                            BankIcon1 = request.BankIcon1,
                            BankIcon2 = request.BankIcon2,
                            BlindPrint = request.BlindPrint,
                            InvoicePrint = request.InvoicePrint,
                            IsBlindPrint = request.IsBlindPrint,
                            AutoPaymentVoucher = request.AutoPaymentVoucher,
                            IsDeliveryNote = request.IsDeliveryNote,
                            ExchangeDays = request.ExchangeDays,
                            Bank1Id = request.Bank1Id,
                            Bank2Id = request.Bank2Id,
                            WelcomeLineEn = request.WelcomeLineEn,
                            WelcomeLineAr = request.WelcomeLineAr,
                            ClosingLineEn = request.ClosingLineEn,
                            ClosingLineAr = request.ClosingLineAr,
                            ContactNo = request.ContactNo,
                            ManagementNo = request.ManagementNo,
                            WalkInInvoiceType = request.WalkInInvoiceType,
                            BusinessAddressEnglish = request.BusinessAddressEnglish,
                            BusinessAddressArabic = request.BusinessAddressArabic,
                            WarrantyImage = request.WarrantyImage,
                            TermAndConditionLength = request.TermAndConditionLength,
                            WithSubTotal = request.WithSubTotal,
                            ContinueWithPage = request.ContinueWithPage,
                            SubTotalWithDashes = request.SubTotalWithDashes,
                            BranchId = request.BranchId,
                            DiscountTypeOption = request.DiscountTypeOption,
                            TaxMethod = request.TaxMethod,
                            TaxRateId = request.TaxRateId,
                        };

                        await _context.PrintSettings.AddAsync(printSetting, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return printSetting.Id;


                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Department Name Already Exist");
                }
            }
        }


    }
}
