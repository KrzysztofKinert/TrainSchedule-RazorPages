using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainSchedule.Data;
using TrainSchedule.Models;

namespace TrainSchedule.Pages.Connections
{
    public class DeleteModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;

        public DeleteModel(TrainSchedule.Data.TrainSchedule_db context)
        {
            _context = context;
        }

        [BindProperty]
        public Connection Connection { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Connection = await _context.Connections.FirstOrDefaultAsync(m => m.ConnectionID == id);

            if (Connection == null)
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

            Connection = await _context.Connections.FindAsync(id);

            if (Connection != null)
            {
                _context.Connections.Remove(Connection);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
