using System;

namespace Focus.Business.Exceptions
{
    public class ObjectAlreadyExistsException : ApplicationException
    {
        public ObjectAlreadyExistsException(string errorMessage) : base(errorMessage)
        {

        }
    }
}