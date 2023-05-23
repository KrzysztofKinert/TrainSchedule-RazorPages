using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainSchedule.Data;
using TrainSchedule.Models;

namespace TrainSchedule.Pages.Stages
{
    public class EditModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;

        public EditModel(TrainSchedule.Data.TrainSchedule_db context)
        {
            _context = context;
        }

        [BindProperty]
        public Stage Stage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Stage = await _context.Stages
                .Include(s => s.Connection).FirstOrDefaultAsync(m => m.StageID == id);

            if (Stage == null)
            {
                return NotFound();
            }
           ViewData["ConnectionID"] = new SelectList(_context.Connections, "ConnectionID", "ConnectionID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Stage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(Stage.StageID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StageExists(int id)
        {
            return _context.Stages.Any(e => e.StageID == id);
        }
    }
}
