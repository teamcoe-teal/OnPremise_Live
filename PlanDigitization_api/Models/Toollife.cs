using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class ToolLifelive
    {
        public string Line_code { get; set; }
        public string ToolName { get; set; }
        public string ToolID { get; set; }
        public string Make { get; set; }
        public string UOM { get; set; }
        public string Part_number { get; set; }
        public string Classification { get; set; }
        public decimal ratedlifecle { get; set; }//
        public string Machine_code { get; set; }
        public string Conversion_parameter { get; set; }
        public decimal currentlifecle { get; set; }
        public string lastmain { get; set; }
        public decimal usage { get; set; }//
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string line_name { get; set; }
        public string RecText { get; set; }// asset //mc code
        public string Status { get; set; }
        public string Next_PM { get; set; }

    }

    public class disetsetting
    {

        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string Flag { get; set; }
        public string QueryType { get; set; }
        public string LineCode { get; set; }
        public string MachineCode { get; set; }
        public int instance { get; set; }
        public DateTime date { get; set; }
        public string id { get; set; }
        public DateTime loaded { get; set; }
        public DateTime unloaded { get; set; }
        public int production { get; set; }
        public int cummulative { get; set; }
        public DateTime starttime { get; set; }
        public DateTime stoptime { get; set; }
        public string reason { get; set; }
        public string toolname { get; set; }
        public string linename { get; set; }
        public string toolid { get; set; }
    }

}