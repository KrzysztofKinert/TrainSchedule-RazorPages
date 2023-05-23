using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TrainSchedule.Models
{
    public class Stage
    {
        [Display(Name ="Id")]
        public int StageID { get; set; }
        [Display(Name ="Connection Id")]
        public int ConnectionID { get; set; }
        [Display(Name = "Sequence")]
        public int Sequence { get; set; }
        [Display(Name = "From")]
        public string DepartureStation { get; set; }
        [Display(Name = "To")]
        public string ArrivalStation { get; set; }
        [Display(Name = "Departure")]
        public DateTime DepartureDate { get; set; }
        [Display(Name = "Arrival")]
        public DateTime ArrivalDate { get; set; }
        [Display(Name = "Distance")]
        public double Distance { get; set; }
        public Connection Connection { get; set; }
    }
}
