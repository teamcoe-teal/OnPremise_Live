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
    public class AndonController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpPost]
        public HttpResponseMessage GetAndonLivedata(Models.AndonLive live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.Line_code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.AndonLive>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    // using (var command = new SqlCommand(@"SELECT [Line_code]
                    //    ,[Machinecode]
                    //   ,[Andonreason]
                    // ,[FromDepartment]
                    //,[ToDepartment]
                    //,[Start_Time]
                    //,[Status]
                    //FROM dbo.tbl_Andon_live WHERE CompanyCode='" + live.CompanyCode + "' AND PlantCode='" + live.PlantCode + "' AND Line_code='" + live.Line_code + "'", connection))
                    SqlCommand command = new SqlCommand("SELECT Line_code,Machinecode,Andonreason,FromDepartment,ToDepartment,Start_Time,Status from dbo.tbl_Andon_live where CompanyCode=@CompanyCode and PlantCode=@PlantCode and Line_code=@Line_code", connection);


                    command.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                    command.Parameters.AddWithValue("@Line_code", live.Line_code);
                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(andonlive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.AndonLive
                            {
                                Line_code = (string)reader["Line_code"],
                                Machine_code = (string)reader["Machinecode"],
                                Andon_reason = (string)reader["Andonreason"],
                                From_department = (string)reader["FromDepartment"],
                                To_department = (string)reader["ToDepartment"],
                                Status = (string)reader["Status"],
                                Start_time = (DateTime)reader["Start_Time"]
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
            }

        //private void andonlive(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        PlantDigitizationhub.SendDatas();
        //    }
        //}
    }
}
