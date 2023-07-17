using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDigitization_api.Models
{
    public class OEE_Live
    {
        public string Duration { get; set; }
        public string Reason { get; set; }
        public string Stoppage { get; set; }
        public string Lastcycletime { get; set; }
        public string Batch_code { get; set; }
        public string MachineCode { get; set; }
        public decimal OEE { get; set; }
        public decimal Availability { get; set; }
        public decimal Performance { get; set; }
        public decimal Quality { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string Line_Code { get; set; }
        public int AvgOEE { get; set; }
        public int LineCount { get; set; }
        public int MachineCount { get; set; }
        public string MachineStatus { get; set; }
        public string MachineIndex { get; set; }
        public string PlantName { get; set; }
        public int totalok { get; set; }
        public int totalnok { get; set; }
        public string shift_id { get; set; }
        public string Dept_name { get; set; }
        public string variant_name { get; set; }
        public string line_name { get; set; }
        public decimal uptime { get; set; }
        public decimal downtime { get; set; }
        public decimal breaktime { get; set; }
        public decimal losstime { get; set; }
        public string last_updatedate { get; set; }
        public string Ideal_cycletime { get; set; }
        public string Reworkparts { get; set; }

    }

    public class NewUI
    {
        public string Shift_ID { get; set; }
        public string Variant { get; set; }
        public string Batch { get; set; }
        public string Start_time { get; set; }
        public string End_time { get; set; }
        public int OK_Parts { get; set; }
        public int NOK_Parts { get; set; }
        public int No_of_stoppage { get; set; }
        public string Machine_code { get; set; }
        public int Uptime { get; set; }
        public int Downtime { get; set; }
        public int Losstime { get; set; }
        public int Breaktime { get; set; }
        public int Availability { get; set; }
        public int Performance { get; set; }
        public int Quality { get; set; }
        public int OEE { get; set; }
        public int MTTR { get; set; }
        public int MTBF { get; set; }
        public decimal Cycletime { get; set; }
        public string Date { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string LineCode { get; set; }

    }


    public class Machinewise_KPI
    {
        public string Shift_ID { get; set; }
        public string Variant { get; set; }
        public string Batch { get; set; }
        public string Start_time { get; set; }
        public string End_time { get; set; }
        public int OK_Parts { get; set; }
        public int NOK_Parts { get; set; }
        public int No_of_stoppage { get; set; }
        public string Machine_code { get; set; }
        public int Uptime { get; set; }
        public int Downtime { get; set; }
        public int Losstime { get; set; }
        public int Breaktime { get; set; }
        public int Availability { get; set; }
        public int Performance { get; set; }
        public int Quality { get; set; }
        public int OEE { get; set; }
        public int MTTR { get; set; }
        public int MTBF { get; set; }
        public decimal Cycletime { get; set; }
        public string Date { get; set; }
        public string CompanyCode { get; set; }
        public string PlantCode { get; set; }
        public string LineCode { get; set; }
        public string MachineCode { get; set; }

    }


}