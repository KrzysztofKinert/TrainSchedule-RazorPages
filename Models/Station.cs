using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TrainSchedule.Models
{
    public class Station
    {
        [Display(Name="Id")]
        public int StationID { get; set; }
        [Display(Name="Name")]
        public string Name { get; set; }
    }
}
