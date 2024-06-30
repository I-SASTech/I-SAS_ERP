using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ChequeBooks.Queries
{
  public  class IssuedToLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public Guid? AccountId { get; set; }

    }
}
