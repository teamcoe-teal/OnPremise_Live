//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlanDigitization_api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShiftWise_Availability
    {
        public int ID { get; set; }
        public string Shift_ID { get; set; }
        public string Line_Code { get; set; }
        public string Machine_Code { get; set; }
        public string Variant_Code { get; set; }
        public Nullable<decimal> UpTime_Min_ { get; set; }
        public Nullable<decimal> DownTime_Min_ { get; set; }
        public Nullable<decimal> LossTime { get; set; }
        public Nullable<decimal> ProductionTime_Min_ { get; set; }
        public Nullable<decimal> Availability { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> BreakTime { get; set; }
        public Nullable<decimal> Availability_considering_losses { get; set; }
    }
}