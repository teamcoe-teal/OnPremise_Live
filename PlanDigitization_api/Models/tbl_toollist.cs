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
    
    public partial class tbl_toollist
    {
        public int ID { get; set; }
        public string ToolName { get; set; }
        public string ToolID { get; set; }
        public string Make { get; set; }
        public string UOM { get; set; }
        public string Part_number { get; set; }
        public string Classification { get; set; }
        public float RatedLife { get; set; }
        public string Machine_Code { get; set; }
    }
}
