using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainSchedule.Data;
using TrainSchedule.Models;

namespace TrainSchedule.Pages.Stations
{
    public class DeleteModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;

        public DeleteModel(TrainSchedule.Data.TrainSchedule_db context)
        {
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Station = await _context.Stations.FirstOrDefaultAsync(m => m.StationID == id);

            if (Station == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Station = await _context.Stations.FindAsync(id);

            if (Station != null)
            {
                _context.Stations.Remove(Station);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
