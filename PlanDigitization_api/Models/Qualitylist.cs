using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class Qualitylist
    {
        public string Machine_Code { get; set; }
        public string Linecode { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string ShiftID { get; set; }
        public string Variantcode { get; set; }
        public string Machine_Status { get; set; }
        public Int32 OkParts { get; set; }
        public Int32 Scrap { get; set; }
        public decimal DownTime { get; set; }
        public decimal LossTime { get; set; }
        public decimal OEE { get; set; }
        public decimal Availability { get; set; }
        public decimal Performance { get; set; }
        public decimal Quality { get; set; }
        public DateTime time_stamp { get; set; }
        public decimal Firstpassyeild { get; set; }
    }
}