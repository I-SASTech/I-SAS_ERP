using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Noble.Report.NobleDefaultServices
{
    public static class GetToken
    {

        public static List<ModuleWiseClaimsLookupModel> TokenValue(Guid companyIdAsParam)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ServerString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                connection.Open();
                string sqlQuery = "SELECT * FROM ReportT where CompanyId='" + companyIdAsParam+"'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Execute the query
                    SqlDataReader reader = command.ExecuteReader();

                    // Process the retrieved records
                    while (reader.Read())
                    {

                        // Access the data using reader["ColumnName"] or reader[index]
                        string companyId = reader["CompanyId"].ToString();

                        if (companyIdAsParam == Guid.Parse(companyId))
                        {

                            string token = reader["Token"].ToString();
                            return JsonConvert.DeserializeObject<List<ModuleWiseClaimsLookupModel>>(token);


                        }

                    }

                    // Close the reader
                    reader.Close();
                }

                connection.Close();
            }

            return null;
        }
    }
}
