﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ReparingOrders.Commands.AddReparingOrder;
using Focus.Business.Users;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ReparingOrders.Queries
{
    public class GetReparingOrderListQuery : PagedRequest, IRequest<PagedResult<List<ReparingOrderLookUp>>>
    {
        public string SearchTerm { get; set; }
        public string MobileNoSearch { get; set; }
        public string Status { get; set; }
        public bool IsDropDown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetReparingOrderListQuery, PagedResult<List<ReparingOrderLookUp>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly UserManager<ApplicationUser> _userManager;


            public Handler(IApplicationDbContext context, ILogger<GetReparingOrderListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;

                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ReparingOrderLookUp>>> Handle(GetReparingOrderListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.ReparingOrders
                            .AsNoTracking()
                            .Include(x => x.Customer)
                            .Include(x => x.Employee)
                            .AsQueryable();

                        if (request.BranchId!=null)
                        {
                            po = po.Where(x => x.BranchId == request.BranchId);
                        }

                        var purchaseOrderList = new List<ReparingOrderLookUp>();

                        foreach (var purchaseOrder in po)
                        {
                            purchaseOrderList.Add(new ReparingOrderLookUp()
                            {
                                Id = purchaseOrder.Id,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                Status = purchaseOrder.Status,
                                EstimateAmount = purchaseOrder.EstimateAmount,
                                ReceivedDate = purchaseOrder.ReceivedDate,
                                EmployeeNameEn = purchaseOrder.Employee?.EnglishName,
                                EmployeeNameAr = purchaseOrder.Employee?.ArabicName,
                                Dates = purchaseOrder.Date.ToString("d"),

                                CustomerNameEn = purchaseOrder.Customer?.EnglishName,
                                CustomerNameAr = purchaseOrder.Customer?.ArabicName,
                                Mobile = purchaseOrder.Customer?.ContactNo1,
                                UpsDes = purchaseOrder.UpsDescription.Name,
                                Issue = purchaseOrder.Problem.Name,

                            });
                        }
                        return new PagedResult<List<ReparingOrderLookUp>>
                        {
                            Results = purchaseOrderList
                        };

                    }
                    else
                    {
                        var query = _context.ReparingOrders
                            .Include(x => x.Customer)
                            .Include(x => x.Employee)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }

                        var userList = _userManager.Users.ToList();

                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate);
                        }
                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate);
                        }

                        if (!string.IsNullOrEmpty(request.Status))
                        {
                            if (request.Status == "Returned")
                            {
                                query = query.Where(x => x.IsReturn);
                            }
                            else if (request.Status == "InProgress")
                            {
                                query = query.Where(x => x.Status == "Printed");
                            }
                            else if (request.Status == "Repared")
                            {
                                query = query.Where(x => x.IsRepared);
                            }

                            else if (request.Status == "PaymentReceived")
                            {
                                query = query.Where(x => x.IsCashed);
                            }
                            else if (request.Status == "CompletedOrders")
                            {
                                query = query.Where(x => x.IsComplete);
                            }

                        }

                        if (!string.IsNullOrEmpty(request.MobileNoSearch))
                        {
                            var mobileNo = request.MobileNoSearch;
                            query = query.Where(x => x.Customer.ContactNo1.Contains(mobileNo));
                        }

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                            query = query.Where(x => x.RegistrationNo == searchTerm || x.Customer.EnglishName.Contains(searchTerm) || x.Employee.EnglishName.Contains(searchTerm));
                        }

                        query = query.OrderByDescending(x => Convert.ToInt32(x.RegistrationNo) );

                        var total = query.Count(x => x.Status == "Printed");

                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var purchaseOrderList = new List<ReparingOrderLookUp>();
                        foreach (var purchaseOrder in query)
                        {
                            purchaseOrderList.Add(new ReparingOrderLookUp()
                            {
                                Id = purchaseOrder.Id,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                Status = purchaseOrder.Status,
                                EstimateAmount = purchaseOrder.EstimateAmount,
                                RemaningPrice = purchaseOrder.RemaningPrice,
                                IsCashed = purchaseOrder.IsCashed,
                                AdvanceAmount = purchaseOrder.AdvanceAmount,
                                CashAmount = purchaseOrder.CashAmount,
                                ReceivedDate = purchaseOrder.ReceivedDate,
                                EmployeeNameEn = purchaseOrder.Employee?.EnglishName,
                                EmployeeNameAr = purchaseOrder.Employee?.ArabicName,
                                Dates = purchaseOrder.Date.ToString("d"),
                                Payment = purchaseOrder.AdvanceAmount - purchaseOrder.CashAmount,
                                DoneBy = userList.FirstOrDefault(x => x.Id == purchaseOrder.JobAssignId.ToString())?.FirstName,
                                CustomerNameEn = purchaseOrder.Customer?.EnglishName,
                                CustomerNameAr = purchaseOrder.Customer?.ArabicName,
                                Mobile = purchaseOrder.Customer?.ContactNo1,
                                UpsDes = purchaseOrder.UpsDescription?.Name,
                                Accessor = purchaseOrder.AcessoryInclude?.Name,
                                Issue = purchaseOrder.Problem?.Name,
                                Total = total,

                            });
                        }

                        return new PagedResult<List<ReparingOrderLookUp>>
                        {
                            Results = purchaseOrderList,
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
