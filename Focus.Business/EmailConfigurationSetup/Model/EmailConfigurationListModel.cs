using Focus.Business.Colors.Queries.GetColorList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.EmailConfigurationSetup.Model
{
    public class EmailConfigurationListModel
    {
        public IList<EmailConfigurationLookupModel> EmailConfigurations { get; set; }
    }
}
