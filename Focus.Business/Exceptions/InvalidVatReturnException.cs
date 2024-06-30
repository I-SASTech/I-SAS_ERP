using System;

namespace Focus.Business.Exceptions
{
    public class InvalidVatReturnException : ApplicationException
    {
        public InvalidVatReturnException(string errorMessage) : base(errorMessage)
        {
            
        }
    }
}