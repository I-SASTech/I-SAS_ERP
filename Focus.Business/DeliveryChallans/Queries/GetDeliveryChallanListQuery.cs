using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.StocksTransfer.Models;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DeliveryChallans.Queries
{
    public class GetDeliveryChallanListQuery : PagedRequest, IRequest<DeliveryChallanListLookUpModel>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public Guid DocumentId { get; set; }
        public Guid? CustomerId { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsSale { get; set; }
        public bool IsService { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }
        
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public Guid? UserId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public class Handler : IRequestHandler<GetDeliveryChallanListQuery, DeliveryChallanListLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context, ILogger<GetDeliveryChallanListQuery> logger, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _contextProvider = contextProvider;
            }
            public async Task<DeliveryChallanListLookUpModel> Handle(GetDeliveryChallanListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    

                    if (request.IsDropDown)
                    {
                        if (request.IsSale)
                        {
                            var po = _context.DeliveryChallans
                           .AsNoTracking()
                           .Include(x => x.SaleInvoice)
                           .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.SaleInvoiceId == request.DocumentId)
                           .AsQueryable();

                            if (request.BranchId != null)
                            {
                                po = po.Where(x => x.BranchId == request.BranchId);
                            }


                            var reservedChallan = _context.DeliveryChallanReserveds.AsNoTracking().FirstOrDefault(x => x.SaleInvoiceId == request.DocumentId);

                            var purchaseOrderList = new List<DeliveryChallanListLookUpModel>();
                            foreach (var purchaseOrder in po)
                            {


                                purchaseOrderList.Add(new DeliveryChallanListLookUpModel
                                {
                                    Id = purchaseOrder.Id,
                                    IsClose = purchaseOrder.IsClose,
                                    IsProcessed = purchaseOrder.IsProcessed,
                                    Reason = purchaseOrder.Reason,
                                    Description = purchaseOrder.Description,
                                    RegistrationNumber = purchaseOrder.RegistrationNo,
                                    DocumentNumberForOrder = purchaseOrder.SaleOrder?.RegistrationNo,
                                    DocumentNumberForSale = purchaseOrder.SaleInvoice?.RegistrationNo,
                                    TemplateName = purchaseOrder.TemplateName,
                                    Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                });
                            }
                            return new DeliveryChallanListLookUpModel
                            {
                                IsReserved = reservedChallan == null ? null : reservedChallan.Id,
                                IsClose = reservedChallan == null ? false : reservedChallan.IsClose,
                                deliveryChallanListLookUpModels = purchaseOrderList
                            };

                        }
                        else if (request.CustomerId != null)
                        {
                            var po = _context.DeliveryChallans
                          .AsNoTracking()
                          .Include(x => x.Contact)
                          .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && !x.IsClose && x.CustomerId == request.CustomerId && x.IsService == request.IsService)
                          .AsQueryable();

                            if (request.BranchId != null)
                            {
                                po = po.Where(x => x.BranchId == request.BranchId);
                            }

                            var purchaseOrderList = new List<DeliveryChallanListLookUpModel>();
                            foreach (var purchaseOrder in po)
                            {


                                purchaseOrderList.Add(new DeliveryChallanListLookUpModel
                                {
                                    Id = purchaseOrder.Id,
                                    Description = purchaseOrder.Description,
                                    RegistrationNumber = purchaseOrder.RegistrationNo,
                                    DocumentNumberForOrder = purchaseOrder.SaleOrder?.RegistrationNo,
                                    DocumentNumberForSale = purchaseOrder.SaleInvoice?.RegistrationNo,
                                    TemplateName = purchaseOrder.TemplateName,
                                    Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                });
                            }
                            return new DeliveryChallanListLookUpModel
                            {
                                deliveryChallanListLookUpModels = purchaseOrderList
                            };

                        }
                        else
                        {
                            var po = _context.DeliveryChallans
                          .AsNoTracking()
                          .Include(x => x.SaleOrder)
                          .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.SaleOrderId == request.DocumentId)
                          .AsQueryable();

                            if (request.BranchId != null)
                            {
                                po = po.Where(x => x.BranchId == request.BranchId);
                            }

                           

                            var reservedChallan = _context.DeliveryChallanReserveds.AsNoTracking().FirstOrDefault(x => x.SaleOrderId == request.DocumentId);


                            var purchaseOrderList = new List<DeliveryChallanListLookUpModel>();
                            foreach (var purchaseOrder in po)
                            {


                                purchaseOrderList.Add(new DeliveryChallanListLookUpModel
                                {
                                    Id = purchaseOrder.Id,
                                    Description = purchaseOrder.Description,
                                    RegistrationNumber = purchaseOrder.RegistrationNo,
                                    DocumentNumberForOrder = purchaseOrder.SaleOrder?.RegistrationNo,
                                    DocumentNumberForSale = purchaseOrder.SaleInvoice?.RegistrationNo,
                                    TemplateName = purchaseOrder.TemplateName,
                                    Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                });
                            }
                            return new DeliveryChallanListLookUpModel
                            {
                                IsClose = reservedChallan == null ? false : reservedChallan.IsClose,
                                IsReserved = reservedChallan == null ? null : reservedChallan.Id,
                                deliveryChallanListLookUpModels = purchaseOrderList
                            };

                        }
                    }
                    else
                    {


                        _context.DisableTenantFilter = true;
                        var userId = _contextProvider.GetUserId();


                        var user = await _context.LoginPermissions.FirstOrDefaultAsync(x => x.UserId == _contextProvider.GetUserId(), cancellationToken: cancellationToken);

                        var query = await _context.DeliveryChallans
                           .AsNoTracking()
                           .Include(x => x.Contact)
                           .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.IsService == request.IsService).ToListAsync();


                        var salesRecord = await _context.Sales.AsNoTracking().Select(x => new { DeliveryChallanId = x.DeliveryChallanId }).ToListAsync();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate).ToList();
                        }
                        if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                        {
                            query = query.Where(x => x.CustomerId == request.CustomerId).ToList();
                        }

                        if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                        {
                            query = query.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year).ToList();

                        }
                        
                        //if (!user.AllowAll)
                        //{
                        //    query = query.Where(x => EF.Property<Guid>(x, "UserId") == userId);

                        //}
                        if (request.UserId != null)
                        {
                            query = query.Where(x => EF.Property<Guid>(x, "UserId") == request.UserId).ToList();
                        }

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                            query = query.Where(x => x.RegistrationNo.ToLower().Contains(searchTerm.ToLower()) 
                                                || x.Date.ToString("dd/MM/yyyy").ToLower().Contains(searchTerm.ToLower())
                                                || (x.Contact.CustomerDisplayName != null && x.Contact.CustomerDisplayName.ToLower().Contains(searchTerm.ToLower()))
                                                || (x.Contact.EnglishName != null && x.Contact.EnglishName.ToLower().Contains(searchTerm.ToLower()))
                                                || (x.Contact.ArabicName != null && x.Contact.ArabicName.ToLower().Contains(searchTerm))
                                                || (x.Contact.Code != null && x.Contact.Code.ToLower().Contains(searchTerm.ToLower()))
                                                 ).ToList();

                        }
                        query = query.OrderBy(x => x.RegistrationNo).ToList();
                        var count = query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                        var purchaseOrderList = new List<DeliveryChallanListLookUpModel>();
                        foreach (var purchaseOrder in query)
                        {
                            purchaseOrderList.Add(new DeliveryChallanListLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                IsClose = purchaseOrder.IsClose,
                                Description = purchaseOrder.Description,
                                CustomerName = purchaseOrder.Contact?.CustomerDisplayName,
                                CustomerNameAr = purchaseOrder.Contact?.ArabicName,
                                RegistrationNumber = purchaseOrder.RegistrationNo,
                                DocumentNumberForOrder = purchaseOrder.SaleOrder?.RegistrationNo,
                                DocumentNumberForSale = purchaseOrder.SaleInvoice?.RegistrationNo,
                                TemplateName = purchaseOrder.TemplateName,
                                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                IsProcessed = purchaseOrder.IsProcessed,
                                Reason = purchaseOrder.Reason,
                                EditDeliveryChallanId = salesRecord.FirstOrDefault(x => x.DeliveryChallanId == purchaseOrder.Id)?.DeliveryChallanId,
                                BranchCode = branches.FirstOrDefault(z => z.Id == purchaseOrder.BranchId)?.Code,
                                IsDefault = purchaseOrder.Contact.IsAllowEmail,
                                CustomerEmail = purchaseOrder.Contact.Email,
                            });
                        }
                      

                        return new DeliveryChallanListLookUpModel
                        {
                            deliveryChallanListLookUpModels = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = purchaseOrderList.Count / request.PageSize
                        };

                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
