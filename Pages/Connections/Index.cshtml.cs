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

namespace TrainSchedule.Pages.Connections
{
    public class IndexModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;
        private readonly IConfiguration Configuration;
        public string FilterId { get; set; }
        public string FilterName { get; set; }
        public string FilterType { get; set; }
        public IndexModel(TrainSchedule.Data.TrainSchedule_db context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<Connection> Connections { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Connection> connectionsIQ = from connections in _context.Connections
                                                   select connections;

            connectionsIQ = connectionsIQ.OrderBy(c => c.ConnectionID);
            Connections = await PaginatedList<Connection>.CreateAsync(connectionsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetId(string Id, int? pageIndex)
        {
            FilterId = Id;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Connection> connectionsIQ = from connections in _context.Connections
                                                   select connections;
            if (!String.IsNullOrEmpty(Id))
            {
                connectionsIQ = connectionsIQ.Where(c => c.ConnectionID == Int32.Parse(Id));
            }

            connectionsIQ = connectionsIQ.OrderBy(c => c.ConnectionID);
            Connections = await PaginatedList<Connection>.CreateAsync(connectionsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task OnGetType(string Type, int? pageIndex)
        {
            FilterType = Type;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Connection> connectionsIQ = from connections in _context.Connections
                                                   select connections;
            if (!String.IsNullOrEmpty(Type))
            {
                connectionsIQ = connectionsIQ.Where(c => c.Type == Type);
            }

            connectionsIQ = connectionsIQ.OrderBy(c => c.ConnectionID);
            Connections = await PaginatedList<Connection>.CreateAsync(connectionsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public async Task OnGetName(string Name, int? pageIndex)
        {
            FilterName = Name;
            var pageSize = Configuration.GetValue("PageSize", 25);

            IQueryable<Connection> connectionsIQ = from connections in _context.Connections
                                                   select connections;
            if (!String.IsNullOrEmpty(Name))
            {
                connectionsIQ = connectionsIQ.Where(c => c.Name == Name);
            }

            connectionsIQ = connectionsIQ.OrderBy(c => c.ConnectionID);
            Connections = await PaginatedList<Connection>.CreateAsync(connectionsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
