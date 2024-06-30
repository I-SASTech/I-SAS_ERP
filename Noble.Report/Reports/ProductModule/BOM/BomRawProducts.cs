using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.ProductModule.BOM
{
    public partial class BomRawProducts : DevExpress.XtraReports.UI.XtraReport
    {
        public BomRawProducts(List<BomRawProductsLookupModel> bom)
        {
            objectDataSource1.DataSource = bom;
            
        }

    }
}
