using System;
using System.Collections.Generic;

namespace Focus.Business.ExpenseTyp.Commands
{
    public class ExpenseTypeLookUpModel
    {
        public Guid Id { get; set; }
        public string ExpenseTypeName { get; set; }
        public string ExpenseTypeArabic { get; set; }
        public string Description { get; set; }
        public Guid? AccountId { get; set; }
        public bool Status { get; set; }
    }
}
