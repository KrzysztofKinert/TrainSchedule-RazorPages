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

namespace TrainSchedule.Pages.Stages
{
    public class IndexModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;
        private readonly IConfiguration Configuration;

        public string FilterId { get; set; }
        public string FilterConnection { get; set; }
        public string FilterDeparture { get; set; }
        public string FilterArrival { get; set; }
        public DateTime FilterDate { get; set; }

        public IndexModel(TrainSchedule.Data.TrainSchedule_db context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<Stage> Stages { get;set; }


        public async Task OnGetAsync(int? pageIndex)
        {
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Stage> stagesIQ = from stages in _context.Stages
                                                   select stages;

            stagesIQ = stagesIQ.OrderBy(s => s.StageID);
            Stages = await PaginatedList<Stage>.CreateAsync(stagesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetId(string Id, int? pageIndex)
        {
            FilterId = Id;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Stage> stagesIQ = from stages in _context.Stages
                                         select stages;

            if (!String.IsNullOrEmpty(Id))
            {
                stagesIQ = stagesIQ.Where(s => s.StageID == Int32.Parse(Id));
            }

            stagesIQ = stagesIQ.OrderBy(s => s.StageID);
            Stages = await PaginatedList<Stage>.CreateAsync(stagesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetDeparture(string Departure, int? pageIndex)
        {
            FilterDeparture = Departure;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Stage> stagesIQ = from stages in _context.Stages
                                         select stages;

            if (!String.IsNullOrEmpty(Departure))
            {
                stagesIQ = stagesIQ.Where(s => s.DepartureStation == Departure);
            }

            stagesIQ = stagesIQ.OrderBy(s => s.StageID);
            Stages = await PaginatedList<Stage>.CreateAsync(stagesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetArrival(string Arrival, int? pageIndex)
        {
            FilterArrival = Arrival;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Stage> stagesIQ = from stages in _context.Stages
                                         select stages;

            if (!String.IsNullOrEmpty(Arrival))
            {
                stagesIQ = stagesIQ.Where(s => s.ArrivalStation == Arrival);
            }

            stagesIQ = stagesIQ.OrderBy(s => s.StageID);
            Stages = await PaginatedList<Stage>.CreateAsync(stagesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetConnection(string Connection, int? pageIndex)
        {
            FilterConnection = Connection;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Stage> stagesIQ = from stages in _context.Stages
                                         select stages;

            if (!String.IsNullOrEmpty(Connection))
            {
                stagesIQ = stagesIQ.Where(s => s.ConnectionID == Int32.Parse(Connection));
            }

            stagesIQ = stagesIQ.OrderBy(s => s.StageID);
            Stages = await PaginatedList<Stage>.CreateAsync(stagesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public async Task OnGetDate(DateTime Date, int? pageIndex)
        {
            FilterDate = Date;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Stage> stagesIQ = from stages in _context.Stages
                                         select stages;

            if (Date != DateTime.MinValue)
            {
                stagesIQ = stagesIQ.Where(s => s.DepartureDate.Date == Date.Date || s.ArrivalDate.Date == Date.Date);
            }

            stagesIQ = stagesIQ.OrderBy(s => s.StageID);
            Stages = await PaginatedList<Stage>.CreateAsync(stagesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
