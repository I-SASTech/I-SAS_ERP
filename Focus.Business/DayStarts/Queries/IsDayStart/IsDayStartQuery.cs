using MediatR;
using System;
using System.Linq;
using Focus.Business.Interface;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Interface;

namespace Focus.Business.DayStarts.Queries.IsDayStart
{
    public class IsDayStartQuery : IRequest<IsDayStartLookupModel>
    {
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool IsLogin { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsSupervisor { get; set; }

        public class Handler : IRequestHandler<IsDayStartQuery, IsDayStartLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IPrincipal _principal;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(IApplicationDbContext context,
                ILogger<IsDayStartQuery> logger,
                IMediator mediator, UserManager<ApplicationUser> userManager, 
                IPrincipal principal,IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _userManager = userManager;
                _logger = logger;
                _mediator = mediator;
                _principal = principal;
                _contextProvider = contextProvider;
            }
            public async Task<IsDayStartLookupModel> Handle(IsDayStartQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var dayLists = _context.DayStarts
                        .AsNoTracking().AsQueryable();

                    // Counter Id
                    var counterId = _contextProvider.GetCounterId();
                    //var dayId = _contextProvider.GetDayId();

                    var totalReturnValue = 0M;
                    var totalReturnCount = 0;
                    var totalInvoice = 0;
                    var fromInvoice = "";
                    var toInvoice = "";



                    var userLoginPermission = await _context.LoginPermissions.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken: cancellationToken);
                    //For first Scenario 
                    if (userLoginPermission == null)
                        throw new NotFoundException("User have not any permission", request.UserId);

                    var isDayStart = dayLists.ToList().LastOrDefault(x => x.IsDayStart && x.IsActive && x.FromTime != null && x.ToTime == null);
                    var userTerminal = _context.Terminals.AsNoTracking().ToList();
                    var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                    if (user == null)
                        throw new NotFoundException("User Not Exist", request.UserId);
                    //Day wise InActive Terminal
                    var inActiveTerminalOfDayWise = new List<List<DayStartLookUpModel>>();
                    var outStandingBalance = 0M;

                    if (request.IsLogin)
                    {
                        if (isDayStart == null)
                        {
                            return new IsDayStartLookupModel
                            {
                                IsFirstUser = true,
                                IsDayStart = false,
                                IsDayStartPermission = userLoginPermission.StartDay,
                                DayWiseList = inActiveTerminalOfDayWise,
                                OutStandingBalance = outStandingBalance,
                            };
                        }
                        else
                        {
                            var isTerminalCheck = dayLists.Where(x => !x.IsDayStart && x.StartTerminalFor== user.UserName && x.IsActive && x.FromTime != null && x.ToTime == null).ToList();
                            if(isTerminalCheck!=null && isTerminalCheck.Count>0)
                            {
                                var lastTerminal = isTerminalCheck.LastOrDefault();
                                if (lastTerminal != null)
                                {
                                    return new IsDayStartLookupModel
                                    {
                                        IsFirstUser = true,
                                        IsDayStart = true,
                                        IsDayStartPermission = userLoginPermission.StartDay,
                                        DayWiseList = inActiveTerminalOfDayWise,
                                        OutStandingBalance = outStandingBalance,
                                    };
                                }
                                else
                                {
                                    return new IsDayStartLookupModel
                                    {
                                        IsFirstUser = true,
                                        IsDayStart = true,
                                        IsDayStartPermission = userLoginPermission.StartDay,
                                        DayWiseList = inActiveTerminalOfDayWise,
                                        OutStandingBalance = outStandingBalance,
                                    };
                                }
                            }
                            else
                            {
                                return new IsDayStartLookupModel
                                {
                                    IsFirstUser = false,
                                    IsDayStart = false,
                                    IsDayStartPermission = userLoginPermission.StartDay,
                                    DayWiseList = inActiveTerminalOfDayWise,
                                    OutStandingBalance = outStandingBalance,
                                };
                            }
                          
                            
                        }
                    }



                    var dayMasterId = _context.DayStarts.Where(x => x.IsDayStart && !x.IsActive && x.DayStartId == null).Select(y => y.Id).ToList();
                    

                    
                    foreach (var inactiveday in dayMasterId)
                    {
                        var inactiveDaysSale =
                            _context.DayStarts.Where(x => x.DayStartId == inactiveday).ToList();
                        var list = new List<DayStartLookUpModel>();
                        foreach (var items in inactiveDaysSale)
                        {
                            if (!items.IsReceived && request.IsSupervisor)
                            {

                                var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                                {
                                    Id = items.CounterId,
                                    DayId = items.Id,
                                    IsReport = true
                                });
                                
                                list.Add(new DayStartLookUpModel
                                {
                                    Id = items.Id,
                                    OpeningCash = items.OpeningCash,
                                    CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                    Date = items.Date?.ToString("MM/dd/yyyy"),
                                    ToTime = items.ToTime?.ToString("hh:mm tt"),
                                    FromTime = items.FromTime?.ToString("hh:mm tt"),
                                    CashInHand = items.CashInHand - items.OpeningCash,
                                    ExpenseCash = calculation.DraftExpense + calculation.PostExpense,
                                    DraftExpense = calculation.DraftExpense,
                                    PostExpense = calculation.PostExpense,
                                    BankExpense = calculation.BankExpense,
                                    DayEndUser = items.DayEndUser,
                                    CarryCash = items.CarryCash,
                                    SuperVisorName = items.SuperVisorName,
                                    StartTerminalBy = items.StartTerminalBy,
                                    StartTerminalFor = items.StartTerminalFor,
                                    TotalCash = (items.CashInHand - items.OpeningCash) + items.OpeningCash - calculation.DraftExpense,
                                    IsReceived = items.IsReceived,
                                    ReceivingAmount = items.ReceivingAmount,
                                    BankDetails = calculation.BankDetails,
                                    EndTerminalBy = items.EndTerminalBy,
                                    EndTerminalFor = items.EndTerminalFor
                                });
                                
                            }
                            else if (!items.IsReceived && !request.IsSupervisor && items.CounterId == counterId)
                            {
                                var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                                {
                                    Id = items.CounterId,
                                    DayId = items.Id,
                                    IsReport = true
                                });
                                list.Add(new DayStartLookUpModel
                                {
                                    Id = items.Id,
                                    OpeningCash = items.OpeningCash,
                                    CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                    Date = items.Date?.ToString("MM/dd/yyyy"),
                                    ToTime = items.ToTime?.ToString("hh:mm tt"),
                                    FromTime = items.FromTime?.ToString("hh:mm tt"),
                                    CashInHand = items.CashInHand - items.OpeningCash,
                                    ExpenseCash = calculation.DraftExpense + calculation.PostExpense,
                                    DraftExpense = calculation.DraftExpense,
                                    PostExpense = calculation.PostExpense,
                                    BankExpense = calculation.BankExpense,
                                    DayEndUser = items.DayEndUser,
                                    CarryCash = items.CarryCash,
                                    SuperVisorName = items.SuperVisorName,
                                    StartTerminalBy = items.StartTerminalBy,
                                    StartTerminalFor = items.StartTerminalFor,
                                    TotalCash = (items.CashInHand - items.OpeningCash) + items.OpeningCash - calculation.DraftExpense,
                                    IsReceived = items.IsReceived,
                                    ReceivingAmount = items.ReceivingAmount,
                                    EndTerminalBy = items.EndTerminalBy,
                                    EndTerminalFor = items.EndTerminalFor,
                                    BankDetails = calculation.BankDetails
                                });
                                
                            }
                        }

