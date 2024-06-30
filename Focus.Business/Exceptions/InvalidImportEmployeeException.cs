using System;

namespace Focus.Business.Exceptions
{
    public class InvalidImportEmployeeException : ApplicationException
    {
        public InvalidImportEmployeeException(string errorMessage) : base(errorMessage)
        {
            
        }
    }
}