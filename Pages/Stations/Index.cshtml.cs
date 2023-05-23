using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainSchedule.Data;
using TrainSchedule.Models;
using Microsoft.Extensions.Configuration;

namespace TrainSchedule.Pages.Stations
{
    public class IndexModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;
        private readonly IConfiguration Configuration;

        public string FilterId { get; set; }
        public string FilterName { get; set; }

        public IndexModel(TrainSchedule.Data.TrainSchedule_db context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<Station> Stations { get; set; }


        public async Task OnGetAsync(int? pageIndex)
        {
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Station> stationsIQ = from stations in _context.Stations
                                         select stations;

            stationsIQ = stationsIQ.OrderBy(s => s.StationID);
            Stations = await PaginatedList<Station>.CreateAsync(stationsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetId(string Id, int? pageIndex)
        {
            FilterId = Id;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Station> stationsIQ = from stations in _context.Stations
                                             select stations;

            if (!String.IsNullOrEmpty(Id))
            {
                stationsIQ = stationsIQ.Where(s => s.StationID == Int32.Parse(Id));
            }

            stationsIQ = stationsIQ.OrderBy(s => s.StationID);
            Stations = await PaginatedList<Station>.CreateAsync(stationsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetName(string Name, int? pageIndex)
        {
            FilterName = Name;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Station> stationsIQ = from stations in _context.Stations
                                             select stations;

            if (!String.IsNullOrEmpty(Name))
            {
                stationsIQ = stationsIQ.Where(s => s.Name == Name);
            }

            stationsIQ = stationsIQ.OrderBy(s => s.StationID);
            Stations = await PaginatedList<Station>.CreateAsync(stationsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
