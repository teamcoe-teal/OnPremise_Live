using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class AndonLive
    {
        public string Line_code { get; set; }
        public string Machine_code { get; set; }
        public string Andon_reason { get; set; }
        public string From_department { get; set; }
        public string To_department { get; set; }
        public DateTime Start_time { get; set; }
        public string Status { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
    }
}