using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class Diagnostics
    {
        public string QueryType { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string Line_Code { get; set; }
        public string DeviceID { get; set; }
        public string Devicename { get; set; }
        public string DeviceRef { get; set; }
        public string Event { get; set; }
        public int Unique_ID { get; set; }
        public DateTime LogTime { get; set; }
        public DateTime Time_stamp { get; set; }
        
    }
}