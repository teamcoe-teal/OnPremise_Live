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
    public class DiagnosticLiveController : ApiController
    {
        // GET: DiagnosticLive
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        // GET: Alert
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetDiagnosticlivedata(Models.Diagnostics live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.Diagnostics>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandTimeout = 60000;

                    //if (live.Line_Code.ToUpper() == "SIM" || live.Line_Code.ToUpper() == "L1" || live.Line_Code.ToUpper() == "W5K")
                    //{
                    //    command = new SqlCommand("SELECT distinct OUTSIDE.* FROM [Diagnostics_RawTable] OUTSIDE,(SELECT distinct[Device_ID], [Device_ref], MAX(Time_stamp) as maxtimestamp " +
                    //    "FROM [Diagnostics_RawTable] where CompanyCode = @CompanyCode AND  PlantCode = @PlantCode " +
                    //    "AND LineCode = @Line_Code " +
                    //    "GROUP BY[Device_ID], [Device_ref]) AS INSIDE " +
                    //    "WHERE OUTSIDE.[Device_ID] = INSIDE.[Device_ID] AND OUTSIDE.[Device_ref] = INSIDE.[Device_ref] " +
                    //    "AND OUTSIDE.Time_stamp = INSIDE.maxtimestamp " +
                    //    "AND OUTSIDE.Device_ref!='message' AND OUTSIDE.device_ref!='code' AND OUTSIDE.device_ref!='success' " +
                    //    "AND OUTSIDE.CompanyCode= @CompanyCode AND OUTSIDE.PlantCode= @PlantCode AND OUTSIDE.LineCode= @Line_Code " +
                    //    "order by devicename, OUTSIDE.[Device_ref] desc", connection);
                    //}
                    //else if (live.Line_Code.ToUpper() == "MPS")
                    //{
                    //    command = new SqlCommand("SELECT distinct OUTSIDE.* FROM [Diagnostics_RawTable] OUTSIDE,(SELECT distinct[Device_ID], [Device_ref], MAX(Time_stamp) as maxtimestamp " +
                    //    "FROM [Diagnostics_RawTable] where CompanyCode = @CompanyCode AND  PlantCode = @PlantCode " +
                    //    "AND LineCode = @Line_Code and device_ref!='plc5_status' and device_ref!='plc4_status' and device_ref!='plc3_status' and device_ref!='plc2_status' " +
                    //    "GROUP BY[Device_ID], [Device_ref]) AS INSIDE " +
                    //    "WHERE OUTSIDE.[Device_ID] = INSIDE.[Device_ID] AND OUTSIDE.[Device_ref] = INSIDE.[Device_ref] " +
                    //    "AND OUTSIDE.Time_stamp = INSIDE.maxtimestamp " +
                    //    "AND OUTSIDE.Device_ref!='message' AND OUTSIDE.device_ref!='code' AND OUTSIDE.device_ref!='success' " +
                    //    "AND OUTSIDE.CompanyCode= @CompanyCode AND OUTSIDE.PlantCode= @PlantCode AND OUTSIDE.LineCode= @Line_Code " +
                    //    "order by devicename, OUTSIDE.[Device_ref] desc", connection);
                    //}
                    //else if (live.Line_Code.ToUpper() == "MPS1")
                    //{
                    //    command = new SqlCommand("SELECT distinct OUTSIDE.* FROM [Diagnostics_RawTable] OUTSIDE,(SELECT distinct[Device_ID], [Device_ref], MAX(Time_stamp) as maxtimestamp " +
                    //    "FROM [Diagnostics_RawTable] where CompanyCode = @CompanyCode AND  PlantCode = @PlantCode " +
                    //    "AND LineCode = @Line_Code and device_ref!='plc5_status' and device_ref!='plc4_status' and device_ref!='plc3_status' and device_ref!='plc1_status' " +
                    //    "GROUP BY[Device_ID], [Device_ref]) AS INSIDE " +
                    //    "WHERE OUTSIDE.[Device_ID] = INSIDE.[Device_ID] AND OUTSIDE.[Device_ref] = INSIDE.[Device_ref] " +
                    //    "AND OUTSIDE.Time_stamp = INSIDE.maxtimestamp " +
                    //    "AND OUTSIDE.Device_ref!='message' AND OUTSIDE.device_ref!='code' AND OUTSIDE.device_ref!='success' " +
                    //    "AND OUTSIDE.CompanyCode= @CompanyCode AND OUTSIDE.PlantCode= @PlantCode AND OUTSIDE.LineCode= @Line_Code " +
                    //    "order by devicename, OUTSIDE.[Device_ref] desc", connection);
                    //}
                    //else if (live.Line_Code.ToUpper() == "MPS2")
                    //{
                    //    command = new SqlCommand("SELECT distinct OUTSIDE.* FROM [Diagnostics_RawTable] OUTSIDE,(SELECT distinct[Device_ID], [Device_ref], MAX(Time_stamp) as maxtimestamp " +
                    //    "FROM [Diagnostics_RawTable] where CompanyCode = @CompanyCode AND  PlantCode = @PlantCode " +
                    //    "AND LineCode = @Line_Code and device_ref!='plc5_status' and device_ref!='plc4_status' and device_ref!='plc2_status' and device_ref!='plc1_status' " +
                    //    "GROUP BY[Device_ID], [Device_ref]) AS INSIDE " +
                    //    "WHERE OUTSIDE.[Device_ID] = INSIDE.[Device_ID] AND OUTSIDE.[Device_ref] = INSIDE.[Device_ref] " +
                    //    "AND OUTSIDE.Time_stamp = INSIDE.maxtimestamp " +
                    //    "AND OUTSIDE.Device_ref!='message' AND OUTSIDE.device_ref!='code' AND OUTSIDE.device_ref!='success' " +
                    //    "AND OUTSIDE.CompanyCode= @CompanyCode AND OUTSIDE.PlantCode= @PlantCode AND OUTSIDE.LineCode= @Line_Code " +
                    //    "order by devicename, OUTSIDE.[Device_ref] desc", connection);
                    //}
                    //else if (live.Line_Code.ToUpper() == "MPS3")
                    //{
                    //    command = new SqlCommand("SELECT distinct OUTSIDE.* FROM [Diagnostics_RawTable] OUTSIDE,(SELECT distinct[Device_ID], [Device_ref], MAX(Time_stamp) as maxtimestamp " +
                    //    "FROM [Diagnostics_RawTable] where CompanyCode = @CompanyCode AND  PlantCode = @PlantCode " +
                    //    "AND LineCode = @Line_Code and device_ref!='plc5_status' and device_ref!='plc3_status' and device_ref!='plc2_status' and device_ref!='plc1_status' " +
                    //    "GROUP BY[Device_ID], [Device_ref]) AS INSIDE " +
                    //    "WHERE OUTSIDE.[Device_ID] = INSIDE.[Device_ID] AND OUTSIDE.[Device_ref] = INSIDE.[Device_ref] " +
                    //    "AND OUTSIDE.Time_stamp = INSIDE.maxtimestamp " +
                    //    "AND OUTSIDE.Device_ref!='message' AND OUTSIDE.device_ref!='code' AND OUTSIDE.device_ref!='success' " +
                    //    "AND OUTSIDE.CompanyCode= @CompanyCode AND OUTSIDE.PlantCode= @PlantCode AND OUTSIDE.LineCode= @Line_Code " +
                    //    "order by devicename, OUTSIDE.[Device_ref] desc", connection);
                    //}
                    //else if (live.Line_Code.ToUpper() == "MPS4")
                    //{
                    //    command = new SqlCommand("SELECT distinct OUTSIDE.* FROM [Diagnostics_RawTable] OUTSIDE,(SELECT distinct[Device_ID], [Device_ref], MAX(Time_stamp) as maxtimestamp " +
                    //    "FROM [Diagnostics_RawTable] where CompanyCode = @CompanyCode AND  PlantCode = @PlantCode " +
                    //    "AND LineCode = @Line_Code and device_ref!='plc4_status' and device_ref!='plc3_status' and device_ref!='plc2_status' and device_ref!='plc1_status' " +
                    //    "GROUP BY[Device_ID], [Device_ref]) AS INSIDE " +
                    //    "WHERE OUTSIDE.[Device_ID] = INSIDE.[Device_ID] AND OUTSIDE.[Device_ref] = INSIDE.[Device_ref] " +
                    //    "AND OUTSIDE.Time_stamp = INSIDE.maxtimestamp " +
                    //    "AND OUTSIDE.Device_ref!='message' AND OUTSIDE.device_ref!='code' AND OUTSIDE.device_ref!='success' " +
                    //    "AND OUTSIDE.CompanyCode= @CompanyCode AND OUTSIDE.PlantCode= @PlantCode AND OUTSIDE.LineCode= @Line_Code " +
                    //    "order by devicename, OUTSIDE.[Device_ref] desc", connection);
                    //}
                    //else
                    //{
                    //    command = new SqlCommand("SELECT distinct OUTSIDE.*,b.devicename FROM [Diagnostics_Event_details] OUTSIDE " +
                    //    "join tbl_ewon_details b on OUTSIDE.device_id = b.device_id," +
                    //    "(SELECT distinct[Device_ID], [Device_ref], MAX(LogTime) as maxtimestamp " +
                    //    "FROM[Diagnostics_Event_details] where CompanyCode = @CompanyCode AND  PlantCode = @PlantCode " +
                    //    "AND LineCode = @Line_Code GROUP BY[Device_ID], [Device_ref]) AS INSIDE " +
                    //    "WHERE OUTSIDE.[Device_ID] = INSIDE.[Device_ID] AND OUTSIDE.[Device_ref] = INSIDE.[Device_ref] " +
                    //    "AND OUTSIDE.LogTime = INSIDE.maxtimestamp " +
                    //    "AND OUTSIDE.Device_ref!='message' AND OUTSIDE.device_ref!='code' AND OUTSIDE.device_ref!='success' " +
                    //    "AND OUTSIDE.CompanyCode= @CompanyCode AND OUTSIDE.PlantCode= @PlantCode AND OUTSIDE.LineCode= @Line_Code " +
                    //    "order by b.devicename, OUTSIDE.[Device_ref] desc", connection);
                    //}
                    SqlCommand cmd = new SqlCommand("sp_Diagnostic_Live", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                    cmd.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                    cmd.Parameters.AddWithValue("@Line_Code", live.Line_Code);
                   

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(avllive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.Diagnostics
                            {

                                DeviceID = Convert.ToString(reader["Device_ID"] == DBNull.Value ? "" : reader["Device_ID"]),
                                Devicename = Convert.ToString(reader["devicename"] == DBNull.Value ? "" : reader["devicename"]),
                                DeviceRef = Convert.ToString(reader["Device_ref"] == DBNull.Value ? "" : reader["Device_ref"]),
                                Event = Convert.ToString(reader["EventName"] == DBNull.Value ? "" : reader["EventName"]),
                                Time_stamp = Convert.ToDateTime(reader["Time_stamp"] == DBNull.Value ? "" : reader["Time_stamp"]),

                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
            }
    }
}