                        if (list.Count > 0)
                        {
                            inActiveTerminalOfDayWise.Add(list);
                            if (!request.IsSupervisor)
                                outStandingBalance += list.Sum(x => x.TotalCash);
                        }
                       
                    }
                    
                    if (isDayStart == null)
                    {
                        return new IsDayStartLookupModel
                        {
                            IsFirstUser = true,
                            IsDayStart = false,
                            IsDayStartPermission = userLoginPermission.StartDay,
                            DayWiseList = inActiveTerminalOfDayWise,
                            OutStandingBalance = outStandingBalance,
                        };
                    }
                
                    var terminalList = dayLists.Where(x => !x.IsDayStart && x.StartTerminalFor== user.UserName && x.IsActive && x.FromTime != null && x.ToTime == null).ToList();

                    var isTerminal = terminalList.ToList().LastOrDefault(x => !x.IsDayStart && x.StartTerminalFor == user.UserName && x.IsActive && x.FromTime != null && x.ToTime == null);


                    var dayStarts = new List<DayStartLookUpModel>();
                    var InactivedayStarts = new List<DayStartLookUpModel>();
                    
                    var transferHistoryList = new List<TransferHistoryLookupModel>();

                    if (isTerminal == null)
                    {
                       
                        if (request.IsSupervisor)
                        {
                            
                            var transferList = _context.TransferHistories.AsNoTracking().Where(x=>x.DayStartId == dayLists.OrderBy(y=>y.IsActive && y.IsDayStart).LastOrDefault().Id).ToList();
                            foreach (var items in transferList)
                            {
                                transferHistoryList.Add(new TransferHistoryLookupModel
                                {
                                    OpeningCash = items.OpeningCash,
                                    CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                    Date = items.Date?.ToString("MM/dd/yyyy "),
                                    CashInHand = items.CashInHand,
                                    BankAmount = items.BankAmount,
                                    ExpenseCash = items.ExpenseCash,
                                    StartTerminalBy = items.StartTerminalBy,
                                    StartTerminalFor = items.StartTerminalFor,
                                    TotalCash = items.TotalCash,
                                    FromTime = items.FromTime?.ToString("hh:mm tt"),
                                    ToTime = items.ToTime?.ToString("hh:mm tt"),
                                });
                            }
                            var inactiveDaysDuringSale = dayLists.Where(x => !x.IsActive && !x.IsDayStart && x.DayStartId == isDayStart.Id).ToList();
                            foreach (var items in inactiveDaysDuringSale)
                            {
                                if (items.IsReceived)
                                {

                                    var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                                    {
                                        Id = items.CounterId,
                                        DayId = items.Id,
                                        IsReport = true
                                    });
                                    InactivedayStarts.Add(new DayStartLookUpModel
                                    {
                                        Id = items.Id,
                                        OpeningCash = calculation.OpeningBalance,
                                        CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                        Date = items.Date?.ToString("MM/dd/yyyy"),
                                        ToTime = items.ToTime?.ToString("hh:mm tt"),
                                        FromTime = items.FromTime?.ToString("hh:mm tt"),
                                        CashInHand = calculation.CashInHand - calculation.OpeningBalance,
                                        BankAmount = calculation.Bank,
                                        ExpenseCash = calculation.DraftExpense + calculation.PostExpense,
                                        DraftExpense = calculation.DraftExpense,
                                        PostExpense = calculation.PostExpense,
                                        BankExpense = calculation.BankExpense,
                                        DayEndUser = items.DayEndUser,
                                        CarryCash = items.CarryCash,
                                        SuperVisorName = items.SuperVisorName,
                                        StartTerminalBy = items.StartTerminalBy,
                                        StartTerminalFor = items.StartTerminalFor,
                                        TotalCash = (calculation.CashInHand - calculation.OpeningBalance) + calculation.OpeningBalance - calculation.DraftExpense,
                                        IsReceived = items.IsReceived,
                                        ReceivingAmount = items.ReceivingAmount,
                                        EndTerminalBy = items.EndTerminalBy,
                                        EndTerminalFor = items.EndTerminalFor
                                    });
                                }
                                else
                                {
                                    var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                                    {
                                        Id = items.CounterId,
                                        DayId = items.Id,
                                        IsReport = true
                                    });
                                    InactivedayStarts.Add(new DayStartLookUpModel
                                    {
                                        Id = items.Id,
                                        OpeningCash = items.OpeningCash,
                                        CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                        Date = items.Date?.ToString("MM/dd/yyyy"),
                                        ToTime = items.ToTime?.ToString("hh:mm tt"),
                                        FromTime = items.FromTime?.ToString("hh:mm tt"),
                                        CashInHand = items.CashInHand - items.OpeningCash,
                                        ExpenseCash = calculation.DraftExpense + calculation.PostExpense,
                                        DraftExpense = calculation.DraftExpense,
                                        PostExpense = calculation.PostExpense,
                                        BankExpense = calculation.BankExpense,
                                        DayEndUser = items.DayEndUser,
                                        CarryCash = items.CarryCash,
                                        SuperVisorName = items.SuperVisorName,
                                        StartTerminalBy = items.StartTerminalBy,
                                        StartTerminalFor = items.StartTerminalFor,
                                        TotalCash = (items.CashInHand - items.OpeningCash) + items.OpeningCash - calculation.DraftExpense,
                                        IsReceived = items.IsReceived,
                                        ReceivingAmount = items.ReceivingAmount,
                                        EndTerminalBy = items.EndTerminalBy,
                                        EndTerminalFor = items.EndTerminalFor
                                    });
                                }
                            }
                            var superVisorList = dayLists.Where(x => x.IsActive && !x.IsDayStart).ToList();
                            foreach (var items in superVisorList)
                            {
                                var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                                {
                                    Id = items.CounterId,
                                    DayId = items.Id
                                });
                                // SaleReturn Data
                                var saleReturnList = _context.Sales.Where(x =>
                                    EF.Property<Guid>(x, "DayId") == items.DayStartId &&
                                    EF.Property<Guid>(x, "CounterId") == items.CounterId && x.IsSaleReturn).AsQueryable();
                                
