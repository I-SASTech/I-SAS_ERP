using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.DataAccess.Sql;
using SqlDataSource = DevExpress.DataAccess.Sql.SqlDataSource;

namespace Noble.Report {
    public partial class Designer : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) 
                BindToData();
            ASPxReportDesigner1.OpenReport(new TestReport());
        }

        void BindToData() {
            // Create a SQL data source with the specified connection string.
            SqlDataSource ds = new SqlDataSource("NWindConnectionString");

            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();

            // Add the created data source to the list of default data sources. 
            ASPxReportDesigner1.DataSources.Add("Northwind", ds);
        }
    }
}