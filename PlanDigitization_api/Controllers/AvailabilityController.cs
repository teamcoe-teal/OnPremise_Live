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
    //[EnableCors(origins: "http://localhost:55974", headers: "*", methods: "*")]
    public class AvailabilityController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetAvllivedata(Models.Availability_Live live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.Availability_Live>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    //using (var command = new SqlCommand(@"SELECT [ShiftID]
                    //                                      ,[Line_Code]
                    //                                      ,[Machine_Code]
                    //                                      ,[Machine_Status]
                    //                                      ,[Availability]
                    //                                      ,[UpTime]
                    //                                      ,[DownTime]
                    //                                      ,[LossTime]
                    //                                      ,[TotalProductionTime]
                    //                                      ,[lastupdate]
                    //                                  FROM [dbo].[Live_Availability] WHERE CompanyCode='" + live.CompanyCode + "' AND PlantCode='" + live.PlantCode + "' AND Line_Code='" + live.Line_Code + "' ", connection))

                    SqlCommand command = new SqlCommand("SELECT ShiftID,Line_Code,Machine_Code,Machine_Status,Availability,UpTime,DownTime,LossTime,TotalProductionTime,lastupdate from dbo.Live_Availability where CompanyCode=@CompanyCode and PlantCode=@PlantCode and Line_Code=@Line_Code and Convert(Date,lastupdate)=@currentdate", connection);


                    command.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                    command.Parameters.AddWithValue("@Line_Code", live.Line_Code);
                    command.Parameters.AddWithValue("@currentdate", DateTime.Now.ToString("yyyy/MM/dd"));
                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(avllive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.Availability_Live
                            {
                                ShiftID = Convert.ToString(reader["ShiftID"] == DBNull.Value ? "" : reader["ShiftID"]),
                                Line_Code = Convert.ToString(reader["Line_Code"] == DBNull.Value ? "" : reader["Line_Code"]),
                                Machine_Code = Convert.ToString(reader["Machine_Code"] == DBNull.Value ? "" : reader["Machine_Code"]),
                                Machine_Status = Convert.ToString(reader["Machine_Status"] == DBNull.Value ? "" : reader["Machine_Status"]),
                                Avail = Convert.ToDecimal(reader["Availability"] == DBNull.Value ? "" : reader["Availability"]),
                                UpTime = Convert.ToDecimal(reader["UpTime"] == DBNull.Value ? "" : reader["UpTime"]),
                                DownTime = Convert.ToDecimal(reader["DownTime"] == DBNull.Value ? "" : reader["DownTime"]),
                                LossTime = Convert.ToDecimal(reader["LossTime"] == DBNull.Value ? "" : reader["LossTime"]),
                                Totaltime = Convert.ToInt32(reader["TotalProductionTime"] == DBNull.Value ? "" : reader["TotalProductionTime"]),
                                lastupdate = Convert.ToDateTime(reader["lastupdate"] == DBNull.Value ? null : reader["lastupdate"]),
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
            
            }

        //private void avllive(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        PlantDigitizationhub.SendDatas();
        //    }
        //}
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetAvllivedata_mcwise(Models.Availability_Live live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.Availability_Live>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    //using (var command = new SqlCommand(@"SELECT [ShiftID]
                    //                                      ,[Line_Code]
                    //                                      ,[Machine_Code]
                    //                                      ,[Machine_Status]
                    //                                      ,[Availability]
                    //                                      ,[UpTime]
                    //                                      ,[DownTime]
                    //                                      ,[LossTime]
                    //                                      ,[TotalProductionTime]
                    //                                      ,[lastupdate]
                    //                                  FROM [dbo].[Live_Availability] WHERE CompanyCode='" + live.CompanyCode + "' AND PlantCode='" + live.PlantCode + "' AND Line_Code='" + live.Line_Code + "' ", connection))

                    SqlCommand command = new SqlCommand("SELECT ShiftID,Line_Code,Machine_Code,Machine_Status,Availability,UpTime,DownTime,LossTime,TotalProductionTime,lastupdate from dbo.Live_Availability where CompanyCode=@CompanyCode and PlantCode=@PlantCode and Line_Code=@Line_Code and machine_code=@machinecode", connection);


                    command.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                    command.Parameters.AddWithValue("@Line_Code", live.Line_Code);
                    command.Parameters.AddWithValue("@machinecode", live.Machine_Code);

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(avllive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.Availability_Live
                            {
                                ShiftID = Convert.ToString(reader["ShiftID"] == DBNull.Value ? "" : reader["ShiftID"]),
                                Line_Code = Convert.ToString(reader["Line_Code"] == DBNull.Value ? "" : reader["Line_Code"]),
                                Machine_Code = Convert.ToString(reader["Machine_Code"] == DBNull.Value ? "" : reader["Machine_Code"]),
                                Machine_Status = Convert.ToString(reader["Machine_Status"] == DBNull.Value ? "" : reader["Machine_Status"]),
                                Avail = Convert.ToDecimal(reader["Availability"] == DBNull.Value ? "" : reader["Availability"]),
                                UpTime = Convert.ToDecimal(reader["UpTime"] == DBNull.Value ? "" : reader["UpTime"]),
                                DownTime = Convert.ToDecimal(reader["DownTime"] == DBNull.Value ? "" : reader["DownTime"]),
                                LossTime = Convert.ToDecimal(reader["LossTime"] == DBNull.Value ? "" : reader["LossTime"]),
                                Totaltime = Convert.ToInt32(reader["TotalProductionTime"] == DBNull.Value ? "" : reader["TotalProductionTime"]),
                                lastupdate = Convert.ToDateTime(reader["lastupdate"] == DBNull.Value ? null : reader["lastupdate"]),
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }
    }
}
