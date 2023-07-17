using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class Availability_Live
    {
        public string ShiftID { get; set; }
        public string Line_Code { get; set; }
        public string Machine_Code { get; set; }
        public string Machine_Status { get; set; }
        public string Variant_Code { get; set; }
        public decimal Avail { get; set; }
        public decimal UpTime { get; set; }
        public decimal DownTime { get; set; }
        public decimal LossTime { get; set; }
        public decimal Totaltime { get; set; }
        public DateTime lastupdate { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
    }
}