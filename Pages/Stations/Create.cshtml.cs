using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainSchedule.Data;
using TrainSchedule.Models;

namespace TrainSchedule.Pages.Stations
{
    public class CreateModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;

        public CreateModel(TrainSchedule.Data.TrainSchedule_db context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Station Station { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Stations.Add(Station);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
