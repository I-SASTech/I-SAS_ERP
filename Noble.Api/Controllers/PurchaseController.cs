using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Focus.Business.AutoPurchasing;
using Focus.Business.Interface;
using Focus.Business.PurchaseOrders;
using Focus.Business.PurchaseOrders.Commands.AddPurchaseOrder;
using Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderList;
using Focus.Business.Purchases.Commands.DeletePurchase;
using Focus.Business.Purchases.Queries.GetPurchaseRegistrationNo;
using Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderDetails;
using Focus.Business.MobileOrders.Queries.GetMobileOrderRegistrationNo;
using Focus.Business.MobileOrders;
using Focus.Business.MobileOrders.Commands.AddMobileOrder;
using Focus.Business.MobileOrders.Queries.GetMobileOrderList;
using Focus.Business.MobileOrders.Queries.GetMobileOrderDetails;
using Focus.Business.WareHouseTransfers;
using Focus.Business.WareHouseTransfers.Commands.AddWareHouseTransfer;
using Focus.Business.WareHouseTransfers.Commands.DeleteWareHouseTransfer;
using Focus.Business.WareHouseTransfers.Queries.GetWareHouseTransferDetail;
using Focus.Business.WareHouseTransfers.Queries.GetWareHouseTransferList;
using Focus.Business.WareHouseTransfers.Queries.GetWareHouseTransferRegistrationNo;
using Noble.Api.Models;
using Focus.Business.SaleOrders.Queries.GetSaleOrderRegistrationNo;
using Focus.Business.SaleOrders;
using Focus.Business.SaleOrders.Commands.AddSaleOrder;
using Focus.Business.SaleOrders.Queries.GetSaleOrderList;
using Focus.Business.SaleOrders.Queries.GetSaleOrderDetails;
using Focus.Domain.Enum;
using Focus.Business.PurchaseOrders.Commands;
using Focus.Business.PurchaseOrders.Queries.PurchaseOrderAction;
using Focus.Business.PurchaseOrders.Queries.PurchaseOrderExpense;
using Focus.Business.PurchaseOrders.Queries.PurchaseOrderPayment;
using Focus.Business.SaleOrders.Queries.SaleList;
using Focus.Business.SaleOrders.Queries.SaleOrderPayment;
using Microsoft.EntityFrameworkCore;
using Focus.Business.PurchaseBills.Queries.GetPurchaseBillRegistrationNo;
using Focus.Business.PurchaseBills;
using Focus.Business.PurchaseBills.Commands.AddPurchaseBill;
using Focus.Business.PurchaseBills.Queries.GetPurchaseBillDetails;
using Focus.Business.PurchaseBills.Queries.GetPurchaseBillList;
using Focus.Business.PurchaseTemplate;
using Focus.Business.PurchaseTemplate.Command;
using Focus.Business.PurchaseTemplate.Queries;
using Focus.Business.SaleOrders.Queries.SaleOrderHistory;
using Focus.Business.GoodReceives.Commands.AddGoodReceive;
using Focus.Business.GoodReceives.Queries;
using Focus.Business.HR.Payroll.QuotationTemplates;
using Focus.Business.QuotationTemplates.Commands.AddQuotationTemplate;
using Focus.Business.QuotationTemplates.Queries;
using Focus.Business.SaleOrders.Queries.GetSaleOrderTracking;
using Focus.Business.DeliveryChallans.Commands.AddDeliveryChallan;
using Focus.Business.DeliveryChallans.Queries;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Focus.Business.DeliveryChallans;
using Focus.Business.GoodReceives;
using Focus.Business.PurchaseOrders.Queries.GetPurchaseOrdeRegistrationNo;
using QRCoder;
using Focus.Business.StockRequests.Queries;
using Focus.Business.StockRequests.Commands;
using Focus.Business.StockRequests.Models;
using Focus.Business.StocksTransfer.Queries;
using Focus.Business.StocksTransfer.Commands;
using Focus.Business.StocksTransfer.Models;
using Focus.Domain.Entities;
using Focus.Business.StocksReceived.Queries;
using Focus.Business.StocksReceived.Commands;
using Focus.Business.StocksReceived.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Focus.Business.PurchaseOrders.Commands.PurchaseOrderReason;
using Focus.Business.PurchaseOrders.Commands.AddPurchaseOrder.DocumentWiseList;


