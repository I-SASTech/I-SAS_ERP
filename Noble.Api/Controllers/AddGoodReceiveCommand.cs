using Focus.Business.PurchaseOrders;
using MediatR;

namespace Noble.Api.Controllers
{
    internal class AddGoodReceiveCommand : IRequest<object>
    {
        public PurchaseOrderLookupModel PurchaseOrder { get; set; }
    }
}