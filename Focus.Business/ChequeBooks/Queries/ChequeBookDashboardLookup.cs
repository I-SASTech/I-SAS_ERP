using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ChequeBooks.Queries
{
   public class ChequeBookDashboardLookup
    {
        public string ChequeType { get; set; }
        public int totalCAdvanceCheque { get; set; }
        public decimal reservedCAdvacne { get; set; }
        public decimal notReservedCAdvacne { get; set; }
        public decimal totalCAmount { get; set; }
        public int totalNAdvanceCheque { get; set; }
        public decimal reservedNAdvacne { get; set; }
        public decimal notReservedNAdvacne { get; set; }
        public decimal totalNAmount { get; set; }
        public int totalCashedCheque { get; internal set; }
        public int reservedPCashedCheque { get; internal set; }
        public int notReservedPCashedCheque { get; internal set; }
        public decimal totalPCashedAmount { get; internal set; }
        public int totalPCashedCheque { get; internal set; }
        public int reservedCashedCheque { get; internal set; }
        public int notReservedCashedCheque { get; internal set; }
        public decimal totalCashedAmount { get; internal set; }
    }
}
