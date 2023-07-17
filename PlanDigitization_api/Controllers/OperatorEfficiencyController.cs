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
    public class OperatorEfficiencyController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetOPElivedatas(Models.OperatorEfficiencyLive live)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(live.CompanyCode, live.PlantCode, live.Line_code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                var messages = new List<Models.OperatorEfficiencyLive>();
                using (var connection = new SqlConnection(con_string))
                {
                    connection.Open();
                    //using (var command = new SqlCommand(@"SELECT [Shift_ID]
                    //                                      ,[Line_code]
                    //                                      ,[Machine_Code]
                    //                                      ,[Operation]
                    //                                      ,[OperatorName]
                    //                                      ,[Expected_Cycle_Time]
                    //                                      ,[Latest_Cycle_Time]
                    //                                      ,[Target_Production]
                    //                                      ,[Total_OKParts]
                    //                                      ,[Total_NOKParts]
                    //                                      ,[LastUpdate]
                    //                                      ,[Date]
                    //                                  FROM [dbo].[tbl_Live_Operator_Efficiency] WHERE CompanyCode='" + live.CompanyCode + "' AND PlantCode='" + live.PlantCode + "' AND Line_code='" + live.Line_code + "' ", connection))

                    SqlCommand command = new SqlCommand("SELECT a.Shift_ID,a.variant_code,c.VariantName,a.Line_code,b.AssetName,a.Machine_Code,a.Operation,a.OperatorName,a.Expected_Cycle_Time,a.Latest_Cycle_Time, a.Target_Production, a.Total_OKParts, a.Total_NOKParts, a.LastUpdate, a.Date " +
                        "from dbo.tbl_Live_Operator_Efficiency a " +
                        "join tbl_assets b on b.FunctionName = a.Line_Code and b.companycode = a.Companycode and b.plantcode = a.Plantcode and b.AssetID = a.Machine_code " +
                        "join tbl_masterproduct c on c.variant_code = a.variant_code and c.Line_Code = a.Line_Code and c.companycode = a.Companycode and c.plantcode = a.Plantcode and c.Machine_code = a.Machine_code " +
                        "where a.CompanyCode = @CompanyCode and a.PlantCode = @PlantCode and a.Line_code = @Line_code", connection);


                    command.Parameters.AddWithValue("@CompanyCode", live.CompanyCode);
                    command.Parameters.AddWithValue("@PlantCode", live.PlantCode);
                    command.Parameters.AddWithValue("@Line_code", live.Line_code);

                    {
                        //command.Notification = null;
                        //var dependency = new SqlDependency(command);
                        //dependency.OnChange += new OnChangeEventHandler(opelivedata);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new Models.OperatorEfficiencyLive
                            {
                                //Line_name = reader.IsDBNull("Line_code") ? "" : (string)dbReader.GetValue(5);
                                shift_id = (reader["Shift_ID"] as string) ?? "",
                                Variant_number = (reader["variant_code"] as string) ?? "",
                                Line_name = (reader["Line_code"] as string) ?? "",
                                Operation = (reader["Operation"] as string) ?? "",
                                Operator_name = (reader["OperatorName"] as string) ?? "",
                                Expected_cycle_time = (reader["Expected_Cycle_Time"] as decimal?) ?? 0,
                                variant_name = Convert.ToString(reader["Variantname"] == DBNull.Value ? "" : reader["Variantname"]),
                                machine_name = Convert.ToString(reader["AssetName"] == DBNull.Value ? "" : reader["AssetName"]),
                                Latest_cycle_time = (reader["Latest_Cycle_Time"] as decimal?) ?? 0,
                                Target_production = (reader["Target_Production"] as int?) ?? 0,
                                Total_OK_parts = (reader["Total_OKParts"] as int?) ?? 0,
                                Total_NOK_parts = (reader["Total_NOKParts"] as int?) ?? 0,
                            });
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Success", msg = "Details Added Successfully", data = messages });
            }
        }

        //private void opelivedata(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        PlantDigitizationhub.SendDatas();
        //    }
        //}
    }
}
