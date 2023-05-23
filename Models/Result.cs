using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TrainSchedule.Models
{
    public class Result
    {
        public int ConnectionID { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "WiFi")]
        public bool IsWiFi { get; set; }
        [Display(Name = "Bicycle carriage")]
        public bool IsBicycleCarriage { get; set; }
        [Display(Name = "Departure")]
        public DateTime Departure { get; set; }
        [Display(Name = "Arrival")]
        public DateTime Arrival { get; set; }
        [Display(Name = "Time")]
        public TimeSpan Time { get; set; }
        [Display(Name = "Distance")]
        public double Distance { get; set; }
        [Display(Name = "Date")]
        public List<Stage> Stages { get; set; }
    }
}
