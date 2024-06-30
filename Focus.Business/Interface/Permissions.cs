using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Interface
{
    public class Permissions
    {
        public class Brand
        {
            public const string Save = "Permissions.Brand.Save";
            public const string Edit = "Permissions.Brand.Edit";
            public const string Delete = "Permissions.Brand.Delete";
            public const string List = "Permissions.Brand.List";
        }
        public class Category
        {
            public const string Save = "Permissions.Category.Save";
            public const string Edit = "Permissions.Category.Edit";
            public const string Delete = "Permissions.Category.Delete";
            public const string List = "Permissions.Category.List";
        }
        public class Color
        {
            public const string Save = "Permissions.Color.Save";
            public const string Edit = "Permissions.Color.Edit";
            public const string Delete = "Permissions.Color.Delete";
            public const string List = "Permissions.Color.List";
        }
        public class Origin
        {
            public const string Save = "Permissions.Origin.Save";
            public const string Edit = "Permissions.Origin.Edit";
            public const string Delete = "Permissions.Origin.Delete";
            public const string List = "Permissions.Origin.List";
        }
    }
}
