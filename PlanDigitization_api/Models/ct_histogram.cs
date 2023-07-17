using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class ct_histogram
    {
        public string Machine_Code { get; set; }
        public string Linecode { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string ShiftID { get; set; }
        public int Occurence { get; set; }
        public decimal cycletime { get; set; }
        public string Flag { get; set; }
    }
    public class stoppage_reason
    {
        public string Machine_Code { get; set; }
        public string Linecode { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string ShiftID { get; set; }
       public string Variantcode { get; set; }
    }

    public class target_live
    {

        public string Linecode { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string ShiftID { get; set; }
        public DateTime Date { get; set; }
        public string variant { get; set; }


    }
    public class target_cycletime
    {

        public string Linecode { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string Machine_Code { get; set; }
        public string variant { get; set; }


    }
    public class status_bar
    {
        public string Machine_Code { get; set; }
        public string Linecode { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string ShiftID { get; set; }
        public string color { get; set; }
        public string starting_time { get; set; }
        public string ending_time { get; set; }
        public string Loss { get; set; }
        public string Alarm { get; set; }

    }
    public class rejection
    {
        public string Machine_Code { get; set; }
        public string Linecode { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string ShiftID { get; set; }
      
       
        public string variantcode { get; set; }
    }
    public class Losses
    {
        public string Line { get; set; }
        public string Machine { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
    }

}