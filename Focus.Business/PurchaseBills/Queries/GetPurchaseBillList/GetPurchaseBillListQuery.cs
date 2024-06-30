using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;

namespace Focus.Business.PurchaseBills.Queries.GetPurchaseBillList
{
    public class GetPurchaseBillListQuery : PagedRequest, IRequest<PagedResult<List<PurchaseBillLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? BillerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }

        public class Handler : IRequestHandler<GetPurchaseBillListQuery, PagedResult<List<PurchaseBillLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetPurchaseBillListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<PurchaseBillLookUpModel>>> Handle(GetPurchaseBillListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.PurchaseBills
                            .AsNoTracking()
                            .Include(x => x.BillerAccount)
                            .Include(x => x.PurchaseBillItems)
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            po = po.Where(x => x.BranchId == request.BranchId);
                        }

                        var purchaseBillList = new List<PurchaseBillLookUpModel>();
                        foreach (var purchaseBill in po)
                        {
                            var sOrder = PurchaseBillLookUpModel.Create(purchaseBill);
                            purchaseBillList.Add(sOrder);
                        }
                        return new PagedResult<List<PurchaseBillLookUpModel>>
                        {
                            Results = purchaseBillList
                        };

                    }
                    else
                    {
                        var query = _context.PurchaseBills
                            .AsNoTracking()
                            .Include(x => x.BillerAccount)
                            .Include(x => x.PurchaseBillItems)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }

                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query = query.Where(x => x.ApprovalStatus == request.Status);
                        }
                        if(request.FromDate != null && request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Value.Date && x.Date.Date <= request.ToDate.Value.Date);
                        }
                        if(request.Amount != 0)
                        {
                            query = query.Where(x => x.TotalAmount == request.Amount);
                        }
                        if(request.BillerId != Guid.Empty && request.BillerId != null)
                        {
                            query = query.Where(x => x.BillerId == request.BillerId);
                        }
                        if(!string.IsNullOrEmpty(request.Reference))
                        {
                            var searchReference = request.Reference.ToLower();

                            query = query.Where(x => x.Reference.ToLower().Contains(searchReference));
                        }
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x =>
                                x.RegistrationNo.ToLower().Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));
                        }
                        if(request.PaymentStatus == "UnPaid")
                        {
                            query = query.Where(x => x.PartiallyInvoices == PartiallyInvoices.UnPaid);
                        }
                        else if(request.PaymentStatus == "Fully Paid")
                        {
                            query = query.Where(x => x.PartiallyInvoices == PartiallyInvoices.Fully);
                        }
                        else if(request.PaymentStatus == "Partially")
                        {
                            query = query.Where(x => x.PartiallyInvoices == PartiallyInvoices.Partially);
                        }

                        query = query.OrderByDescending(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var purchaseBillList = new List<PurchaseBillLookUpModel>();
                        foreach (var purchaseBill in query)
                        {
                            var sOrder = PurchaseBillLookUpModel.Create(purchaseBill);
                            purchaseBillList.Add(sOrder);
                        }


                        return new PagedResult<List<PurchaseBillLookUpModel>>
                        {
                            Results = purchaseBillList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = purchaseBillList.Count / request.PageSize
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
