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
    public class ToollifeController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetToollivelivedatas(Models.ToolLifelive live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.Line_code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                try
                {
                    var messages = new List<Models.ToolLifelive>();
                    using (var connection = new SqlConnection(con_string))
                    {
                        connection.Open();
                        //    using (var command = new SqlCommand(@"SELECT [Line_code],[make],[ToolName],[Machine_code],[Part_number],[Classification],[Conversion_parameter],
                        //                                        ISNULL([currentlifecle],'') AS 'currentlifecy',ISNULL([ratedlifecle],'') AS 'ratedlifecy',ISNULL([usage],'') AS 'usg',[UOM],[lastmain] FROM dbo.tbl_temp_toollife_rawdata WHERE  CompanyCode='" + live.CompanyCode + "' AND PlantCode='" + live.PlantCode + "' AND Line_code='"+live.Line_code + "' ", connection))

                        SqlCommand command = new SqlCommand("SELECT a.Line_code,b.AssetName,a.make,a.ToolName,a.Machine_code,a.Part_number," +
                            "a.Classification,a.Conversion_parameter,ISNULL(CONVERT(varchar, a.currentlifecle, 23),'') AS 'currentlifecy'," +
                            "ISNULL(CONVERT(varchar, a.ratedlifecle, 23),'') AS 'ratedlifecy',ISNULL(CONVERT(varchar, a.usage, 23),'') AS 'usg',a.[UOM],a.[lastmain],a.RecommendationText,a.[Status],a.Next_PM " +
                            "from dbo.tbl_temp_toollife_rawdata a " +
                            "join tbl_assets b on b.FunctionName = a.Line_Code and b.companycode = a.Companycode and b.plantcode = a.Plantcode and b.AssetID = a.Machine_code " +
                            "where a.CompanyCode = @CompanyCode and a.PlantCode = @PlantCode and a.Line_code = @Line_code", connection);


                        command.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                        command.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                        command.Parameters.AddWithValue("@Line_code", live.Line_code);
                        {
                            //command.Notification = null;
                            //var dependency = new SqlDependency(command);
                            //dependency.OnChange += new OnChangeEventHandler(livedata);

                            if (connection.State == ConnectionState.Closed)
                                connection.Open();

                            var reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                messages.Add(item: new Models.ToolLifelive
                                {
                                    //Line_code = Convert.ToString(reader["Line_code"] == DBNull.Value ? "" : reader["Line_code"]),
                                    //Make = Convert.ToString(reader["make"] == DBNull.Value ? "" : reader["make"]),
                                    //ToolName = Convert.ToString(reader["ToolName"] == DBNull.Value ? "" : reader["ToolName"]),
                                    //Machine_code = Convert.ToString(reader["Machine_code"] == DBNull.Value ? "" : reader["Machine_code"]),
                                    //Part_number = Convert.ToString(reader["Part_number"] == DBNull.Value ? "" : reader["Part_number"]),
                                    //Classification = Convert.ToString(reader["Classification"] == DBNull.Value ? "" : reader["Classification"]),
                                    //Conversion_parameter = Convert.ToString(reader["Conversion_parameter"] == DBNull.Value ? "" : reader["Conversion_parameter"]),
                                    //currentlifecle = Convert.ToDecimal(reader["currentlifecy"] == DBNull.Value ? "" : reader["currentlifecy"]),
                                    //ratedlifecle = Convert.ToDecimal(reader["ratedlifecy"] == DBNull.Value ? "" : reader["ratedlifecy"]),
                                    //usage = Convert.ToDecimal(reader["usg"] == DBNull.Value ? "" : reader["usg"]),
                                    //UOM = Convert.ToString(reader["UOM"] == DBNull.Value ? "" : reader["UOM"]),
                                    //lastmain = Convert.ToString(reader["lastmain"] == DBNull.Value ? "" : reader["lastmain"]),
                                    //line_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),
                                    //RecText = Convert.ToString(reader["RecommendationText"] == DBNull.Value ? "" : reader["RecommendationText"]),
                                    //Next_PM = Convert.ToString(reader["Next_PM"] == DBNull.Value ? "" : reader["Next_PM"])

                                    Line_code = Convert.ToString(reader["Line_code"] == DBNull.Value ? "" : reader["Line_code"]),
                                    Make = Convert.ToString(reader["make"] == DBNull.Value ? "" : reader["make"]),
                                    ToolName = Convert.ToString(reader["ToolName"] == DBNull.Value ? "" : reader["ToolName"]),
                                    Machine_code = Convert.ToString(reader["Machine_code"] == DBNull.Value ? "" : reader["Machine_code"]),
                                    Part_number = Convert.ToString(reader["Part_number"] == DBNull.Value ? "" : reader["Part_number"]),
                                    Classification = Convert.ToString(reader["Classification"] == DBNull.Value ? "" : reader["Classification"]),
                                    Conversion_parameter = Convert.ToString(reader["Conversion_parameter"] == DBNull.Value ? "" : reader["Conversion_parameter"]),
                                    currentlifecle = Convert.ToDecimal(reader["currentlifecy"] == DBNull.Value ? "" : reader["currentlifecy"]),
                                    ratedlifecle = Convert.ToDecimal(reader["ratedlifecy"] == DBNull.Value ? "" : reader["ratedlifecy"]),
                                    usage = Convert.ToDecimal(reader["usg"] == DBNull.Value ? "" : reader["usg"]),
                                    UOM = Convert.ToString(reader["UOM"] == DBNull.Value ? "" : reader["UOM"]),
                                    lastmain = Convert.ToString(reader["lastmain"] == DBNull.Value ? "" : reader["lastmain"]),
                                    line_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),
                                    RecText = Convert.ToString(reader["RecommendationText"] == DBNull.Value ? "" : reader["RecommendationText"]),
                                    Status = Convert.ToString(reader["Status"] == DBNull.Value ? "" : reader["Status"]),
                                    Next_PM = Convert.ToString(reader["Next_PM"] == DBNull.Value ? "" : reader["Next_PM"])

                                

                                });
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Added Successfully", data = messages });
                }
                catch(Exception ex)
                {
                    var res = ex;
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Details Added Successfully", data = "" });

                }
            }
        }


        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetDiesetlivedatas(Models.disetsetting live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.LineCode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.disetsetting>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    //    using (var command = new SqlCommand(@"SELECT [Line_code],[make],[ToolName],[Machine_code],[Part_number],[Classification],[Conversion_parameter],
                    //                                        ISNULL([currentlifecle],'') AS 'currentlifecy',ISNULL([ratedlifecle],'') AS 'ratedlifecy',ISNULL([usage],'') AS 'usg',[UOM],[lastmain] FROM dbo.tbl_temp_toollife_rawdata WHERE  CompanyCode='" + live.CompanyCode + "' AND PlantCode='" + live.PlantCode + "' AND Line_code='"+live.Line_code + "' ", connection))

                    SqlCommand command = new SqlCommand("select a.[ToolID],c.ToolName,a.[Machine_code],b.AssetName,a.[Start_Time],a.[EndTime],a.[Instance],a.[Production_Qty],a.[Cummulative_Qty] " +
                        "from[dbo].[Dieset_stopandstart_Rawtable] a " +
                        "join[dbo].[tbl_toollist] c on c.ToolID = a.ToolID " +
                        "join tbl_assets b on b.FunctionName = a.Line_code and b.companycode = a.Companycode and b.plantcode = a.Plantcode and b.AssetID = a.Machine_code " +
                        "where a.Instance = (select max(Instance) from Dieset_stopandstart_Rawtable) order by start_time desc", connection);


                    command.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                    //command.Parameters.AddWithValue("@Line_code", live.Line_code);
                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(livedata);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.disetsetting
                            {
                                toolid = Convert.ToString(reader["ToolID"] == DBNull.Value ? "" : reader["ToolID"]),
                                toolname = Convert.ToString(reader["ToolName"] == DBNull.Value ? "" : reader["ToolName"]),
                                MachineCode = Convert.ToString(reader["Machine_code"] == DBNull.Value ? "" : reader["Machine_code"]),
                                linename = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),
                                instance = Convert.ToInt32(reader["Instance"] == DBNull.Value ? 0 : reader["Instance"]),
                                starttime = Convert.ToDateTime(reader["Start_Time"] == DBNull.Value ? null : reader["Start_Time"]),
                                stoptime = Convert.ToDateTime(reader["EndTime"] == DBNull.Value ? null : reader["EndTime"]),
                                production = Convert.ToInt32(reader["Production_Qty"] == DBNull.Value ? 0 : reader["Production_Qty"]),
                                cummulative = Convert.ToInt32(reader["Cummulative_Qty"] == DBNull.Value ? 0 : reader["Cummulative_Qty"])

                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Added Successfully", data = messages });
            }
        }

        //private void livedata(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        PlantDigitizationhub.SendDatas();
        //    }
        //}
    }
}

