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
    
    public partial class DowntimeDetail
    {
        public int ID { get; set; }
        public string Shift_ID { get; set; }
        public string Line_Code { get; set; }
        public string Machine_Code { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.TimeSpan> DowntimeStart { get; set; }
        public Nullable<System.TimeSpan> DownTimeEnd { get; set; }
        public Nullable<int> DurationInSeconds { get; set; }
        public string DownTimeReason { get; set; }
        public string Alarms { get; set; }
    }
}
