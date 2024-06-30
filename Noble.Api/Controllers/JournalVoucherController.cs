using Focus.Business.JournalVouchers.Commands.AddUpdateJournalVoucher;
using Focus.Business.JournalVouchers.Commands.DeleteJV;
using Focus.Business.JournalVouchers.Queries.GetJournalVoucherDetail;
using Focus.Business.JournalVouchers.Queries.GetJournalVoucherList;
using Focus.Business.JournalVouchers.Queries.GetJournalVoucherNumber;
using Focus.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Focus.Domain.Enum;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalVoucherController : BaseController
    {

        [DisplayName("Auto Code Generate")]
        [Route("api/JournalVoucher/AutoGenerateCode")]
        [HttpGet("AutoGenerateCode")]
        [Roles("CanDraftOC", "Noble Admin", "CanDraftJV", "CanAddJV", "CanAddOC", "CanEditOC", "CanEditJV")]
        public async Task<IActionResult> AutoGenerateCode(string formName, Guid? branchId)
        {
            
            var jvNumber = await Mediator.Send(new GetJournalVoucherNumberQuery
            {
                formName = formName,
                BranchId = branchId

            });
         
            return Ok(jvNumber);
        }
        [DisplayName("Add Jv ")]
        [Route("api/JournalVoucher/AddJvAsync")]
        [HttpPost("AddJvAsync")]
        [Roles("CanDraftOC", "Noble Admin", "CanDraftJV", "CanAddJV", "CanAddOC", "CanEditOC", "CanEditJV")]
        public async Task<IActionResult> AddJvAsync([FromBody] JournalVoucherVm journalVoucher)
        {
            var id = journalVoucher.Id;
            var journalVoucherItem = new List<JournalVoucherItem>();
            foreach (var item in journalVoucher.JournalVoucherItems)
            {
                var jvItem = new JournalVoucherItem
                {
                    Id = item.Id,
                    Description = item.Description,
                    Debit = item.Debit,
                    Credit = item.Credit,
                    ContactId = item.ContactId,
                    AccountId = item.AccountId,
                    PaymentMode = item.PaymentMode,
                    PaymentMethod = item.PaymentMethod,
                    ChequeNo = item.ChequeNo,
                    JournalVoucherId =item.JournalVoucherId
                };
                journalVoucherItem.Add(jvItem);
            }

            var message = await Mediator.Send(new AddUpdateJournalVoucherCommand
            {
                Id = journalVoucher.Id,
                VoucherNumber = journalVoucher.VoucherNumber,
                View = journalVoucher.View,
                Date = journalVoucher.Date,
                OpeningCash = journalVoucher.OpeningCash,
                Narration = journalVoucher.Narration,
                Comments = journalVoucher.Comments,
                JournalVoucherItems = journalVoucherItem,
                ApprovalStatus = journalVoucher.ApprovalStatus,
                AttachmentList = journalVoucher.AttachmentList,
                BranchId = journalVoucher.BranchId,
            });

            if (id == Guid.Empty)
            {
                return Ok(new { Message = message, type = "Add" });
            }
            else
            {
                return Ok(new { Message = message, type = "Edit" });
            }
        }

        [DisplayName("Journal Voucher List ")]
        [Route("api/JournalVoucher/JournalVoucherList")]
        [HttpGet("JournalVoucherList")]
        [Roles("CanDraftOC", "Noble Admin", "CanDraftJV", "CanViewJV", "CanViewOC")]

        public async Task<IActionResult> JournalVoucherList(string searchTerm, int? pageNumber, ApprovalStatus status,bool isOpeningCash,Guid? branchId)
        {
            var journalVoucher = await Mediator.Send(new GetJournalVoucherListQuery()
            {
                SearchTerm = searchTerm,
                IsOpeningCash = isOpeningCash,
                PageNumber = pageNumber ?? 1,
                Status = status,
                BranchId = branchId
            });

            return Ok(journalVoucher);
        }
        [Route("api/JournalVoucher/UpdateApprovalStatus")]
        [HttpGet("UpdateApprovalStatus")]
        [Roles("Can View Journal Voucher as Draft", "Noble Admin", "Can View Journal Voucher as Post")]

        //public async Task<IActionResult> UpdateApprovalStatus([FromBody] UpdateApprovalStatusVm approvalStatusVm)
        //{
        //    var message = await Mediator.Send(new UpdateApprovalStatusJvCommand
        //    {
        //        Id = approvalStatusVm.Id,
        //        ApprovalStatus = approvalStatusVm.ApprovalStatus
        //    });
        //    return Ok(message);
        //}

        [Route("api/JournalVoucher/DeleteJournalVoucher")]
        [HttpGet("DeleteJournalVoucher")]
        [Roles( "Noble Admin", "Can Delete  Journal Voucher as Draft", "Can Delete  Journal Voucher as Post", "Can Delete  Opening Cash as Draft", "Can Delete  Opening Cash as Post")]
        public async Task<IActionResult> DeleteJournalVoucher(Guid Id)
        {
            var message = await Mediator.Send(new DeleteJournalVoucherCommand
            {
                Id = Id
            });
            return Ok(new { message });
        }

        [Route("api/JournalVoucher/JournalVoucherDetail")]
        [HttpGet("JournalVoucherDetail")]
        [Roles("CanViewDetailJV", "Noble Admin", "CanViewDetailOC", "CanEditOC", "CanEditJV", "CanAddOC", "CanAddOC")]


        public async Task<IActionResult> JournalVoucherDetail(Guid Id)
        {
            var jv = await Mediator.Send(new JournalVoucherDetailQuery()
            {
                Id = Id
            });

            return Ok(jv);
        }

    }
}