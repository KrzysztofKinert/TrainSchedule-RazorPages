using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TrainSchedule.Models
{
    public class Connection
    {
        [Display(Name = "Id")]
        public int ConnectionID { get; set; }
        [Display(Name="Name")]
        public string Name { get; set; }
        [Display(Name="Type")]
        public string Type { get; set; }
        [Display(Name = "WiFi")]
        public bool IsWiFi { get; set; }
        [Display(Name = "Bicycle carriage")]
        public bool IsBicycleCarriage { get; set; }
        public List<Stage> Stages { get; set; }
    }
}
