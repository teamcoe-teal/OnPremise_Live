using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class OperatorEfficiencyLive
    {
        public string Line_name { get; set; }
        public string Operation { get; set; }
        public string Operator_name { get; set; }
        public string Variant_number { get; set; }
        public decimal Expected_cycle_time { get; set; }
        public decimal Latest_cycle_time { get; set; }
        public int Target_production { get; set; }
        public int Total_OK_parts { get; set; }
        public int Total_NOK_parts { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string Line_code { get; set; }
        public string shift_id { get; set; }
        public string machine_name { get; set; }
        public string variant_name { get; set; }
    }
}