//AfterDiscountAmount
namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : BaseController
    {
        public readonly IApplicationDbContext Context;
        public PurchaseController(IApplicationDbContext context)
        {
            Context = context;
        }
        #region For Purchase
        [Route("api/Purchase/PurchaseAutoGenerateNo")]
        [HttpGet("PurchaseAutoGenerateNo")]
        [Roles("CanAddPurchaseInvoice", "CanViewPurchaseDraft")]
        public async Task<IActionResult> PurchaseAutoGenerateNo(Guid? terminalId, string invoicePrefix, Guid? userId, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetPurchaseRegistrationNoQuery
            {
                TerminalId = terminalId,
                InvoicePrefix = invoicePrefix,
                UserId = userId,
                BranchId = branchId,
            });
            return Ok(autoNo);
        }

        //[Route("api/Purchase/SavePurchaseInformation")]
        //[HttpPost("SavePurchaseInformation")]
        //[Roles("Can Save  Purchase Invoice as Draft", "Can View  Supplier", "Can View  Product", "Can View Ware House", "Noble Admin")]

        //public async Task<IActionResult> SavePurchaseInformation([FromBody] PurchaseLookupModel purchase)
        //{
        //    if (purchase.Id == Guid.Empty)
        //    {
        //        var message = await Mediator.Send(new AddPurchaseCommand()
        //        {
        //            Purchase = purchase,

        //        });
        //        if (message.Id != Guid.Empty)
        //        {
        //            return Ok(new { message = message, Action = "Add" });

        //        }
        //        else
        //        {
        //            return Ok(new { Message = message, Action = "Error" });
        //        }
        //    }
        //    else
        //    {
        //        var message = await Mediator.Send(new AddPurchaseCommand()
        //        {
        //            Purchase = purchase
        //        });
        //        if (message.Id != Guid.Empty)
        //        {
        //            return Ok(new { message = message, Action = "Update" });

        //        }
        //        else
        //        {
        //            return Ok(new { Message = message, Action = "Error" });
        //        }
        //    }
        //}

        //[Route("api/Purchase/PurchaseDetail")]
        //[HttpGet("PurchaseDetail")]
        //[Roles("Can Save  Purchase Invoice as Draft", "Can Edit Purchase Invoice as Draft", "Can View  Supplier", "Can View  Product", "Can View Ware House", "Noble Admin")]

        //public async Task<IActionResult> PurchaseDetail(Guid id)
        //{
        //    var purchase = await Mediator.Send(new PurchaseDetailQuery()
        //    {
        //        Id = id
        //    });
        //    return Ok(purchase);
        //}


        //[Route("api/Purchase/PurchaseList")]
        //[HttpGet("PurchaseList")]
        //[Roles("Can View Purchase Invoice as Draft", "Noble Admin")]
        //public async Task<IActionResult> PurchaseList(string searchTerm, int? pageNumber, bool IsDropDown, DateTime? fromDate, DateTime? toDate)
        //{
        //    var purchaseOrder = await Mediator.Send(new GetPurchaseListQuery
        //    {
        //        SearchTerm = searchTerm,
        //        PageNumber = pageNumber ?? 1,
        //        IsDropDown = IsDropDown,
        //        FromDate = fromDate,
        //        ToDate = toDate,
        //    });

        //    return Ok(purchaseOrder);
        //}

        [Route("api/Purchase/DeletePurchase")]
        [HttpGet("DeletePurchase")]
        [Roles("CanDeletePurchaseInvoice")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            var message = await Mediator.Send(new DeletePurchaseCommand
            {
                Id = id
            });
            return Ok(new { message });
        }
        //[Route("api/Purchase/UpdateApprovalStatus")]
        //[HttpPost("UpdateApprovalStatus")]
        //[Authorize(Roles = "User")]
        //public async Task<IActionResult> UpdateApprovalStatus([FromBody] UpdateApprovalStatusVm approvalStatusVm)
        //{
        //    var message = await Mediator.Send(new UpdateApprovalStatusCommand()
        //    {
        //        Id = approvalStatusVm.Id,
        //    });
        //    return Ok(new { message });
        //}
        [Route("api/Purchase/DeletePo")]
        [HttpGet("DeletePo")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeletePo([FromBody] List<Guid> file)
        {
            var message = await Mediator.Send(new DeleteMultiplePurchaseCommand
            {
                Id = file
            });
            return Ok(new { message });
        }

        #endregion
        #region For PurchaseOrder
        [Route("api/Purchase/PurchaseOrderAutoGenerateNo")]
        [HttpGet("PurchaseOrderAutoGenerateNo")]
        [Roles("CanAddPurchaseOrder", "CanViewDraftOrder", "CanViewInProcessOrder", "CanViewServiceQuotation")]
        public async Task<IActionResult> PurchaseOrderAutoGenerateNo(Guid? terminalId, string invoicePrefix, Guid? userId, string documentType, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetPurchaseOrderRegistrationNoQuery
            {
                TerminalId = terminalId,
                InvoicePrefix = invoicePrefix,
                DocumentType = documentType,
                UserId = userId,
                BranchId = branchId,
            });
            return Ok(autoNo);
        }


        [Route("api/Purchase/ProccessedByDocumentId")]
        [HttpGet("ProccessedByDocumentId")]
        public async Task<IActionResult> ProccessedByDocumentId(Guid? id , string documentName)
        {


            var message = await Mediator.Send(new PurchaseOrderDocumentWiseList()
            {
                DocumentId = id,
                FormName = documentName
            });
            return Ok(message);


        }

        [Route("api/Purchase/UpdatePurchaseOrderCloseReason")]
        [HttpPost("UpdatePurchaseOrderCloseReason")]
        public async Task<IActionResult> UpdatePurchaseOrderCloseReason([FromBody] PurchaseOrderLookupModel purchaseOrder, string formName)
        {
            

                var message = await Mediator.Send(new AddPurchaseOrderReasonToClose()
                {
                    PurchaseOrder = purchaseOrder,
                    FormName = formName
                });
               return Ok(message);

                
        }


        [Route("api/Purchase/SavePurchaseOrderInformation")]
        [HttpPost("SavePurchaseOrderInformation")]
        [Roles("CanAddPurchaseOrder", "CanViewDraftOrder", "CanViewInProcessOrder", "CanEditPurchaseOrder", "CanViewServiceQuotation")]
        public async Task<IActionResult> SavePurchaseOrderInformation([FromBody] PurchaseOrderLookupModel purchaseOrder)
        {
            if (purchaseOrder.Id == Guid.Empty)
            {
                if (purchaseOrder.DocumentType != "SupplierQuotation")
                {
                
                    var autoNo = await Mediator.Send(new GetPurchaseOrderRegistrationNoQuery()
                    {
                        BranchId = purchaseOrder.BranchId
                    });
                    purchaseOrder.RegistrationNo = autoNo;
                }
              

                var message = await Mediator.Send(new AddPurchaseOrderCommand()
                {
                    PurchaseOrder = purchaseOrder
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
                var message = await Mediator.Send(new AddPurchaseOrderCommand()
                {
                    PurchaseOrder = purchaseOrder
                });

                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }

        [Route("api/Purchase/PurchaseOrderList")]
        [HttpGet("PurchaseOrderList")]
        [Roles("CanAddPurchaseInvoice", "CanViewPurchaseDraft", "CanEditPurchaseInvoice", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanEditPurchaseOrder", "CanViewServiceQuotation")]
        public async Task<IActionResult> PurchaseOrderList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, DateTime? fromDate, DateTime? toDate, string supplierId, string documentType, decimal? netAmount, Guid? supplierForFilterId, int? month, int? year, Guid? userId, string customerType, Guid? poSupplierId, Guid? branchId, Guid? contactAccountId,bool isPartially)
        {
            var purchaseOrder = await Mediator.Send(new GetPurchaseOrderListQuery
            {
                SearchTerm = searchTerm,
                DocumentType = documentType,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate,
                SupplierId = supplierId,
                Year = year,
                Month = month,
                UserId = userId,
                SupplierType = customerType,
                IsPartially = isPartially,
                SupplierForFilterId = supplierForFilterId,
                TotalAmount = netAmount,
                PoSupplierId = poSupplierId,
                SupperAccountId = contactAccountId,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/Purchase/PurchaseOrderDetail")]
        [HttpGet("PurchaseOrderDetail")]
        [Roles("CanEditPurchaseOrder", "CanViewOrderDetail")]
        public async Task<IActionResult> PurchaseOrderDetail(Guid id, bool isPoView, bool isMultiUnit,bool?isReport,string documentType,bool simpleQuery)
        {
            try
            {
                if (isReport == true)
                {
                    simpleQuery = true;
                }


                var purchaseOrder = await Mediator.Send(new GetPurchaseOrderDetailQuery()
                {
                    Id = id,
                    IsPoView = isPoView,
                    DocumentType = documentType,
                    SimpleQuery = simpleQuery,
                    IsMultiUnit = isMultiUnit
                });
                if (isReport == true)
                {
                    TLVCls tlv = new TLVCls(purchaseOrder.SupplierName, purchaseOrder.SupplierVAT == null ? "0" : purchaseOrder.SupplierVAT, Convert.ToDateTime(purchaseOrder.Date), Convert.ToDouble(purchaseOrder.TotalAmount), Convert.ToDouble(purchaseOrder.VatAmount));
                    var qrValue = tlv.ToBase64();
                    QRCodeData qrCodeData;
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    {
                        qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                    }
                    var imgType = Base64QRCode.ImageType.Jpeg;
                    var qrCode = new Base64QRCode(qrCodeData);
                    string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

                    purchaseOrder.QrCode = qrCodeImageAsBase64;
                }

                return Ok(purchaseOrder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Route("api/Purchase/SavePurchaseOrderAttachment")]
        [HttpGet("SavePurchaseOrderAttachment")]
        [Roles("CanUploadAttachment", "CanUploadExpenseAttachment", "CanUploadExpenseBillAttachment")]
        public async Task<IActionResult> SavePurchaseOrderAttachment(Guid? id, string path, DateTime? date, string description)
        {
            var result = await Mediator.Send(new AddPurchaseOrderAttachment
            {
                Id = id,
                Path = path,
                Date = date,
                Description = description,
            });
            if (result != Guid.Empty)
            {
                return Ok(new { IsSuccess = true });
            }
            return Ok(new { IsSuccess = false });
        }


        [Route("api/Purchase/PurchaseOrderAttachmentList")]
        [HttpGet("PurchaseOrderAttachmentList")]
        [Roles("CanViewOrderDetail", "CanUploadAttachment")]
        public async Task<IActionResult> PurchaseOrderAttachmentList(Guid id)
        {
            var action = await Mediator.Send(new PurchaseOrderAttachment
            {
                Id = id,
            });
            return Ok(action);
        }


        [Route("api/Purchase/PurchaseOrderPaymentList")]
        [HttpGet("PurchaseOrderPaymentList")]
        [Roles("CanViewOrderDetail", "CanAddAdvancePayment", "CanViewAdvancePayment")]
        public async Task<IActionResult> PurchaseOrderPaymentList(Guid id)
        {
            var payment = await Mediator.Send(new PurchaseOrderPaymentListQuery
            {
                Id = id,
            });
            return Ok(payment);
        }


        [Route("api/Purchase/PurchaseOrderPaymentDetail")]
        [HttpGet("PurchaseOrderPaymentDetail")]
        [Roles("CanViewDetailAdvancePayment", "CanViewDetailOrderExpense", "CanViewDetailAdvancePayment", "CanViewDetailOrderExpense")]
        public async Task<IActionResult> PurchaseOrderPaymentDetail(Guid id, bool expense)
        {
            if (expense)
            {
                var data = await Context.PurchaseOrderExpenses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return Ok(data);
            }
            else
            {
                var data = await Context.PurchaseOrderPayments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return Ok(data);
            }
        }


        [Route("api/Purchase/SaleOrderPaymentDetail")]
        [HttpGet("SaleOrderPaymentDetail")]
        [Roles("CanViewDetailServiceSaleOrder", "CanPayAdvanceFromView", "CanViewDetailSaleOrder")]
        public async Task<IActionResult> SaleOrderPaymentDetail(Guid id, bool isEmail)
        {
            if (isEmail)
                Context.DisableTenantFilter = true;
            var data = await Context.SaleOrderPayments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            Context.DisableTenantFilter = false;

            return Ok(data);
        }


        [Route("api/Purchase/PurchaseOrderExpensePaymentList")]
        [HttpGet("PurchaseOrderExpensePaymentList")]
        [Roles("CanViewOrderExpense", "CanAddOrderExpense")]
        public async Task<IActionResult> PurchaseOrderExpensePaymentList(Guid id)
        {
            var payment = await Mediator.Send(new PurchaseOrderExpenseList
            {
                Id = id,
            });
            return Ok(payment);
        }


        [Route("api/Purchase/SavePurchaseOrderAction")]
        [HttpPost("SavePurchaseOrderAction")]
        [Roles("CanAddOrderAction")]
        public async Task<IActionResult> SavePurchaseOrderAction([FromBody] ActionLookUpModel action)
        {
            var id = await Mediator.Send(new AddUpdateActionCommand
            {
                Action = action
            });
            if (id != Guid.Empty)
            {
                return Ok(new { IsSuccess = true });
            }
            return Ok(new { IsSuccess = false });
        }

        [Route("api/Purchase/PurchaseOrderActionList")]
        [HttpGet("PurchaseOrderActionList")]
        [Roles("CanViewOrderAction")]
        public async Task<IActionResult> PurchaseOrderActionList(Guid id)
        {
            var action = await Mediator.Send(new PurchaseOrderActionListQuery
            {
                Id = id,
            });
            return Ok(action);
        }
        #endregion


        #region For GRN
        [Route("api/Purchase/GoodReceiveAutoGenerateNo")]
        [HttpGet("GoodReceiveAutoGenerateNo")]
        public async Task<IActionResult> GoodReceiveAutoGenerateNo(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetGoodReceiveCode()
            {
                BranchId= branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveGoodReceiveInformation")]
        [HttpPost("SaveGoodReceiveInformation")]
        //[Roles("CanAddPurchaseOrder", "CanViewDraftOrder", "CanViewInProcessOrder", "CanEditPurchaseOrder")]
        public async Task<IActionResult> SaveGoodReceiveInformation([FromBody] GoodReceiveLookUp goodReceive)
        {
            if (goodReceive.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetGoodReceiveCode()
                {
                    BranchId = goodReceive.BranchId
                });
                goodReceive.RegistrationNo = autoNo;
                var message = await Mediator.Send(new AddUpdateGoodReceiveCommand()
                {
                    GoodReceive = goodReceive
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
                var message = await Mediator.Send(new AddUpdateGoodReceiveCommand()
                {
                    GoodReceive = goodReceive
                });

                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }

        [Route("api/Purchase/GoodReceiveList")]
        [HttpGet("GoodReceiveList")]
        //[Roles("CanAddPurchaseInvoice", "CanViewPurchaseDraft", "CanEditPurchaseInvoice", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanEditPurchaseOrder")]
        public async Task<IActionResult> GoodReceiveList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, DateTime? fromDate, DateTime? toDate, Guid? supplierId, Guid? branchId)
        {
            var purchaseOrder = await Mediator.Send(new GetGoodReceiveListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate,
                SupplierId = supplierId,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/Purchase/GoodReceiveDetail")]
        [HttpGet("GoodReceiveDetail")]
        [Roles("CanEditPurchaseOrder", "CanViewOrderDetail")]
        public async Task<IActionResult> GoodReceiveDetail(Guid id, bool isMultiUnit,bool isReport,bool isSimpleQuery)
        {
            if (isReport == true)
            {
                isSimpleQuery = true;
            }
            var purchaseOrder = await Mediator.Send(new GetGoodReceiveDetailQuery()
            {
                Id = id,
                SimpleQuery = isSimpleQuery,
                IsMultiUnit = isMultiUnit
            });
            if (isReport == true)
            {
                TLVCls tlv = new TLVCls(purchaseOrder.SupplierName, purchaseOrder.SupplierVAT == null ? "0" : purchaseOrder.SupplierVAT, Convert.ToDateTime(purchaseOrder.Date), Convert.ToDouble(purchaseOrder.TotalAmount), Convert.ToDouble(purchaseOrder.VatAmount));
                var qrValue = tlv.ToBase64();
                QRCodeData qrCodeData;
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                }
                var imgType = Base64QRCode.ImageType.Jpeg;
                var qrCode = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

                purchaseOrder.QrCode = qrCodeImageAsBase64;
            }

            return Ok(purchaseOrder);
        }














        #endregion
        #region For QuotationTemplate
        [Route("api/Purchase/QuotationTemplateAutoGenerateNo")]
        [HttpGet("QuotationTemplateAutoGenerateNo")]
        public async Task<IActionResult> QuotationTemplateAutoGenerateNo(bool IsService)
        {
            var autoNo = await Mediator.Send(new GetQuotationTemplateCode()
            {
                IsService = IsService
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveQuotationTemplateInformation")]
        [HttpPost("SaveQuotationTemplateInformation")]
        public async Task<IActionResult> SaveQuotationTemplateInformation([FromBody] QuotationTemplateLookUp quotationTemplate)
        {
            if (quotationTemplate.Id == Guid.Empty)
            {

                var message = await Mediator.Send(new AddUpdateQuotationTemplateCommand()
                {
                    QuotationTemplate = quotationTemplate
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
                var message = await Mediator.Send(new AddUpdateQuotationTemplateCommand()
                {
                    QuotationTemplate = quotationTemplate
                });

                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }

        [Route("api/Purchase/QuotationTemplateList")]
        [HttpGet("QuotationTemplateList")]
        public async Task<IActionResult> QuotationTemplateList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, bool isService)
        {
            var purchaseOrder = await Mediator.Send(new GetQuotationTemplateListQuery
            {
                IsService = isService,
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/Purchase/QuotationTemplateDetail")]
        [HttpGet("QuotationTemplateDetail")]
        public async Task<IActionResult> QuotationTemplateDetail(Guid id)
        {
            var purchaseOrder = await Mediator.Send(new GetQuotationTemplateDetailQuery()
            {
                Id = id
            });
            return Ok(purchaseOrder);
        }
        #endregion
        #region For DeliveryChallan
        [Route("api/Purchase/DeliveryChallanAutoGenerateNo")]
        [HttpGet("DeliveryChallanAutoGenerateNo")]
        public async Task<IActionResult> DeliveryChallanAutoGenerateNo(bool IsService, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetDeliveryChallanCode()
            {
                IsService = IsService,
                BranchId = branchId,
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveDeliveryChallanInformation")]
        [HttpPost("SaveDeliveryChallanInformation")]
        public async Task<IActionResult> SaveDeliveryChallanInformation([FromBody] DeliveryChallanLookUp deliveryChallan)
        {
            if (deliveryChallan.Id == Guid.Empty)
            {

                var message = await Mediator.Send(new AddUpdateDeliveryChallanCommand()
                {
                    DeliveryChallan = deliveryChallan
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
                var message = await Mediator.Send(new AddUpdateDeliveryChallanCommand()
                {
                    DeliveryChallan = deliveryChallan
                });

                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }

        [Route("api/Purchase/DeliveryChallanList")]
        [HttpGet("DeliveryChallanList")]
        public async Task<IActionResult> DeliveryChallanList(string searchTerm, int? pageNumber, DateTime? fromDate, DateTime? toDate, Guid? userId, int? month, int? year, ApprovalStatus status, bool isDropdown, bool isSale, Guid documentId, Guid? customerId, bool IsService, Guid? branchId)
        {
            var purchaseOrder = await Mediator.Send(new GetDeliveryChallanListQuery
            {
                IsSale = isSale,
                CustomerId = customerId,
                SearchTerm = searchTerm,
                Status = status,
                IsService = IsService,
                IsDropDown = isDropdown,
                DocumentId = documentId,
                BranchId = branchId,
                PageNumber = pageNumber ?? 1,
                Year = year,
                Month = month,
                FromDate = fromDate,
                ToDate = toDate,
                UserId = userId,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/Purchase/DeliveryChallanDetail")]
        [HttpGet("DeliveryChallanDetail")]
        public async Task<IActionResult> DeliveryChallanDetail(Guid id, bool isSale, bool isReserved, bool manualClose, bool isDeliveryChallan,bool isSimpleQuery,bool isReport)
        {
            if (isReport == true)
            {
                isSimpleQuery = true;
            }
            var purchaseOrder = await Mediator.Send(new GetDeliveryChallanDetailQuery()
            {
                Id = id,
                IsSale = isSale,
                IsReserved = isReserved,
                IsSmpleQuery = isSimpleQuery,
                ManualClose = manualClose,
                IsDeliveryChallan = isDeliveryChallan,
            });
            return Ok(purchaseOrder);
        }
        #endregion


        #region For MobileOrder
        [Route("api/Purchase/MobileOrderAutoGenerateNo")]
        [HttpGet("MobileOrderAutoGenerateNo")]
        [Roles("Can Save  Mobile Order", "Noble Admin")]
        public async Task<IActionResult> MobileOrderAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetMobileOrderRegistrationNoQuery());
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveMobileOrderInformation")]
        [HttpPost("SaveMobileOrderInformation")]
        [Roles("Can Save  Mobile Order", "Noble Admin")]
        public async Task<IActionResult> SaveMobileOrderInformation([FromBody] MobileOrderLookupModel mobileOrder)
        {
            var id = await Mediator.Send(new AddMobileOrderCommand()
            {
                mobileOrder = mobileOrder
            });

            return Ok(new { IsSuccess = true });
        }

        [Route("api/Purchase/MobileOrderList")]
        [HttpGet("MobileOrderList")]
        [Roles("Can View   Mobile Order", "Noble Admin")]
        public async Task<IActionResult> MobileOrderList(string searchTerm, int? pageNumber, Guid CustomerId)
        {
            var mobileOrder = await Mediator.Send(new GetMobileOrderListQuery
            {
                SearchTerm = searchTerm,
                CustomerId = CustomerId,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(mobileOrder);
        }
        [Route("api/Purchase/MobileOrderDetail")]
        [HttpGet("MobileOrderDetail")]
        [Roles("Can Save  Mobile Order", "Can Edit  Mobile Order", "Noble Admin")]

        public async Task<IActionResult> MobileOrderDetail(Guid id)
        {
            var mobileOrder = await Mediator.Send(new GetMobileOrderDetailQuery()
            {
                Id = id
            });
            return Ok(mobileOrder);
        }


        #endregion

        #region Stock Received

        [Route("api/Purchase/GetStockReceivedAutoCode")]
        [HttpGet("GetStockReceivedAutoCode")]
        public async Task<IActionResult> GetStockReceivedAutoCode(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetStockReceivedAutoCodeQuery()
            {
                BranchId = branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveStockReceivedInformation")]
        [HttpPost("SaveStockReceivedInformation")]
        public async Task<IActionResult> SaveStockReceivedInformation([FromBody] StockReceivedLookupModel stockReceived)
        {
            var stockReq = await Mediator.Send(new StockReceivedAddUpdateCommand()
            {
                StockReceived = stockReceived
            });
            return Ok(stockReq);
        }

        [Route("api/Purchase/GetStockReceivedList")]
        [HttpGet("GetStockReceivedList")]
        public async Task<IActionResult> GetStockReceivedList(ApprovalStatus status, string searchTerm, int? pageNumber, bool IsDropDown, DateTime? fromDate, DateTime? toDate, Guid? branchId, bool isMainBranch)
        {
            var wareHouseTransfer = await Mediator.Send(new StockReceivedListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                IsDropDown = IsDropDown,
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId,
                Status = status,
            });

            return Ok(wareHouseTransfer);
        }

        [Route("api/Purchase/GetStockReceivedDetails")]
        [HttpGet("GetStockReceivedDetails")]
        public async Task<IActionResult> GetStockReceivedDetails(Guid id, Guid? branchId)
        {
            var wareHouseTransfer = await Mediator.Send(new GetStockReceivedDetailsQuery()
            {
                Id = id,
                BranchId = branchId
            });
            return Ok(wareHouseTransfer);
        }


        #endregion

        #region Stock Transfer

        [Route("api/Purchase/GetStockTransferAutoNumber")]
        [HttpGet("GetStockTransferAutoNumber")]
        public async Task<IActionResult> GetStockTransferAutoNumber(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new StockTransferAutoNumber()
            {
                BranchId = branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveStockTransfertInformation")]
        [HttpPost("SaveStockTransfertInformation")]
        public async Task<IActionResult> SaveStockTransfertInformation([FromBody] StockTransferLookupModel stockTransfer)
        {
            var stockReq = await Mediator.Send(new StockTransferAddUpdateCommand()
            {
                StockTransfer = stockTransfer
            });
            return Ok(stockReq);
        }

        [Route("api/Purchase/GetStockTransferList")]
        [HttpGet("GetStockTransferList")]
        public async Task<IActionResult> GetStockTransferList(ApprovalStatus status, string searchTerm, int? pageNumber, bool IsDropDown, DateTime? fromDate, DateTime? toDate, Guid? branchId, bool isMainBranch)
        {
            var wareHouseTransfer = await Mediator.Send(new GetStockTransferListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                IsDropDown = IsDropDown,
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId,
                Status = status,
                IsMainBranch = isMainBranch
            });

            return Ok(wareHouseTransfer);
        }

        [Route("api/Purchase/GetStockTransferDetails")]
        [HttpGet("GetStockTransferDetails")]
        public async Task<IActionResult> GetStockTransferDetails(Guid id, Guid? branchId, bool isStockReceived)
        {
            var wareHouseTransfer = await Mediator.Send(new GetStockTransferDetailsQuery()
            {
                Id = id,
                BranchId = branchId,
                IsStockReceived = isStockReceived
            });
            return Ok(wareHouseTransfer);
        }
        
        [Route("api/Purchase/GetStockRequestForStockTransfer")]
        [HttpGet("GetStockRequestForStockTransfer")]
        public async Task<IActionResult> GetStockRequestForStockTransfer(Guid id, Guid? branchId)
        {
            var wareHouseTransfer = await Mediator.Send(new GetStockRequestDetailsForStockTransfer()
            {
                Id = id,
                BranchId = branchId
            });
            return Ok(wareHouseTransfer);
        }

        #endregion

        #region Stock Requests
        [Route("api/Purchase/GetStockRequestAutoNumber")]
        [HttpGet("GetStockRequestAutoNumber")]
        public async Task<IActionResult> GetStockRequestAutoNumber(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetStockRequestAutoNumberQuery()
            {
                BranchId = branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveStockRequestInformation")]
        [HttpPost("SaveStockRequestInformation")]
        public async Task<IActionResult> SaveStockRequestInformation([FromBody] StockRequestLookupModel stockRequest)
        {
            var stockReq = await Mediator.Send(new StockRequestAddUpdateCommand()
            {
                StockRequest = stockRequest
            });
            return Ok(stockReq);
        }
        [Route("api/Purchase/GetStockRequestDetails")]
        [HttpGet("GetStockRequestDetails")]
        public async Task<IActionResult> GetStockRequestDetails(Guid id, Guid? branchId)
        {
            var wareHouseTransfer = await Mediator.Send(new GetStockRequestDetailsQuery()
            {
                Id = id,
                BranchId = branchId
            });
            return Ok(wareHouseTransfer);
        }
        [Route("api/Purchase/GetStockRequestList")]
        [HttpGet("GetStockRequestList")]
        public async Task<IActionResult> GetStockRequestList(ApprovalStatus status, string searchTerm, int? pageNumber, bool IsDropDown, DateTime? fromDate, DateTime? toDate, Guid? branchId,bool isStockTransfer, bool isStockReceived)
        {
            var wareHouseTransfer = await Mediator.Send(new GetStockRequestListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                IsDropDown = IsDropDown,
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId,
                Status= status,
                IsStockTransfer = isStockTransfer,
                IsStockReceived = isStockReceived
            });

            return Ok(wareHouseTransfer);
        }
        #endregion

        #region For WareHouseTranfer
        [Route("api/Purchase/WareHouseTranferAutoGenerateNo")]
        [HttpGet("WareHouseTranferAutoGenerateNo")]
        [Roles("CanDraftStockTransfer", "CanAddStockTransfer")]
        public async Task<IActionResult> WareHouseTransferAutoGenerateNo(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetWareHouseTransferRegistrationNoQuery()
            {
                BranchId= branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveWareHouseTransferInformation")]
        [HttpPost("SaveWareHouseTransferInformation")]
        [Roles("CanAddStockTransfer", "CanEditStockTransfer", "CanDraftStockTransfer")]
        public async Task<IActionResult> SaveWareHouseTransferInformation([FromBody] WareHouseTransferLookupModel wareHouseTransfer)
        {
            if (wareHouseTransfer.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetWareHouseTransferRegistrationNoQuery(){BranchId = wareHouseTransfer.BranchId});
                wareHouseTransfer.Code = autoNo;
                await Mediator.Send(new AddWareHouseTransferCommand()
                {
                    WareHouseTransfer = wareHouseTransfer
                });
            }
            else
            {
                await Mediator.Send(new AddWareHouseTransferCommand()
                {
                    WareHouseTransfer = wareHouseTransfer
                });
            }

            return Ok(new { IsSuccess = true });
        }

        [Route("api/Purchase/WareHouseTransferDetail")]
        [Roles("CanEditStockTransfer")]
        [HttpGet("WareHouseTransferDetail")]
        [Roles("CanEditStockTransfer")]
        public async Task<IActionResult> WareHouseTransferDetail(Guid id)
        {
            var wareHouseTransfer = await Mediator.Send(new WareHouseTransferDetailQuery()
            {
                Id = id
            });
            return Ok(wareHouseTransfer);
        }

        [Route("api/Purchase/WareHouseTransferList")]
        [HttpGet("WareHouseTransferList")]
        [Roles("CanViewStockTransfer", "CanDraftStockTransfer")]
        public async Task<IActionResult> WareHouseTransferList(ApprovalStatus status, string searchTerm, int? pageNumber, bool IsDropDown, DateTime? fromDate, DateTime? toDate, Guid? branchId)
        {
            var wareHouseTransfer = await Mediator.Send(new GetWareHouseTransferListQuery
            {
                Status = status,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                IsDropDown = IsDropDown,
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId,
            });

            return Ok(wareHouseTransfer);
        }
        [Route("api/Purchase/DeleteWareHouseTransfer")]
        [HttpGet("DeleteWareHouseTransfer")]
        [Roles("Can Delete Stock Transfer", "Noble Admin")]
        public async Task<IActionResult> DeleteWareHouseTransfer(Guid id)
        {
            var message = await Mediator.Send(new DeleteWareHouseTransferCommand
            {
                Id = id
            });
            return Ok(new { message });
        }



        #endregion

        #region For SaleOrder

        [Route("api/Report/SaleOrderTrackingReport")]
        [HttpGet("SaleOrderTrackingReport")]
        public async Task<IActionResult> SupplierPurchaseReport(Guid userId, DateTime fromDate, DateTime toDate, bool isMultiUnit)
        {
            var list = await Mediator.Send(new SaleOrderTrackingReportQuery()
            {
                UserId = userId,
                FromDate = fromDate,
                ToDate = toDate,
                IsMultiUnit = isMultiUnit,
            });
            return Ok(list);
        }



        [Route("api/Purchase/SaleOrderAutoGenerateNo")]
        [HttpGet("SaleOrderAutoGenerateNo")]
        [Roles("CanAddServiceQuotation", "CanDraftServiceQuotation", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddSaleOrder", "CanDraftSaleOrder", "CanDuplicateSaleOrder", "CanAddSaleOrderTracking", "CanDraftSaleOrderTracking", "CanDuplicateSaleOrderTracking", "CanDraftQuotation", "CanAddQuotation")]
        public async Task<IActionResult> SaleOrderAutoGenerateNo(bool isQuotation, string quotationType, Guid? terminalId, string invoicePrefix, bool isSaleOrderTracking, Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetSaleOrderRegistrationNoQuery()
            {
                IsQuotation = isQuotation,
                QuotationType = quotationType,
                TerminalId = terminalId,
                InvoicePrefix = invoicePrefix,
                IsSaleOrderTracking = isSaleOrderTracking,
                BranchId = branchId,

            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveSaleOrderInformation")]
        [HttpPost("SaveSaleOrderInformation")]
        [Roles("CanCloseSaleOrder", "CanAddSaleOrder", "CanDraftSaleOrder", "CanEditSaleOrder", "CanCloseSaleOrderTracking", "CanAddSaleOrderTracking", "CanDraftSaleOrderTracking", "CanEditSaleOrderTracking", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddServiceQuotation", "CanEditServiceQuotation", "CanDraftServiceQuotation")]
        public async Task<IActionResult> SaveSaleOrderInformation([FromBody] SaleOrderLookupModel saleOrder)
        {
            if (saleOrder.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetSaleOrderRegistrationNoQuery()
                {
                    IsQuotation = saleOrder.IsQuotation,
                    TerminalId = saleOrder.TerminalId,
                    InvoicePrefix = saleOrder.InvoicePrefix,
                    IsSaleOrderTracking = saleOrder.IsSaleOrderTracking
                });

                saleOrder.RegistrationNo = autoNo.DocumentNo;
                var id = await Mediator.Send(new AddSaleOrderCommand()
                {
                    SaleOrder = saleOrder
                });
                return Ok(id);

            }
            else
            {
                var id = await Mediator.Send(new AddSaleOrderCommand()
                {
                    SaleOrder = saleOrder
                });
                return Ok(id);

            }

        }

        [Route("api/Purchase/SaleOrderList")]
        [HttpGet("SaleOrderList")]
        [Roles("CanViewQuotation", "CanDraftQuotation", "CanDraftSaleOrder", "CanDraftSaleOrderTracking", "CanViewSaleOrder", "CanViewSaleOrderTracking", "CanAddSaleOrder", "CanAddSaleOrderTracking", "CanDraftSaleOrder", "CanDraftSaleOrderTracking", "CanEditSaleOrder", "CanEditSaleOrderTracking", "CanDuplicateSaleOrder", "CanDuplicateSaleOrderTracking", "CreditServiceInvoices", "CashServiceInvoices", "CanHoldServiceInvoices", "CanEditHoldServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices", "CanAddProductionBatch", "CanAddDispatchNote", "CanAddPurchaseOrder","Simple")]

        public async Task<IActionResult> SaleOrderList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, bool isQuotation, bool isSaleOrderTracking,bool isForBom, Guid? customerId, Guid? branchId, Guid? contactAccountId)
        {
            var saleOrder = await Mediator.Send(new GetSaleOrderListQuery
            {
                SearchTerm = searchTerm,
                CustomerId = customerId,
                Status = status,
                IsDropDown = isDropdown,
                IsQuotation = isQuotation,
                PageNumber = pageNumber ?? 1,
                IsService = false,
                IsSaleOrderTracking = isSaleOrderTracking,
                BranchId = branchId,
                ContactAccountId = contactAccountId,
                IsForBom = isForBom
            });

            return Ok(saleOrder);
        }

        [Route("api/Purchase/SaleServiceOrderList")]
        [HttpGet("SaleServiceOrderList")]
        //[Roles("CanDraftServiceQuotation", "CanViewServiceQuotation", "CanViewServiceSaleOrder", "CanDraftServiceSaleOrder", "CanAddSaleOrder", "CanDraftSaleOrder", "CanEditSaleOrder", "CanDuplicateSaleOrder", "CreditServiceInvoices", "CashServiceInvoices", "CanHoldServiceInvoices", "CanEditHoldServiceInvoices", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices", "CanAddProductionBatch", "CanAddDispatchNote", "CanAddPurchaseOrder", "CanEditPurchaseOrder","Simple")]

        public async Task<IActionResult> SaleServiceOrderList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, bool isQuotation,Guid? customerId,bool isService, DateTime? fromDate, DateTime? toDate, Guid? contactId, DateTime? fromTime, DateTime? toTime, Guid? userId, int? month, int? year, Guid? branchId,Guid? contactAccountId,bool isPartially)
        {
            var saleOrder = await Mediator.Send(new GetSaleOrderListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                FromDate = fromDate,
                ToDate = toDate,
                CustomerId = customerId,
                UserId = userId,
                IsPartially = isPartially,
                Year = year,
                Month = month,
                IsDropDown = isDropdown,
                IsQuotation = isQuotation,
                PageNumber = pageNumber ?? 1,
                IsService = isService,
                BranchId = branchId,
                ContactAccountId = contactAccountId,
            });

            return Ok(saleOrder);
        }



        [Route("api/Purchase/SaleOrderDetail")]
        [HttpGet("SaleOrderDetail")]
        [Roles("CanEditServiceQuotation", "CanViewServiceQuotationDetail", "CanPrintServiceQuotation", "CanSendSaleEmailAsLink", "CanSendSaleEmailAsPdf", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanViewDetailServiceSaleOrder", "CanPrintServiceSaleOrder", "CanEditSaleOrder", "CanDuplicateSaleOrder", "CanViewDetailSaleOrder", "CanPrintSaleOrder", "CanEditQuotation", "CanViewQuotationDetail", "CanPrintQuotation", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanPrintSaleOrder", "CreditServiceInvoices", "CashServiceInvoices", "CanHoldServiceInvoices", "CanEditHoldServiceInvoices", "CanPrintServiceInvoices", "CanAddSaleOrder", "CanDraftSaleOrder", "CanEditSaleOrder", "CanDuplicateSaleOrder", "CanPrintSaleOrder", "CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices", "CanPrintInvoices", "CanAddDispatchNote", "CanAddPurchaseOrder")]
        public async Task<IActionResult> SaleOrderDetail(Guid id, bool isFifo, int openBatch, bool isEmail, bool deliveryChallan, bool? isReport,bool isSimpleQuery)
        {
            try
            {
                if (isReport == true)
                {
                    isSimpleQuery = true;
                }
                var saleOrder = await Mediator.Send(new GetSaleOrderDetailQuery()
                {
                    Id = id,
                    IsFifo = isFifo,
                    OpenBatch = openBatch,
                    IsSimpleQuery = isSimpleQuery,
                    DeliveryChallan = deliveryChallan,
                    IsEmail = isEmail
                });
                if (isReport == true)
                {
                    //QrCode
                    TLVCls tlv = new TLVCls(saleOrder.CustomerName, saleOrder.CustomerVat == null ? "0" : saleOrder.CustomerVat, Convert.ToDateTime(saleOrder.Date), Convert.ToDouble(saleOrder.TotalAmount), Convert.ToDouble(saleOrder.VatAmount));
                    var qrValue = tlv.ToBase64();
                    QRCodeData qrCodeData;
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    {
                        qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                    }
                    var imgType = Base64QRCode.ImageType.Jpeg;
                    var qrCode = new Base64QRCode(qrCodeData);
                    string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

                    //BarCode
                    if (saleOrder.BarCode != "" && saleOrder.BarCode !=null)
                    {
                        string invoiceBarcode;
                        BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
                        Image img = barcode.Encode(BarcodeLib.TYPE.UPCA, saleOrder.BarCode, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 45);
                        using (var ms = new MemoryStream())
                        {
                            using (var bitmap = new Bitmap(img))
                            {
                                bitmap.Save(ms, ImageFormat.Jpeg);
                                invoiceBarcode = Convert.ToBase64String(ms.GetBuffer());
                            }
                        }

                        saleOrder.BarCode = invoiceBarcode;
                    }
                    saleOrder.QRCode = qrCodeImageAsBase64;
                }
                return Ok(saleOrder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("api/Purchase/SaleOrderHistoryDetail")]
        [HttpGet("SaleOrderHistoryDetail")]
        public async Task<IActionResult> SaleOrderHistoryDetail(Guid id, bool isFifo, int openBatch, bool isEmail)
        {
            var saleOrder = await Mediator.Send(new GetSaleOrderHistoryDetail()
            {
                Id = id,
                IsFifo = isFifo,
                OpenBatch = openBatch,
                IsEmail = isEmail
            });
            return Ok(saleOrder);
        }

        [Route("api/Purchase/SaleOrderPaymentList")]
        [HttpGet("SaleOrderPaymentList")]
        [Roles("CanPayAdvanceFromView")]
        public async Task<IActionResult> SaleOrderPaymentList(Guid id, bool isEmail)
        {
            var payment = await Mediator.Send(new SaleOrderPaymentQuery
            {
                Id = id,
                IsEmail = isEmail
            });
            return Ok(payment);
        }
        [Route("api/Purchase/SaleInvoicePaymentList")]
        [HttpGet("SaleInvoicePaymentList")]
        [Roles("CreditServiceInvoices", "CashServiceInvoices", "CanHoldServiceInvoices", "CanEditHoldServiceInvoices", "CreditInvoices", "CashInvoices", "CanHoldInvoices", "CanEditHoldInvoices")]
        public async Task<IActionResult> SaleInvoicePaymentList(Guid? id)
        {
            var payment = await Mediator.Send(new GetSaleListQueryAmount
            {
                Id = id,
            });
            return Ok(payment);
        }
        #endregion


        #region For PurchaseBill
        [Route("api/Purchase/PurchaseBillAutoGenerateNo")]
        [HttpGet("PurchaseBillAutoGenerateNo")]
        [Roles("CanDraftExpenseBill", "CanAddExpenseBill")]
        public async Task<IActionResult> PurchaseBillAutoGenerateNo(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetPurchaseBillRegistrationNoQuery(
                )
            {
                BranchId = branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SavePurchaseBillInformation")]
        [HttpPost("SavePurchaseBillInformation")]
        [Roles("CanDraftExpenseBill", "CanAddExpenseBill", "CanEditExpenseBill")]
        public async Task<IActionResult> SavePurchaseBillInformation([FromBody] PurchaseBillLookupModel purchaseBill)
        {
            var id = await Mediator.Send(new AddPurchaseBillCommand()
            {
                PurchaseBill = purchaseBill
            });

            return Ok(new { IsSuccess = true });
        }

        [Route("api/Purchase/PurchaseBillList")]
        [HttpGet("PurchaseBillList")]
        [Roles("CanDraftExpenseBill", "CanViewExpenseBill", "CanAddOrderExpense")]
        public async Task<IActionResult> PurchaseBillList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropDown, Guid? branchId, DateTime? fromDate, DateTime? toDate, string reference, decimal amount, Guid? billerId, string paymentStatus)
        {
            var mobileOrder = await Mediator.Send(new GetPurchaseBillListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1,
                BranchId = branchId,
                ToDate = toDate,
                FromDate = fromDate,
                BillerId = billerId,
                Amount = amount,
                Reference = reference,
                PaymentStatus = paymentStatus,
            });

            return Ok(mobileOrder);
        }
        [Route("api/Purchase/PurchaseBillDetail")]
        [HttpGet("PurchaseBillDetail")]
        [Roles("CanEditExpenseBill", "CanViewExpenseBill", "CanViewDetailExpenseBill", "CanPrintExpenseBill")]
        public async Task<IActionResult> PurchaseBillDetail(Guid id, bool isPayement)
        {
            var mobileOrder = await Mediator.Send(new GetPurchaseBillDetailQuery()
            {
                Id = id,
                IsPayement = isPayement
            });
            return Ok(mobileOrder);
        }


        #endregion

        #region Auto Purchase Order Setting
        [Route("api/Purchase/AutoPurchaseAutoGenerateNo")]
        [HttpGet("AutoPurchaseAutoGenerateNo")]
        [Roles("CanAddAutoTemplate")]
        public async Task<IActionResult> AutoPurchaseAutoGenerateNo(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new AutoPurchaseAutoNoQuery()
            {
                BranchId = branchId
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveAutoPurchaseTemplate")]
        [HttpPost("SaveAutoPurchaseTemplate")]
        [Roles("CanAddAutoTemplate", "CanEditAutoTemplate")]
        public async Task<IActionResult> SaveAutoPurchaseTemplate([FromBody] PurchaseTemplateLookUpModel purchase)
        {
            if (purchase.Id == Guid.Empty)
            {
                var message = await Mediator.Send(new AddPurchaseTemplateCommand()
                {
                    PurchaseTemplate = purchase

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
                var message = await Mediator.Send(new AddPurchaseTemplateCommand()
                {
                    PurchaseTemplate = purchase
                });
                if (message.Id != Guid.Empty)
                {
                    return Ok(new { message = message, Action = "Update" });

                }
                else
                {
                    return Ok(new { Message = message, Action = "Error" });
                }
            }
        }

        [Route("api/Purchase/PurchaseTemplateList")]
        [HttpGet("PurchaseTemplateList")]
        [Roles("CanViewAutoTemplate", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanEditPurchaseOrder")]
        public async Task<IActionResult> PurchaseTemplateList(string searchTerm, int? pageNumber, bool isDropdown, DateTime? fromDate, DateTime? toDate, Guid? branchId)
        {
            var purchaseOrder = await Mediator.Send(new AutoPurchaseTemplateListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                FromDate = fromDate,
                ToDate = toDate,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/Purchase/PurchaseTemplateDetail")]
        [HttpGet("PurchaseTemplateDetail")]
        [Roles("CanEditAutoTemplate", "CanPrintAutoTemplate", "CanViewDetailAutoTemplate", "CanViewDraftOrder", "CanViewInProcessOrder", "CanAddPurchaseOrder", "CanEditPurchaseOrder")]
        public async Task<IActionResult> PurchaseTemplateDetail(Guid id, bool isMultiUnit)
        {
            var purchaseOrder = await Mediator.Send(new AutoPurchaseTemplateDetail()
            {
                Id = id,
                IsMultiUnit = isMultiUnit
            });
            return Ok(purchaseOrder);
        }

        [Route("api/Purchase/AutoPurchaseOrderSetting")]
        [HttpPost("AutoPurchaseOrderSetting")]
        public async Task<IActionResult> AutoPurchaseOrderSetting([FromBody] AutoPurchaseSettingLookUp autoPurchaseSetting)
        {
            var result = await Mediator.Send(new AutoPurchaseSettingCommand
            {
                AutoPurchaseSetting = autoPurchaseSetting,
            });
            return Ok(result);
        }

        [Route("api/Purchase/AutoPurchaseOrderSettingDetail")]
        [HttpGet("AutoPurchaseOrderSettingDetail")]
        public async Task<IActionResult> AutoPurchaseOrderSettingDetail()
        {
            var result = await Mediator.Send(new AutoPurchaseDetailQuery());
            return Ok(result);
        }
        #endregion
    }
}
