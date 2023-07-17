using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace PlanDigitization_api.Controllers
{
    public class AlertController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        // GET: Alert
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetAlertlivedata(Models.Alert live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.Alert>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT Line_Code,StartTime,Machine_Code,AlertID,Variable,PropertyGroup,Parameter,P_Values " +
                        "from dbo.tbl_Live_Values_Alert where CompanyCode=@CompanyCode and PlantCode=@PlantCode and Line_Code=@Line_Code", connection);


                    command.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                    command.Parameters.AddWithValue("@Line_Code", live.Line_Code);

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(avllive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.Alert
                            {

                                Line_Code = Convert.ToString(reader["Line_Code"] == DBNull.Value ? "" : reader["Line_Code"]),
                                Machine_Code = Convert.ToString(reader["Machine_Code"] == DBNull.Value ? "" : reader["Machine_Code"]),
                                AlertID = Convert.ToString(reader["AlertID"] == DBNull.Value ? "" : reader["AlertID"]),
                                Variable = Convert.ToString(reader["Variable"] == DBNull.Value ? "" : reader["Variable"]),
                                PropertyGroup = Convert.ToString(reader["PropertyGroup"] == DBNull.Value ? "" : reader["PropertyGroup"]),
                                Parameter = Convert.ToString(reader["Parameter"] == DBNull.Value ? "" : reader["Parameter"]),
                                P_Values = Convert.ToString(reader["P_Values"] == DBNull.Value ? "" : reader["P_Values"]),
                                StartTime = Convert.ToString(reader["StartTime"] == DBNull.Value ? "" : reader["StartTime"]),
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }
    }
}