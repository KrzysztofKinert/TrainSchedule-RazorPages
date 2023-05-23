using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainSchedule.Data;
using TrainSchedule.Models;

namespace TrainSchedule.Pages.Stages
{
    public class DetailsModel : PageModel
    {
        private readonly TrainSchedule.Data.TrainSchedule_db _context;

        public DetailsModel(TrainSchedule.Data.TrainSchedule_db context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
