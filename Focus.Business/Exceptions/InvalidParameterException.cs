using System;

namespace Focus.Business.Exceptions
{
   public class InvalidParameterException: ApplicationException
    {
        public InvalidParameterException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
