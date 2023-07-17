using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Cors;

namespace PlanDigitization_api.Controllers
{
    //[EnableCors(origins: "http://localhost:55974", headers: "*", methods: "*")]
    public class QualityController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetQualitylivedata(Models.Qualitylist list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode,list.PlantCode,list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Live_Productiondashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
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
        public HttpResponseMessage GetCtHistogram(Models.ct_histogram list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_CycleTime_Histogram", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID;
                    cmd.Parameters.Add("@Machinecode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    cmd.CommandTimeout = 6000;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
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
        public HttpResponseMessage GetPartsdetails(Models.ct_histogram list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Production_details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID;
                    cmd.Parameters.Add("@Machinecode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    cmd.Parameters.Add("@Flag", SqlDbType.NVarChar, 150).Value = list.Flag;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    if (ds.Tables.Count != 0)
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
        public HttpResponseMessage GetLossdetails(Models.ct_histogram list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Production_Reasons", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID.ToString();
                    cmd.Parameters.Add("@Machinecode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    cmd.Parameters.Add("@Flag", SqlDbType.NVarChar, 150).Value = list.Flag;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
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
        public HttpResponseMessage downtimegraph_details(Models.ct_histogram list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    DataTable dt_Sorted = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_Production_Reasons", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID;
                    cmd.Parameters.Add("@Machinecode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    cmd.Parameters.Add("@Flag", SqlDbType.NVarChar, 150).Value = list.Flag;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                    foreach (DataTable table in ds.Tables)
                    {
                        //foreach (DataRow dr in table.Rows)
                        //{
                        //    var res = dr["Downtime_Reason"];

                        //}
                        var result = table.AsEnumerable().GroupBy(dr => dr.Field<string>("Downtime_Reason")).Select(group => new { Datevalue = group.Key, Count = group.Sum(col => col.Field<decimal>("Duration_In_Sec")) });

                        dt_Sorted = table.Clone();
                        foreach (var grp in result)
                        {
                            var n1 = grp.Count;
                            dt_Sorted.Rows.Add(grp.Datevalue, "", "", grp.Count);
                        }

                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = dt_Sorted });

                }
            }

        }
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage losetimegraph_details(Models.ct_histogram list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    DataTable dt_Sorted = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_Production_Reasons", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID;
                    cmd.Parameters.Add("@Machinecode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    cmd.Parameters.Add("@Flag", SqlDbType.NVarChar, 150).Value = list.Flag;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                    foreach (DataTable table in ds.Tables)
                    {
                        //foreach (DataRow dr in table.Rows)
                        //{
                        //    var res = dr["Downtime_Reason"];

                        //}
                        var result = table.AsEnumerable().GroupBy(dr => dr.Field<string>("Downtime_Reason")).Select(group => new { Datevalue = group.Key, Count = group.Sum(col => col.Field<decimal>("Duration_In_Sec")) });

                        dt_Sorted = table.Clone();
                        decimal sum = 0;
                        foreach (var grp in result)
                        {
                            sum = sum + grp.Count;
                            //var n1 = grp.Count;
                            //dt_Sorted.Rows.Add(grp.Datevalue, "", "", grp.Count);
                        }
                        foreach (var grp in result)
                        {
                            var temp1 = (grp.Count / sum);
                            var temp = temp1 * 100;
                            var n1 = grp.Count;
                            dt_Sorted.Rows.Add(grp.Datevalue, Math.Round(temp, 0), sum, grp.Count);
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = dt_Sorted });

                }
            }

        }
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetTop_Breakup(Models.ct_histogram list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Productiondasboard_AlarmBreakup", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@line_code", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID;
                    cmd.Parameters.Add("@Machine_code", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    cmd.Parameters.Add("@Flag", SqlDbType.NVarChar, 150).Value = list.Flag;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
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
        public HttpResponseMessage Gettarget_count(Models.target_live list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");

                    con.Open();
                    int count = 0;
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand(@"SELECT [TargetProduction] FROM dbo.tbl_Production_setting WHERE CompanyCode='" + list.CompanyCode + "' AND PlantCode='" + list.PlantCode + "' AND Line_code='" + list.Linecode + "'AND Productname='" + list.variant + "'AND Shift_id='" + list.ShiftID + "'and '" + date + "' between fromdate and todate ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    object ret = cmd.ExecuteScalar();
                    if (ret != null)
                    {
                        count = (int)cmd.ExecuteScalar();
                    }

                    else
                    {
                        count = 0;
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = count });


                }
            }
        }

        //public HttpResponseMessage Getmachine_status(Models.target_live list)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionstring))
        //    {
        //        string date = DateTime.Now.ToString("yyyy-MM-dd");
        //        DataSet ds = new DataSet();
        //        DataTable ds_new = new DataTable();
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(@"SELECT [Time_Stamp] as starting_time,[Time_Stamp] as ending_time,Machine_Status as color FROM dbo.SYNONYM_Rawtable WHERE CompanyCode='" + list.CompanyCode + "' AND PlantCode='" + list.PlantCode + "' AND Line_code='" + list.Linecode + "'AND variant_code='" + list.variant + "'AND Shift_id='" + list.ShiftID + "'and date='" + date + "'", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        //object ret = cmd.ExecuteScalar();

        //        da.Fill(ds);
        //        con.Close();
        //        var i = 0;
        //        string color = "";
        //        var end = "";
        //        foreach(DataTable set in ds.Tables)
        //        {
        //            var result = set.AsEnumerable();

        //            foreach (var set1 in result)
        //            {



        //                if (set1[2].ToString() == "0" || set1[2].ToString() == "2")
        //                {
        //                    color = "red";

        //                }
        //                if (set1[2].ToString() == "1")
        //                {
        //                    color = "green";
        //                }
        //                if (set1[2].ToString() == "3")
        //                {
        //                    color = "yellow";
        //                }
        //                if (set1[2].ToString() == "5")
        //                {
        //                    color="blue";
        //                }
        //                if (set1[2].ToString() == "4")
        //                {
        //                    color = "grey";
        //                }
        //                set1.SetField(2,color);
        //                var time = Convert.ToDateTime(set1[0]);
        //                var time1 = time.ToShortTimeString();
        //                set1.SetField(0, time1);
        //                i = i + 1;
        //            }

        //        }

        //        if (ds.Tables[0].Rows.Count != 0)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "No Data Available", data = "" });
        //        }
        //       // return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = count });


        //    }
        //}
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Getmachine_status(Models.ct_histogram list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.status_bar>();

                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Live_Bargraph", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60000;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID;
                    cmd.Parameters.Add("@Machinecode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    var reader = cmd.ExecuteReader();
                    var color = "";
                    while (reader.Read())
                    {
                        if (reader["machine_status"].ToString() == "0" || reader["machine_status"].ToString() == "2")
                        {
                            color = "#F39C12"; //orange

                        }
                        if (reader["machine_status"].ToString() == "0" || reader["machine_status"].ToString().ToString() == "2")
                        {
                            color = "#F39C12"; //orange

                        }
                        if (reader["machine_status"].ToString().ToString() == "1")
                        {
                            color = "#87c300"; //green
                        }
                        if (reader["machine_status"].ToString().ToString() == "3")
                        {
                            color = "#fac316"; //yellow
                        }
                        if (reader["machine_status"].ToString().ToString() == "5")
                        {
                            color = "grey"; //grey
                        }
                        if (reader["machine_status"].ToString().ToString() == "4")
                        {
                            color = "#3d85c6"; //lightblue
                        }

                        messages.Add(item: new Models.status_bar
                        {
                            ShiftID = Convert.ToString(reader["Shift_ID"] == DBNull.Value ? "" : reader["Shift_ID"]),
                            Machine_Code = Convert.ToString(reader["Machine_Code"] == DBNull.Value ? "" : reader["Machine_Code"]),
                            Linecode = Convert.ToString(reader["line_code"] == DBNull.Value ? "" : reader["line_code"]),
                            CompanyCode = Convert.ToString(reader["companycode"] == DBNull.Value ? "" : reader["companycode"]),
                            PlantCode = Convert.ToString(reader["plantcode"] == DBNull.Value ? "" : reader["plantcode"]),
                            color = color,
                            starting_time = Convert.ToDateTime(reader["Start"]).ToString(new CultureInfo("en-US")),
                            ending_time = Convert.ToDateTime(reader["end"]).ToString(new CultureInfo("en-US")),
                            //Alarm = Convert.ToString(reader["plantcode"] == DBNull.Value ? "" : reader["Alarm"]),
                            //Loss = Convert.ToString(reader["plantcode"] == DBNull.Value ? "" : reader["Loss"]),
                            //starting_time = Convert.ToDateTime(reader["Start"]).ToString("hh:mm:ss"),
                            //ending_time = Convert.ToDateTime(reader["end"]).ToString("hh:mm:ss"),



                        });
                    }
                    con.Close();



                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "No Data Available", data = "" });
                    }
                    // return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = count });



                }
            }
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage getmachine_code(Models.Qualitylist list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");

                    con.Open();

                    DataSet ds = new DataSet();
                    SqlCommand command = new SqlCommand("select Machine_code as AssetID from tbl_batchwise_live_data where companycode=@CompanyCode and Plantcode=@PlantCode and linecode=@Line_code and date=@date group by len(machine_code), machine_code", con);


                    command.Parameters.AddWithValue("@CompanyCode", list.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", list.PlantCode);
                    command.Parameters.AddWithValue("@Line_code", list.Linecode);
                    command.Parameters.AddWithValue("@date", date);
                    // SqlCommand cmd = new SqlCommand(@"SELECT ID as id,Shift_Id as Shift_ID,Line_COde as Line_Code,Productname as  Product_Name,TargetProduction as target_production,Fromdate as fromdate,Todate as todate FROM dbo.tbl_Production_setting WHERE CompanyCode='" + U.Parameter1 + "' AND PlantCode='" + U.Parameter + "'And id='" + U.QueryType + "'  ", con);
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    da.Fill(ds);
                    if (ds.Tables.Count != 0)
                    {
                        // return Ok(ds.Tables[0]);
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
                    }

                    //return Ok(new String[0]);
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });


                }
            }
        }
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage getrejectiondetails(Models.rejection list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");

                    con.Open();

                    DataSet ds = new DataSet();
                    SqlCommand command = new SqlCommand("select c.time_stamp as Timestamp,r.rejectiondescription as Reject_Reason   from cycletime c inner join tbl_rejection r   on convert(date, c.time_stamp) = @date  and c.reject_reason <> 'Null' and c.reject_reason <> '' and c.reject_reason is Not Null and c.plantcode = @plantcode and c.companycode = @CompanyCode and c.line_code = @Line_code and c.variant_code = @variantcode and c.machine_code = @machine and c.shift_id = @shift order by time_stamp", con);


                    command.Parameters.AddWithValue("@CompanyCode", list.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", list.PlantCode);
                    command.Parameters.AddWithValue("@Line_code", list.Linecode);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@variantcode", list.variantcode);
                    command.Parameters.AddWithValue("@machine", list.Machine_Code);
                    command.Parameters.AddWithValue("@shift", list.ShiftID);
                    command.CommandTimeout = 600;
                    // SqlCommand cmd = new SqlCommand(@"SELECT ID as id,Shift_Id as Shift_ID,Line_COde as Line_Code,Productname as  Product_Name,TargetProduction as target_production,Fromdate as fromdate,Todate as todate FROM dbo.tbl_Production_setting WHERE CompanyCode='" + U.Parameter1 + "' AND PlantCode='" + U.Parameter + "'And id='" + U.QueryType + "'  ", con);
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    da.Fill(ds);
                    if (ds.Tables.Count != 0)
                    {
                        // return Ok(ds.Tables[0]);
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
                    }

                    //return Ok(new String[0]);
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });


                }
            }
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Gettarget_cycletime(Models.target_cycletime list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    //string date = DateTime.Now.ToString("yyyy-MM-dd");

                    con.Open();
                    decimal count = 0;
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand(@"SELECT cycletime FROM tbl_MasterProduct WHERE CompanyCode=@CompanyCode  AND PlantCode=@PlantCode AND Line_code=@Line_code AND Variant_code=@variant and machine_code=@Machine_Code ", con);
                    cmd.Parameters.AddWithValue("@CompanyCode", list.CompanyCode);
                    cmd.Parameters.AddWithValue("@PlantCode", list.PlantCode);
                    cmd.Parameters.AddWithValue("@Line_code", list.Linecode);
                    cmd.Parameters.AddWithValue("@variant", list.variant);
                    cmd.Parameters.AddWithValue("@Machine_Code", list.Machine_Code);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    object ret = cmd.ExecuteScalar();
                    if (ret != null)
                    {
                        count = (decimal)cmd.ExecuteScalar();
                    }

                    else
                    {
                        count = 0;
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = count });


                }
            }
        }
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Gettarget_cycletime1(Models.target_cycletime list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    //string date = DateTime.Now.ToString("yyyy-MM-dd");

                    con.Open();
                    decimal count = 0;
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand(@"SELECT cycletime,auto_cycletime,manual_cycletime FROM tbl_MasterProduct WHERE CompanyCode=@CompanyCode  AND PlantCode=@PlantCode AND Line_code=@Line_code AND Variant_code=@variant and machine_code=@Machine_Code ", con);
                    cmd.Parameters.AddWithValue("@CompanyCode", list.CompanyCode);
                    cmd.Parameters.AddWithValue("@PlantCode", list.PlantCode);
                    cmd.Parameters.AddWithValue("@Line_code", list.Linecode);
                    cmd.Parameters.AddWithValue("@variant", list.variant);
                    cmd.Parameters.AddWithValue("@Machine_Code", list.Machine_Code);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds.Tables.Count != 0)
                    {
                        // return Ok(ds.Tables[0]);
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
                    }

                    //return Ok(new String[0]);
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
                }
            }
        }


        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetStoppageReason(Models.stoppage_reason list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand(@"select Live_Alarm,Live_Loss from tbl_ProductionCount_live where machine_code=@Machinecode and CompanyCode=@CompanyCode and PlantCode=@PlantCode and Linecode=@linecode and Variantcode=@Variantcode and ShiftID=@Shift  and substring(batch_code,2,len(batch_code))=(select Max(substring(batch_code,2,len(batch_code))) from [dbo].[tbl_ProductionCount_live] where machine_code=@Machinecode and CompanyCode=@CompanyCode and PlantCode=@PlantCode and Linecode=@linecode and Variantcode=@Variantcode and ShiftID=@Shift ) ", con);

                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 150).Value = list.ShiftID;
                    cmd.Parameters.Add("@Variantcode", SqlDbType.NVarChar, 150).Value = list.Variantcode;
                    cmd.Parameters.Add("@Machinecode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    cmd.CommandTimeout = 6000;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
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
        public HttpResponseMessage GetQualitylivedataMachinewise(Models.Qualitylist list)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(list.CompanyCode, list.PlantCode, list.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_Live_Productiondashboard_Machinewise", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = list.CompanyCode;
                    cmd.Parameters.Add("@linecode", SqlDbType.NVarChar, 150).Value = list.Linecode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = list.PlantCode;
                    cmd.Parameters.Add("@MachineCode", SqlDbType.NVarChar, 150).Value = list.Machine_Code;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
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




    }
}