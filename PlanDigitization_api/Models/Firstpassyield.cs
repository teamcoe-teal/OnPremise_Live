using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class FirstPasslive
    {
        public string Linecode { get; set; }
        public string ShiftID { get; set; }
        public string Variantcode { get; set; }
        public Int32 TotalOkParts { get; set; }
        public Int32 TotalNokParts { get; set; }
        public Int32 TotalReworkParts { get; set; }
        public decimal Firstpassyeild { get; set; }
        public decimal COPQ { get; set; }
        public int hour { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public DateTime starttime { get; set; }
        public DateTime lastupdate { get; set; }
        public string OperatorID { get; set; }
        public string OperatorName { get; set; }
        public int TotalCount { get; set; }
        public string machine_name { get; set; }
        public string line_name { get; set; }
        public string variant_name { get; set; }
    }

}