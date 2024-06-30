using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class ForeignKeyAttribute: Attribute
    {
        public string ForeignKeyName { get; }

        public ForeignKeyAttribute(string foreignKeyName)
        {
            ForeignKeyName = foreignKeyName;
        }

    }
}
