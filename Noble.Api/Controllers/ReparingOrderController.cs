using Focus.Business.MultiUps;
using Focus.Business.MultiUps.Commands.AddMultiUp;
using Focus.Business.MultiUps.Queries;
using Focus.Business.ReparingOrders;
using Focus.Business.ReparingOrders.AdvancePayment;
using Focus.Business.ReparingOrders.Commands.AddReparingOrder;
using Focus.Business.ReparingOrders.Queries;
using Focus.Business.ReparingOrderTypes.Commands;
using Focus.Business.ReparingOrderTypes.Queries;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Threading.Tasks;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparingOrderController : BaseController
    {
        #region For ReparingOrderType


        [Route("api/ReparingOrder/SaveReparingOrderTypeInformation")]
        [HttpPost("SaveReparingOrderTypeInformation")]
        [Roles("CanAddWarrantyCategory", "CanEditWarrantyCategory", "CanEditDescription", "CanEditProblem", "CanEditAccessory", "CanAddDescription", "CanAddProblem", "CanAddAccessory", "CanAddReparingOrder", "CanEditReparingOrder")]
        public async Task<IActionResult> SaveReparingOrderTypeInformation([FromBody] ReparingOrderTypeLookUpModel reparingOrderType)
        {
            if (reparingOrderType.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateReparingOrderType()
                {
                    ReparingOrdertType = reparingOrderType
                });
                return Ok(new { id = id, isSuccess=true });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateReparingOrderType()
                {
                    ReparingOrdertType = reparingOrderType
                });
                return Ok(new { id = id, isSuccess = true });

            }


        }

        [Route("api/ReparingOrder/ReparingOrderTypeList")]
        [HttpGet("ReparingOrderTypeList")]
        [Roles("CanViewWarrantyCategory", "CanViewDescription", "CanViewProblem", "CanViewAccessory", "CanAddReparingOrder", "CanEditReparingOrder")]
        public async Task<IActionResult> ReparingOrderTypeList(string searchTerm, int? pageNumber, bool isDropdown, ReparingOrderTypeEnum ReparingOrderTypes, Guid? branchId)
        {
            var result = await Mediator.Send(new ReparingOrderTypeListQuery
            {
                SearchTerm = searchTerm,
                RepairingOrderTypes = ReparingOrderTypes,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                BranchId = branchId,
            });

            return Ok(result);
        }

        [Route("api/ReparingOrder/ReparingOrderTypeDetail")]
        [HttpGet("ReparingOrderTypeDetail")]
        [Roles("CanEditWarrantyCategory", "CanEditDescription", "CanEditProblem", "CanEditAccessory")]
        public async Task<IActionResult> ReparingOrderTypeDetail(Guid id)
        {
            var reparingOrderType = await Mediator.Send(new GetReparingOrderTypeDetailQuery()
            {
                Id = id
            });
            return Ok(reparingOrderType);

        }

        #endregion


        #region For ReparingOrder
        [Route("api/ReparingOrder/ReparingOrderAutoGenerateNo")]
        [HttpGet("ReparingOrderAutoGenerateNo")]
        //[Roles("CanAddReparingOrder", "CanEditReparingOrder")]

        public async Task<IActionResult> ReparingOrderAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetReparingOrderCode());
            return Ok(autoNo);
        }

        [Route("api/ReparingOrder/SaveReparingOrderInformation")]
        [HttpPost("SaveReparingOrderInformation")]
        [Roles("CanAddReparingOrder", "CanEditReparingOrder")]
        public async Task<IActionResult> SaveReparingOrderInformation([FromBody] ReparingOrderLookUp goodReceive)
        {
            if (goodReceive.Type == "Add")
            {
                var message = await Mediator.Send(new AddUpdateReparingOrderCommand()
                {
                    ReparingOrder = goodReceive
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
                var message = await Mediator.Send(new AddUpdateReparingOrderCommand()
                {
                    ReparingOrder = goodReceive
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

        [Route("api/ReparingOrder/ReparingOrderList")]
        [HttpGet("ReparingOrderList")]
        //[Roles("CanViewReparingOrder")]
        public async Task<IActionResult> ReparingOrderList(string searchTerm, string mobileNo, int? pageNumber, bool isDropdown, DateTime? fromDate, DateTime? toDate,string status,Guid? branchId)
        {
           
            var purchaseOrder = await Mediator.Send(new GetReparingOrderListQuery
            {
                SearchTerm = searchTerm,
                MobileNoSearch = mobileNo,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                Status = status,
                FromDate = fromDate,
                PageSize = 50,
                ToDate = toDate,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/ReparingOrder/ReparingOrderDetail")]
        [HttpGet("ReparingOrderDetail")]
        [Roles("CanEditReparingOrder")]
        public async Task<IActionResult> ReparingOrderDetail(Guid id)
        {
            var purchaseOrder = await Mediator.Send(new GetReparingOrderDetailQuery()
            {
                Id = id,
            });
            return Ok(purchaseOrder);
        }

        [Route("api/ReparingOrder/AdvancePaymentListQuery")]
        [HttpGet("AdvancePaymentListQuery")]
        [Roles("CanAddReparingOrder", "CanEditReparingOrder", "CanViewReparingOrder")]
        public async Task<IActionResult> AdvancePaymentListQuery(Guid id)
        {
            var purchaseOrder = await Mediator.Send(new AdvancePaymentListQuery()
            {
                Id = id,
            });
            return Ok(purchaseOrder);
        }










        #endregion
        #region For MultiUps
        [Route("api/ReparingOrder/MultiUpsAutoGenerateNo")]
        [HttpGet("MultiUpsAutoGenerateNo")]
        public async Task<IActionResult> MultiUpsAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetMultiUpCode());
            return Ok(autoNo);
        }

        [Route("api/ReparingOrder/SaveMultiUpsInformation")]
        [HttpPost("SaveMultiUpsInformation")]
        public async Task<IActionResult> SaveMultiUpsInformation([FromBody] MultiUpLookUp goodReceive)
        {
            if (goodReceive.Type == "Add")
            {
                var message = await Mediator.Send(new AddUpdateMultiUpCommand()
                {
                    MultiUp = goodReceive
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
                var message = await Mediator.Send(new AddUpdateMultiUpCommand()
                {
                    MultiUp = goodReceive
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

        [Route("api/ReparingOrder/MultiUpsList")]
        [HttpGet("MultiUpsList")]
        public async Task<IActionResult> MultiUpsList(string searchTerm, string mobileNo, int? pageNumber, bool isDropdown, DateTime? fromDate, DateTime? toDate, string status, Guid? branchId)
        {

            var purchaseOrder = await Mediator.Send(new GetMultiUpListQuery
            {
                SearchTerm = searchTerm,
                MobileNoSearch = mobileNo,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                Status = status,
                FromDate = fromDate,
                PageSize = 500,
                ToDate = toDate,
                BranchId = branchId,
            });

            return Ok(purchaseOrder);
        }

        [Route("api/ReparingOrder/MultiUpsDetail")]
        [HttpGet("MultiUpsDetail")]
        public async Task<IActionResult> MultiUpsDetail(Guid id,string isPrint,DateTime date)
        {
            var purchaseOrder = await Mediator.Send(new GetMultiUpDetailQuery()
            {
                Id = id,
                IsPrint = isPrint,
                dateTime = date,
            });
            return Ok(purchaseOrder);
        }

      









        #endregion

    }
}
