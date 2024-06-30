using Newtonsoft.Json;
using Noble.Report.NobleDefaultServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Noble.Report
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                var myData = new List<ModuleWiseClaimsLookupModel>();
                var jsonString = new StreamReader(Request.InputStream).ReadToEnd();
                myData = JsonConvert.DeserializeObject<List<ModuleWiseClaimsLookupModel>>(jsonString);
                var clientCompanyId = myData.Where(x => x.Token == "ServerAddress").Select(x => x.CompanyId).FirstOrDefault();
                var serverPath = myData.Where(x => x.Token == "ServerAddress").Select(x => x.TokenName).FirstOrDefault();
                string connectionString = ConfigurationManager.ConnectionStrings["ServerString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))

                {
                    connection.Open();
                    {
                        var token1 = JsonConvert.SerializeObject(myData);

                        string sqlQueryUpdate = $"Update ReportT SET Token='{token1}', [ServerName]='{serverPath}' Where CompanyId='{clientCompanyId}'";
                        using (SqlCommand command12 = new SqlCommand(sqlQueryUpdate, connection))
                        {
                            int rowsAffected = command12.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                connection.Close();
                            }
                            else
                            {
                                connection.Close();

                            }

                        }

                    }
                    connection.Open();
                    string sqlQuery = "SELECT * FROM ReportT Where CompanyId='" + clientCompanyId + "'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            string companyId = reader["CompanyId"].ToString();

                            if (clientCompanyId == Guid.Parse(companyId))
                            {


                                string token = reader["Token"].ToString();

                                myData = JsonConvert.DeserializeObject<List<ModuleWiseClaimsLookupModel>>(token);

                            }
                            else
                            {
                                var token1 = JsonConvert.SerializeObject(myData);
                                string sqlQuery1 = "INSERT INTO ReportT (CompanyId, Token) VALUES (@CompanyId, @Token)";

                                using (SqlCommand command1 = new SqlCommand(sqlQuery1, connection))
                                {
                                    // Add parameters to the command
                                    command1.Parameters.AddWithValue("@CompanyId", clientCompanyId);
                                    command1.Parameters.AddWithValue("@ServerName", serverPath);
                                    command1.Parameters.AddWithValue("@Token", token1);

                                    // Execute the query
                                    int rowsAffected = command1.ExecuteNonQuery();

                                    // Check if the insertion was successful
                                    if (rowsAffected > 0)
                                    {
                                        // Data saved successfully
                                    }
                                    else
                                    {
                                        // Failed to save data
                                    }
                                }

                            }
                            // ...
                        }

                        if (!reader.HasRows)
                        {
                            connection.Close();
                            var token12 = JsonConvert.SerializeObject(myData);
                            string sqlQuery12 = "INSERT INTO ReportT (CompanyId, Token,ServerName) VALUES (@CompanyId, @Token,@ServerName)";
                            //using (SqlConnection sqlConnection = new SqlConnection(connectionString))

                            {

                                connection.Open();

                                using (SqlCommand command12 = new SqlCommand(sqlQuery12, connection))
                                {
                                    // Add parameters to the command
                                    command12.Parameters.AddWithValue("@CompanyId", clientCompanyId);
                                    command12.Parameters.AddWithValue("@ServerName", serverPath);
                                    command12.Parameters.AddWithValue("@Token", token12);

                                    // Execute the query
                                    int rowsAffected = command12.ExecuteNonQuery();

                                    // Check if the insertion was successful
                                    if (rowsAffected > 0)
                                    {
                                        //sqlConnection.Close();
                                        // Data saved successfully
                                    }
                                    else
                                    {
                                        //sqlConnection.Close();

                                        // Failed to save data
                                    }
                                }
                            }

                        }



                        // Close the reader
                        reader.Close();
                    }
                    connection.Close();
                }

                Session["ServerAddress"] = myData.Where(x => x.Token == "ServerAddress").Select(x => x.TokenName).FirstOrDefault();
                Session["CompanyId"] = myData.Where(x => x.TokenName == "ServerAddress").Select(x => x.CompanyId).FirstOrDefault();
                Session["Sales"] = myData.Where(x => x.TokenName == "Sales").Select(x => x.Token).FirstOrDefault();
                Session["Expenses"] = myData.Where(x => x.TokenName == "Expenses").Select(x => x.Token).FirstOrDefault();
                Session["Other"] = myData.Where(x => x.TokenName == "Other").Select(x => x.Token).FirstOrDefault();
                Session["Setups_And_Configuration"] = myData.Where(x => x.TokenName == "Setups_And_Configuration").Select(x => x.Token).FirstOrDefault();
                Session["Purchase"] = myData.Where(x => x.TokenName == "Purchase").Select(x => x.Token).FirstOrDefault();
                Session["Product_And_Inventory_Management"] = myData.Where(x => x.TokenName == "Product_And_Inventory_Management").Select(x => x.Token).FirstOrDefault();
                Session["WareHouse_Management"] = myData.Where(x => x.TokenName == "WareHouse_Management").Select(x => x.Token).FirstOrDefault();
                Session["HR_And_PayRoll"] = myData.Where(x => x.TokenName == "HR_And_PayRoll").Select(x => x.Token).FirstOrDefault();
                Session["Settings And Permission"] = myData.Where(x => x.TokenName == "Settings And Permission").Select(x => x.Token).FirstOrDefault();
                Session["Accounting"] = myData.Where(x => x.TokenName == "Accounting").Select(x => x.Token).FirstOrDefault();
                Session["DayStart"] = myData.Where(x => x.TokenName == "DayStart").Select(x => x.Token).FirstOrDefault();
                Session["Reporting"] = myData.Where(x => x.TokenName == "Reporting").Select(x => x.Token).FirstOrDefault();
                Session["Token"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic2F1ZGtoYW5AYWwtdGFtbWFtLmNvbSIsInN1YiI6InNhdWRraGFuQGFsLXRhbW1hbS5jb20iLCJqdGkiOiJmN2UxYThiYy03MDExLTRjMjUtYjFjNi0wY2FjYmFhYjJkMGYiLCJSb2xlIjoiVXNlciIsIkNvbXBhbnlJZCI6ImI5ODBjNjc5LTJlOWUtNDNlZS0wMjQ2LTA4ZDliYWRlYTA4NiIsIlVzZXJJZCI6IjEzZGVlYjI1LTllODItNGYxOS1hMTliLWIwZmQ1MzRiMmFkZCIsIkVtYWlsIjoic2F1ZGtoYW5AYWwtdGFtbWFtLmNvbSIsIlVzZXJOYW1lIjoic2F1ZGtoYW4iLCJOb2JsZUNvbXBhbnlJZCI6IjVmOGQ1NjE0LTJjN2UtNGVjMC04NjhjLWQyNTRlNjUxNmI0ZCIsIkJ1c2luZXNzSWQiOiI1MTAzY2ZkNS1jZDZlLTRiODQtMDI0NS0wOGQ5YmFkZWEwODYiLCJDbGllbnRQYXJlbnRJZCI6ImZjMDIyNjg2LWIxNWUtNGFlNy0wMjQ0LTA4ZDliYWRlYTA4NiIsIkVtcGxveWVlSWQiOiJhNWUzNzQzMi04ZTQ2LTQzY2QtMjhlYS0wOGQ5YmFlMDkyMmMiLCJDb3VudGVySWQiOiJhNjcxNzZkMS0xYTc3LTQ0ZjUtMDRjZC0wOGQ5YmFlMDkwYmUiLCJEYXlJZCI6IjM5NGJmYzkyLTAxMWEtNGZkMi03YTI2LTA4ZGE2ZTE5Mzg4YSIsIklzUHJvY2VlZCI6dHJ1ZSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTY4Mzc4MzE3OSwiaXNzIjoiaHR0cDovL3lvdXJkb21haW4uY29tIiwiYXVkIjoiaHR0cDovL3lvdXJkb21haW4uY29tIn0.0Yyf0CyxRnHgXnTEH8zvL6fzOL5kJ-YjpqcynebjceE";

            }
        }
    }
}