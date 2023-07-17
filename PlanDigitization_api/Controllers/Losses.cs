using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http.Cors;
using System.Security.Cryptography;
using System.Text;

namespace PlanDigitization_api.Controllers
{
    public class LossesController : ApiController
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();

        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Get_Loss_live_details(Models.Losses L)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(L.CompanyCode, L.PlantCode, L.Line);
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
                    SqlCommand cmd = new SqlCommand("SP_16_Big_loss_live", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 150).Value = L.CompanyCode;
                    cmd.Parameters.Add("@PlantCode", SqlDbType.NVarChar, 150).Value = L.PlantCode;
                    cmd.Parameters.Add("@Line", SqlDbType.NVarChar, 150).Value = L.Line;
                    cmd.Parameters.Add("@Machine", SqlDbType.NVarChar, 150).Value = L.Machine;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds.Tables.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { data = ds });
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
