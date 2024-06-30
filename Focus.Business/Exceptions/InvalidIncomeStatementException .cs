using System;

namespace Focus.Business.Exceptions
{
    public class InvalidIncomeStatementException  : ApplicationException
    {
        public InvalidIncomeStatementException(string errorMessage) : base(errorMessage)
        {
            
        }
    }
}