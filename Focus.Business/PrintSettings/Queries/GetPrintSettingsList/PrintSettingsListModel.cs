using System.Collections.Generic;

namespace Focus.Business.PrintSettings.Queries.GetPrintSettingsList
{
    public class PrintSettingListModel
    {
        public IList<PrintSettingLookUpModel> PrintSettings { get; set; }
    }
}
