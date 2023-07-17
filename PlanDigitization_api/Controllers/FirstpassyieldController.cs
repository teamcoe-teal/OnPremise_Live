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
    public class FirstpassyieldController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetFirstPasslivedata(Models.FirstPasslive first)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(first.CompanyCode,first.PlantCode,first.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.FirstPasslive>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    //using (var command = new SqlCommand(@"SELECT [Linecode]
                    //                                    ,[ShiftID]
                    //                                    ,[Variantcode]
                    //                                    ,[TotalOkParts]
                    //                                    ,[TotalNokParts]
                    //                                    ,[TotalReworkParts]
                    //                                    ,[Firstpassyeild],[COPQ] FROM dbo.Live WHERE CompanyCode='" + first.CompanyCode + "' AND PlantCode='" + first.PlantCode + "' AND Linecode='" + first.Linecode + "'  ", connection))

                    SqlCommand command = new SqlCommand("SELECT a.Linecode,b.AssetName,a.ShiftID,a.Variantcode,c.VariantName,a.TotalOkParts," +
                        "a.TotalNokParts,a.TotalReworkParts,a.Firstpassyeild,a.COPQ " +
                        "from dbo.Live a " +
                        "join tbl_assets b on b.FunctionName = a.LineCode and b.companycode = a.Companycode and b.plantcode = a.Plantcode " +
                        "join tbl_masterproduct c on c.variant_code = a.variantcode and b.AssetID = c.Machine_Code and c.Line_Code = a.LineCode and c.companycode = a.Companycode and c.plantcode = a.Plantcode " +
                        "where a.CompanyCode = @CompanyCode and a.PlantCode = @PlantCode and a.Linecode = @Linecode and Convert(Date, a.Time_stamp) = @currentdate and EOL = 1", connection);
                    //SqlCommand command = new SqlCommand("SELECT a.Linecode,b.AssetName,a.ShiftID,a.Variantcode,c.VariantName,a.TotalOkParts,a.TotalNokParts,a.TotalReworkParts,a.Firstpassyeild,a.COPQ " +
                    //    "from dbo.Live a " +
                    //    "join tbl_assets b on b.FunctionName = a.LineCode and b.companycode = a.Companycode and b.plantcode = a.Plantcode " +
                    //    "join tbl_masterproduct c on c.variant_code = a.variantcode and c.Line_Code = a.LineCode and c.companycode = a.Companycode and c.plantcode = a.Plantcode " +
                    //    "where a.CompanyCode = @CompanyCode and a.PlantCode = @PlantCode and a.Linecode = @Linecode", connection);



                    command.Parameters.AddWithValue("@CompanyCode", first.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", first.PlantCode);
                    command.Parameters.AddWithValue("@Linecode", first.Linecode);
                    command.Parameters.AddWithValue("@currentdate", DateTime.Now.ToString("yyyy-MM-dd"));

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(firstpasslive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.FirstPasslive
                            {
                                Linecode = Convert.ToString(reader["Linecode"] == DBNull.Value ? "" : reader["Linecode"]),
                                ShiftID = Convert.ToString(reader["ShiftID"] == DBNull.Value ? "" : reader["ShiftID"]),
                                Variantcode = Convert.ToString(reader["Variantcode"] == DBNull.Value ? "" : reader["Variantcode"]),
                                TotalOkParts = Convert.ToInt32(reader["TotalOkParts"] == DBNull.Value ? "" : reader["TotalOkParts"]),
                                TotalNokParts = Convert.ToInt32(reader["TotalNokParts"] == DBNull.Value ? "" : reader["TotalNokParts"]),
                                TotalReworkParts = Convert.ToInt32(reader["TotalReworkParts"] == DBNull.Value ? "" : reader["TotalReworkParts"]),
                                Firstpassyeild = Convert.ToDecimal(reader["Firstpassyeild"] == DBNull.Value ? "" : reader["Firstpassyeild"]),
                                COPQ = Convert.ToDecimal(reader["COPQ"] == DBNull.Value ? "" : reader["COPQ"]),
                                line_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),
                                variant_name = Convert.ToString(reader["Variantname"] == DBNull.Value ? "" : reader["Variantname"]),
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = messages });
            }
        }


        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetHourlylivedata(Models.FirstPasslive first)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(first.CompanyCode, first.PlantCode, first.Linecode);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.FirstPasslive>();
                DataSet ds = new DataSet();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    //using (var command = new SqlCommand(@"SELECT [Shift_ID]
                    //                                              ,[Variant_code]
                    //                                              ,[Hour]
                    //                                              ,[Ok_Parts]
                    //                                              ,[Rework_Parts]
                    //                                              ,[NOK_Parts]
                    //                                              ,[StartTime]
                    //                                              ,[Lastupdate] FROM dbo.Hourly_Live WHERE CompanyCode='" + first.CompanyCode + "' AND PlantCode='" + first.PlantCode + "' AND Line_Code='" + first.Linecode + "'; SELECT [OperatorID],[OperatorName] FROM dbo.tbl_Operator_Live WHERE CompanyCode = '" + first.CompanyCode + "' AND PlantCode = '" + first.PlantCode + "' AND Line_Code = '" + first.Linecode + "'  ", connection))
                    SqlCommand command = new SqlCommand("SELECT a.Shift_ID,a.Machine_code,b.AssetName,a.Variant_code,c.VariantName,a.Hour,a.Ok_Parts,a.Rework_Parts,a.NOK_Parts,a.LastUpdate,a.StartTime," +
                        "(select sum(h.[TotalOkParts]) from live h join [Hourly_Live] k on k.Shift_ID=h.ShiftID and k.Line_Code=h.Linecode and k.Companycode=h.companycode and k.Plantcode=h.plantcode " +
                        "where h.CompanyCode = @CompanyCode and h.PlantCode = @PlantCode and h.Linecode = @Line_Code and h.Date=@date)as TotalOkParts " +
                        "from dbo.Hourly_Live a " +
                        "join tbl_assets b on b.FunctionName = a.Line_Code and b.companycode = a.Companycode and b.plantcode = a.Plantcode and b.AssetID = a.Machine_code " +
                        "join tbl_masterproduct c on c.variant_code = a.variant_code and c.Line_Code = a.Line_Code and c.companycode = a.Companycode and c.plantcode = a.Plantcode and c.Machine_code = a.Machine_code " +
                        "where a.CompanyCode = @CompanyCode and a.PlantCode = @PlantCode and a.Line_Code = @Line_Code ;" +
                        "SELECT[OperatorID],[OperatorName] FROM dbo.tbl_Operator_Live where CompanyCode = @CompanyCode and PlantCode = @PlantCode and Line_Code = @Line_Code ; ", connection);


                    command.Parameters.AddWithValue("@CompanyCode", first.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", first.PlantCode);
                    command.Parameters.AddWithValue("@Line_Code", first.Linecode);
                    command.Parameters.AddWithValue("@date", DateTime.Now.Date);
                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(firstpasslive);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.FirstPasslive
                            {
                                ShiftID = Convert.ToString(reader["Shift_ID"] == DBNull.Value ? "" : reader["Shift_ID"]),
                                hour = Convert.ToInt32(reader["Hour"] == DBNull.Value ? 0 : reader["Hour"]),
                                Variantcode = Convert.ToString(reader["Variant_code"] == DBNull.Value ? "" : reader["Variant_code"]),
                                TotalOkParts = Convert.ToInt32(reader["Ok_Parts"] == DBNull.Value ? 0 : reader["Ok_Parts"]),
                                TotalNokParts = Convert.ToInt32(reader["NOK_Parts"] == DBNull.Value ? 0 : reader["NOK_Parts"]),
                                TotalReworkParts = Convert.ToInt32(reader["Rework_Parts"] == DBNull.Value ? 0 : reader["Rework_Parts"]),
                                lastupdate = Convert.ToDateTime(reader["LastUpdate"] == DBNull.Value ? null : reader["LastUpdate"]),
                                starttime = Convert.ToDateTime(reader["StartTime"] == DBNull.Value ? null : reader["StartTime"]),
                                TotalCount = Convert.ToInt32(reader["TotalOkParts"] == DBNull.Value ? 0 : reader["TotalOkParts"]),
                                variant_name = Convert.ToString(reader["VariantName"] == DBNull.Value ? "" : reader["VariantName"]),
                                machine_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"])
                                //OperatorID = (string)reader["OperatorID"],
                                //OperatorName = (string)reader["OperatorName"]
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Found", data = ds });
            }
        }

        //private void firstpasslive(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        PlantDigitizationhub.SendDatas();
        //    }
        //}
    }
}
