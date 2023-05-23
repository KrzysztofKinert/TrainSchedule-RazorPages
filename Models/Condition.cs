using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace TrainSchedule.Models
{
    public class Condition
    {
        [Required(ErrorMessage ="The From field is required")]
        [MinLength(1)]
        [Display(Name ="From")]
        public string FromStation { get; set; }
        [Required(ErrorMessage = "The To field is required")]
        [MinLength(1)]
        [Display(Name = "To")]
        public string ToStation { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public bool IsDeparture { get; set; }
        [Display(Name = "Express")]
        public bool IsExpress { get; set; }
        [Display(Name = "Intercity")]
        public bool IsIntercity { get; set; }
        [Display(Name = "Regional")]
        public bool IsRegional { get; set; }
        [Display(Name = "WiFi")]
        public bool IsWifi { get; set; }
        [Display(Name = "Bicycle carriage")]
        public bool IsBicycleCarriage { get; set; }
    }
}
