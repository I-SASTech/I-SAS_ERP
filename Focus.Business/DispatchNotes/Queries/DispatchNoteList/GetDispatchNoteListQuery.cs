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

namespace Focus.Business.DispatchNotes.Queries.DispatchNoteList
{
    public class GetDispatchNoteListQuery : PagedRequest, IRequest<PagedResult<List<DispatchNoteLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetDispatchNoteListQuery, PagedResult<List<DispatchNoteLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetDispatchNoteListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<DispatchNoteLookUpModel>>> Handle(GetDispatchNoteListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        if (request.CustomerId!=Guid.Empty)
                        {
                            var dispatch = _context.DispatchNotes
                                .AsNoTracking()
                                .Include(x => x.DispatchNoteItems)
                                .ThenInclude(x => x.Product)
                                .Include(x => x.Customer)
                                .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.CustomerId==request.CustomerId)
                                .AsQueryable();

                            if (request.BranchId != null)
                            {
                                dispatch = dispatch.Where(x => x.BranchId == request.BranchId);
                            }

                            var saleOrderList = new List<DispatchNoteLookUpModel>();
                            foreach (var dispatchNote in dispatch)
                            {
                                var sOrder = DispatchNoteLookUpModel.Create(dispatchNote);
                                saleOrderList.Add(sOrder);
                            }
                            return new PagedResult<List<DispatchNoteLookUpModel>>
                            {
                                Results = saleOrderList
                            };
                        }
                        else
                        {
                            var dispatch = _context.DispatchNotes
                                .AsNoTracking()
                                .Include(x => x.DispatchNoteItems)
                                .ThenInclude(x => x.Product)
                                .Include(x => x.Customer)
                                .Where(x => x.ApprovalStatus == ApprovalStatus.Approved)
                                .AsQueryable();

                            if (request.BranchId != null)
                            {
                                dispatch = dispatch.Where(x => x.BranchId == request.BranchId);
                            }

                            var saleOrderList = new List<DispatchNoteLookUpModel>();
                            foreach (var dispatchNote in dispatch)
                            {
                                var sOrder = DispatchNoteLookUpModel.Create(dispatchNote);
                                saleOrderList.Add(sOrder);
                            }
                            return new PagedResult<List<DispatchNoteLookUpModel>>
                            {
                                Results = saleOrderList
                            };
                        }

                    }
                    else
                    {
                        var query = _context.DispatchNotes
                            .AsNoTracking()
                            .Include(x=>x.DispatchNoteItems)
                            .ThenInclude(x => x.Product)
                            .Include(x=>x.Customer)
                            .AsQueryable();


                        if (request.BranchId!=null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }


                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                           
                           
                                query = query.Where(x =>
                                    x.RegistrationNo.Contains(searchTerm));
                            
                        }
                        query = query.OrderByDescending(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        
                        var saleOrderList = new List<DispatchNoteLookUpModel>();
                        foreach (var saleOrder in query)
                        {
                            var sOrder = DispatchNoteLookUpModel.Create(saleOrder);
                            saleOrderList.Add(sOrder);
                        }

                        return new PagedResult<List<DispatchNoteLookUpModel>>
                        {
                            Results = saleOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = saleOrderList.Count / request.PageSize
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
