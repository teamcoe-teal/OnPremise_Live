using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class Alert
    {
       
        public string Line_Code { get; set; }
        public string Machine_Code { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string AlertID { get; set; }
        public string Variable { get; set; }
        public string PropertyGroup { get; set; }
        public string Parameter { get; set; }
        public string P_Values { get; set; }
        public string StartTime { get; set; }
    }
}