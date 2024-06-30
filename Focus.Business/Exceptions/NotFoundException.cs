using System;

namespace Focus.Business.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
               : base(name)
        {
        }
    }
}
