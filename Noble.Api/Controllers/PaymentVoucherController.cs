using Focus.Business.PaymentVouchers.Commands.AddUpdatePaymentVoucher;
using Focus.Business.PaymentVouchers.Commands.DeletePaymentVoucher;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherDetails;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherNumber;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchVouchers.Commands;
using Focus.Business.PaymentVouchers.Commands.AttachAdvanceBill;
using Focus.Business.TemporaryCashAllocations.Commands;
using Focus.Business.TemporaryCashAllocations.Queries;
using Focus.Business.CreditNotes.Queries;
using Focus.Business.CreditNotes.Commands;
using Focus.Business.CreditNotes.Model;
using QRCoder;
using Focus.Business.PaymentRefunds.Models;
using Focus.Business.PaymentRefunds.Commands;
using Focus.Business.PaymentRefunds.Queries;
using Focus.Business.BranchVouchers.Models;
using Focus.Business.BranchVouchers.Queries;
using Focus.Business.PaymentVouchers.Queries.GetAdvanceBalance;
using Focus.Domain.Entities;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentVoucherController : BaseController
    {

        [DisplayName("Auto Code Generate")]
        [Route("api/PaymentVoucher/AutoGenerateCode")]
        [HttpGet("AutoGenerateCode")]
        [Roles("CanAddPettyCash", "Noble Admin", "CanEditPettyCash", "CanDraftPettyCash", "CanPayAdvanceFromView", "CanAddCPR", "CanEditCPR", "CanDraftCPR", "CanDraftSPR", "CanEditSPR", "CanAddSPR", "CanAddAdvancePayment", "CanAddOrderExpense", "CanAdvancePaymentFromList", "CanServicePayAdvanceFromView")]
        public async Task<IActionResult> AutoGenerateCode(PaymentVoucherType paymentVoucherType, Guid? branchId)
        {
            var pvNumber = await Mediator.Send(new GetPaymentVoucherNumberQuery
            {
                PaymentVoucherType = paymentVoucherType,
                BranchId = branchId,
            });

            return Ok(pvNumber);
        }

        private bool DayStart()
        {
            var dayId = User.Claims.FirstOrDefault(x => x.Type == "DayId");

            if (dayId.Value == "00000000-0000-0000-0000-000000000000")
            {
                return false;
            }

            return true;
        }

        [DisplayName("Advance Bill Attachment")]
        [Route("api/PaymentVoucher/AdvanceBillAttachment")]
        [HttpGet("AdvanceBillAttachment")]
        public async Task<IActionResult> AdvanceBillAttachment(Guid billsId, Guid purchaseId, bool isInvoice)
        {


            var result = await Mediator.Send(new AttachAdvanceBillCommand
            {
                BillsId = billsId,
                PurchaseId = purchaseId,
                IsInvoice = isInvoice,

            });

            return Ok(result);
        }

        //Advance Account Transaction Details
        [Route("api/PaymentVoucher/GetSaleToVoucher")]
        [HttpGet("GetSaleToVoucher")]
        public async Task<IActionResult> GetSaleToVoucher(Guid id, bool isPurchase, Guid? branchId, string formName)
        {
            var result = await Mediator.Send(new GetSaleToVoucherQuery
            {
                Id = id,
                IsPurchase = isPurchase,
                BranchId = branchId,
                FormName = formName
            });

            return Ok(result);
        }
        
        // Sale Invoice to Payment Voucher
        [Route("api/PaymentVoucher/GetAdvanceBalance")]
        [HttpGet("GetAdvanceBalance")]
        public async Task<IActionResult> GetAdvanceBalance(Guid? contactId, Guid? branchId, string formName)
        {


            var result = await Mediator.Send(new GetAdvanceBalanceQuery
            {
                ContactId = contactId,
                BranchId = branchId,
                FormName = formName
            });

            return Ok(result);
        }

        [DisplayName("Add Payment Voucher")]
        [Route("api/PaymentVoucher/AddPaymentVoucher")]
        [HttpPost("AddPaymentVoucher")]
        //[Roles("CanAddPettyCash", "Noble Admin", "CanEditPettyCash", "CanDraftPettyCash", "CanPayAdvanceFromView", "CanAddCPR", "CanEditCPR", "CanDraftCPR", "CanDraftSPR", "CanEditSPR", "CanAddSPR", "CanAddAdvancePayment", "CanAddOrderExpense", "CanAdvancePaymentFromList", "CanServicePayAdvanceFromView")]

        public async Task<IActionResult> AddPaymentVoucher([FromBody] PaymentVoucherVm paymentVoucher, Guid? purchaseOrderId, bool isPurchase, bool isSaleOrder, bool isPurchaseOrderExpense, Guid? expenseTypeId, bool isContractor, Guid? batchProcessContractorId,bool premiumAdvance)
        {
            var id = paymentVoucher.Id;
            if (id == Guid.Empty)
            {
                //var pvNumber = await Mediator.Send(new GetPaymentVoucherNumberQuery { PaymentVoucherType = paymentVoucher.PaymentVoucherType });

                var message = await Mediator.Send(new AddUpdatePaymentVoucherCommand
                {
                    MultipleDocument = paymentVoucher.MultipleDocument,
                    DocumentId = paymentVoucher.DocumentId,
                    DocumentType = paymentVoucher.DocumentType,
                    Id = paymentVoucher.Id,
                    TransactionId = paymentVoucher.TransactionId,
                    IsView = paymentVoucher.IsView,
                    NatureOfPayment = paymentVoucher.NatureOfPayment,
                    Prefix = paymentVoucher.Prefix,
                    Date = paymentVoucher.Date,
                    PettyCash = paymentVoucher.PettyCash,
                    VoucherNumber = paymentVoucher.VoucherNumber,
                    Narration = paymentVoucher.Narration,
                    ChequeNumber = paymentVoucher.ChequeNumber,
                    BankCashAccountId = paymentVoucher.BankCashAccountId,
                    ContactAccountId = paymentVoucher.ContactAccountId,
                    PaymentVoucherType = paymentVoucher.PaymentVoucherType,
                    Amount = paymentVoucher.Amount,
                    SaleInvoice = paymentVoucher.SaleInvoice,
                    PurchaseInvoice = paymentVoucher.PurchaseInvoice,
                    ApprovalStatus = paymentVoucher.ApprovalStatus,
                    UserName = paymentVoucher.UserName,
                    PaymentMethod = paymentVoucher.PaymentMethod,
                    PaymentMode = paymentVoucher.PaymentMode,
                    Path = paymentVoucher.Path,
                    PurchaseOrderId = purchaseOrderId,
                    IsPurchase = isPurchase,
                    IsSaleOrder = isSaleOrder,
                    IsPurchaseOrderExpense = isPurchaseOrderExpense,
                    ExpenseTypeId = expenseTypeId,
                    TaxRateId = paymentVoucher.TaxRateId,
                    TaxMethod = paymentVoucher.TaxMethod,
                    VatAccountId = paymentVoucher.VatAccountId,
                    BillsId = paymentVoucher.BillsId,
                    ReparingOrderId = paymentVoucher.ReparingOrderId,
                    IsPurchasePostExpense = paymentVoucher.IsPurchasePostExpense,
                    AttachmentList = paymentVoucher.AttachmentList,
                    TemporaryCashIssueId = paymentVoucher.TemporaryCashIssueId,
                    PaymentVoucherItems = paymentVoucher.PaymentVoucherItems,
                    BranchId = paymentVoucher.BranchId,
                    BatchProcessContractorId = batchProcessContractorId,
                    IsContractor = isContractor,
                    PaymentDate = paymentVoucher.PaymentDate,
                    InvoiceAmount = paymentVoucher.InvoiceAmount,
                    RemainingBalance = paymentVoucher.RemainingBalance,
                    PremiumAdvance = premiumAdvance,
                });
                return Ok(new { Message = message, type = "Add" });
            }
            else
            {
                var message = await Mediator.Send(new AddUpdatePaymentVoucherCommand
                {
                    Id = paymentVoucher.Id,
                    DocumentId = paymentVoucher.DocumentId,
                    DocumentType = paymentVoucher.DocumentType,
                    Date = paymentVoucher.Date,
                    IsView = paymentVoucher.IsView,
                    TransactionId = paymentVoucher.TransactionId,
                    NatureOfPayment = paymentVoucher.NatureOfPayment,
                    Prefix = paymentVoucher.Prefix,
                    PettyCash = paymentVoucher.PettyCash,
                    VoucherNumber = paymentVoucher.VoucherNumber,
                    Narration = paymentVoucher.Narration,
                    ChequeNumber = paymentVoucher.ChequeNumber,
                    BankCashAccountId = paymentVoucher.BankCashAccountId,
                    ContactAccountId = paymentVoucher.ContactAccountId,
                    PaymentVoucherType = paymentVoucher.PaymentVoucherType,
                    Amount = paymentVoucher.Amount,
                    SaleInvoice = paymentVoucher.SaleInvoice,
                    PurchaseInvoice = paymentVoucher.PurchaseInvoice,
                    ApprovalStatus = paymentVoucher.ApprovalStatus,
                    UserName = paymentVoucher.UserName,
                    PaymentMethod = paymentVoucher.PaymentMethod,
                    PaymentMode = paymentVoucher.PaymentMode,
                    Path = paymentVoucher.Path,
                    PurchaseOrderId = purchaseOrderId,
                    IsPurchase = isPurchase,
                    IsSaleOrder = isSaleOrder,
                    IsPurchaseOrderExpense = isPurchaseOrderExpense,
                    ExpenseTypeId = expenseTypeId,
                    TaxRateId = paymentVoucher.TaxRateId,
                    TaxMethod = paymentVoucher.TaxMethod,
                    VatAccountId = paymentVoucher.VatAccountId,
                    BillsId = paymentVoucher.BillsId,
                    IsPurchasePostExpense = paymentVoucher.IsPurchasePostExpense,
                    AttachmentList = paymentVoucher.AttachmentList,
                    PaymentVoucherItems = paymentVoucher.PaymentVoucherItems,
                    TemporaryCashIssueId = paymentVoucher.TemporaryCashIssueId,
                    PaymentDate = paymentVoucher.PaymentDate,
                    BranchId = paymentVoucher.BranchId,
                    InvoiceAmount = paymentVoucher.InvoiceAmount,
                    RemainingBalance = paymentVoucher.RemainingBalance,
                    PremiumAdvance = premiumAdvance,

                });
                return Ok(new { Message = message, type = "Edit" });
            }
        }

        [DisplayName("Payment Voucher List")]
        [Route("api/PaymentVoucher/PaymentVoucherList")]
        [HttpGet("PaymentVoucherList")]
        [Roles("CanViewPettyCash", "Noble Admin", "CanDraftPettyCash", "CanRejectPettyCash", "CanViewCPR", "CanDraftCPR", "CanRejectCPR", "CanDraftSPR", "CanRejectSPR", "CanViewSPR", "CanServicePayAdvanceFromView", "CanViewSaleInvoiceReport")]
        public async Task<IActionResult> PaymentVoucherList(PaymentVoucherType paymentVoucherType, ApprovalStatus status, string searchTerm, int? pageNumber, bool isDashboard, DateTime? fromDate, DateTime? toDate, Guid? contactId, string natureOfPayment, string newNatureOfPayment, string paymentMode, string paymentMethod, string userId, int? month, int? year, Guid? branchId)
        {
            var paymentVoucher = await Mediator.Send(new GetPaymentVoucherListQuery
            {
                PaymentVoucherType = paymentVoucherType,
                IsDashboard = isDashboard,
                ApprovalStatus = status,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate,
                ContactId = contactId,
                NatureOfPayment = natureOfPayment,
                NewNatureOfPayment = newNatureOfPayment,
                PaymentMode = paymentMode,
                PaymentMethod = paymentMethod,
                UserId = userId,
                Year = year,
                Month = month,
                BranchId = branchId,
            });
            return Ok(paymentVoucher);
        }

        [DisplayName("PaymentVoucherListDashboard")]
        [Route("api/PaymentVoucher/PaymentVoucherListDashboard")]
        [HttpGet("PaymentVoucherListDashboard")]
        public async Task<IActionResult> PaymentVoucherListDashboard(PaymentVoucherType paymentVoucherType, Guid? branchId)
        {
            var paymentVoucher = await Mediator.Send(new PaymentVoucherDashboard
            {
                PaymentVoucherType = paymentVoucherType,
                BranchId = branchId,

            });
            return Ok(paymentVoucher);
        }




        [DisplayName("Payment Voucher Delete")]
        [Route("api/PaymentVoucher/PaymentVoucherDelete")]
        [HttpGet("PaymentVoucherDelete")]
        [Roles("Can Delete Cash Receipt as Draft", "Can Delete Cash Receipt as Post", "Can Delete Cash Pay  as Draft", "Can Delete Cash Pay  as Post", "Can Delete Bank Receipt as Draft", "Can Delete Bank Receipt as Post", "Can Delete Bank Pay as Draft", "Can Delete Bank Pay as Post", "Noble Admin")]
        public async Task<IActionResult> PaymentVoucherDelete(Guid id)
        {
            var paymentVoucherId = await Mediator.Send(new DeletePaymentVoucherCommand
            {
                Id = id
            });
            return Ok(new { Message = paymentVoucherId });
        }

        [DisplayName("Payment Voucher Detail Query")]
        [Route("api/PaymentVoucher/PaymentVoucherDetails")]
        [HttpGet("PaymentVoucherDetails")]
        [Roles("CanEditPettyCash", "Noble Admin", "CanRejectPettyCash", "CanViewDetailPettyCash", "CanPrintPettyCashTemplate1", "CanPrintPettyCashTemplate2", "CanPayAdvanceFromView", "CanEditCPR", "CanRejectCPR", "CanViewDetailCPR", "CanPrintCPR", "CanEditSPR", "CanRejectSPR", "CanViewDetailSPR", "CanPrintSPR", "CanViewDetailAdvancePayment", "CanViewDetailOrderExpense", "CanAdvancePaymentFromList", "CanServicePayAdvanceFromView", "CanViewCustomerBalance")]

        public async Task<IActionResult> PaymentVoucherDetails(Guid id, bool isEmail, bool? isReport)
        {
            var paymentVoucherId = await Mediator.Send(new PaymentVoucherDetailQuery
            {
                Id = id,
                IsEmail = isEmail
            });
            if (isReport == true)
            {

                TLVCls tlv = new TLVCls(paymentVoucherId.ContactAccountName, "", Convert.ToDateTime(paymentVoucherId.Date), Convert.ToDouble(paymentVoucherId.Amount), Convert.ToDouble(paymentVoucherId.Amount));
                var qrValue = tlv.ToBase64();
                QRCodeData qrCodeData;
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                }
                var imgType = Base64QRCode.ImageType.Jpeg;
                var qrCode = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

                paymentVoucherId.QRCode = qrCodeImageAsBase64;
            }
            return Ok(paymentVoucherId);
        }

        //[DisplayName("Payment Voucher Transaction Query")]
        //[Route("api/PaymentVoucher/PaymentVoucherTransactionDetails")]
        //[HttpGet("PaymentVoucherTransactionDetails")]
        //[Roles("Can Save Cash Receipt as Draft", "Can Save Cash Receipt as Post", "Can Save Cash Pay  as Draft", "Can Save Cash Pay  as Post", "Can Save Bank Receipt as Draft", "Can Save Bank Receipt as Post", "Can Save Bank Pay as Draft", "Can Save Bank Pay as Post", "Noble Admin", "Can View Chart Of Account", "Can Save Chart Of Account", "Can Edit Cash Receipt as Draft", "Can Edit Cash Receipt as Post", "Can Edit Cash Pay  as Draft", "Can Edit Cash Pay  as Post", "Can Edit Bank Receipt as Draft", "Can Edit Bank Receipt as Post", "Can Edit Bank Pay as Draft", "Can Edit Bank Pay as Post")]
        //public async Task<IActionResult> PaymentVoucherTransactionDetails(Guid id)
        //{
        //    var paymentVoucherId = await Mediator.Send(new PaymentVoucherTransactionsDetailQuery
        //    {
        //        Id = id
        //    });
        //    return Ok(new { Message = paymentVoucherId });
        //}


        [Route("api/PaymentVoucher/UpdateApprovalStatus")]
        [HttpPost("UpdateApprovalStatus")]
        [Roles("CanDraftCPR", "CanViewCPR", "CanViewSPR", "CanDraftSPR", "CanViewPettyCash", "CanDraftPettyCash", "Noble Admin")]

        public async Task<IActionResult> UpdateApprovalStatus([FromBody] ApprovalStatusPaymentVoucher approvalStatusVm)
        {
            var message = await Mediator.Send(new PaymentVoucherUpdateBulkAction()
            {
                Id = approvalStatusVm.Id,
                ApprovalStatus = approvalStatusVm.ApprovalStatus,
                UserName = approvalStatusVm.UserName

            });
            return Ok(message);
        }

        [Route("api/PaymentVoucher/TemporaryCashAllocationCode")]
        [HttpGet("TemporaryCashAllocationCode")]
        [Roles("CanAddTCAllocation", "CanDraftTCAllocation")]
        public async Task<IActionResult> TemporaryCashAllocationCode()
        {

            var pvNumber = await Mediator.Send(new AutoTemporaryCashAllocationCode());

            return Ok(pvNumber);
        }

        [Route("api/PaymentVoucher/AddTemporaryCashAllocation")]
        [HttpPost("AddTemporaryCashAllocation")]
        [Roles("CanAddTCAllocation", "CanDraftTCAllocation", "CanEditTCAllocation")]
        public async Task<IActionResult> AddTemporaryCashAllocation([FromBody] TemporaryCashAllocationLookUp paymentVoucher)
        {
            var id = paymentVoucher.Id;
            if (id == Guid.Empty)
            {
                //var pvNumber = await Mediator.Send(new GetPaymentVoucherNumberQuery { PaymentVoucherType = paymentVoucher.PaymentVoucherType });

                var message = await Mediator.Send(new AddUpdateTemporaryCashAllocation
                {
                    Id = paymentVoucher.Id,
                    Date = paymentVoucher.Date,
                    VoucherNumber = paymentVoucher.VoucherNumber,
                    Narration = paymentVoucher.Narration,
                    ChequeNumber = paymentVoucher.ChequeNumber,
                    BankCashAccountId = paymentVoucher.BankCashAccountId,
                    PaymentVoucherType = paymentVoucher.PaymentVoucherType,
                    Amount = paymentVoucher.Amount,
                    ApprovalStatus = paymentVoucher.ApprovalStatus,
                    UserName = paymentVoucher.UserName,
                    UserEmployeeId = paymentVoucher.UserEmployeeId,
                    PaymentMethod = paymentVoucher.PaymentMethod,
                    PaymentMode = paymentVoucher.PaymentMode,
                    AttachmentList = paymentVoucher.AttachmentList,
                    BranchId = paymentVoucher.BranchId,

                });
                return Ok(new { Message = message, type = "Add" });
            }
            else
            {

                var message = await Mediator.Send(new AddUpdateTemporaryCashAllocation
                {
                    Id = paymentVoucher.Id,
                    Date = paymentVoucher.Date,
                    VoucherNumber = paymentVoucher.VoucherNumber,
                    Narration = paymentVoucher.Narration,
                    ChequeNumber = paymentVoucher.ChequeNumber,
                    BankCashAccountId = paymentVoucher.BankCashAccountId,
                    PaymentVoucherType = paymentVoucher.PaymentVoucherType,
                    Amount = paymentVoucher.Amount,
                    ApprovalStatus = paymentVoucher.ApprovalStatus,
                    UserName = paymentVoucher.UserName,
                    UserEmployeeId = paymentVoucher.UserEmployeeId,
                    PaymentMethod = paymentVoucher.PaymentMethod,
                    PaymentMode = paymentVoucher.PaymentMode,
                    AttachmentList = paymentVoucher.AttachmentList,
                    BranchId = paymentVoucher.BranchId,
                });
                return Ok(new { Message = message, type = "Edit" });
            }


        }
        [Route("api/PaymentVoucher/TemporaryCashAllocationList")]
        [HttpGet("TemporaryCashAllocationList")]
        [Roles("CanViewTCAllocation", "CanDraftTCAllocation")]
        public async Task<IActionResult> TemporaryCashAllocationList(ApprovalStatus status, string searchTerm, int? pageNumber)
        {
            var paymentVoucher = await Mediator.Send(new TemporaryCashAllocationListQuery
            {
                ApprovalStatus = status,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(paymentVoucher);
        }

        [Route("api/PaymentVoucher/TemporaryCashAllocationDetails")]
        [HttpGet("TemporaryCashAllocationDetails")]
        [Roles("CanPrintTCAllocation", "CanEditTCAllocation")]

        public async Task<IActionResult> TemporaryCashAllocationDetails(Guid id)
        {
            var paymentVoucherId = await Mediator.Send(new GetTemporaryCashAllocationDetail
            {
                Id = id
            });

            return Ok(new { Message = paymentVoucherId });
        }

        [Route("api/PaymentVoucher/CreditNoteAutoGenerateNo")]
        [HttpGet("CreditNoteAutoGenerateNo")]
        public async Task<IActionResult> CreditNoteAutoGenerateNo(bool isCreditNote, string terminalId, string invoicePrefix, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetCreditNoteAutoNo()
            {
                TerminalId = terminalId,
                InvoicePrefix = invoicePrefix,
                IsCreditNote = isCreditNote,
                BranchId = branchId,

            });
            return Ok(autoNo);
        }

        [Route("api/PaymentVoucher/CreditNoteList")]
        [HttpGet("CreditNoteList")]
        public async Task<IActionResult> CreditNoteList(ApprovalStatus status, string searchTerm, int? pageNumber, bool isCreditNote, Guid? branchId, DateTime? fromDate, DateTime? toDate, Guid? userId, int? month, int? year, Guid? customerId, Guid? terminalId)
        {
            var paymentVoucher = await Mediator.Send(new GetCreditNoteListQuery
            {
                IsCreditNote = isCreditNote,
                ApprovalStatus = status,
                SearchTerm = searchTerm,
                BranchId = branchId,
                PageNumber = pageNumber ?? 1,
                Year = year,
                Month = month,
                FromDate = fromDate,
                ToDate = toDate,
                CustomerId = customerId,
                UserId = userId,
                TerminalId = terminalId,
            });
            return Ok(paymentVoucher);
        }

        [Route("api/PaymentVoucher/CreditNoteSave")]
        [HttpPost("CreditNoteSave")]
        public async Task<IActionResult> CreditNoteSave([FromBody] CreditNotesModel creditNotes)
        {
            var message = await Mediator.Send(new AddUpdateCreditNoteCommand()
            {
                CreditNotes = creditNotes

            });
            return Ok(message);
        }

        [Route("api/PaymentVoucher/CreditNoteDetail")]
        [HttpGet("CreditNoteDetail")]
        public async Task<IActionResult> CreditNoteDetail(Guid id, bool? isReport, bool simpleQuery)
        {
            try
            {
                if (isReport == true)
                {
                    simpleQuery = true;
                }
                var autoNo = await Mediator.Send(new GetCreditNoteDetailQuery()
                {
                    Id = id,
                    SimpleQuery = simpleQuery,


                });
                if (isReport == true)
                {
                    TLVCls tlv = new TLVCls(autoNo.CustomerNameEn, autoNo.CustomerVat == null ? "0" : autoNo.CustomerVat, Convert.ToDateTime(autoNo.Date), Convert.ToDouble(autoNo.GrossAmount), Convert.ToDouble(autoNo.VatAmount));
                    var qrValue = tlv.ToBase64();
                    QRCodeData qrCodeData;
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    {
                        qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                    }
                    var imgType = Base64QRCode.ImageType.Jpeg;
                    var qrCode = new Base64QRCode(qrCodeData);
                    string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

                    autoNo.QrCode = qrCodeImageAsBase64;
                }
                return Ok(autoNo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        #region Payment Refund

        [DisplayName("Add Payment Refund")]
        [Route("api/PaymentVoucher/SavePaymentRefund")]
        [HttpPost("SavePaymentRefund")]
        //[Roles("CanAddPettyCash", "Noble Admin", "CanEditPettyCash", "CanDraftPettyCash", "CanPayAdvanceFromView", "CanAddCPR", "CanEditCPR", "CanDraftCPR", "CanDraftSPR", "CanEditSPR", "CanAddSPR", "CanAddAdvancePayment", "CanAddOrderExpense", "CanAdvancePaymentFromList", "CanServicePayAdvanceFromView")]
        public async Task<IActionResult> SavePaymentRefund([FromBody] PaymentRefundModel paymentRefund)
        {
            var message = await Mediator.Send(new AddUpdatePaymentRefundCommands
            {
                PaymentRefundModel = paymentRefund
            });

            return Ok(message);
        }

        [DisplayName("Payment Refund List")]
        [Route("api/PaymentVoucher/PaymentRefundList")]
        [HttpGet("PaymentRefundList")]
        //[Roles("CanViewPettyCash", "Noble Admin", "CanDraftPettyCash", "CanRejectPettyCash", "CanViewCPR", "CanDraftCPR", "CanRejectCPR", "CanDraftSPR", "CanRejectSPR", "CanViewSPR", "CanServicePayAdvanceFromView", "CanViewSaleInvoiceReport")]
        public async Task<IActionResult> PaymentRefundList(ApprovalStatus status, string searchTerm, int? pageNumber)
        {
            var paymentVoucher = await Mediator.Send(new GetPaymentRefundListQuery
            {
                ApprovalStatus = status,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(paymentVoucher);
        }

        [DisplayName("Payment Refund Detail")]
        [Route("api/PaymentVoucher/PaymentRefundDetail")]
        [HttpGet("PaymentRefundDetail")]
        //[Roles("CanViewPettyCash", "Noble Admin", "CanDraftPettyCash", "CanRejectPettyCash", "CanViewCPR", "CanDraftCPR", "CanRejectCPR", "CanDraftSPR", "CanRejectSPR", "CanViewSPR", "CanServicePayAdvanceFromView", "CanViewSaleInvoiceReport")]
        public async Task<IActionResult> PaymentRefundDetail(Guid id)
        {
            var paymentVoucher = await Mediator.Send(new GetPaymentRefundDetailQuery
            {
                Id = id,
            });
            return Ok(paymentVoucher);
        }

        #endregion

        #region Branch Voucher
        [Route("api/PaymentVoucher/BranchVoucherCode")]
        [HttpGet("BranchVoucherCode")]
        public async Task<IActionResult> BranchVoucherCode(Guid? branchId)
        {
            var pvNumber = await Mediator.Send(new GetBranchVoucherNumberQuery()
            {
                BranchId = branchId
            });

            return Ok(pvNumber);
        }


        [DisplayName("Add Branch Voucher")]
        [Route("api/PaymentVoucher/SaveBranchVoucher")]
        [HttpPost("SaveBranchVoucher")]
        //[Roles("CanAddPettyCash", "Noble Admin", "CanEditPettyCash", "CanDraftPettyCash", "CanPayAdvanceFromView", "CanAddCPR", "CanEditCPR", "CanDraftCPR", "CanDraftSPR", "CanEditSPR", "CanAddSPR", "CanAddAdvancePayment", "CanAddOrderExpense", "CanAdvancePaymentFromList", "CanServicePayAdvanceFromView")]
        public async Task<IActionResult> SaveBranchVoucher([FromBody] BranchVoucherModel paymentRefund)
        {
            var message = await Mediator.Send(new AddUpdateBranchVoucherCommands
            {
                PaymentRefundModel = paymentRefund
            });

            return Ok(message);
        }

        [DisplayName("Branch Voucher List")]
        [Route("api/PaymentVoucher/BranchVoucherList")]
        [HttpGet("BranchVoucherList")]
        //[Roles("CanViewPettyCash", "Noble Admin", "CanDraftPettyCash", "CanRejectPettyCash", "CanViewCPR", "CanDraftCPR", "CanRejectCPR", "CanDraftSPR", "CanRejectSPR", "CanViewSPR", "CanServicePayAdvanceFromView", "CanViewSaleInvoiceReport")]
        public async Task<IActionResult> BranchVoucherList(ApprovalStatus status, string searchTerm, int? pageNumber, Guid? branchId)
        {
            var paymentVoucher = await Mediator.Send(new GetBranchVoucherListQuery
            {
                BranchId = branchId,
                ApprovalStatus = status,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(paymentVoucher);
        }

        [DisplayName("Branch Voucher Detail")]
        [Route("api/PaymentVoucher/BranchVoucherDetail")]
        [HttpGet("BranchVoucherDetail")]
        //[Roles("CanViewPettyCash", "Noble Admin", "CanDraftPettyCash", "CanRejectPettyCash", "CanViewCPR", "CanDraftCPR", "CanRejectCPR", "CanDraftSPR", "CanRejectSPR", "CanViewSPR", "CanServicePayAdvanceFromView", "CanViewSaleInvoiceReport")]
        public async Task<IActionResult> BranchVoucherDetail(Guid id)
        {
            var paymentVoucher = await Mediator.Send(new GetBranchVoucherDetailQuery
            {
                Id = id,
            });
            return Ok(paymentVoucher);
        }

        #endregion

    }
}
