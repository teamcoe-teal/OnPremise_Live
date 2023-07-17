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

    public class OEEController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetEmployee()
        {
            return Request.CreateResponse(HttpStatusCode.OK, value: "Successfully valid");
        }

        /// <summary>
        /// Inssert & Update Function Details
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage check_holiday(Models.Holidaylive oee)
        {
            try
            {
                var messages = new List<Models.Holidaylive>();

                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();


                    SqlCommand cmd = new SqlCommand("SP_check_holiday_live", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add("@company", SqlDbType.NVarChar, 150).Value = oee.CompanyCode;
                    cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 150).Value = DateTime.Today.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@plant", SqlDbType.NVarChar, 150).Value = oee.PlantCode;


                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(oeelive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.Holidaylive
                            {

                                holiday_name = Convert.ToString(reader["HolidayReason"] == DBNull.Value ? "" : reader["HolidayReason"])

                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
            catch(Exception ex)
            {
                var exc = ex;
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Details Found", data = "" });
            }
        }






        [HttpPost]

        [CustomAuthenticationFilter]
        public HttpResponseMessage GetOEElivedata(Models.OEE_Live oee)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.OEE_Live>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    //using (var command = new SqlCommand(@"SELECT [Machine_Code]
                    //                                    ,[Availability]
                    //                                    ,[Performance]
                    //                                    ,[Quality]
                    //                                    ,[OEE]
                    //                                    FROM dbo.tbl_Live_OEE WHERE CompanyCode='" + oee.CompanyCode + "' AND PlantCode='" + oee.PlantCode + "' AND Line_Code='" + oee.Line_Code + "'  ", connection))

                    SqlCommand command = new SqlCommand("SELECT Shift_ID,Machine_Code,Availability,Performance,Quality,OEE,Machine_Status from dbo.tbl_Live_OEE where CompanyCode=@CompanyCode and PlantCode=@PlantCode and Line_Code=@Line_Code and Convert(Date,lastupdate)=@currentdate", connection);


                    command.Parameters.AddWithValue("@CompanyCode", oee.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", oee.PlantCode);
                    command.Parameters.AddWithValue("@Line_Code", oee.Line_Code);
                    command.Parameters.AddWithValue("@currentdate", DateTime.Now.ToString("yyyy/MM/dd"));

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(oeelive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.OEE_Live
                            {
                                shift_id = Convert.ToString(reader["Shift_ID"] == DBNull.Value ? "" : reader["Shift_ID"]),
                                MachineCode = Convert.ToString(reader["Machine_Code"] == DBNull.Value ? "" : reader["Machine_Code"]),
                                OEE = Convert.ToDecimal(reader["OEE"] == DBNull.Value ? 0.00 : reader["OEE"]),
                                Availability = Convert.ToDecimal(reader["Availability"] == DBNull.Value ? 0.00 : reader["Availability"]),
                                Performance = Convert.ToDecimal(reader["Performance"] == DBNull.Value ? 0.00 : reader["Performance"]),
                                Quality = Convert.ToDecimal(reader["Quality"] == DBNull.Value ? 0.00 : reader["Quality"]),
                                MachineStatus = Convert.ToString(reader["Machine_Status"] == DBNull.Value ? "" : reader["Machine_Status"]),
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetLineOEE(Models.OEE_Live oee)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.OEE_Live>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT distinct a.[Line_Code],a.CompanyCode,b.AssetName as AssetName
                        FROM dbo.tbl_Live_OEE a inner join tbl_assets b on a.machine_code=b.AssetID and a.Line_code=b.FunctionName and a.CompanyCode=b.CompanyCode and a.PlantCode=b.PlantCode
                        
                        
                        WHERE a.CompanyCode=@company AND a.PlantCode=@plant and a.Line_code=@line and b.EOL=1 ", connection);

                    cmd.Parameters.AddWithValue("@company", oee.CompanyCode);
                    cmd.Parameters.AddWithValue("@plant", oee.PlantCode);
                    cmd.Parameters.AddWithValue("@line", oee.Line_Code);

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(oeelive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.OEE_Live
                            {
                               // CompanyCode = Convert.ToString(reader["CompanyCode"] == DBNull.Value ? "" : reader["CompanyCode"]),
                               // PlantCode = Convert.ToString(reader["PlantName"] == DBNull.Value ? "" : reader["PlantName"]),
                                Line_Code = Convert.ToString(reader["Line_Code"] == DBNull.Value ? "" : reader["Line_Code"]),
                                line_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),

                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }


        //[HttpPost]
        //[CustomAuthenticationFilter]
        //public HttpResponseMessage GetDashboardOEEData(Models.OEE_Live oee)
        //{
        //    database_connection d = new database_connection();
        //    string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.Line_Code);
        //    if (con_string == "0")
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

        //    }
        //    else
        //    {
        //        var messages = new List<Models.OEE_Live>();
        //        using (var connection = new SqlConnection(con_string))
        //        {
        //            connection.Open();


        //            SqlCommand cmd = new SqlCommand("SP_Live_MainDashboard", connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;

        //            cmd.Parameters.Add("@company", SqlDbType.NVarChar, 150).Value = oee.CompanyCode;
        //            cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = oee.Line_Code;
        //            cmd.Parameters.Add("@plant", SqlDbType.NVarChar, 150).Value = oee.PlantCode;

        //            //           SqlCommand cmd;
        //            //           cmd = new SqlCommand(@"SELECT b.EOL,a.[Line_Code],a.CompanyCode,c.PlantName,Machine_Status,s.[Variantname],v.FunctionName as AssetName
        //            //              ,(select count(distinct Line_Code) FROM dbo.tbl_Live_OEE WHERE CompanyCode=@company AND PlantCode=@plant AND Line_Code=@linecode) as LineCount
        //            //              ,(select count(distinct Machine_code) FROM dbo.tbl_Live_OEE WHERE CompanyCode=@company AND PlantCode=@plant AND Line_Code=@linecode) as MachineCount
        //            //              ,[OEE]
        //            //  ,a.Machine_code
        //            //   ,Shift_ID
        //            //               ,(SELECT [Dept_name] FROM [dbo].[tbl_department] where CompanyCode=@company and PlantCode=@plant and Dept_id=@linecode)as Dept_name
        //            //              ,(SELECT sum(TotalOkParts) FROM [dbo].[Live] WHERE CompanyCode=@company AND PlantCode=@plant AND Linecode=@linecode)  as totalok
        //            //               ,(SELECT sum(TotalNokParts) FROM [dbo].[Live] WHERE CompanyCode=@company AND PlantCode=@plant AND Linecode=@linecode) as totalnok
        //            //              FROM dbo.tbl_Live_OEE a 
        //            //  inner join tbl_function v on a.Line_code=v.FunctionID and a.CompanyCode=v.CompanyCode and a.PlantCode=v.ParentPlant
        //            //  inner join tbl_assets b on a.machine_code=b.AssetName and a.Line_code=b.FunctionName and a.CompanyCode=b.CompanyCode and a.PlantCode=b.PlantCode
        //            //               inner join tbl_plant c on a.CompanyCode=c.ParentOrganization and a.PlantCode=c.PlantID
        //            //join [dbo].[tbl_MasterProduct] s on s.Machine_code=b.AssetID
        //            //join [dbo].[Live] k on s.Variant_Code=k.Variantcode 
        //            //               WHERE a.CompanyCode=@company AND a.PlantCode=@plant AND a.Line_Code=@linecode 
        //            //and k.time_stamp = (SELECT MAX(time_stamp) FROM live where CompanyCode = @company and PlantCode = @plant and linecode = @linecode )
        //            //order by EOL ", connection);
        //            //cmd.Parameters.AddWithValue("@company", oee.CompanyCode);
        //            //cmd.Parameters.AddWithValue("@plant", oee.PlantCode);
        //            //cmd.Parameters.AddWithValue("@linecode", oee.Line_Code);



        //            {
        //                //command.Notification = null;
        //                //var dependency = new SqlDependency(command);
        //                //dependency.OnChange += new OnChangeEventHandler(oeelive);

        //                if (connection.State == ConnectionState.Closed)
        //                    connection.Open();

        //                var reader = cmd.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    messages.Add(item: new Models.OEE_Live
        //                    {
        //                        shift_id = (string)reader["Shift_ID"],
        //                        MachineCode = (string)reader["Machine_Code"],
        //                        Line_Code = (string)reader["Line_Code"],
        //                        OEE = Convert.ToDecimal(reader["OEE"] == DBNull.Value ? "" : reader["OEE"]),
        //                        // Dept_name = Convert.ToString(reader["Dept_name"] == DBNull.Value ? "" : reader["Dept_name"]),
        //                        variant_name = Convert.ToString(reader["Variantname"] == DBNull.Value ? "" : reader["Variantname"]),
        //                        // line_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),
        //                        LineCount = Convert.ToInt32(reader["LineCount"] == DBNull.Value ? "" : reader["LineCount"]),
        //                        MachineCount = Convert.ToInt32(reader["MachineCount"] == DBNull.Value ? "" : reader["MachineCount"]),
        //                        totalok = Convert.ToInt32(reader["totalok"] == DBNull.Value ? 0 : reader["totalok"]),
        //                        totalnok = Convert.ToInt32(reader["totalnok"] == DBNull.Value ? 0 : reader["totalnok"]),
        //                        MachineStatus = Convert.ToString(reader["Machine_Status"] == DBNull.Value ? "" : reader["Machine_Status"]),
        //                        MachineIndex = Convert.ToString(reader["EOL"] == DBNull.Value ? "" : reader["EOL"]),
        //                        // PlantName = Convert.ToString(reader["PlantName"] == DBNull.Value ? "" : reader["PlantName"]),
        //                        Availability = Convert.ToDecimal(reader["Availability"] == DBNull.Value ? 0 : reader["Availability"]),
        //                        Performance = Convert.ToDecimal(reader["Performance"] == DBNull.Value ? 0 : reader["Performance"]),
        //                        Quality = Convert.ToDecimal(reader["Quality"] == DBNull.Value ? 0 : reader["Quality"]),
        //                        uptime = Convert.ToDecimal(reader["UpTime"] == DBNull.Value ? 0 : reader["UpTime"]),
        //                        downtime = Convert.ToDecimal(reader["DownTime"] == DBNull.Value ? 0 : reader["DownTime"]),
        //                        losstime = Convert.ToDecimal(reader["LossTime"] == DBNull.Value ? 0 : reader["LossTime"]),
        //                        breaktime = Convert.ToDecimal(reader["BreakTime"] == DBNull.Value ? 0 : reader["BreakTime"]),
        //                        last_updatedate = Convert.ToString(reader["last_updatedate"] == DBNull.Value ? "" : reader["last_updatedate"]),
        //                        Ideal_cycletime = Convert.ToString(reader["Ideal_cycletime"] == DBNull.Value ? "" : reader["Ideal_cycletime"]),
        //                        Reworkparts = Convert.ToString(reader["TotalReworkParts"] == DBNull.Value ? "" : reader["TotalReworkParts"]),
        //                        Batch_code = Convert.ToString(reader["Batch_code"] == DBNull.Value ? "" : reader["Batch_code"]),
        //                        Duration = Convert.ToString(reader["Duration"] == DBNull.Value ? "" : reader["Duration"]),
        //                        Reason = Convert.ToString(reader["Reason"] == DBNull.Value ? "" : reader["Reason"]),
        //                        Stoppage = Convert.ToString(reader["no_of_stoppage"] == DBNull.Value ? "" : reader["no_of_stoppage"]),
        //                        Lastcycletime = Convert.ToString(reader["Lastcycletime"] == DBNull.Value ? "" : reader["Lastcycletime"])

        //                    });
        //                }
        //            }
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
        //    }
        //}



        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetDashboardOEEData(Models.OEE_Live oee)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.OEE_Live>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SP_Live_MainDashboard", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add("@company", SqlDbType.NVarChar, 150).Value = oee.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = oee.Line_Code;
                    cmd.Parameters.Add("@plant", SqlDbType.NVarChar, 150).Value = oee.PlantCode;


                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(oeelive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.OEE_Live
                            {
                                shift_id = (string)reader["Shift_ID"],
                                MachineCode = (string)reader["Machine_Code"],
                                Line_Code = (string)reader["Line_Code"],
                                OEE = Convert.ToDecimal(reader["OEE"] == DBNull.Value ? "" : reader["OEE"]),
                                variant_name = Convert.ToString(reader["Variantname"] == DBNull.Value ? "" : reader["Variantname"]),
                                LineCount = Convert.ToInt32(reader["LineCount"] == DBNull.Value ? "" : reader["LineCount"]),
                                MachineCount = Convert.ToInt32(reader["MachineCount"] == DBNull.Value ? "" : reader["MachineCount"]),
                                totalok = Convert.ToInt32(reader["totalok"] == DBNull.Value ? 0 : reader["totalok"]),
                                totalnok = Convert.ToInt32(reader["totalnok"] == DBNull.Value ? 0 : reader["totalnok"]),
                                MachineStatus = Convert.ToString(reader["Machine_Status"] == DBNull.Value ? "" : reader["Machine_Status"]),
                                MachineIndex = Convert.ToString(reader["EOL"] == DBNull.Value ? "" : reader["EOL"]),
                                Availability = Convert.ToDecimal(reader["Availability"] == DBNull.Value ? 0 : reader["Availability"]),
                                Performance = Convert.ToDecimal(reader["Performance"] == DBNull.Value ? 0 : reader["Performance"]),
                                Quality = Convert.ToDecimal(reader["Quality"] == DBNull.Value ? 0 : reader["Quality"]),
                                uptime = Convert.ToDecimal(reader["UpTime"] == DBNull.Value ? 0 : reader["UpTime"]),
                                downtime = Convert.ToDecimal(reader["DownTime"] == DBNull.Value ? 0 : reader["DownTime"]),
                                losstime = Convert.ToDecimal(reader["LossTime"] == DBNull.Value ? 0 : reader["LossTime"]),
                                breaktime = Convert.ToDecimal(reader["BreakTime"] == DBNull.Value ? 0 : reader["BreakTime"]),
                                last_updatedate = Convert.ToString(reader["last_updatedate"] == DBNull.Value ? "" : reader["last_updatedate"]),
                                Ideal_cycletime = Convert.ToString(reader["Ideal_cycletime"] == DBNull.Value ? "" : reader["Ideal_cycletime"]),
                                Reworkparts = Convert.ToString(reader["TotalReworkParts"] == DBNull.Value ? "" : reader["TotalReworkParts"]),
                                Batch_code = Convert.ToString(reader["Batch_code"] == DBNull.Value ? "" : reader["Batch_code"]),
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetDashboardOEEData_Machinewise(Models.OEE_Live oee)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.OEE_Live>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[SP_Live_MainDashboard_Machinewise]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add("@company", SqlDbType.NVarChar, 150).Value = oee.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = oee.Line_Code;
                    cmd.Parameters.Add("@plant", SqlDbType.NVarChar, 150).Value = oee.PlantCode;
                    cmd.Parameters.Add("@machine", SqlDbType.NVarChar, 150).Value = oee.MachineCode;


                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(oeelive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.OEE_Live
                            {
                                shift_id = (string)reader["Shift_ID"],
                                MachineCode = (string)reader["Machine_Code"],
                                Line_Code = (string)reader["Line_Code"],
                                OEE = Convert.ToDecimal(reader["OEE"] == DBNull.Value ? "" : reader["OEE"]),
                                // Dept_name = Convert.ToString(reader["Dept_name"] == DBNull.Value ? "" : reader["Dept_name"]),
                                variant_name = Convert.ToString(reader["Variantname"] == DBNull.Value ? "" : reader["Variantname"]),
                                // line_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),
                                LineCount = Convert.ToInt32(reader["LineCount"] == DBNull.Value ? "" : reader["LineCount"]),
                                MachineCount = Convert.ToInt32(reader["MachineCount"] == DBNull.Value ? "" : reader["MachineCount"]),
                                totalok = Convert.ToInt32(reader["totalok"] == DBNull.Value ? 0 : reader["totalok"]),
                                totalnok = Convert.ToInt32(reader["totalnok"] == DBNull.Value ? 0 : reader["totalnok"]),
                                MachineStatus = Convert.ToString(reader["Machine_Status"] == DBNull.Value ? "" : reader["Machine_Status"]),
                                MachineIndex = Convert.ToString(reader["EOL"] == DBNull.Value ? "" : reader["EOL"]),
                                // PlantName = Convert.ToString(reader["PlantName"] == DBNull.Value ? "" : reader["PlantName"]),
                                Availability = Convert.ToDecimal(reader["Availability"] == DBNull.Value ? 0 : reader["Availability"]),
                                Performance = Convert.ToDecimal(reader["Performance"] == DBNull.Value ? 0 : reader["Performance"]),
                                Quality = Convert.ToDecimal(reader["Quality"] == DBNull.Value ? 0 : reader["Quality"]),
                                uptime = Convert.ToDecimal(reader["UpTime"] == DBNull.Value ? 0 : reader["UpTime"]),
                                downtime = Convert.ToDecimal(reader["DownTime"] == DBNull.Value ? 0 : reader["DownTime"]),
                                losstime = Convert.ToDecimal(reader["LossTime"] == DBNull.Value ? 0 : reader["LossTime"]),
                                breaktime = Convert.ToDecimal(reader["BreakTime"] == DBNull.Value ? 0 : reader["BreakTime"]),
                                last_updatedate = Convert.ToString(reader["last_updatedate"] == DBNull.Value ? "" : reader["last_updatedate"]),
                                Ideal_cycletime = Convert.ToString(reader["Ideal_cycletime"] == DBNull.Value ? "" : reader["Ideal_cycletime"]),
                                Reworkparts = Convert.ToString(reader["TotalReworkParts"] == DBNull.Value ? "" : reader["TotalReworkParts"]),
                                Batch_code = Convert.ToString(reader["Batch_code"] == DBNull.Value ? "" : reader["Batch_code"]),

                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }


        //New UI 
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Get_NewUI_data(Models.NewUI oee)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.LineCode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.NewUI>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Live_Batchwise_Carousel", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@company", SqlDbType.NVarChar, 150).Value = oee.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = oee.LineCode;
                    cmd.Parameters.Add("@plant", SqlDbType.NVarChar, 150).Value = oee.PlantCode;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "No Data Available", data = "" });
                    }
                }
            }
        }

        //New UI 
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Get_Machinewise_KPI_Live_data(Models.Machinewise_KPI oee)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.LineCode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.Machinewise_KPI>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Machinewise_KPI_Live", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@company", SqlDbType.NVarChar, 150).Value = oee.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = oee.LineCode;
                    cmd.Parameters.Add("@plant", SqlDbType.NVarChar, 150).Value = oee.PlantCode;
                    cmd.Parameters.Add("@machine", SqlDbType.NVarChar, 150).Value = oee.MachineCode;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "No Data Available", data = "" });
                    }
                }
            }
        }



        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage New_GetLineOEE(Models.OEE_Live oee)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(oee.CompanyCode, oee.PlantCode, oee.Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.OEE_Live>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT distinct a.[LineCode],a.CompanyCode,b.AssetName as AssetName
                        FROM dbo.tbl_batchwise_live_data a inner join tbl_assets b on a.machine_code=b.AssetID and a.LineCode=b.FunctionName and a.CompanyCode=b.CompanyCode and a.PlantCode=b.PlantCode
                        WHERE a.CompanyCode=@company AND a.PlantCode=@plant and a.LineCode=@line and b.EOL=1 and b.EOL=1 and a.Date=CAST(GETDATE() as date) ", connection);

                    cmd.Parameters.AddWithValue("@company", oee.CompanyCode);
                    cmd.Parameters.AddWithValue("@plant", oee.PlantCode);
                    cmd.Parameters.AddWithValue("@line", oee.Line_Code);

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(oeelive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.OEE_Live
                            {
                                // CompanyCode = Convert.ToString(reader["CompanyCode"] == DBNull.Value ? "" : reader["CompanyCode"]),
                                // PlantCode = Convert.ToString(reader["PlantName"] == DBNull.Value ? "" : reader["PlantName"]),
                                Line_Code = Convert.ToString(reader["LineCode"] == DBNull.Value ? "" : reader["LineCode"]),
                                line_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),

                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }

    }
}