                                totalInvoice = calculation.TotalInvoices;
                                fromInvoice = calculation.FromInvoices;
                                toInvoice = calculation.ToInvoices;
                                foreach (var returnList in saleReturnList)
                                {
                                    var returnData = await _mediator.Send(new GetSaleDetailQuery()
                                    {
                                        Id = returnList.Id,
                                        IsReturn = true
                                    });
                                    totalReturnValue += Convert.ToDecimal(returnData.PaymentAmount);
                                    totalReturnCount += 1;
                                }

                                dayStarts.Add(new DayStartLookUpModel
                                {
                                    OpeningCash = calculation.OpeningBalance,
                                    CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                    Date = items.Date?.ToString("MM/dd/yyyy"),
                                    ToTime = items.ToTime?.ToString("hh:mm tt"),
                                    FromTime = items.FromTime?.ToString("hh:mm tt"),
                                    CashInHand = calculation.CashInHand - calculation.OpeningBalance,
                                    BankAmount = calculation.Bank,
                                    ExpenseCash = calculation.DraftExpense + calculation.PostExpense,
                                    DraftExpense = calculation.DraftExpense,
                                    PostExpense = calculation.PostExpense,
                                    BankExpense = calculation.BankExpense,
                                    StartTerminalBy = items.StartTerminalBy,
                                    StartTerminalFor = items.StartTerminalFor,
                                    TotalCash = (calculation.CashInHand - calculation.OpeningBalance) + calculation.OpeningBalance - calculation.DraftExpense,
                                    IsReceived = items.IsReceived,
                                    ReceivingAmount = items.ReceivingAmount,
                                    BankDetails = calculation.BankDetails
                                });
                            }
                            var balanceOfWholeDay1 = await _mediator.Send(new GetBalanceOfWholeDay());

                            var lookUpModel1 = new IsDayStartLookupModel
                            {
                                IsDayStart = false,
                                IsDayStartPermission = userLoginPermission.StartDay,
                                DayStarts = dayStarts,
                                Bank = balanceOfWholeDay1.Bank,
                                CashInHand = balanceOfWholeDay1.CashInHand,
                                Expense =  balanceOfWholeDay1.PostExpense,
                                DraftExpense = balanceOfWholeDay1.DraftExpense,
                                PostExpense = balanceOfWholeDay1.PostExpense,
                                BankExpense = balanceOfWholeDay1.BankExpense,
                                OpeningCash = balanceOfWholeDay1.OpeningCash,
                                TransferHistories = transferHistoryList,
                                InacticeDayStarts= InactivedayStarts
                            };
                            lookUpModel1.TotalCash = balanceOfWholeDay1.CashInHand + balanceOfWholeDay1.OpeningCash + balanceOfWholeDay1.PostExpense;
                            lookUpModel1.TotalInvoices = totalInvoice;
                            lookUpModel1.ToInvoice = toInvoice;
                            lookUpModel1.FromInvoice = fromInvoice;
                            lookUpModel1.TotalReturnCount = totalReturnCount;
                            lookUpModel1.TotalReturnValue = totalReturnValue;

                            lookUpModel1.DayWiseList = inActiveTerminalOfDayWise;
                            lookUpModel1.OutStandingBalance = outStandingBalance;
                            
                            return lookUpModel1;

                        }
                        return new IsDayStartLookupModel
                        {
                            IsDayStart = false,
                            IsDayStartPermission = userLoginPermission.StartDay,
                            DayWiseList = inActiveTerminalOfDayWise,
                            OutStandingBalance = outStandingBalance,

                            

                        };


                    }
                    else
                    {
                        //if (isTerminal.StartTerminalFor != user.UserName)
                        //{
                        //    throw new ObjectAlreadyExistsException("Terminal already Assigned to this " + isTerminal.StartTerminalFor);
                        //}


                    }
                    
                    object token = null;
                    
                    var openingCash = 0m;
                    if (request.IsSupervisor)
                    {
                        var superVisorList = dayLists.Where(x => x.IsActive && !x.IsDayStart).ToList();
                        var inactiveDaysDuringSale = dayLists.Where(x => !x.IsActive && !x.IsDayStart && x.DayStartId==isDayStart.Id).ToList();
                        foreach (var items in inactiveDaysDuringSale)
                        {
                            var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                            {
                                Id = items.CounterId,
                                DayId = items.Id,
                                IsReport = true
                            });
                            InactivedayStarts.Add(new DayStartLookUpModel
                            {
                                Id = items.Id,
                                OpeningCash = calculation.OpeningBalance,
                                CarryCash = items.CarryCash,
                                CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                Date = items.Date?.ToString("MM/dd/yyyy"),
                                ToTime = items.ToTime?.ToString("hh:mm tt"),
                                FromTime = items.FromTime?.ToString("hh:mm tt"),
                                CashInHand = calculation.CashInHand - calculation.OpeningBalance,
                                BankAmount = calculation.Bank,
                                ExpenseCash = calculation.DraftExpense + calculation.PostExpense,
                                DraftExpense = calculation.DraftExpense,
                                PostExpense = calculation.PostExpense,
                                BankExpense = calculation.BankExpense,
                                SuperVisorName = items.SuperVisorName,
                                StartTerminalBy = items.StartTerminalBy,
                                StartTerminalFor = items.StartTerminalFor,
                                DayEndUser = items.DayEndUser,
                                TotalCash = (calculation.CashInHand - calculation.OpeningBalance) + calculation.OpeningBalance - calculation.DraftExpense,
                                IsReceived = items.IsReceived,
                                ReceivingAmount = items.ReceivingAmount,
                                BankDetails = calculation.BankDetails
                            });
                        }

                        foreach (var items in superVisorList)
                        {
                            var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                            {
                                Id = items.CounterId,
                                DayId = items.Id
                            });
                            openingCash = +calculation.OpeningBalance;

                            // SaleReturn Data
                            var saleReturnList = _context.Sales.Where(x =>
                                EF.Property<Guid>(x, "DayId") == items.DayStartId &&
                                EF.Property<Guid>(x, "CounterId") == items.CounterId && x.IsSaleReturn).AsQueryable().ToList();

                            totalInvoice = calculation.TotalInvoices;
                            fromInvoice = calculation.FromInvoices;
                            toInvoice = calculation.ToInvoices;
                            foreach (var returnList in saleReturnList)
                            {
                                var returnData = await _mediator.Send(new GetSaleDetailQuery()
                                {
                                    Id = returnList.Id,
                                    IsReturn = true
                                });
                                totalReturnValue += Convert.ToDecimal(returnData.PaymentAmount);
                                totalReturnCount += 1;
                            }
                            dayStarts.Add(new DayStartLookUpModel
                            {
                                OpeningCash= calculation.OpeningBalance,
                                CounterName = userTerminal.FirstOrDefault(x=>x.Id== items.CounterId)?.Code,
                                Date= items.Date?.ToString("MM/dd/yyyy"),
                                ToTime = items.ToTime?.ToString("hh:mm tt"),
                                FromTime = items.FromTime?.ToString("hh:mm tt"),
                                CashInHand = calculation.CashInHand- calculation.OpeningBalance,
                                BankAmount = calculation.Bank,
                                ExpenseCash= calculation.DraftExpense + calculation.PostExpense,
                                DraftExpense= calculation.DraftExpense,
                                PostExpense= calculation.PostExpense,
                                BankExpense = calculation.BankExpense,
                                StartTerminalBy = items.StartTerminalBy,
                                StartTerminalFor = items.StartTerminalFor,
                                TotalCash = (calculation.CashInHand - calculation.OpeningBalance) + calculation.OpeningBalance - calculation.DraftExpense,
                                IsReceived = items.IsReceived,
                                ReceivingAmount = items.ReceivingAmount,
                                BankDetails = calculation.BankDetails
                            });
                        }
                        var transferList = _context.TransferHistories.AsNoTracking().Where(x => x.DayStartId == dayLists.OrderBy(y => y.IsActive && y.IsDayStart).LastOrDefault().Id).ToList();
                        //var transferList = _context.TransferHistories.AsNoTracking().ToList();
                        foreach (var items in transferList)
                        {
                            transferHistoryList.Add(new TransferHistoryLookupModel
                            {
                                OpeningCash = items.OpeningCash,
                                CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                Date = items.Date?.ToString("MM/dd/yyyy"),
                                CashInHand = items.CashInHand,
                                BankAmount = items.BankAmount,
                                ExpenseCash = items.ExpenseCash,
                                StartTerminalBy = items.StartTerminalBy,
                                StartTerminalFor = items.StartTerminalFor,
                                TotalCash = items.TotalCash,
                                FromTime = items.FromTime?.ToString("hh:mm tt"),
                                ToTime = items.ToTime?.ToString("hh:mm tt"),

                            });
                        }

                    }
                    else
                    {
                        var userList = dayLists.Where(x => !x.IsDayStart && x.StartTerminalFor == user.UserName && x.IsActive && x.FromTime != null && x.ToTime == null).ToList();
                        foreach (var items in userList)
                        {
                            var calculation = await _mediator.Send(new GetOpeningBalanceQuery()
                            {
                                Id = items.CounterId,
                                DayId = items.DayStartId
                            });
                            // SaleReturn Data
                            var saleReturnList = _context.Sales.Where(x =>
                                EF.Property<Guid>(x, "DayId") == items.DayStartId &&
                                EF.Property<Guid>(x, "CounterId") == items.CounterId && x.IsSaleReturn).AsQueryable();

                            totalInvoice = calculation.TotalInvoices;
                            fromInvoice = calculation.FromInvoices;
                            toInvoice = calculation.ToInvoices;
                            foreach (var returnList in saleReturnList)
                            {
                                var returnData = await _mediator.Send(new GetSaleDetailQuery()
                                {
                                    Id = returnList.Id,
                                    IsReturn = true
                                });
                                totalReturnValue += Convert.ToDecimal(returnData.PaymentAmount);
                                totalReturnCount += 1;
                            }
                            dayStarts.Add(new DayStartLookUpModel
                            {
                                Id = items.Id,
                                OpeningCash = calculation.OpeningBalance,
                                CounterName = userTerminal.FirstOrDefault(x => x.Id == items.CounterId)?.Code,
                                Date = items.Date?.ToString("MM/dd/yyyy"),
                                ToTime = items.ToTime?.ToString("hh:mm tt"),
                                FromTime = items.FromTime?.ToString("hh:mm tt"),
                                CashInHand = calculation.CashInHand-calculation.OpeningBalance,
                                BankAmount = calculation.Bank,
                                ExpenseCash = calculation.PostExpense + calculation.DraftExpense,
                                DraftExpense = calculation.DraftExpense,
                                PostExpense = calculation.PostExpense,
                                BankExpense = calculation.BankExpense,
                                StartTerminalBy = items.StartTerminalBy,
                                StartTerminalFor = items.StartTerminalFor,
                                TotalCash = (calculation.CashInHand - calculation.OpeningBalance) + calculation.OpeningBalance - calculation.DraftExpense,

                                BankDetails = calculation.BankDetails
                            });
                        }

                    }

                    var balanceOfWholeDay = await _mediator.Send(new GetBalanceOfWholeDay());

                    var lookUpModel =  new IsDayStartLookupModel
                    {
                        Date = isTerminal.Date,
                        FromTime = isTerminal.FromTime,
                        CounterCode = userTerminal.FirstOrDefault(x => x.Id == isTerminal.CounterId)?.Code,
                        PrinterName = userTerminal.FirstOrDefault(x => x.Id == isTerminal.CounterId)?.PrinterName,
                        IsDayStart = isTerminal.IsActive,
                        DayStartId = isTerminal.IsActive ? isTerminal.Id : Guid.Empty,
                        IsExpenseDay = isTerminal.IsExpenseDay,
                        Token = token,
                        DayStarts= dayStarts,
                        Bank = balanceOfWholeDay.Bank,
                        CashInHand = balanceOfWholeDay.CashInHand ,
                        Expense = balanceOfWholeDay.DraftExpense + balanceOfWholeDay.PostExpense,
                        OpeningCash = balanceOfWholeDay.OpeningCash,
                        TransferHistories = transferHistoryList,
                        InacticeDayStarts= InactivedayStarts,
                        DayWiseList = inActiveTerminalOfDayWise,
                        OutStandingBalance = outStandingBalance,
                        DraftExpense = balanceOfWholeDay.DraftExpense,
                        PostExpense = balanceOfWholeDay.PostExpense,
                        BankExpense = balanceOfWholeDay.BankExpense,
                    };
                    lookUpModel.TotalCash = balanceOfWholeDay.CashInHand  + balanceOfWholeDay.OpeningCash - balanceOfWholeDay.DraftExpense;
                    lookUpModel.TotalInvoices = totalInvoice;
                    lookUpModel.ToInvoice = toInvoice;
                    lookUpModel.FromInvoice = fromInvoice;
                    lookUpModel.TotalReturnCount = totalReturnCount;
                    lookUpModel.TotalReturnValue = totalReturnValue;
                    return lookUpModel;
                 
                }
                catch (NotFoundException e)
                {
                    _logger.LogInformation(e.Message);
                    throw new NotFoundException(e.Message, request.UserId);
                }
                catch (ObjectAlreadyExistsException e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ObjectAlreadyExistsException(e.Message);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException("Something Went Wrong");
                }

            }
        }
    }
}
