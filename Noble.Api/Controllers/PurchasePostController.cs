using Focus.Business.PurchasePosts;
using Focus.Business.PurchasePosts.Commands.AddPurchasePost;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.PurchaseOrders.Queries.PurchaseOrderAction;
using Focus.Business.PurchasePosts.Commands.PurchasePostCosting;
using Focus.Business.PurchasePosts.Queries.PurchasePostExpenseList;
using Focus.Business.PurchasePosts.Queries.PurchaseReturnHistory;
using Focus.Business.Purchases.Queries.GetPurchaseRegistrationNo;
using Microsoft.EntityFrameworkCore;
using Noble.Api.Models;
using QRCoder;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasePostController : BaseController
    {
        public readonly IApplicationDbContext Context;
        public PurchasePostController(IApplicationDbContext context)
        {
            Context = context;
        }

        [Route("api/PurchasePost/PurchaseReturnAutoGenerateNo")]
        [HttpGet("PurchaseReturnAutoGenerateNo")]
        [Roles( "CanAddPurchaseReturn")]

        public async Task<IActionResult> PurchaseReturnAutoGenerateNo(Guid? terminalId, string invoicePrefix, Guid? userId, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetPurchaseRegistrationNoQuery {
                TerminalId = terminalId,
                InvoicePrefix = invoicePrefix,
                UserId = userId,
                BranchId = branchId,
            });
            return Ok(autoNo);
        }
        [Route("api/PurchasePost/SavePurchasePostInformation")]
        [HttpPost("SavePurchasePostInformation")]
        [Roles("CanViewPurchaseDraft", "CanAddPurchaseInvoice", "CanAddPurchaseReturn")]
        public async Task<IActionResult> SavePurchasePostInformation([FromBody] PurchasePostLookupModel purchasePost)
        {
            if (purchasePost.AllowPreviousFinancialPeriod)
            {
                purchasePost.Date = new DateTime(purchasePost.Date.Year, purchasePost.Date.Month, purchasePost.Date.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
            }
            decimal payments = 0;
            if (purchasePost.PurchaseOrderId != null && (purchasePost.AutoPurchaseVoucher))
            {
                var purchaseOrderList = new List<PurchasePostLookUpModel>();
                var po = await Context.PurchaseOrders.AsNoTracking()
                .Include(x=>x.PurchaseOrderItems)
                .Include(x=>x.PurchasePosts)
                .ThenInclude(x=>x.PurchasePostItems)
                    .ThenInclude(x=>x.TaxRate)
                    .FirstOrDefaultAsync(x => x.Id == purchasePost.PurchaseOrderId);

                foreach (var purchaseOrder in po.PurchasePosts)
                {
                    var netAmount = new TotalAmount();
                    var lookUpModel = new PurchasePostLookUpModel
                    {
                        Id = purchaseOrder.Id,
                        ExpenseUse = purchaseOrder.ExpenseUse,
                        NetAmount = netAmount.CalculateTotalAmount(purchaseOrder)
                    };
                    purchaseOrderList.Add(lookUpModel);
                }

                payments = purchaseOrderList.Sum(x => x.NetAmount);
            }

            if (purchasePost.Id==Guid.Empty)
            {
                //var autoNo = await Mediator.Send(new GetPurchasePostRegistrationNoQuery { IsPurchaseReturn = purchasePost.IsPurchaseReturn });

                var message  = await Mediator.Send(new AddPurchasePostCommand()
                {
                    PurchasePost = purchasePost,
                    Payments = payments,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                var message = await Mediator.Send(new AddPurchasePostCommand()
                {
                    PurchasePost = purchasePost,
                    Payments = payments,
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }

        }
        [Route("api/PurchasePost/SavePurchasePostCosting")]
        [HttpPost("SavePurchasePostCosting")]
        [Roles("CanPurchaseInvoiceCosting")]
        public async Task<IActionResult> SavePurchasePostCosting([FromBody] PurchasePostLookupModel purchasePost)
        {
            if (purchasePost.Id == Guid.Empty)
            {
                var message = await Mediator.Send(new AddPurchasePostCosting()
                {
                    PurchasePost = purchasePost
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
            else
            {
                var message = await Mediator.Send(new AddPurchasePostCosting()
                {
                    PurchasePost = purchasePost
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Add" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }

        }
        [Route("api/PurchasePost/PurchasePostList")]
        [HttpGet("PurchasePostList")]
        [Roles("CanViewPurchaseDraft", "CanViewPurchasePost", "CanViewPurchaseReturn", "CanAddPurchaseReturn", "CanDraftSPR", "CanEditSPR", "CanRejectSPR", "CanAddSPR", "CanViewSPR", "CanViewDetailSPR")]
        public async Task<IActionResult> PurchasePostList(string status, string searchTerm, int? pageNumber, bool isDropDown,bool isPost, Guid supplierId,bool isExpense, Guid supplierAccountId, DateTime? fromDate, DateTime? toDate,string paymentMode,decimal? netAmount, Guid? supplierForFilterId, int? month, int? year, Guid? userId,string customerType, Guid? branchId)
        {
            var purchaseOrder = await Mediator.Send(new GetPurchasePostListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                IsDropDown = isDropDown,
                SupplierId = supplierId,
                IsPost= isPost,
                IsExpense = isExpense,
                FromDate = fromDate,
                ToDate = toDate,
                SupplierAccountId = supplierAccountId,
                Status = status,
                SupplierForFilterId = supplierForFilterId,
                TotalAmount = netAmount,
                PaymentMode = paymentMode,
                Year = year,
                Month = month,
                UserId = userId,
                SupplierType = customerType,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }
        [Route("api/PurchasePost/PurchasePostDetail")]
        [HttpGet("PurchasePostDetail")]
        [Roles("CanViewPurchaseDetail", "CanPrintPurchaseInvoice", "CanEditPurchaseInvoice", "CanViewDetailPurchaseReturn", "CanAddPurchaseReturn")]
        public async Task<IActionResult> PurchasePostDetail(Guid Id, bool isReturnView,bool isMultiUnit,bool?isReport, bool simpleQuery)
        {
            try
            {
                if (isReport == true)
                {
                    simpleQuery = true;
                }

                var purchase = await Mediator.Send(new PurchasePostDetailQuery()
                {
                    Id = Id,
                    IsReturnView = isReturnView,
                    SimpleQuery = simpleQuery,
                    IsMultiUnit = isMultiUnit
                });
                if (isReport == true)
                {
                    TLVCls tlv = new TLVCls(purchase.SupplierName, purchase.SupplierVAT == null ? "0" : purchase.SupplierVAT, Convert.ToDateTime(purchase.Date), Convert.ToDouble(purchase.TotalAmount), Convert.ToDouble(purchase.VatAmount));
                    var qrValue = tlv.ToBase64();
                    QRCodeData qrCodeData;
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    {
                        qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                    }
                    var imgType = Base64QRCode.ImageType.Jpeg;
                    var qrCode = new Base64QRCode(qrCodeData);
                    string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

                    purchase.QrCode = qrCodeImageAsBase64;
                }

                return Ok(purchase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("api/PurchasePost/PurchaseReturnHistoryCounter")]
        [HttpGet("PurchaseReturnHistoryCounter")]
        [Roles("CanViewReturnHistory")]
        public async Task<IActionResult> PurchaseReturnHistoryCounter(Guid Id, bool isReturnView, bool isMultiUnit)
        {
            var purchase = await Mediator.Send(new PurchaseReturnHistoryCounter()
            {
                Id = Id,
                IsReturnView = isReturnView,
                IsMultiUnit = isMultiUnit
            });
            return Ok(purchase);
        }
        [Route("api/PurchasePost/PurchaseReturnHistory")]
        [HttpGet("PurchaseReturnHistory")]
        [Roles("CanViewReturnHistory")]
        public async Task<IActionResult> PurchaseReturnHistory(Guid Id, bool isReturnView, bool isMultiUnit)
        {
            var purchase = await Mediator.Send(new PurchaseReturnHistoryQuery()
            {
                Id = Id,
                IsReturnView = isReturnView,
                IsMultiUnit = isMultiUnit
            });
            return Ok(purchase);
        }
        
        
        [Route("api/PurchasePost/PurchasePostExpensePaymentList")]
        [HttpGet("PurchasePostExpensePaymentList")] 
        [Roles("CanAddOrderExpense")]
        public async Task<IActionResult> PurchasePostExpensePaymentList(Guid id)
        {
            var payment = await Mediator.Send(new PurchasePostExpenseList
            {
                Id = id,
            });
            return Ok(payment);
        }
        
        
        [Route("api/PurchasePost/PurchasePostActionList")]
        [HttpGet("PurchasePostActionList")]
        [Roles("CanAddOrderExpense")]
        public async Task<IActionResult> PurchasePostActionList(Guid id)
        {
            var action = await Mediator.Send(new PurchaseOrderActionListQuery
            {
                Id = id,
            });
            return Ok(action);
        }
    }
}
