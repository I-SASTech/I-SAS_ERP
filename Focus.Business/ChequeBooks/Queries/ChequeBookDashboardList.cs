using Focus.Business.ChequeBooks.Commands;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ChequeBooks.Queries
{
    public class ChequeBookDashboardList : IRequest<List<ChequeBookDashboardLookup>>
    {
        public List<Guid> BankId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public class Handler : IRequestHandler<ChequeBookDashboardList, List<ChequeBookDashboardLookup>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ChequeBookDashboardList> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<ChequeBookDashboardLookup>> Handle(ChequeBookDashboardList request, CancellationToken cancellationToken)
            {
                try
                {

                    var cheques = _context.ChequeBookItems.AsNoTracking()
                        .Where(x=>request.BankId.Contains(x.BankId))
                        .AsQueryable();

                    var nextmonth = DateTime.UtcNow.AddMonths(+1).Month;
                    var currentMonth = DateTime.UtcNow.Month;
                    var previousMonth = DateTime.UtcNow.AddMonths(-1).Month;


                    if (request.BankId != null)
                    {
                        //cheques = cheques.Where(x => x.BankId == request.BankId);
                    }

                    var currentMonthRecord = cheques.Where(x => x.ChequeTypeDate.Value.Date.Month == currentMonth);
                    var previousMonthRecord = cheques.Where(x => x.ChequeTypeDate.Value.Date.Month == previousMonth);
                    var NextMonthRecord = cheques.Where(x => x.ChequeTypeDate.Value.Date.Month == nextmonth);


                    var chequebookList = new List<ChequeBookDashboardLookup>();

                    if (cheques != null)
                    {


                        chequebookList.Add(new ChequeBookDashboardLookup()
                        {

                            //Current Month
                            ChequeType = "Advance",
                            totalCAdvanceCheque = currentMonthRecord.Count(x => x.ChequeType == ChequeType.Advance),
                            reservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Advance && x.CashType == CashType.Reserved && x.StatusType==default).Sum(x=>x.Amount),
                            notReservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Advance && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),
                            totalCAmount = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Advance).Sum(x => x.Amount),

                            //Next Month
                            totalNAdvanceCheque = NextMonthRecord.Count(x => x.ChequeType == ChequeType.Advance),
                            reservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Advance && x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Advance && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),

                            totalNAmount = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Advance).Sum(x => x.Amount),

                            //Current Month Cahsed
                            totalCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Advance) ,
                            reservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Advance),
                            notReservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Advance),
                            totalCashedAmount = currentMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Advance).Sum(x => x.Amount),

                            //Previosus Month Cahsed
                            totalPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Advance),
                            reservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Advance),
                            notReservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Advance),
                            totalPCashedAmount = previousMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Advance).Sum(x => x.Amount),

                        });
                        chequebookList.Add(new ChequeBookDashboardLookup()
                        {

                            //Current Month
                            ChequeType = "Normal",
                            totalCAdvanceCheque = currentMonthRecord.Count(x => x.ChequeType == ChequeType.Normal),
                            reservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Normal && x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Normal && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),
                            totalCAmount = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Normal).Sum(x => x.Amount),

                            //Next Month
                            totalNAdvanceCheque = NextMonthRecord.Count(x => x.ChequeType == ChequeType.Normal),
                            reservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Normal && x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Normal && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),

                            totalNAmount = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Normal).Sum(x => x.Amount),

                            //Current Month Cahsed
                            totalCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Normal),
                            reservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Normal),
                            notReservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Normal),
                            totalCashedAmount = currentMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Normal).Sum(x => x.Amount),

                            //Previosus Month Cahsed
                            totalPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Normal),
                            reservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Normal),
                            notReservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Normal),
                            totalPCashedAmount = previousMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Normal).Sum(x => x.Amount),

                        });
                        chequebookList.Add(new ChequeBookDashboardLookup()
                        {

                            //Current Month
                            ChequeType = "Security",
                            totalCAdvanceCheque = currentMonthRecord.Count(x => x.ChequeType == ChequeType.Security),
                            reservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Security && x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Security && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),
                            totalCAmount = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Security).Sum(x => x.Amount),

                            //Next Month
                            totalNAdvanceCheque = NextMonthRecord.Count(x => x.ChequeType == ChequeType.Security),
                            reservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Security && x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Security && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),

                            totalNAmount = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Advance).Sum(x => x.Amount),

                            //Current Month Cahsed
                            totalCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Security),
                            reservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Security),
                            notReservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Security),
                            totalCashedAmount = currentMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Security).Sum(x => x.Amount),

                            //Previosus Month Cahsed
                            totalPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Security),
                            reservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Security),
                            notReservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Security),
                            totalPCashedAmount = previousMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Security).Sum(x => x.Amount),

                        });

                         chequebookList.Add(new ChequeBookDashboardLookup()
                        {

                            //Current Month
                            ChequeType = "Guarantee",
                            totalCAdvanceCheque = currentMonthRecord.Count(x => x.ChequeType == ChequeType.Guarantee),
                            reservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Guarantee && x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedCAdvacne = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Guarantee && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),
                            totalCAmount = currentMonthRecord.Where(x => x.ChequeType == ChequeType.Guarantee).Sum(x => x.Amount),

                            //Next Month
                            totalNAdvanceCheque = NextMonthRecord.Count(x => x.ChequeType == ChequeType.Guarantee),
                            reservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Guarantee && x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedNAdvacne = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Guarantee && x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),

                            totalNAmount = NextMonthRecord.Where(x => x.ChequeType == ChequeType.Advance).Sum(x => x.Amount),

                            //Current Month Cahsed
                            totalCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Guarantee),
                            reservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Guarantee),
                            notReservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Guarantee),
                            totalCashedAmount = currentMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Guarantee).Sum(x => x.Amount),

                            //Previosus Month Cahsed
                            totalPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Guarantee),
                            reservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved && x.ChequeType == ChequeType.Guarantee),
                            notReservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved && x.ChequeType == ChequeType.Guarantee),
                            totalPCashedAmount = previousMonthRecord.Where(x => x.StatusType == StatusType.Cashed && x.ChequeType == ChequeType.Guarantee).Sum(x => x.Amount),

                        });
                        chequebookList.Add(new ChequeBookDashboardLookup()
                        {

                            //Current Month
                            ChequeType = "Total",
                            totalCAdvanceCheque = currentMonthRecord.Count(),
                            reservedCAdvacne = currentMonthRecord.Where(x => x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedCAdvacne = currentMonthRecord.Where(x =>  x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),
                            totalCAmount = currentMonthRecord.Sum(x => x.Amount),

                            //Next Month
                            totalNAdvanceCheque = NextMonthRecord.Count(),
                            reservedNAdvacne = NextMonthRecord.Where(x =>  x.CashType == CashType.Reserved && x.StatusType == default).Sum(x => x.Amount),
                            notReservedNAdvacne = currentMonthRecord.Where(x => x.CashType == CashType.NotReserved && x.StatusType == default).Sum(x => x.Amount),

                            totalNAmount = NextMonthRecord.Sum(x => x.Amount),

                            //Current Month Cahsed
                            totalCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed  ),
                            reservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved),
                            notReservedCashedCheque = currentMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved ),
                            totalCashedAmount = currentMonthRecord.Where(x => x.StatusType == StatusType.Cashed ).Sum(x => x.Amount),

                            //Previosus Month Cahsed
                            totalPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed),
                            reservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.Reserved ),
                            notReservedPCashedCheque = previousMonthRecord.Count(x => x.StatusType == StatusType.Cashed && x.CashType == CashType.NotReserved ),
                            totalPCashedAmount = previousMonthRecord.Where(x => x.StatusType == StatusType.Cashed ).Sum(x => x.Amount),

                        });






                        return chequebookList;
                    }





                    return null;

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
