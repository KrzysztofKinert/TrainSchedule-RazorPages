using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainSchedule.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TrainSchedule.Services;
using TrainSchedule.Data;

namespace TrainSchedule.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TrainSchedule_db _context;
        public IndexModel(ILogger<IndexModel> logger, TrainSchedule_db context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Condition Conditions { get; set; }

        public IActionResult OnGet()
        {
            if(Request.Cookies["ValidForm"] == "True")
            {
                Conditions = new Condition();
                Conditions.FromStation = Request.Cookies["FromStation"];
                Conditions.ToStation = Request.Cookies["ToStation"];
                Conditions.Date = DateTime.Parse(Request.Cookies["Date"]);
                Conditions.Time = DateTime.Parse(Request.Cookies["Time"]);
                Conditions.IsDeparture = Boolean.Parse(Request.Cookies["IsDeparture"]);
                Conditions.IsExpress = Boolean.Parse(Request.Cookies["IsExpress"]);
                Conditions.IsIntercity = Boolean.Parse(Request.Cookies["IsIntercity"]);
                Conditions.IsRegional = Boolean.Parse(Request.Cookies["IsRegional"]);
                Conditions.IsWifi = Boolean.Parse(Request.Cookies["IsWifi"]);
                Conditions.IsBicycleCarriage = Boolean.Parse(Request.Cookies["IsBicycleCarriage"]);
            }
            else
            {
                Conditions = new Condition();
                Conditions.FromStation = "";
                Conditions.ToStation = "";
                Conditions.Date = DateTime.Now;
                Conditions.Time = DateTime.Now;
                Conditions.IsDeparture = true;
                Conditions.IsExpress = true;
                Conditions.IsIntercity = true;
                Conditions.IsRegional = true;
                Conditions.IsWifi = false;
                Conditions.IsBicycleCarriage = false;
            }
            return Page();
        }
        public IActionResult OnPostAutoComplete(string prefix)
        {
            var stations = _context.Stations.Where(s => s.Name.Contains(prefix)).Select(s => s.Name).ToList();
            return new JsonResult(stations);

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            Response.Cookies.Append("ValidForm", "True");
            Response.Cookies.Append("FromStation", Conditions.FromStation);
            Response.Cookies.Append("ToStation", Conditions.ToStation);
            Response.Cookies.Append("Date", Conditions.Date.ToString());
            Response.Cookies.Append("Time", Conditions.Time.ToString());
            Response.Cookies.Append("IsDeparture", Conditions.IsDeparture.ToString());
            Response.Cookies.Append("IsExpress", Conditions.IsExpress.ToString());
            Response.Cookies.Append("IsIntercity", Conditions.IsIntercity.ToString());
            Response.Cookies.Append("IsRegional", Conditions.IsRegional.ToString());
            Response.Cookies.Append("IsWifi", Conditions.IsWifi.ToString());
            Response.Cookies.Append("IsBicycleCarriage", Conditions.IsBicycleCarriage.ToString());
            return RedirectToPage("/Results");
        }
    }
}
